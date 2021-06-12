# generic-console-application-as-a-service
Template for processes to run in background based on a cron schedule

To add a new scheduled job
1. Write a new Quartz IJob in the BackgroundServices.Jobs namespace (https://github.com/ravikhubchandani/generic-console-application-as-a-service/tree/master/src/BackgroundServices/BackgroundServices/Jobs) OR import a reference to an already existing IJob
2. Define schedule trigger and data to relay for the job in QuartzJobBuilder (https://github.com/ravikhubchandani/generic-console-application-as-a-service/blob/master/src/BackgroundServices/BackgroundServices/QuartzJobBuilder.cs)
3. Add job details produced in the previous step in the job list to run (https://github.com/ravikhubchandani/generic-console-application-as-a-service/blob/master/src/BackgroundServices/BackgroundServices/Program.cs)

##Extra
* Add dependency resolvers in DependencyResolver.cs https://github.com/ravikhubchandani/generic-console-application-as-a-service/blob/master/src/BackgroundServices/BackgroundServices/DependencyResolver.cs
* Add settings entries in appsettings.json and ApplicationSettings.cs https://github.com/ravikhubchandani/generic-console-application-as-a-service/blob/master/src/BackgroundServices/BackgroundServices/appsettings.json https://github.com/ravikhubchandani/generic-console-application-as-a-service/blob/master/src/BackgroundServices/BackgroundServices/ApplicationSettings.cs
