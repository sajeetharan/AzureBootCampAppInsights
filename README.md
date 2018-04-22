# AzureBootCampAppInsights
This repository contains the slides and the demo materials for the session presented at azure bootcamp colombo 2018

# Demo 1 - ASP.NET Core with Application Insights
## Add Azure Application Insights to your ASP.NET Core webapplication


In this demo, you will learn how to:

  * [Create a default ASP.NET Core webapplication](#create-an-ASPNET-core-webapp)
  * [Publish the webapplication to Azure](#publish-to-azure)
  * [Add Application Insights](#application-insights)
  * [Add tracing information](#add-tracing)
  
<a name="create-an-ASPNET-core-webapp"></a>
## Create a default ASP.NET Core webapplication

1.	Start Visual Studio 2017 and hit **CTRL**+**SHIFT**+**N** to create a new project (or click File/New/Project)
2.	In the 'New Project' view, select **.NET Core** under *Visual C#* and on the right select **ASP.NET Core Web Application**. 
3.  Enter a name for your project and click OK.

	![001 Create a new ASP.NET Core project][1]
   
4.  As a Template select **Web Application** and make sure the authentication is set to **No authentication**. Finish the project creation by clicking **OK**.
	
	![002 Select the Web Application project template][2]

5.  Build (CTRL+SHIFT+B) and run (F5) the application to make sure everything is working. If everything is OK the web application should show up in your predefined browser with a title identical to the name of the project you set earlier.
  
	![003 Build and run your ASP.NET Core project][3]

6.  You just created a full ASP.NET Core webapplication. It's as simple as that. Next up. Publishing this webapplication to Azure.


<a name="publish-to-azure"></a>
## Publish the webapplication to Azure

1. If the application is still running in debug, now is the time so stop it (either close the browser or in Visual Studio hit SHIFT+F5)
2. In the Solution Explorer (hit CTRL+W,S to view the Solution Explorer) right click on the project and select **Publish** from the context menu.
   
   ![004 Select Publish in the Solution Explorer context menu][4]

3. Next select **Microsoft Azure App Service** and select **Create New** before hitting the **Publish** button.
   
   ![005 Select Publish in the Solution Explorer context menu][5]

4. In the *Create Ap Service* screen you have to set a unique _App Name_ value as this will be part of url. Also make sure to select the correct type of App Service being **Web App**. For this demo it's best to change the App Serivce Plan to a **Free** plan so there won't be any additional charges involved.
   
   ![006 Select Publish in the Solution Explorer context menu][6]

5. When you press 'Create' the application is being deployed to Azure, which takes a few minutes. After a while your browser will open and show you the web application online.

   ![007 Your app is online!][7]

<a name="application-insights"></a>
## Add Application Insights

1. In the Solution Explorer (hit CTRL+W,S to view the Solution Explorer) right click on the project and select **Configure Application Insights** from the context menu.
   
   ![008 Select Configure Application Insights in the Solution Explorer context menu][8]

2. In the Application Insights view click the **Start Free** button.
   
   ![009 Click Start Free][9]

3. Click on the *Configure settings* link to change the default settings to your liking. Selecting preferred resources groups and account to use.
   
   > **!! Don't forget to change the plan settings to halt data collection when the limit is reached. This is not set by default this way!!**
	
   ![010 Configure Application Insights][10]

4. Click **Register** to start the Application Insights configuration. When it's done (it takes a moment so relax) press **Finish** to continue. You should now see a screen which states: **Configured: 100%**.

5. Before we can actually see some information we need to publish the appication to Azure again. There right click the project in the *Solution Explorer* and select **Publish** again.

6. As we published the application earlier on we can now simply press the **Publish** button. This will first build the application and then it will be published to Azure again.

   ![011 Configure Application Insights][11]
   ![012 Configure Application Insights][12]

5. Now open the [Azure Portal](http://portal.azure.com) and navigate to **All Resources**.

6. Type ```insight``` in the *All Resources* search bar to search for the Application Insights which we just created.

	![013 Configure Application Insights][13]

7. Open the *purple* (the one with the purple lamp icon) Application Insights node.

8. Now you can see that you application is actually handling requests and you can also view response times and lots of other interesting data.

	![014 Configure Application Insights][14]


<a name="add-tracting"></a>
## Add Tracing information

If you want more than just some request and response times you can add some tracing to you application.

1. Open **Startup.cs** and add the following line of code to the **Configure()** function:
	```C#
	loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
	```
	The **Configure()** function should start somewhat like this:
	![015 AddApplicationInsights tracing to the Startup class][15]

2. Now open **HomeController.cs**.
3. On line 12 *(which is the first line within the HomeController class)* type ```ctor``` followed by a tab. This will automatically create the **HomeController** constructor for you. (you just used a [Visual Studio code snippet](https://docs.microsoft.com/en-us/visualstudio/ide/code-snippets))
4. Append the following using: ```using Microsoft.Extensions.Logging;```
5. Now before the **HomeController** constructor create a private ILogger property called "_logger".
6. Modify the constructor in such a way that it takes a ```ILogger<HomeController>``` named *logger* and sets this *logger* object to the previously defined *_logger* property.
	
7. The **HomeController** constructor should end up like this:
	```C#
	public class HomeController : Controller
	{
		private ILogger _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
	```
	
8. Finally we write a warning in the log whenever someone opens the About page. To do this navigate to the **About()** function within the **HomeController** and add the following line of code to the start of the function:
	```C#
	_logger.LogWarning("SOMEBODY OPENED THE ABOUT PAGE!!");
	```

9. Now save the changes and publish the web application again. (Right click on the project within the Solution Explorer, click Publish followed by the Publish button)

10. After publish is succeeded the webbrowser opens the webapplication in Azure. Now click **About** in the application menu to go to the About page of your application.

11. You can view the Trace information from within Visual Studio by using the **Application Insights** menu. You can find this menu by right clicking next to the **Help** menu. This will open a context menu where you can select **Application Insights**.

	![016 Bring up the Application Insights menu button][16]

11. This should bring up the Application Insights menu button (obviously the notification number may vary):
	
	![017 A nice purple button][17]

12. Click on the Application Insights button to bring up a context menu and select **Search Telemetry**
   
	![018 A nice purple button][18]

13. In the **Application Insights Search** window set the Time range to **Last 30 minutes** and hit the Search button (it's the magnifying glass icon)
14. When you scroll down the results you should be able to find the warning which you just created:
   
	![019 That's a warning][19]



And that's all there is to it.


# Demo 2 & 3 - Data Analysis with Jupyter Notebook and integration with AppInsights
## Lets dive into jupyter notebook with some basic data analysis

1.	Navigate in your browser to https://notebooks.azure.com/

2.  Sign in with your microsoft account

3.  Navigate to Libraries -> New -> Import the csv file - matches.csv

4.  Click new -> Python 2.7 Notebook -> open in new tab

5.  Configure the notebook with necessary imports and visualization
	```Python
	import numpy as np # numerical computing 
	import pandas as pd # data processing, CSV file I/O (e.g. pd.read_csv)
	import matplotlib.pyplot as plt #visualization
	import seaborn as sns #modern visualization
	%matplotlib inline
	sns.set_style("darkgrid")
	plt.rcParams['figure.figsize'] = (14, 8)
	``` 
6. Run the following commands to do the basic analysis on the dataset.
   ```Python 
    matches = pd.read_csv('matches.csv')
    matches.shape
    matches.head()
	matches.describe()
	matches.info()
	matches['season'].unique()
	``` 
7. Lets create some visualizations
   The most successful IPL Team
    ```Python 
	data = matches.winner.value_counts()
	sns.barplot(y = data.index, x = data, orient='h');

	Winning by runs
	 ```Python 
	#sns.barplot(x="day", y="total_bill", data=tips)
	fig, ax = plt.subplots()
	#fig.figsize = [16,10]
	#ax.set_ylim([0,20])
	ax.set_title("Winning by Runs - Team Performance")
	#top_players.plot.bar()
	sns.boxplot(y = 'winner', x = 'win_by_runs', data=matches[matches['win_by_runs']>0], orient = 'h'); #palette="Blues");
	plt.show()

8. Lets integrate appinsights with Jupyter notebook. Create a new notebook and Import the following helper class inside the notebook.
	
9. Create a client to get data from appinsights and create a graph from the response
	```Python 
	jupyterObj = Jupyter('DEMO_APP', 'DEMO_KEY')
	result = jupyterObj.getAIData("requests | where timestamp > ago(1d) | summarize count() by bin(timestamp, 1h)")
	#axes = jupyterObj.sortAxes(result["Rows"], itemgetter(0), 0, 1)
	result = jupyterObj.getAIMetricData(metric="requests/count", \
			startTime=(datetime.now() - timedelta(minutes=15)).isoformat(), \
			endTime=datetime.now().isoformat(), \
			interval="PT5M",\
			aggregation="sum")
	```
10. If you are stuck at any place, go to the notebook which is hosted public , you will be able to find the working demo here,

    https://notebooks.azure.com/anon-p5haka/libraries/analysis



<!--Image references-->
[1]: media/001_aspnet_core.png
[2]: media/002_aspnet_core.png
[3]: media/003_aspnet_core.png
[4]: media/004_publish.png
[5]: media/005_publish.png
[6]: media/006_publish.png
[7]: media/007_publish.png
[8]: media/008_application_insights.png
[9]: media/009_application_insights.png
[10]: media/010_application_insights.png
[11]: media/011_application_insights.png
[12]: media/012_application_insights.png
[13]: media/013_application_insights.png
[14]: media/014_application_insights.png
[15]: media/015_tracing.png
[16]: media/016_tracing.png
[17]: media/017_tracing.png
[18]: media/018_tracing.png
[19]: media/019_tracing.png