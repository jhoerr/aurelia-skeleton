import {inject} from "aurelia-framework";
import {Router} from "aurelia-router";
import {HttpClient, json} from 'aurelia-fetch-client';
import {Configure} from 'aurelia-configuration';

@inject(Router, HttpClient, Configure)
export class DataService {
    
    constructor (router, httpClient, configuration) {
        httpClient.configure(config =>
            config
            .useStandardConfiguration()
            .withBaseUrl(configuration.get("baseUrl") + "/api")
            .withDefaults({
                headers: {
                    'content-type': 'application/json',
                    'Accept': 'application/json',
                    'X-Requested-With': 'aurelia-fetch-client'
                }
            })
        );
        this.httpClient = httpClient;
        this.endpoint = router.history.fragment;
    }

    getAll() {
        return new Promise((resolve, reject) => {
            this.httpClient.fetch(this.endpoint)
                .then(response => response.json())
                .then(data => {
                    console.dir(data);
                    resolve(data);
                })
                .catch(err => reject(err));
        });
    }

    post(entity) {
        return new Promise((resolve, reject) => {
            this.httpClient.fetch(this.endpoint,
                {
                    method: 'post',
                    body: JSON.stringify(entity)
                })
                .then(response => response.json())
                .then(data => resolve(data))
                .catch(err => reject(err));
        });
    }

    put(id, entity) {
        return new Promise((resolve, reject) => {
            this.httpClient.fetch(this.endpoint + '/' + id,
                {
                    method: 'put',
                    body: JSON.stringify(entity)
                })
                .then(response => response.json())
                .then(data => resolve(data))
                .catch(err => reject(err));
        });
    }

    delete(id) {
        return new Promise((resolve, reject) => {
            this.httpClient.fetch(this.endpoint + '/' + id,
                {
                    method: 'delete'
                })
                .then(response => resolve(response))
                .catch(err => reject(err));
        });
    }


}