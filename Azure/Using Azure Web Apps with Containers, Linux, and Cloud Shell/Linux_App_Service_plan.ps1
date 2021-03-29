$resourceGroup = az group list --query '[0].name' --output json
$appServicePlan = 'Linux-App-ServicePlan'

az appservice plan create -g $resourceGroup -n $appServicePlan --is-linux --number-of-workers 1 --sku B1
