import {inject} from 'aurelia-framework';  
import {AuthorizeStep} from 'aurelia-auth';
import {Configure} from 'aurelia-configuration';
import {Router} from 'aurelia-router';

@inject(Router, Configure)
export class AppRouterConfig{

    constructor(router, configuration) {
        this.router = router;
        this.configuration = configuration;
    }
    
    configure(){

        let title = this.configuration.get("name", "Test App");

        var appRouterConfig = function(config) {
            config.title = title;
            
            config.addPipelineStep('authorize', AuthorizeStep);
       
            config.map([
                { route: ['','welcome'],    name: 'welcome',    moduleId: 'welcome',    nav: true,  title:'Welcome' },
                { route: 'customers',         name: 'customers',      moduleId: './clients/list',      nav: true,  title:'Customers', auth:true },
                { route: 'login',           name: 'login',      moduleId: 'login',      nav: false, title:'Login'}, 
                { route: 'logout',          name: 'logout',     moduleId: 'logout',     nav: false, title:'Logout', auth: true }
            ]);
        };
        this.router.configure(appRouterConfig);
    }
}