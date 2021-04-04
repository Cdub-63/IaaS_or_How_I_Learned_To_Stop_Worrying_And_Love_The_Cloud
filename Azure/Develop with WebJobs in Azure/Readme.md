![image](https://user-images.githubusercontent.com/44756128/113201474-282ba800-922f-11eb-8f24-f1af1e455a9c.png)

The puppose of this lab is for us to deploy a WebJob that resizes images. These images have been uploaded to blob storage, but we need smaller versions. This is a process we could use for something like a blog site, where smaller images are required for thumbnails and galleries.

In the Azure portal, create an RDP session to a VM of your choosing

![image](https://user-images.githubusercontent.com/44756128/113051438-9313ab00-916b-11eb-98b2-2eda4ce3ddde.png)

Once you are in the RDP session, run the following commands to download the PhotoStor Web App, the Thumbnailer WebJob, and some sample images that we will use in the lab:

Add-Type -AssemblyName System.IO.Compression.FileSystem

$url = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/create-an-azure-web-app/LA_PhotoStor.zip?raw=true"

$url2 = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/create-an-azure-web-app/images.zip?raw=true"

$url3 = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/develop-with-webjobs-in-azure/Thumbnailer.zip?raw=true"

$zipfile = "C:\Users\azureuser\Desktop\LA_PhotoStor.zip"

$zipfile2 = "C:\Users\azureuser\Desktop\images.zip"

$zipfile3 = "C:\Users\azureuser\Desktop\Thumbnailer.zip"

$folder = "C:\Users\azureuser\Documents\VS"

$folder2 = "C:\Users\azureuser\Pictures"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

Invoke-WebRequest -UseBasicParsing -OutFile $zipfile $url

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $folder)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

Invoke-WebRequest -UseBasicParsing -OutFile $zipfile2 $url2

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile2, $folder2)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

Invoke-WebRequest -UseBasicParsing -OutFile $zipfile3 $url3

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile3, $folder)

Remove-Item -Path $zipfile

Remove-Item -Path $zipfile2

Remove-Item -Path $zipfile3

#

