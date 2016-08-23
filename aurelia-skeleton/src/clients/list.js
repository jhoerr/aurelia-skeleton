import {inject} from "aurelia-framework";
import {DataService} from "../services/data-service";
import {DialogService} from 'aurelia-dialog';
import {GeoEntityDialog} from '../views/geo-entity-dialog';
import {ConfirmDelete} from '../views/confirm-delete';

@inject(DataService, DialogService)
export class EntityList {
    constructor(dataService, dialogService) {
        this.dataService = dataService;
        this.dialogService = dialogService;
    }

    activate(params, routeConfig, navigationInstruction) {
        this.entities = [];
        this.router = navigationInstruction.router;
        return this.dataService.getAll()
            .then(entities => this.entities = entities);
    }

    add() {
        var model = {};
        this.dialogService
            .open({ viewModel: GeoEntityDialog, model: model })
            .then(result => {
                if (!result.wasCancelled) {
                    this.dataService.post(model)
                        .then(data =>  this.addToModel(data));
                }
            });
    }

    edit(entity) {
        var model = JSON.parse(JSON.stringify(entity));
        this.dialogService
            .open({ viewModel: GeoEntityDialog, model: model })
            .then(result => {
                if (!result.wasCancelled) {
                    this.dataService.put(entity.id, model)
                        .then(data => {
                            this.removeFromModel(entity);
                            this.addToModel(data);
                        });
                }
            });
    }

    delete(entity) {
        this.dialogService
            .open({ viewModel: ConfirmDelete, model: entity })
            .then(result => {
                if (!result.wasCancelled) {
                    this.dataService.delete(entity.id)
                        .then(resp => this.removeFromModel(entity));
                }
            });

    }

    removeFromModel(entity) {
        var index = this.entities.map(function(x) {return x.id; }).indexOf(entity.id);
        this.entities.splice(index, 1);
    }

    addToModel(entity) {
        this.entities.push(entity);
    }
}