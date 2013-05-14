#Creating an angular front end

##First Time: Install Stuff

#Install NPM (node package manager)

Install yeoman
> npm install -g yo

Install bower 

> npm install -g bower

Install the yeoman angular generator

> npm install -g generator-angular
> 

##Make The Front-End Visual Studio Project
We just want a place to see the files. VS doesn't offer that, so, depending on what you have installed in VS, you may be able to find a blank project. On my machine, I have Typescript installed, so I use New Project... "HTML Application with TypeScript". That gives me a project folder.

TODO: !!!! I had to remove the typescript... stuff in the csproj to make it build.

For this example, the project name is "myFrontEnd".

The example here is for angularjs, but you can use whatever approach you like when designing your front-end (e.g. knockoutjs, backbonejs, ember). So we're going to use yo to set up the angular application.

> cd myFrontEnd

> yo angular myCoolFrontEnd

When asked

    Would you like to include angular-resource.js? (Y/n)

Say yes.

Now back in Visual Studio, Make sure the "Show all files" is on in the Solution Explorer. You should see a directories. The most important one is the "app" directory.

### Setup VS to open your app

In Project Settings:Web, set the Startup URL to something like:
> http://localhost:47779/app/index.html

### Try it out

Run the project, and you should get a browse showing the 'Allo, 'Allo! page of index.html.

### What next

If you're new to Angular... this may not be the best place to start. But the most interesting files to look at would be those found in:

app\scripts
app\views
app\styles

## New Views

As you need to add things to your angular app, you can use [yo with angular generators](https://github.com/yeoman/generator-angular) to add them. It adds the right files, modifies existing ones, doing all the wiring in a consistent way.

For example:

> yo angular:route settings

Does all this for you:

+ creates app\scripts\controllers\settings.js
+ creates test\spec\controllers\settings.js
+ creates app\views\settings.html
+ and wires in the new controller in app.js

