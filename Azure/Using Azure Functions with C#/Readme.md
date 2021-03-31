![image](https://user-images.githubusercontent.com/44756128/113200485-f9610200-922d-11eb-912f-1c39b03e43e2.png)


We will be using the Azure SDK and C# code, to create and invoke an Azure function.

To do this, we will build an Azure function using C# code that is triggered by a webhook and saves data to Azure Table storage using a Table storage binding. We will run a console application to see everything working and will verify that table data was saved using Azure Storage Explorer. Finally, we will check the Azure portal to ensure telemetry data was saved to Application Insights.

Log in to the Azure portal

- Under Navigate, click Resource groups.
- Take note of the resource group location and the 5 character suffix for all of the deployed resources. We will use these later in the lab.
![image](https://user-images.githubusercontent.com/44756128/113193648-d03c7380-9225-11eb-9ba9-44cb412caa1b.png)

At the top of the page, click + Add.
Search for Function App.
![image](https://user-images.githubusercontent.com/44756128/113193774-f82bd700-9225-11eb-97f3-7c3f83915cc7.png)

On the Create Function App page, set the following values:
Subscription: Existing subscription
Resource Group: Existing resource group
Function App name: funclab-xxxxx, where xxxxx is the lab suffix for all lab resources (see above)
Publish: Code
Runtime stack: .NET
Version: 3.1
Region: Region where resource group resides (get from opening the resource group in the path in a new tab above)
![image](https://user-images.githubusercontent.com/44756128/113194066-535dc980-9226-11eb-9b5d-e4c6b2dd4827.png)

Click Next: Hosting >, and set the following values:
Storage Account: Existing storage account
Operating System: Windows
Plan type: Consumption (Serverless)
![image](https://user-images.githubusercontent.com/44756128/113194266-8f912a00-9226-11eb-858c-994517a68704.png)

Click Next: Monitoring >. Set the following value:
Enable Application Insights: No (This is due to a current bug in the Azure Portal. We will enable this in a later step.)
![image](https://user-images.githubusercontent.com/44756128/113194368-ae8fbc00-9226-11eb-8a4a-784c052f28c4.png)

Click Review + create > and then click Create.
Once the deployment is complete, click Go to resource.
In the left-hand menu, under Settings, click Application Insights.
Click Turn on Application Insights. Leave all settings as default and click Apply. Click Yes to confirm.
![image](https://user-images.githubusercontent.com/44756128/113194707-062e2780-9227-11eb-937a-2c097da2fa4d.png)

Click View Application Insights data.
On the Overview page, copy the instrumentation key to the clipboard. Paste this into a text editor. You will use this key in future steps.
![image](https://user-images.githubusercontent.com/44756128/113194887-3f669780-9227-11eb-83e9-630605581835.png)

RDP into the VM, Configure Storage Explorer, and Open the Visual Studio Solution

Click the burger menu in the top left corner, and go to Virtual machines.
Start an RDP session with your VM that has Git Bash installed.
From the virtual machine, open Git Bash.

Make a new directory named "git":
mkdir git

Change to the new directory:
cd git

Clone the repository for the Azure labs:
git clone https://github.com/linuxacademy/content-azure-labs.git

Extract the solution file for this lab into the Downloads directory:
unzip content-azure-labs/zips/azure-hol-functions.zip -d ~/Downloads
![image](https://user-images.githubusercontent.com/44756128/113195697-2dd1bf80-9228-11eb-912c-be8a58026002.png)

Open a web browser, and navigate to the download page for Azure Storage Explorer (https://aka.ms/portalfx/downloadstorageexplorer).
![image](https://user-images.githubusercontent.com/44756128/113196326-fd3e5580-9228-11eb-8996-c564326bc509.png)
Download the Windows version, and once the download has finished, install the program with the Setup Wizard.

In the Azure portal, click the burger menu in the top left corner, and go to Storage accounts..
Open the pre-provisioned storage account.
Under Settings, click Access keys, and then click Show keys.
![image](https://user-images.githubusercontent.com/44756128/113196624-560dee00-9229-11eb-9d4b-8f2154707b5d.png)
Copy the key1 connection string to the clipboard. Paste this into a text editor. You will use this string in future steps.

Under Table service, click Tables, and click + Table.
Give the table a name, and click OK. Copy the name down for later use.
![image](https://user-images.githubusercontent.com/44756128/113196830-8bb2d700-9229-11eb-9c04-b2af8efd88e1.png)
![image](https://user-images.githubusercontent.com/44756128/113196906-a2592e00-9229-11eb-9bce-80304f354c77.png)

Back in the VM, open Azure Storage Explorer.
From the left-hand user menu, click Open Connect Dialog.
Select Storage Account, click account name and key and click Next.
Paste in the key1 connection string previously copied from the Azure portal and the name of the storage account you copied from.
Click Next, and then click Connect.
![image](https://user-images.githubusercontent.com/44756128/113197608-7be7c280-922a-11eb-82fa-9e108a287cda.png)

Once the storage account appears in the list, expand the account and verify that the table created earlier is available.
![image](https://user-images.githubusercontent.com/44756128/113197696-95890a00-922a-11eb-8bcb-298651c1bee8.png)

In the VM, open the File Explorer, and navigate to the Downloads folder.
![image](https://user-images.githubusercontent.com/44756128/113197938-edc00c00-922a-11eb-88e8-345297585e38.png)

Open the azure-hol-functions folder, and doubleclick the .sln file. When prompted, open the file using Visual Studio 2019.
Click Sign in.
Sign in using the your Azure Portal credentials.
Leave all of the settings as they are, and click Start Visual Studio.
![image](https://user-images.githubusercontent.com/44756128/113198002-ffa1af00-922a-11eb-9466-873c3cbb19b4.png)

!!Update and Run Visual Studio Solution, Verify Table Data using Storage Explorer, and Verify Application Insights Data!!

Open the Visual Studio Solution Explorer window and open the local.settings.json file.
![image](https://user-images.githubusercontent.com/44756128/113198171-37a8f200-922b-11eb-9fe2-f55636c51945.png)

Paste the instrumentation key and connection string from the text editor into the local.settings.json file, replacing "" and "" with the copied value:

"Values": {
  "APPINSIGHTS_INSTRUMENTATIONKEY": "<INSTRUMENTATION_KEY>"
  "StorageConnectionAppSetting": "<KEY1_CONNECTION_STRING>"
  "FUNCTIONS_WORKER_RUNTIME":  "dotnet"
}

Save and close the file.
From the Solution Explorer window, open the Function1.cs file.
![image](https://user-images.githubusercontent.com/44756128/113198479-97070200-922b-11eb-8205-ad9c8cef883d.png)

Add the following code to the file, replacing "<TABLE_NAME>" with the name of the table you created in the previous objective:

public static class Function1
{
    [FunctionName("Function1")]
    [return: Table("<TABLE_NAME>", Connection = "StorageConnectionAppSetting")]
    public static SampleData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string name = req.Query["name"];

        return new SampleData()
        {
            PartitionKey = "Http",
            RowKey = Guid.NewGuid().ToString(),
            Text = name
        };
    }
}

    public class SampleData : TableEntity
    {
        public string Text { get; set; }
    }
    
Click Save.
Click the play button to start the function.
Once the function runs, copy the Function1 URL.
In a new browser tab, paste the copied URL with the name query string appended:
http://localhost:7071/api/Function1?name=joe
Press Enter.
Refresh the browser page multiple times to populate multiple requests.
To verify our data was stored in Table, navigate back to Azure Storage Explorer.
Click down through the storage account name, then tables, then the name of the table that we've been using in this lab.
To verify the data in Application Insights, navigate back to the Azure portal.
From the All resources page, select the Application Insights resource (the lightbulb icon) provisioned with this lab.
Review the Overview page and note the telemetry data for failed requests, server response time, and server requests.




