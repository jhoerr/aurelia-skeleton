import {bindable} from 'aurelia-framework';  
import {inject} from 'aurelia-framework';  
import {computedFrom} from 'aurelia-framework';  
import {AuthService} from 'aurelia-auth';
import {Configure} from 'aurelia-configuration';

@inject(AuthService, Configure)
export class NavBar {  
    // User isn't authenticated by default
    _isAuthenticated = false;
    profile = {}; 
    @bindable router = null;

    // We can check if the user is authenticated
    // to conditionally hide or show nav bar items
    get isAuthenticated() {
        this._isAuthenticated = this.auth.isAuthenticated();
        return this._isAuthenticated;
    }

    @computedFrom('profile')
    get displayName(){

        return `${this.profile.preferred_username}`;
    }

    constructor(auth, configure) {
        this.auth = auth;
        this.manageUrl = configure.get('baseUrl') + "/Manage";
        console.log(this.manageUrl);
        this._isAuthenticated = this.auth.isAuthenticated();
        if(this._isAuthenticated)
        {
            this.auth.getMe().then(data => {
                this.profile = data;
            });
        }
    }

    authenticate(name) {
        return this.auth.authenticate(name, false, null)
            .then((response) => {
                // nop
            })
            .catch(error => {
                console.log("Authentication error login.authenticate:39");
            });
    }

    logout() {
        return this.auth.logout("#")
            .then(response => {
                console.log("Logged Out");
            })
            .catch(err => {
                console.log("Error Logging Out");
            });
    }
}