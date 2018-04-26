Author: Eglė Plėštytė
Organization: Mathemathics and Informatics faculty, Vilnius University
Date: 2018-04-25


Description
===========

Scheduler is an application for a medical center patients who would like 
to create an appointments with medical center doctors over the internet.

Its is an ASP.NET Core 2.0 web application written using C# language.

Basic user steps to use the Scheduler Medical System.
 - Users can register or login with Google or Facebook OAuth system.
 - User must add one or more people to his account.
 - Select a doctor to make an appointment with
 - Make an appointments with the doctor by selecting appointment time.
 - Person gets a notification of his appointment with a doctor 24 hours 
	before the meeting (using email).
 - User can cancel his appointments.
 

Steps to run the application
============================

To run the project, you need to import scheduler.sql to your database.
Also you need to make some configurations.

	- Create appsettings.json file by the template template:
```	{
	  "ConnectionStrings": {
		"DefaultConnection": "[Your MS SQL Server connection string]"
	  },
	  "Logging": {
		"IncludeScopes": false,
		"LogLevel": {
		  "Default": "Warning"
		}
	  },
	  "Authentication": {
		"Facebook": {
		  "AppId": "[Your Facebook AppId]",
		  "AppSecret": "[Your Facebook AppSectet]"
		},
		"Google": {
		  "ClientId": "[Your Google ClientId]",
		  "ClientSecret": "[Your Google ClientSecret]"
		}
	  },
	  "AdminUser": {
		"UserEmail": "[Admin user account login email]",
		"UserPassword": "[Admin user account login password]"
	  },
	  "Cron": {
		"VisitNotifications": {
		  "notifyBefore": 24
		}
	  }
	}
```
	- Setup  "Services/EmailSender.cs" and change your SMTP server config:
	[smtpserveraddress], [smtpusertname], [smtppassword]
