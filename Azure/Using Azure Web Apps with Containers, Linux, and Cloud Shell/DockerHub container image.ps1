$app = 'LinuxDockerApp' + (Get-Date).ticks

az webapp create --resource-group $resourceGroup --plan $appServicePlan --name $app --deployment-container-image-name microsoft/dotnet-samples:aspnetapp
