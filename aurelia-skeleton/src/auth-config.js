var config = {
    baseUrl : 'https://localhost:44348/core',
    //tokenName : 'id_token',
    profileUrl: '/connect/userinfo',
    unlinkUrl : '/connect/endsession',
    logoutRedirect: '/',
    loginurl: '/',
    loginRedirect : window.location.origin || window.location.protocol + '//' + window.location.host + ":" + window.location.port,
    providers : {
        identSrv : {
            name: 'identSrv',
            url: '/connect/token',
            authorizationEndpoint: 'https://localhost:44348/core/connect/authorize',
            redirectUri: window.location.origin || window.location.protocol + '//' + window.location.host + ":" + window.location.port, 
            scope: ['profile', 'apiAccess','openid'],
            responseType :'code id_token token',
            scopePrefix: '',
            scopeDelimiter: ' ',
            requiredUrlParams: ['scope', 'nonce'],
            optionalUrlParams: ['display'],
            state: 'session_state',
            display: 'popup',
            type: '2.0',
            clientId: 'jsClient',
            flow: 'hybrid',
            nonce : function(){
                var val = ((Date.now() + Math.random()) * Math.random()).toString().replace(".", "");
                return encodeURIComponent(val);
            },
            popupOptions: { width: 452, height: 633 }
        }
    }
}

export default config;


