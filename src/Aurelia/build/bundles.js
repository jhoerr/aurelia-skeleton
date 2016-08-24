module.exports = {
  "bundles": {
    "dist/app-build": {
      "includes": [
        "[**/*.js]",
        "**/*.html!text",
        "**/*.css!text",
        "**/*.json!text"
      ],
      "options": {
        "inject": true,
        "minify": true,
        "depCache": true,
        "rev": false
      }
    },
    "dist/aurelia": {
      "includes": [
        "aurelia-animator-css",
        "aurelia-auth",
        "aurelia-bootstrapper",
        "aurelia-configuration",
        "aurelia-dependency-injection",
        "aurelia-dialog",
        "aurelia-fetch-client",
        "aurelia-framework",
        "aurelia-history-browser",
        "aurelia-loader-default",
        "aurelia-logging-console",
        "aurelia-pal-browser",
        "aurelia-polyfills",
        "aurelia-router",
        "aurelia-templating-binding",
        "aurelia-templating-resources",
        "aurelia-templating-router",
        "aurelia-validatejs",
        "aurelia-validation",
        "bluebird",
        "bootstrap",
        "bootstrap/css/bootstrap.css!text",
        "fetch",
        "font-awesome",
        "jquery",
        "reflect-metadata"
      ],
      "options": {
        "inject": true,
        "minify": true,
        "depCache": false,
        "rev": false
      }
    }
  }
};
