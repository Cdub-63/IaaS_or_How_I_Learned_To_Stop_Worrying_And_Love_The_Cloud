![image](https://user-images.githubusercontent.com/44756128/113318274-eb66bc00-92d5-11eb-96a3-83bf0288079d.png)

We will be using C#, the Azure Cloud Shell/PowerShell/CLI, and Visual Studio to connect to and use Azure Service Bus queues

We will use the Azure Cloud Shell to create a Service Bus namespace and queue. Then, We'll RDP into a Windows VM and update a pre-built Visual Studio solution with the appropriate C# code to connect to the queue, send messages to the queue, and subscribe to the queue to read messages. When We're finished, We will run the console application to see everything working. Finally, We will verify messages were sent and received from the queue using the Azure Portal.

# Set up Cloud Shell
Opne Cloud Shell. Cloud Shell asks us if we would like to use Bash or PowerShell.
  - Click on the PowerShell option.
  - Click Show Advanced Settings when Cloud Shell prompts for a storage account.
    - From here, our Subscription and Resource Group have been populated for us already.
  - Choose the resource group location for our Cloud Shell region.
 
  ![image](https://user-images.githubusercontent.com/44756128/113319621-40570200-92d7-11eb-83c4-258f41b26fff.png)
  
  - Select the Use existing radio button under Storage account to populate our existing account in the associated field.
  - Name your File Share by typing in a name in the File Share field. This name can be anything you want or even random letters.
  - Now that all of the Advanced Settings are filled in, click the Create Storage button to begin creating the storage. After the storage is created for Cloud Shell, the Cloud Shell terminal is ready for us to use!

![image](https://user-images.githubusercontent.com/44756128/113319697-5664c280-92d7-11eb-8bf7-97d4a3638417.png)

 # Create a Service Bus namespace and queue
 ```sh
 $resourceGroup = az group list --query '[0].name' --output json
 $namespaceName  = "LALab" + (Get-Date).ticks
 az servicebus namespace create --resource-group $resourceGroup --name $namespaceName --location southcentralus
 az servicebus queue create --resource-group $resourceGroup --namespace-name $namespaceName --name myQueue
 #
 ```
![image](https://user-images.githubusercontent.com/44756128/113320608-4c8f8f00-92d8-11eb-8fda-416c47fd00cf.png)
  
![image](https://user-images.githubusercontent.com/44756128/113320735-78127980-92d8-11eb-945b-3fe7d2120d61.png)
  
We can verify this process completed successfully in the Azure Portal.

Start by clicking All resources in the navigation bar on the left of the screen.

Our Service Bus has been created and we can click on its name to show more information about it.

![image](https://user-images.githubusercontent.com/44756128/113321242-02f37400-92d9-11eb-8d55-ed832d64b580.png)

Next, click on Queues under the Entities section to show the queue that was created and named myqueue.

All of the necessary steps for setting up our Cloud Shell and creating our namespace and queue are done! Now that that's complete, we can use RDP to connect to our virtual machine.

![image](https://user-images.githubusercontent.com/44756128/113321318-18689e00-92d9-11eb-9fdc-f7711252e6ee.png)

# RDP into VM
To use RDP to connect to your VM, click the All resources button to get back to the resource list. Click on the name of our VM to bring up more information about your VM.

From this page, click on Connect to bring up the connection window.

Click on the Download RDP File button to download a file to use to connect to this VM.

Choose the Save File option and open your browser's Downloads folder.

Choose the icon to View the downloaded item in a folder.

Next, right-click the file and choose Edit.

![image](https://user-images.githubusercontent.com/44756128/113321617-641b4780-92d9-11eb-8846-4a50c547096d.png)

The VM will finish setting up and logging in. Once it is ready, we need to download the C# project used to send and receive messages to our queue. Thankfully, this can be completed with a few handy PowerShell commands:
```sh
Add-Type -Path "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.IO.Compression.FileSystem.dll"
$url = "https://github.com/linuxacademy/content-azure-labs/blob/master/zips/azure-service-bus-queues.zip?raw=true"
$zipfile = "C:\Users\azureuser\Desktop\azure-service-bus-queues.zip"
$folder = "C:\Users\azureuser\Desktop"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -UseBasicParsing -OutFile $zipfile $url
[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $folder)
Remove-Item -Path $zipfile
#
```

![image](https://user-images.githubusercontent.com/44756128/113322080-ddb33580-92d9-11eb-8483-ed4e1df1e07d.png)

You will notice the necessary .zip file appears on the Desktop of your VM. The commands we entered in our PowerShell window will extract this file and then remove the original .zip file. We can close the PowerShell window once the commands have completed. Now we are ready to set up Visual Studio!

![image](https://user-images.githubusercontent.com/44756128/113322213-05a29900-92da-11eb-9d82-1fd5cf0057a6.png)

Open the folder that was extracted to the Desktop of your VM. Double-click on the azure-service-bus-queues file to open this in Visual Studio. In the How do you want to open this file? window, click OK to use Microsoft Visual Studio Version Selector. Visual Studio will start to load, which may take a moment.

# Update Visual Studio Solution, Run Console App, and Verify Queue Message Count in Azure Portal

Update Visual Studio Solution

Start by expanding the Dependencies and then the NuGet tabs, found under azure-service-bus-queues in the Solution Explorer on the right in Visual Studio. We are able to verify we have the Service Bus SDK, shown as Microsoft.Azure.ServiceBus, listed under NuGet. Next, click Program.cs in the Solution Explorer to open our console application. As we are reviewing the code, we notice the code has a few blanks that will need to be filled in.

![image](https://user-images.githubusercontent.com/44756128/113322701-a3966380-92da-11eb-9017-440cd478fe99.png)

The ServiceBusConnectionString line has a blank that needs to be filled in. This information can be found in the Azure Portal.

In the Azure Portal, click All Resources next to Home at the top of the page.

Click on the name of your Service Bus namespace in the list of resources.

Under Settings in the menu on the left of this page, click Shared access policies.

Next, click on the RootManageSharedAccessKey policy.

![image](https://user-images.githubusercontent.com/44756128/113322863-d6405c00-92da-11eb-9d2e-ba0e1e0b2470.png)

The information we are looking for is found here in the Primary Connection String field. Copy this string to your clipboard.

Back in Visual Studio, paste the connection string we copied into the blank next to ServiceBusConnectionString.

![image](https://user-images.githubusercontent.com/44756128/113322965-fd972900-92da-11eb-9bfd-f3370f43a834.png)

Further down in the code, under the static async Task SendMessagesAsync(int numberOfMessagesToSend) section, there is another blank that needs to be filled in.

The line await.queueClient.__________(message); has a blank that needs to be filled in.

The commented code just before this line contains the options we have to pick from. We are looking for the option that sends a message immediately.

Cut or copy the SendAsync option and paste it in to replace the blank in the line.

The line should now look like await.queueClient.SendAsync(message);.

![image](https://user-images.githubusercontent.com/44756128/113323171-44851e80-92db-11eb-8e9f-90acc06c4b0b.png)

Another blank that needs to be filled in is found in the static void RegisterOnMessageHandlerAndReceiveMessages() section.

The line queueClient.__________(ProcessMessageAsync, messageHandlerOptions); has a blank that can be filled in with the options listed directly above it.

Cut or copy the RegisterMessageHandler option and paste it in to replace the blank in the line.

The line should now look like queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);.

![image](https://user-images.githubusercontent.com/44756128/113323305-6c748200-92db-11eb-9bba-1cf0cb0f0b29.png)

The last blank we need to fill in is found in the static async Task ProcessMessagesAsync(Message message, CancellationToken token) section.

Just as before, the line await queueClient.__________(message.SystemProperties.LockToken); has a blank that can be filled in with the options listed directly above it.

Cut or copy the CompleteAsync option and paste it in to replace the blank in the line.

The line should now look like await queueClient.CompleteAsync(message.SystemProperties.LockToken);.

![image](https://user-images.githubusercontent.com/44756128/113323395-87df8d00-92db-11eb-843d-fce51ad09535.png)

Now that all of the blanks have been filled in, let's save our changes by clicking the Save icon in the bar at the top of Visual Studio (or by using the Ctrl+S keyboard shortcut). We are now ready to run our console application!

# Run Console App
It's time to run our console application and watch it work! Click the play icon in the bar at the top of the page. This will execute our code and display its progress in a new window. Our console application first displays that it is sending 10 messages. It then pauses for us to manually verify the queue message count in the Azure Portal.

![image](https://user-images.githubusercontent.com/44756128/113323567-b8272b80-92db-11eb-96b6-861b05228461.png)

# Verify Queue Message Count in Azure Portal
Back in the Azure Portal, we should still be in our Service Bus namespace window. Click on Queues in the menu on the left of this page, found under Entities in the list. Next, click on myqueue to display more information on the queue. The Active message count section will display 10 messages, verifying that the 10 messages sent by our console application have definitely been sent to the queue. Perfect! We can now go back to our console application in Visual Studio and let it continue running.

![image](https://user-images.githubusercontent.com/44756128/113323725-e99ff700-92db-11eb-910b-1303bfc82aa7.png)

In Visual Studio running on our VM, press any key to continue running the console application. The remainder of the code will begin executing and display messages verifying that messages were received from the queue. We can verify that the messages were removed from the queue in the Azure Portal.

![image](https://user-images.githubusercontent.com/44756128/113324003-371c6400-92dc-11eb-9e9e-d2944f3581b0.png)

Here, we can see the Active message count now displays 0 messages, verifying that our console application has received and cleared those messages from the queue.

![image](https://user-images.githubusercontent.com/44756128/113324050-469bad00-92dc-11eb-8a79-c1675c6fc500.png)
