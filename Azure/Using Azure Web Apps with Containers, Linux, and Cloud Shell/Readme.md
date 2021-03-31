![image](https://user-images.githubusercontent.com/44756128/113201762-83f63100-922f-11eb-93a1-7ca1e18a620a.png)


The purpose of this lab was to gain experience using the Azure Cloud Shell to create App Service plans using Linux, and web apps that use Docker containers. In order to do that I will do the following implementations:
 - Create a Linux App Service plan, and a web app under that plan which uses Docker images on DockerHub and GitHub. 

Log into the Azure portal and open up "Cloud Shell" > Powershell.

!!Use the following to create the linux Service plan:

$resourceGroup = az group list --query '[0].name' --output json
$appServicePlan = 'Linux-App-ServicePlan'

az appservice plan create -g $resourceGroup -n $appServicePlan --is-linux --number-of-workers 1 --sku B1

OUTPUT:
VERBOSE: Authenticating to Azure ...
VERBOSE: Building your Azure drive ...
PS /home/cloud> $resourceGroup = az group list --query '[0].name' --output json
PS /home/cloud> $appServicePlan = 'Linux-App-ServicePlan'
PS /home/cloud>
PS /home/cloud> az appservice plan create -g $resourceGroup -n $appServicePlan --is-linux --number-of-workers 1 --sku B1
{- Finished ..
  "freeOfferExpirationTime": "2021-04-28T15:17:46.363333",
  "geoRegion": "West US",
  "hostingEnvironmentProfile": null,
  "hyperV": false,
  "id": "[<REMOVED>]",
  "isSpot": false,
  "isXenon": false,
  "kind": "linux",
  "location": "westus",
  "maximumElasticWorkerCount": 1,
  "maximumNumberOfWorkers": 0,
  "name": "Linux-App-ServicePlan",
  "numberOfSites": 0,
  "perSiteScaling": false,
  "provisioningState": "Succeeded",
  "reserved": true,
  "resourceGroup": "[<REMOVED>]",
  "sku": {
    "capabilities": null,
    "capacity": 1,
    "family": "B",
    "locations": null,
    "name": "B1",
    "size": "B1",
    "skuCapacity": null,
    "tier": "Basic"
  },
  "spotExpirationTime": null,
  "status": "Ready",
  "subscription": "[<REMOVED>]",
  "tags": null,
  "targetWorkerCount": 0,
  "targetWorkerSizeId": 0,
  "type": "Microsoft.Web/serverfarms",
  "workerTierName": null
}

Use the following to create file shares:
$resourceGroup = az group list --query '[0].name' --output json
$appServicePlan = 'Linux-App-ServicePlan'

az appservice plan create -g $resourceGroup -n $appServicePlan --is-linux --number-of-workers 1 --sku B1

OUTPUT
PS /home/cloud> $app = 'LinuxDockerApp' + (Get-Date).ticks
PS /home/cloud>
PS /home/cloud> az webapp create --resource-group $resourceGroup --plan $appServicePlan --name $app --deployment-container-image-name microsoft/dotnet-samples:aspnetapp
{- Finished ..
  "availabilityState": "Normal",
  "clientAffinityEnabled": true,
  "clientCertEnabled": false,
  "clientCertExclusionPaths": null,
  "cloningInfo": null,
  "containerSize": 0,
  "dailyMemoryTimeQuota": 0,
  "defaultHostName": "linuxdockerapp637526281105222282.azurewebsites.net",
  "enabled": true,
  "enabledHostNames": [
    "linuxdockerapp637526281105222282.azurewebsites.net",
    "linuxdockerapp637526281105222282.scm.azurewebsites.net"
  ],
  "ftpPublishingUrl": "ftp://waws-prod-bay-151.ftp.azurewebsites.windows.net/site/wwwroot",
  "hostNameSslStates": [
    {
      "hostType": "Standard",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "linuxdockerapp637526281105222282.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    },
    {
      "hostType": "Repository",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "linuxdockerapp637526281105222282.scm.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    }
  ],
  "hostNames": [
    "linuxdockerapp637526281105222282.azurewebsites.net"
  ],
  "hostNamesDisabled": false,
  "hostingEnvironmentProfile": null,
  "httpsOnly": false,
  "hyperV": false,
  "id": "[<REMOVED>]",
  "identity": null,
  "inProgressOperationId": null,
  "isDefaultContainer": null,
  "isXenon": false,
  "kind": "app,linux,container",
  "lastModifiedTimeUtc": "2021-03-29T15:21:56",
  "location": "West US",
  "maxNumberOfWorkers": null,
  "name": "LinuxDockerApp637526281105222282",
  "outboundIpAddresses": "13.91.137.115,13.91.137.162,13.91.137.189,13.91.137.194,13.91.137.210,13.91.138.6,40.82.255.130",
  "possibleOutboundIpAddresses": "13.91.137.115,13.91.137.162,13.91.137.189,13.91.137.194,13.91.137.210,13.91.138.6,13.91.138.10,13.91.138.48,13.91.138.79,13.91.138.81,13.91.138.87,13.91.138.91,52.234.92.223,52.234.93.29,52.234.93.65,52.234.93.112,52.234.93.153,52.234.94.28,40.82.255.130",
  "redundancyMode": "None",
  "repositorySiteName": "LinuxDockerApp637526281105222282",
  "reserved": true,
  "resourceGroup": "[<REMOVED>]",
  "scmSiteAlsoStopped": false,
  "serverFarmId": "[<REMOVED>]",
  "siteConfig": {
    "acrUseManagedIdentityCreds": false,
    "acrUserManagedIdentityId": null,
    "alwaysOn": null,
    "apiDefinition": null,
    "apiManagementConfig": null,
    "appCommandLine": null,
    "appSettings": null,
    "autoHealEnabled": null,
    "autoHealRules": null,
    "autoSwapSlotName": null,
    "azureMonitorLogCategories": null,
    "azureStorageAccounts": null,
    "connectionStrings": null,
    "cors": null,
    "customAppPoolIdentityAdminState": null,
    "customAppPoolIdentityTenantState": null,
    "defaultDocuments": null,
    "detailedErrorLoggingEnabled": null,
    "documentRoot": null,
    "experiments": null,
    "fileChangeAuditEnabled": null,
    "ftpsState": null,
    "functionAppScaleLimit": null,
    "functionsRuntimeScaleMonitoringEnabled": null,
    "handlerMappings": null,
    "healthCheckPath": null,
    "http20Enabled": null,
    "httpLoggingEnabled": null,
    "ipSecurityRestrictions": [
      {
        "action": "Allow",
        "description": "Allow all access",
        "ipAddress": "Any",
        "name": "Allow all",
        "priority": 1,
        "subnetMask": null,
        "subnetTrafficTag": null,
        "tag": null,
        "vnetSubnetResourceId": null,
        "vnetTrafficTag": null
      }
    ],
    "javaContainer": null,
    "javaContainerVersion": null,
    "javaVersion": null,
    "keyVaultReferenceIdentity": null,
    "limits": null,
    "linuxFxVersion": null,
    "loadBalancing": null,
    "localMySqlEnabled": null,
    "logsDirectorySizeLimit": null,
    "machineKey": null,
    "managedPipelineMode": null,
    "managedServiceIdentityId": null,
    "metadata": null,
    "minTlsVersion": null,
    "minimumElasticInstanceCount": 0,
    "netFrameworkVersion": null,
    "nodeVersion": null,
    "numberOfWorkers": null,
    "phpVersion": null,
    "powerShellVersion": null,
    "preWarmedInstanceCount": null,
    "publishingPassword": null,
    "publishingUsername": null,
    "push": null,
    "pythonVersion": null,
    "remoteDebuggingEnabled": null,
    "remoteDebuggingVersion": null,
    "requestTracingEnabled": null,
    "requestTracingExpirationTime": null,
    "routingRules": null,
    "runtimeADUser": null,
    "runtimeADUserPassword": null,
    "scmIpSecurityRestrictions": [
      {
        "action": "Allow",
        "description": "Allow all access",
        "ipAddress": "Any",
        "name": "Allow all",
        "priority": 1,
        "subnetMask": null,
        "subnetTrafficTag": null,
        "tag": null,
        "vnetSubnetResourceId": null,
        "vnetTrafficTag": null
      }
    ],
    "scmIpSecurityRestrictionsUseMain": null,
    "scmMinTlsVersion": null,
    "scmType": null,
    "tracingOptions": null,
    "use32BitWorkerProcess": null,
    "virtualApplications": null,
    "vnetName": null,
    "vnetPrivatePortsCount": null,
    "vnetRouteAllEnabled": null,
    "webSocketsEnabled": null,
    "websiteTimeZone": null,
    "winAuthAdminState": null,
    "winAuthTenantState": null,
    "windowsFxVersion": null,
    "xManagedServiceIdentityId": null
  },
  "slotSwapStatus": null,
  "state": "Running",
  "suspendedTill": null,
  "tags": null,
  "targetSwapSlot": null,
  "trafficManagerHostNames": null,
  "type": "Microsoft.Web/sites",
  "usageState": "Normal"
}

We can verify this by navigating to our Azure Portal tab. Click All Resources and then click on our Docker App Service in the list of resources. In the Settings section, click Container settings and note the Image source is set to Docker Hub.

![image](https://user-images.githubusercontent.com/44756128/112860163-320baa80-9079-11eb-839e-5cfc9c45787e.png)

Finally to update web app container image from DockerHub to GitHub. Paste the following:
az webapp config container set --resource-group $resourceGroup --name $app --docker-registry-server-url 'https://github.com/dotnet/dotnet-docker/tree/master/samples/aspnetapp'

OUTPUT
PS /home/cloud> az webapp config container set --resource-group $resourceGroup --name $app --docker-registry-server-url 'https://github.com/dotnet/dotnet-docker/tree/master/samples/aspnetapp'
[
  {
    "name": "WEBSITES_ENABLE_APP_SERVICE_STORAGE",
    "slotSetting": false,
    "value": "false"
  },
  {
    "name": "DOCKER_REGISTRY_SERVER_URL",
    "slotSetting": false,
    "value": "https://github.com/dotnet/dotnet-docker/tree/master/samples/aspnetapp"
  },
  {
    "name": "DOCKER_CUSTOM_IMAGE_NAME",
    "value": "DOCKER|microsoft/dotnet-samples:aspnetapp"
  }
]

We can verify that this command ran successfully by navigating back to the Azure Portal. Refresh the Container settings page. Our Image source now shows as Private Registry and the Server URL is the same URL we provided in our command.

![image](https://user-images.githubusercontent.com/44756128/112860671-a47c8a80-9079-11eb-9980-6a23b2228688.png)

We can verify that our site is running by clicking Overview in the App Service menu. Our endpoint is shown in the URL section on this page. Click on this URL to load our app and verify that it is running.

![image](https://user-images.githubusercontent.com/44756128/112860787-be1dd200-9079-11eb-8697-0116967fd455.png)

![image](https://user-images.githubusercontent.com/44756128/112860992-fe7d5000-9079-11eb-8abf-5b4b2bc3d87b.png)


