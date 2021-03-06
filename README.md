[![Build status](https://ci.appveyor.com/api/projects/status/xp12i86o3ni5dis8/branch/master?svg=true)](https://ci.appveyor.com/project/jhoerr/aurelia-skeleton/branch/master)

This repository implements a minimal, secure Aurelia + Asp.Net WebAPI application with the following features:

1. The Aurelia and WebAPI apps are implemented as separate Asp.Net web applications. They are intended to be hosted separately.
2. The WebAPI has a clean, simple data model backed by generic controllers and services.
3. The Aurelia app provides simple dialog-driven data management.
4. Token- and cookie-based authentication are faciliated by [IdentityServer3](https://github.com/IdentityServer) backed by the standard Asp.Net Identity model. Options exist to allow users to log in with local accounts or external Google/Facebook/etc accounts.
5. User management is provided by [IdentityManager](https://github.com/IdentityManager).

### Prerequisites

* Download and install Node.JS from [NodeJS.org](https://nodejs.org/en/). This will install `npm`, a package manager.
* Open a cmd prompt. Type `npm version` to confirm npm is installed.
```
C:\Users\jhoerr>npm version
{ npm: '2.15.8',
  ares: '1.10.1-DEV',
  http_parser: '2.5.2',
  icu: '56.1',
  modules: '46',
  node: '4.4.7',
  openssl: '1.0.2h',
  uv: '1.8.0',
  v8: '4.5.103.36',
  zlib: '1.2.8' }
```
* Install `jspm` (another package manager)
```
npm install jspm -g
```
* Install `gulp` (another package manager) 
```
npm install gulp -g
```

### Default Users

The following default users are added with the database is created.

1. Admin: username: *admin@example.com*, password: *@bcd1234!*
2. User:  username: *user@example.com*,  password: *@bcd0987!*

### Default URLs

WebApi
* running in Visual Studio debugger: [https://localhost:44348](https://localhost:44348)

Aurelia 
 * running in dotnet CLI): [http://localhost:5000](http://localhost:5000)
 * running in Visual Studio debugger: [https://localhost:44368](https://localhost:44368)

### Running the sites

1. Clone this repo.
2. Open the `aurelia-skeleton` solution in Visual Studio
3. Rebuild the solution. Nuget and npm packages should be automatically restored.
4. Create the database. From the Package Manager Console change the default project to `WebApi` and execute `EntityFramework\update-database`.
5. Press F5 to launch debug instances of the sites.

### Running the Aurelia app from the command line.

The WebAPI app is backed by Asp.Net MVC 5 + WebAPI 2.2 and is best run from Visual Studio. 
The Aurelia app is backed by an Asp.Net core application and can be run from the command line. 
You must have the [.Net Core SDK](https://www.microsoft.com/net/core) installed first.

```
cd .\aurelia-skeleton\src\Aurelia
npm install         # restore npm packages
jspm install -y     # restore jspm packages
dotnet restore      # restore nuget packages
dotnet build        # build the asp.net core project that serves the aurelia files
gulp build          # build the aurelia app
gulp watch          # watch for subsequent changes to aurelia files and automatically rebuild the app
dotnet run          # run the aurelia app
```
You should now be able to browse the site at http://localhost:5000

### Managing Users and Roles

User and role management is provided by [IdentityManager](https://github.com/IdentityManager).

1. Launch the WebAPI app
2. Browse to [https://localhost:44348/idm](https://localhost:44348/idm)
3. Log in with the Admin credentials from above.
4. Change the Admin password to something unique.
