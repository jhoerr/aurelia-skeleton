System.config({
  defaultJSExtensions: true,
  transpiler: false,
  paths: {
    "*": "dist/*",
    "github:*": "jspm_packages/github/*",
    "npm:*": "jspm_packages/npm/*"
  },
  map: {
    "aurelia-animator-css": "npm:aurelia-animator-css@1.0.0",
    "aurelia-auth": "npm:aurelia-auth@3.0.2",
    "aurelia-bootstrapper": "npm:aurelia-bootstrapper@1.0.0",
    "aurelia-configuration": "github:vheissu/aurelia-configuration@1.0.4",
    "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
    "aurelia-dialog": "npm:aurelia-dialog@1.0.0-beta.3.0.0",
    "aurelia-fetch-client": "npm:aurelia-fetch-client@1.0.0",
    "aurelia-framework": "npm:aurelia-framework@1.0.1",
    "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0",
    "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0",
    "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
    "aurelia-pal-browser": "npm:aurelia-pal-browser@1.0.0",
    "aurelia-polyfills": "npm:aurelia-polyfills@1.0.0",
    "aurelia-router": "npm:aurelia-router@1.0.2",
    "aurelia-templating-binding": "npm:aurelia-templating-binding@1.0.0",
    "aurelia-templating-resources": "npm:aurelia-templating-resources@1.0.0",
    "aurelia-templating-router": "npm:aurelia-templating-router@1.0.0",
    "aurelia-validatejs": "npm:aurelia-validatejs@0.7.0",
    "aurelia-validation": "npm:aurelia-validation@0.11.0",
    "babel": "npm:babel-core@5.8.38",
    "babel-runtime": "npm:babel-runtime@5.8.38",
    "bluebird": "npm:bluebird@3.4.1",
    "bootstrap": "github:twbs/bootstrap@3.3.6",
    "core-js": "npm:core-js@1.2.7",
    "fetch": "github:github/fetch@1.0.0",
    "font-awesome": "npm:font-awesome@4.6.3",
    "jquery": "npm:jquery@2.2.4",
    "reflect-metadata": "npm:reflect-metadata@0.1.8",
    "text": "github:systemjs/plugin-text@0.0.8",
    "github:jspm/nodelibs-assert@0.1.0": {
      "assert": "npm:assert@1.4.1"
    },
    "github:jspm/nodelibs-buffer@0.1.0": {
      "buffer": "npm:buffer@3.6.0"
    },
    "github:jspm/nodelibs-path@0.1.0": {
      "path-browserify": "npm:path-browserify@0.0.0"
    },
    "github:jspm/nodelibs-process@0.1.2": {
      "process": "npm:process@0.11.8"
    },
    "github:jspm/nodelibs-util@0.1.0": {
      "util": "npm:util@0.10.3"
    },
    "github:jspm/nodelibs-vm@0.1.0": {
      "vm-browserify": "npm:vm-browserify@0.0.4"
    },
    "github:twbs/bootstrap@3.3.6": {
      "jquery": "npm:jquery@2.2.4"
    },
    "github:vheissu/aurelia-configuration@1.0.4": {
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-loader": "npm:aurelia-loader@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0"
    },
    "npm:assert@1.4.1": {
      "assert": "github:jspm/nodelibs-assert@0.1.0",
      "buffer": "github:jspm/nodelibs-buffer@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "util": "npm:util@0.10.3"
    },
    "npm:aurelia-animator-css@1.0.0": {
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-auth@3.0.2": {
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.0",
      "aurelia-fetch-client": "npm:aurelia-fetch-client@1.0.0",
      "aurelia-router": "npm:aurelia-router@1.0.2"
    },
    "npm:aurelia-binding@1.0.1": {
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0"
    },
    "npm:aurelia-bootstrapper@1.0.0": {
      "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.0",
      "aurelia-framework": "npm:aurelia-framework@1.0.1",
      "aurelia-history": "npm:aurelia-history@1.0.0",
      "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0",
      "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0",
      "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-pal-browser": "npm:aurelia-pal-browser@1.0.0",
      "aurelia-polyfills": "npm:aurelia-polyfills@1.0.0",
      "aurelia-router": "npm:aurelia-router@1.0.2",
      "aurelia-templating": "npm:aurelia-templating@1.0.0",
      "aurelia-templating-binding": "npm:aurelia-templating-binding@1.0.0",
      "aurelia-templating-resources": "npm:aurelia-templating-resources@1.0.0",
      "aurelia-templating-router": "npm:aurelia-templating-router@1.0.0"
    },
    "npm:aurelia-dependency-injection@1.0.0": {
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-dialog@1.0.0-beta.3.0.0": {
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-event-aggregator@1.0.0": {
      "aurelia-logging": "npm:aurelia-logging@1.0.0"
    },
    "npm:aurelia-framework@1.0.1": {
      "aurelia-binding": "npm:aurelia-binding@1.0.1",
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-loader": "npm:aurelia-loader@1.0.0",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0",
      "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-history-browser@1.0.0": {
      "aurelia-history": "npm:aurelia-history@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-loader-default@1.0.0": {
      "aurelia-loader": "npm:aurelia-loader@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-loader@1.0.0": {
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0"
    },
    "npm:aurelia-logging-console@1.0.0": {
      "aurelia-logging": "npm:aurelia-logging@1.0.0"
    },
    "npm:aurelia-metadata@1.0.0": {
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-pal-browser@1.0.0": {
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-polyfills@1.0.0": {
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-route-recognizer@1.0.0": {
      "aurelia-path": "npm:aurelia-path@1.0.0"
    },
    "npm:aurelia-router@1.0.2": {
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.0",
      "aurelia-history": "npm:aurelia-history@1.0.0",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0",
      "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.0.0"
    },
    "npm:aurelia-task-queue@1.0.0": {
      "aurelia-pal": "npm:aurelia-pal@1.0.0"
    },
    "npm:aurelia-templating-binding@1.0.0": {
      "aurelia-binding": "npm:aurelia-binding@1.0.1",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-templating-resources@1.0.0": {
      "aurelia-binding": "npm:aurelia-binding@1.0.1",
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-loader": "npm:aurelia-loader@1.0.0",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0",
      "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-templating-router@1.0.0": {
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0",
      "aurelia-router": "npm:aurelia-router@1.0.2",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:aurelia-templating@1.0.0": {
      "aurelia-binding": "npm:aurelia-binding@1.0.1",
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-loader": "npm:aurelia-loader@1.0.0",
      "aurelia-logging": "npm:aurelia-logging@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-pal": "npm:aurelia-pal@1.0.0",
      "aurelia-path": "npm:aurelia-path@1.0.0",
      "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0"
    },
    "npm:aurelia-validatejs@0.7.0": {
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-validation": "npm:aurelia-validation@0.11.0",
      "validate.js": "npm:validate.js@0.10.0"
    },
    "npm:aurelia-validation@0.11.0": {
      "aurelia-binding": "npm:aurelia-binding@1.0.1",
      "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0",
      "aurelia-metadata": "npm:aurelia-metadata@1.0.0",
      "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0",
      "aurelia-templating": "npm:aurelia-templating@1.0.0"
    },
    "npm:babel-runtime@5.8.38": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:bluebird@3.4.1": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:buffer@3.6.0": {
      "base64-js": "npm:base64-js@0.0.8",
      "child_process": "github:jspm/nodelibs-child_process@0.1.0",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "ieee754": "npm:ieee754@1.1.6",
      "isarray": "npm:isarray@1.0.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:core-js@1.2.7": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "systemjs-json": "github:systemjs/plugin-json@0.1.2"
    },
    "npm:font-awesome@4.6.3": {
      "css": "github:systemjs/plugin-css@0.1.27"
    },
    "npm:inherits@2.0.1": {
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:path-browserify@0.0.0": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:process@0.11.8": {
      "assert": "github:jspm/nodelibs-assert@0.1.0",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "vm": "github:jspm/nodelibs-vm@0.1.0"
    },
    "npm:util@0.10.3": {
      "inherits": "npm:inherits@2.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:validate.js@0.10.0": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:vm-browserify@0.0.4": {
      "indexof": "npm:indexof@0.0.1"
    }
  },
  bundles: {
    "app-build.js": [
      "app.html!github:systemjs/plugin-text@0.0.8.js",
      "app.js",
      "auth-config.js",
      "bootstrap-validation/bootstrap-form-validation-renderer.js",
      "bootstrap-validation/index.js",
      "clients/list.html!github:systemjs/plugin-text@0.0.8.js",
      "clients/list.js",
      "config/config.json!github:systemjs/plugin-text@0.0.8.js",
      "converters.js",
      "logout.html!github:systemjs/plugin-text@0.0.8.js",
      "logout.js",
      "main.js",
      "nav-bar.html!github:systemjs/plugin-text@0.0.8.js",
      "nav-bar.js",
      "router-config.js",
      "services/data-service.js",
      "views/confirm-delete.html!github:systemjs/plugin-text@0.0.8.js",
      "views/confirm-delete.js",
      "views/geo-entity-dialog.html!github:systemjs/plugin-text@0.0.8.js",
      "views/geo-entity-dialog.js",
      "views/validation-summary.html!github:systemjs/plugin-text@0.0.8.js",
      "welcome.html!github:systemjs/plugin-text@0.0.8.js",
      "welcome.js"
    ],
    "aurelia.js": [
      "github:github/fetch@1.0.0.js",
      "github:github/fetch@1.0.0/fetch.js",
      "github:jspm/nodelibs-process@0.1.2.js",
      "github:jspm/nodelibs-process@0.1.2/index.js",
      "github:twbs/bootstrap@3.3.6.js",
      "github:twbs/bootstrap@3.3.6/css/bootstrap.css!github:systemjs/plugin-text@0.0.8.js",
      "github:twbs/bootstrap@3.3.6/js/bootstrap.js",
      "github:vheissu/aurelia-configuration@1.0.4.js",
      "github:vheissu/aurelia-configuration@1.0.4/aurelia-configuration.js",
      "npm:aurelia-animator-css@1.0.0.js",
      "npm:aurelia-animator-css@1.0.0/aurelia-animator-css.js",
      "npm:aurelia-auth@3.0.2.js",
      "npm:aurelia-auth@3.0.2/aurelia-auth.js",
      "npm:aurelia-auth@3.0.2/auth-fetch-config.js",
      "npm:aurelia-auth@3.0.2/auth-filter.js",
      "npm:aurelia-auth@3.0.2/auth-service.js",
      "npm:aurelia-auth@3.0.2/auth-utilities.js",
      "npm:aurelia-auth@3.0.2/authentication.js",
      "npm:aurelia-auth@3.0.2/authorize-step.js",
      "npm:aurelia-auth@3.0.2/base-config.js",
      "npm:aurelia-auth@3.0.2/oAuth1.js",
      "npm:aurelia-auth@3.0.2/oAuth2.js",
      "npm:aurelia-auth@3.0.2/popup.js",
      "npm:aurelia-auth@3.0.2/storage.js",
      "npm:aurelia-binding@1.0.1.js",
      "npm:aurelia-binding@1.0.1/aurelia-binding.js",
      "npm:aurelia-bootstrapper@1.0.0.js",
      "npm:aurelia-bootstrapper@1.0.0/aurelia-bootstrapper.js",
      "npm:aurelia-dependency-injection@1.0.0.js",
      "npm:aurelia-dependency-injection@1.0.0/aurelia-dependency-injection.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/ai-dialog-body.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/ai-dialog-footer.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/ai-dialog-header.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/ai-dialog.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/attach-focus.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/aurelia-dialog.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-configuration.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-controller.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-options.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-renderer.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-result.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/dialog-service.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/lifecycle.js",
      "npm:aurelia-dialog@1.0.0-beta.3.0.0/renderer.js",
      "npm:aurelia-event-aggregator@1.0.0.js",
      "npm:aurelia-event-aggregator@1.0.0/aurelia-event-aggregator.js",
      "npm:aurelia-fetch-client@1.0.0.js",
      "npm:aurelia-fetch-client@1.0.0/aurelia-fetch-client.js",
      "npm:aurelia-framework@1.0.1.js",
      "npm:aurelia-framework@1.0.1/aurelia-framework.js",
      "npm:aurelia-history-browser@1.0.0.js",
      "npm:aurelia-history-browser@1.0.0/aurelia-history-browser.js",
      "npm:aurelia-history@1.0.0.js",
      "npm:aurelia-history@1.0.0/aurelia-history.js",
      "npm:aurelia-loader-default@1.0.0.js",
      "npm:aurelia-loader-default@1.0.0/aurelia-loader-default.js",
      "npm:aurelia-loader@1.0.0.js",
      "npm:aurelia-loader@1.0.0/aurelia-loader.js",
      "npm:aurelia-logging-console@1.0.0.js",
      "npm:aurelia-logging-console@1.0.0/aurelia-logging-console.js",
      "npm:aurelia-logging@1.0.0.js",
      "npm:aurelia-logging@1.0.0/aurelia-logging.js",
      "npm:aurelia-metadata@1.0.0.js",
      "npm:aurelia-metadata@1.0.0/aurelia-metadata.js",
      "npm:aurelia-pal-browser@1.0.0.js",
      "npm:aurelia-pal-browser@1.0.0/aurelia-pal-browser.js",
      "npm:aurelia-pal@1.0.0.js",
      "npm:aurelia-pal@1.0.0/aurelia-pal.js",
      "npm:aurelia-path@1.0.0.js",
      "npm:aurelia-path@1.0.0/aurelia-path.js",
      "npm:aurelia-polyfills@1.0.0.js",
      "npm:aurelia-polyfills@1.0.0/aurelia-polyfills.js",
      "npm:aurelia-route-recognizer@1.0.0.js",
      "npm:aurelia-route-recognizer@1.0.0/aurelia-route-recognizer.js",
      "npm:aurelia-router@1.0.2.js",
      "npm:aurelia-router@1.0.2/aurelia-router.js",
      "npm:aurelia-task-queue@1.0.0.js",
      "npm:aurelia-task-queue@1.0.0/aurelia-task-queue.js",
      "npm:aurelia-templating-binding@1.0.0.js",
      "npm:aurelia-templating-binding@1.0.0/aurelia-templating-binding.js",
      "npm:aurelia-templating-resources@1.0.0.js",
      "npm:aurelia-templating-resources@1.0.0/abstract-repeater.js",
      "npm:aurelia-templating-resources@1.0.0/analyze-view-factory.js",
      "npm:aurelia-templating-resources@1.0.0/array-repeat-strategy.js",
      "npm:aurelia-templating-resources@1.0.0/aurelia-hide-style.js",
      "npm:aurelia-templating-resources@1.0.0/aurelia-templating-resources.js",
      "npm:aurelia-templating-resources@1.0.0/binding-mode-behaviors.js",
      "npm:aurelia-templating-resources@1.0.0/binding-signaler.js",
      "npm:aurelia-templating-resources@1.0.0/compose.js",
      "npm:aurelia-templating-resources@1.0.0/css-resource.js",
      "npm:aurelia-templating-resources@1.0.0/debounce-binding-behavior.js",
      "npm:aurelia-templating-resources@1.0.0/dynamic-element.js",
      "npm:aurelia-templating-resources@1.0.0/focus.js",
      "npm:aurelia-templating-resources@1.0.0/hide.js",
      "npm:aurelia-templating-resources@1.0.0/html-resource-plugin.js",
      "npm:aurelia-templating-resources@1.0.0/html-sanitizer.js",
      "npm:aurelia-templating-resources@1.0.0/if.js",
      "npm:aurelia-templating-resources@1.0.0/map-repeat-strategy.js",
      "npm:aurelia-templating-resources@1.0.0/null-repeat-strategy.js",
      "npm:aurelia-templating-resources@1.0.0/number-repeat-strategy.js",
      "npm:aurelia-templating-resources@1.0.0/repeat-strategy-locator.js",
      "npm:aurelia-templating-resources@1.0.0/repeat-utilities.js",
      "npm:aurelia-templating-resources@1.0.0/repeat.js",
      "npm:aurelia-templating-resources@1.0.0/replaceable.js",
      "npm:aurelia-templating-resources@1.0.0/sanitize-html.js",
      "npm:aurelia-templating-resources@1.0.0/set-repeat-strategy.js",
      "npm:aurelia-templating-resources@1.0.0/show.js",
      "npm:aurelia-templating-resources@1.0.0/signal-binding-behavior.js",
      "npm:aurelia-templating-resources@1.0.0/throttle-binding-behavior.js",
      "npm:aurelia-templating-resources@1.0.0/update-trigger-binding-behavior.js",
      "npm:aurelia-templating-resources@1.0.0/with.js",
      "npm:aurelia-templating-router@1.0.0.js",
      "npm:aurelia-templating-router@1.0.0/aurelia-templating-router.js",
      "npm:aurelia-templating-router@1.0.0/route-href.js",
      "npm:aurelia-templating-router@1.0.0/route-loader.js",
      "npm:aurelia-templating-router@1.0.0/router-view.js",
      "npm:aurelia-templating@1.0.0.js",
      "npm:aurelia-templating@1.0.0/aurelia-templating.js",
      "npm:aurelia-validatejs@0.7.0.js",
      "npm:aurelia-validatejs@0.7.0/aurelia-validatejs.js",
      "npm:aurelia-validation@0.11.0.js",
      "npm:aurelia-validation@0.11.0/aurelia-validation.js",
      "npm:aurelia-validation@0.11.0/property-info.js",
      "npm:aurelia-validation@0.11.0/validate-binding-behavior.js",
      "npm:aurelia-validation@0.11.0/validate-trigger.js",
      "npm:aurelia-validation@0.11.0/validation-controller.js",
      "npm:aurelia-validation@0.11.0/validation-error.js",
      "npm:aurelia-validation@0.11.0/validation-errors-custom-attribute.js",
      "npm:aurelia-validation@0.11.0/validation-renderer-custom-attribute.js",
      "npm:aurelia-validation@0.11.0/validation-renderer.js",
      "npm:aurelia-validation@0.11.0/validator.js",
      "npm:bluebird@3.4.1.js",
      "npm:bluebird@3.4.1/js/browser/bluebird.js",
      "npm:font-awesome@4.6.3.js",
      "npm:font-awesome@4.6.3/css/font-awesome.css!github:systemjs/plugin-css@0.1.27.js",
      "npm:jquery@2.2.4.js",
      "npm:jquery@2.2.4/dist/jquery.js",
      "npm:process@0.11.8.js",
      "npm:process@0.11.8/browser.js",
      "npm:reflect-metadata@0.1.8.js",
      "npm:reflect-metadata@0.1.8/Reflect.js",
      "npm:validate.js@0.10.0.js",
      "npm:validate.js@0.10.0/validate.js"
    ]
  },
  depCache: {
    "app.js": [
      "bootstrap",
      "aurelia-framework",
      "aurelia-router",
      "aurelia-auth",
      "./router-config"
    ],
    "bootstrap-validation/bootstrap-form-validation-renderer.js": [
      "aurelia-dependency-injection",
      "aurelia-validation"
    ],
    "bootstrap-validation/index.js": [
      "./bootstrap-form-validation-renderer"
    ],
    "clients/list.js": [
      "aurelia-framework",
      "../services/data-service",
      "aurelia-dialog",
      "../views/geo-entity-dialog",
      "../views/confirm-delete"
    ],
    "logout.js": [
      "aurelia-auth",
      "aurelia-framework"
    ],
    "main.js": [
      "jquery",
      "bootstrap",
      "./auth-config",
      "reflect-metadata",
      "aurelia-configuration"
    ],
    "nav-bar.js": [
      "aurelia-framework",
      "aurelia-auth",
      "aurelia-configuration"
    ],
    "router-config.js": [
      "aurelia-framework",
      "aurelia-auth",
      "aurelia-configuration",
      "aurelia-router"
    ],
    "services/data-service.js": [
      "aurelia-framework",
      "aurelia-router",
      "aurelia-fetch-client",
      "aurelia-configuration"
    ],
    "views/confirm-delete.js": [
      "aurelia-dialog",
      "aurelia-framework"
    ],
    "views/geo-entity-dialog.js": [
      "aurelia-dialog",
      "aurelia-dependency-injection",
      "aurelia-validation",
      "aurelia-validatejs"
    ]
  }
});