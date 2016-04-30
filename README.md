##The GeckkotaSharp Control

GekkotaSharp is an experiment to see how easy/hard it is to use normal web application frameworks to develop .net desktop applications using HTML5 GUIs, so that the same code base can be use on the web, desktops, tablets, etc.

At this point, the control does just two things:

1) Wraps [GeckoFx](https://bitbucket.org/geckofx) so that there is zero setup needed in your code (e.g. to initialize XulRunner).

2) Silently runs a self-hosted (embedded) [Web Api](http://www.asp.net/web-api) REST server.

##Intended Application Architecture

The idea is that you create three projects:

1) Web App Frontend

Create your frontend in AngularJS, BackboneJS, KnockoutJS, whatever. No .net, no c#. This will be deploy-able through normal web browsers.

2) Simple c# backend. Mostly just a c# model of the data, maybe some logic that you don't/can't do in the frontend. The embedded REST server in Gekkota will take care of converting between json and your c#.

3) Simple c# desktop app. This is mostly just windows dialogs which each contain a single GekkotaSharpControl. You hand that control a path to an html file from your Web App Frontend project.



##Why Bother?

I work on teams that build free, cross-platform applications. We have these constraints:

+ Can't be just a web app
  + Many of our users are often poor rural people (non) developing nations. They can't be online all the time, or in many cases, at all. 
+ Can't afford to do a number of native apps
  + Because our products are free, we don't get new resources by releasing on new platforms & getting new users... we do it because we can then do more good.
+ Can't limit to one platform
  + Users have different needs, can't always just buy the hardware to match our chosen platform. So we would like to deliver on Windows, Mac, tablets, and the web. 
+ Can't start from scratch, with, say, java.
  + Even with Java, you have to do something special to get on the web (e.b. GWT).
  + We have 12 years of domain-specific c# libraries we'd like to not have to re-write.

So GekkotaSharp is trying to find out if we can have the best of both worlds, and have a single code base that would server all of our users. For the frontend, we do have to work with the lowest common denominator, which is javascript. But for the backend, now-a-days you can use c# everywhere. In non-web scenarios, the front and backend can be on the same machine.

### The name
 The <i>Gekkota</i> are an infraorder of reptiles to which geckos belong, and this project uses [geckofx](https://bitbucket.org/geckofx), a .net wrapper for Mozilla's xulrunner embed-able browser.
 
 
### Building

netsh http add urlacl url=http://+:5432/  user=everyone
You'll need npm, grunt, bower, etc.
Open solution, build. That should run npm install and run then angularjs-based sample app.