![image](https://user-images.githubusercontent.com/44756128/113052125-62804100-916c-11eb-88c1-df54f86a9de6.png)


!!!The trailing hashtag is used so we paste and run the script in full.!!!

# Deploy the PhotoStor Web App using Visual Studio

Now we are going to deploy a fully functional web application that will allow us to upload images into Azure Blob Storage. We will deploy the code into the pre-provisioned App Service Web App.

Let's fire up Visual Studio 2017 in the VM and open the solution (.sln) file that we just downloaded. It should be sitting in our %userprofile%/Documents/VS/LA_PhotoStor/PhotoStor folder. 

![image](https://user-images.githubusercontent.com/44756128/113053169-99a32200-916d-11eb-8de1-397bc145a4a5.png)

Give it a minute to open everything up (there will be a "completed" type message at the bottom of the window), then get into the View menu and select Error list. We can look down in the lower portion of the window for any errors or warnings. If we don't see any, we're golden, and we can move along.

![image](https://user-images.githubusercontent.com/44756128/113053430-e2f37180-916d-11eb-985b-34facd7e5e26.png)

We'll do that by publishing the application. Right-click the PhotoStor project and click Publish. In the Pick a publish target window, choose Azure App Service, then pick Select existing from the list on the right. In the next window that pops up, the Subscription dropdown should autopopulate with our project, but we've got to pick our web app from the lower box (webapp-aueux), then click the OK button.

![image](https://user-images.githubusercontent.com/44756128/113053753-4382ae80-916e-11eb-82f5-456b391c4d35.png)

Visual Studio will publish the Web App and open it in a web browser when the publishing is complete. We'll see a site that looks like this:

![image](https://user-images.githubusercontent.com/44756128/113053894-6e6d0280-916e-11eb-877a-af714cdf7186.png)

# Test the PhotoStor Web App by uploading sample images

Open a new browser tab (or use the browser window that was automatically opened in the virtual machine) and navigate to the Web App URL. You can copy this value from the Overview pane of the Web App menu.

Upload the sample images we downloaded earlier by either clicking the upload box or dragging them from File Explorer (but note that drag and drop does not work with Edge). Don't upload them all right now. We're going to need a couple for testing later.

You will see the images being uploaded to blob storage, and once there, each image will be listed below the upload box:

![image](https://user-images.githubusercontent.com/44756128/113054718-5fd31b00-916f-11eb-8842-7f9b57d7d697.png)

![image](https://user-images.githubusercontent.com/44756128/113054658-4fbb3b80-916f-11eb-9223-b2c5acc9170a.png)

Clicking any of the hyperlinks will result in the image being displayed.

# Create additional Web App settings for use by the WebJob
To save time, the Web App was already pre-deployed with settings to interact with blob storage. However, additional settings are required for the WebJob to interact with blob storage. We will configure those now.

We're creating three settings:

App Setting Name	                      Value

AzureStorageConfig__ThumbnailContainer	thumbnails

AzureWebJobsDashboard	                  DefaultEndpointsProtocol=https;AccountName=;AccountKey=

AzureWebJobsStorage	                    DefaultEndpointsProtocol=https;AccountName=;AccountKey=

Note: Insert the storage account name and access key values into the AzureWebJobsDashboard and AzureWebJobStorage values, between the =** and the **;, like this:

DefaultEndpointsProtocol=https;AccountName=STORAGE_ACCOUNT_NAME;AccountKey=STORAGE_ACCOUNT_KEY

There's some preparation before this step. We need to get those values. Let's open up a text editor and paste that line into it. We can paste the other two values as we get them. It will save typing, and possibly avoiding a fat-fingering type of mistake.

To find the values we need, we've got to get into our Azure Portal. Click on Storage accounts in the main menu on the left, and then click on our storage account. We need the name of the storage account, and our storage account key. The first is easy to find, because it's right there in front of us on the overview page. For the key though, click on Access keys under Setttings in the menu to the left. Copy the first key and paste it into the text editor we opened up earlier.

![image](https://user-images.githubusercontent.com/44756128/113055276-04edf380-9170-11eb-88df-571ac2089bac.png)

Navigate to the Web App in the Portal. In the Web App menu, click Application Settings.

![image](https://user-images.githubusercontent.com/44756128/113055721-880f4980-9170-11eb-94c0-515e36264919.png)

In the Application settings section, there's an Add new setting link. If we click on that, we're greeted with a pair of input boxes where we can enter a name and a value.

![image](https://user-images.githubusercontent.com/44756128/113056277-34513000-9171-11eb-8c29-01e770ca94a5.png)

Go ahead and save.

# Publish the Thumbnailer WebJob to your Web App
Now we are ready to publish our WebJob. On the virtal machine, close the PhotoStor Web App solution, saving if prompted. Open the Thumbnailer.sln file that we downloaded earlier (the one that's sitting in %userprofile%/Documents/VS/Thumbnailer/).

![image](https://user-images.githubusercontent.com/44756128/113056540-8a25d800-9171-11eb-933a-207b59c752f6.png)

In Solution Explorer, right-click the Thumbnailer project and click Build. After a few seconds, the Output window should show that the application was successfully built. Right-click the Thumbnailer project again, but this time click Publish as Azure Webjob. In the window that pops up, select Microsoft Azure App Service as the publish target, pick our web app in that popout window, and then click OK. Back on the first popout, we can click Validate Connection to make sure our connection is good, then we can click Publish. You will see the following output when publishing has successfully completed:

![image](https://user-images.githubusercontent.com/44756128/113056658-afb2e180-9171-11eb-9af1-276e1d0f93b6.png)

![image](https://user-images.githubusercontent.com/44756128/113056743-d07b3700-9171-11eb-9eb8-a92740b247e5.png)

Thumbnailer -> C:\Users\azureuser\Documents\VS\Thumbnailer\Thumbnailer\bin\Release\Thumbnailer.exe

Copying all files to temporary location below for package/publish:

obj\Release\Package\PackageTmp.

Start Web Deploy Publish the Application/package to https://webapp-vdyit.scm.azurewebsites.net/msdeploy.axd?site=webapp-vdyit ...

Adding sitemanifest (sitemanifest).

Adding directory (webapp-vdyit\app_data).

Adding directory (webapp-vdyit\app_data\jobs).

Adding directory (webapp-vdyit\app_data\jobs\continuous).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\de).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\es).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\fr).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\it).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ja).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ko).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ru).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hans).
Adding directory (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hant).
Adding ACLs for path (webapp-vdyit)
Adding ACLs for path (webapp-vdyit)
Adding ACLs for path (webapp-vdyit/App_Data)
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\de\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\de\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\de\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\de\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\es\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\es\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\es\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\es\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\fr\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\fr\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\fr\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\fr\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ImageProcessor.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ImageProcessor.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\it\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\it\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\it\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\it\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ja\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ja\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ja\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ja\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ko\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ko\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ko\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ko\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Azure.WebJobs.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Azure.WebJobs.Host.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Azure.WebJobs.Host.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Azure.WebJobs.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.Edm.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.Edm.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.OData.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.OData.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.Services.Client.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.Data.Services.Client.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.WindowsAzure.Configuration.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.WindowsAzure.Configuration.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.WindowsAzure.Storage.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.WindowsAzure.Storage.pdb).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Microsoft.WindowsAzure.Storage.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Newtonsoft.Json.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Newtonsoft.Json.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ru\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ru\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ru\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\ru\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\System.Spatial.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\System.Spatial.xml).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Thumbnailer.exe).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Thumbnailer.exe.config).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\Thumbnailer.pdb).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\WebJob1.exe).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\WebJob1.exe.config).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\WebJob1.pdb).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hans\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hans\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hans\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hans\System.Spatial.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hant\Microsoft.Data.Edm.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hant\Microsoft.Data.OData.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hant\Microsoft.Data.Services.Client.resources.dll).
Adding file (webapp-vdyit\app_data\jobs\continuous\Thumbnailer\zh-Hant\System.Spatial.resources.dll).
Adding ACLs for path (webapp-vdyit)
Adding ACLs for path (webapp-vdyit)
Adding ACLs for path (webapp-vdyit/App_Data)
Publish Succeeded.
Web App was published successfully http://webapp-vdyit.azurewebsites.net/

