import 'jquery';
import 'bootstrap';
import config from './auth-config';
import 'reflect-metadata';
import {Configure} from "aurelia-configuration";

export function configure(aurelia) {

    aurelia.use
        .standardConfiguration()
        .developmentLogging()
        .plugin('aurelia-configuration',
            config => {
                config.setEnvironments({
                    development: ['localhost', 'dev.local'],
                    test: ['test.mywebsite.com', 'stage.mywebsite.com'],
                    production: ['mywebsite.com']
                });
            }
        )
        .plugin('aurelia-dialog')
        .plugin('aurelia-validation')
        .plugin('aurelia-validatejs')
        .plugin('aurelia-auth',
            baseConfig => {
                let baseUrl = aurelia.container.get(Configure).get('baseUrl');
                config.baseUrl = baseUrl + '/core';
                config.providers.identSrv.authorizationEndpoint = baseUrl + "/core/connect/authorize";
                baseConfig.configure(config);
            }
        )
        .feature('bootstrap-validation'); 

    //Uncomment the line below to enable animation.
    //aurelia.use.plugin('aurelia-animator-css');
    //if the css animator is enabled, add swap-order="after" to all router-view elements

    //Anyone wanting to use HTMLImports to load views, will need to install the following plugin.
    //aurelia.use.plugin('aurelia-html-import-template-loader')

    aurelia.start().then(a => a.setRoot());

}
