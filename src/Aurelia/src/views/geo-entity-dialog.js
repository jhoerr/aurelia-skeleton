import {DialogController} from 'aurelia-dialog';
import {inject, NewInstance} from 'aurelia-dependency-injection';
import {ValidationController, validateTrigger} from 'aurelia-validation';
import {required, email, ValidationRules} from 'aurelia-validatejs';

@inject(DialogController, NewInstance.of(ValidationController))
export class GeoEntityDialog {
    
    @required
    name='';

    @required
    address1='';
    
    address2='';
    
    @required
    city='';
    
    @required
    stateOrProvince='';
    
    @required
    postalCode='';
    
    @required
    country='';

    constructor(dialogController, validationController) {
        this.geoEntity = {};
        this.dialogController = dialogController;
        this.validationController = validationController;
        this.validationController.validateTrigger = validateTrigger.manual;
    }

    activate(geoEntity) {
        this.geoEntity = geoEntity;
        this.map(geoEntity, this);
    }

    save() {
        let errors = this.validationController.validate();
        if (errors.length == 0) {
            this.map(this, this.geoEntity);
            this.dialogController.ok();    
        }
    }

    cancel() {
        this.dialogController.cancel();
    }

    map(src, dst) {
        dst.name = src.name;
        dst.address1 = src.address1;
        dst.address2 = src.address2;
        dst.city = src.city;
        dst.stateOrProvince = src.stateOrProvince;
        dst.postalCode = src.postalCode;
        dst.country = src.country;
        
    }
}