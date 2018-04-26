To run the project, you need to import scheduler.sql to your database.
Also you need to make some configuration:
	- Create appsettings.json file
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