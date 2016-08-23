import {DialogController} from 'aurelia-dialog';
import {inject} from 'aurelia-framework';

@inject(DialogController)
export class ConfirmDelete {
    constructor(dialogController) {
        this.dialogController = dialogController;
    }

    activate(entity) {
        this.entity = entity;
    }

    delete() {
        this.dialogController.ok();
    }

    cancel() {
        this.dialogController.cancel();
    }
}