![image](https://user-images.githubusercontent.com/44756128/113056946-10dab500-9172-11eb-8e51-9c192205fa88.png)

!!STOP HERE!!

Remember the mention of pausing while a WebJob restarts? This is it. Hang on here for a bit, and let the web app publish completely. Now is a good time to grab a coffee, go get the mail, or catch up on your Twitter feed. In a minute or two, it's safe to proceed.

Refresh your web browser to view the thumbnails

Refresh your web browser (or reopen it if you closed it). The WebJob should have already completed its first pass on images in the images container. Now, in addition to the list of image URLs, you should also see thumbnails of the images on the right-side of the page!

![image](https://user-images.githubusercontent.com/44756128/113057477-bc840500-9172-11eb-8b4e-4ef1273cbdbb.png)

Upload a few more images to blob storage

Upload a few more of the sample images we downloaded with that Powershell script earlier, by either clicking the upload box or dragging them from File Explorer (again, drag and drop does not work with Edge). The list of images should now include the images you just uploaded.

Where are my thumbnails?

The images were uploaded successfully. However, the WebJob has not yet run against the images container. Why?

Although our WebJob is "continuous" in nature, meaning that it runs all the time, it only checks blob storage containers every 10 minutes for new incoming blobs. This means that our WebJob will eventually process the new incoming blobs, but it might take a while. We can either wait, or simply restart the WebJob, which forces a check of the blob container every time it is started and stopped.

In the Azure Portal, navigate to the Web App, and scroll down to in the menu on the left. Click on WebJobs, highlight our Thumbnailer job, then click Stop up above. Once the WebJob is stopped (wait about a minute), refresh the Webjobs page (using the Azure Refresh button, not our web browser's) to make sure we see the job is stopped, then highlight it and click the Start button.

Now, if we head back to the end-user's view of our web app, we can hit our browser's refresh button, and the thumbnails will have appeared.

![image](https://user-images.githubusercontent.com/44756128/113057901-3a481080-9173-11eb-9d01-93f63d8214aa.png)

Review the WebJob logs in the WebJobs dashboard

The WebJob dashboard is a convenient place to review the logs of a particular WebJob, including a list of the recent invocations.

In the WebJob lens, select the WebJob and then click Logs. The dashboard opens in a new browser tab. Here you can view the current status of the WebJob, its associated functions, and its recent invocations.

![image](https://user-images.githubusercontent.com/44756128/113058437-bcd0d000-9173-11eb-85e7-a8b49def3e0d.png)

![image](https://user-images.githubusercontent.com/44756128/113058514-d70aae00-9173-11eb-9f52-42879f7f3cae.png)

We did it! Not only does our web app allow us to upload images but it automatically creates thumbnails of those images, so that visitors aren't chewing up bandwidth or waiting a long time for the big images to load.
