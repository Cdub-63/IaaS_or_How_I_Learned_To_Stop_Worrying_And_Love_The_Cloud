![image](https://user-images.githubusercontent.com/44756128/113424983-abb3d900-9396-11eb-93f1-2423c6f18ae9.png)

Azure Key Vault is a tool that allows IT personnel to securely store and access items such as API keys, passwords, access keys to Azure storage accounts, certificates, and more. Application developers can also reference the Key Vault in their code to access these secrets, as opposed to hard-coding them into their applications.

We will create an Azure Key Vault and review the different components of the vault via the Azure portal. We also use the portal to store and retrieve a password. Finally, we use the vault to store a local password for a Windows virtual machine and deploy the virtual machine using an ARM template. Instead of supplying the password in plaintext, we have the template reference the secret in the Key Vault.

Lessons learned:
  - Create and configure Azure Key Vault.
  - Interface with the Key Vault using Azure portal.
  - Use Azure Key Vault to pass a secure parameter value during deployment.

# Set up Azure Key Vault
In the resource group pane, click + Add > Marketplace.

Search for and click on Key Vault.

Click Create.

![image](https://user-images.githubusercontent.com/44756128/113425474-870c3100-9397-11eb-85fd-3d8c3936bd7c.png)

Set up the basics for you account.

Click Next: Access policy.

Under Enable Access to, select Azure Resource Manager for template deployment.

![image](https://user-images.githubusercontent.com/44756128/113425699-f5e98a00-9397-11eb-9dbd-8ff4d2de35b3.png)

Click Review + create.

Click Create.

Click Go to resource when it appears.

![image](https://user-images.githubusercontent.com/44756128/113425798-1ca7c080-9398-11eb-82ba-cd74d64fde61.png)

Create a Secret in the Key Vault for the VM Password

Click Secrets in the left-hand menu.

Click Generate/Import.

![image](https://user-images.githubusercontent.com/44756128/113425895-3ea14300-9398-11eb-9ded-1b3f227c113e.png)

Configure the secret with the following settings:

Upload options: Manual

Name: VMPass

Value: Something memorable and unique (e.g., P@ssw0rd!1234). Make sure you note this password, as we'll use it later.

Content type: password

![image](https://user-images.githubusercontent.com/44756128/113426026-790ae000-9398-11eb-9c1a-710c5c4fc987.png)

Click Create.

Click Properties in the left-hand menu.

Click the copy icon next to the Resource ID.

Paste this value in a text file, as we will use it in our ARM template.

![image](https://user-images.githubusercontent.com/44756128/113426230-c8511080-9398-11eb-982b-c991f5704485.png)

# Create and Download a Virtual Machine ARM Template
Note: DO NOT click to create the VM once you've configured all its settings. Instead, we will download a template for automation.

Click Home at the top.

Click Virtual machines.

Click Add > Virtual machine.

![image](https://user-images.githubusercontent.com/44756128/113426373-fd5d6300-9398-11eb-89e2-27980861f0d0.png)

On the Basics page:
 - Subscription: Leave as-is
 - Resource group: Select the one in the dropdown
 - Virtual machine name: vm-XXXXX, where XXXXX represents the five-character code you noted earlier
 - Region: Same as your resources
 - Availability options: No infrastructure redundancy required
 - Image: CentOS-based 8.2 (or the most recent version of it)
 - Size: Standard_B1s
 - Authentication type: Password

Enter username and Password

Confirm password: Repeat the password

Inbound port rules: Leave as-is

![image](https://user-images.githubusercontent.com/44756128/113426834-a2783b80-9399-11eb-8fbc-4e751569f4c3.png)

Click Next: Disks, and set the following values:

OS disk type: Standard HDD

![image](https://user-images.githubusercontent.com/44756128/113426904-b9b72900-9399-11eb-9b87-e57e916fab39.png)

Leave everything else as-is.

Click Next: Networking, and set the following values:
 - Virtual network: Select the VNet in your resource group
 - Subnet: default (10.0.0.0/24)
 - Public IP: Select the resource group public IP address you created
 - Leave all other settings as their defaults.

![image](https://user-images.githubusercontent.com/44756128/113427038-f5ea8980-9399-11eb-8145-c53d5a18e483.png)

Click Next: Management, and set the following values:

Boot diagnostics: Disable

Leave all other settings as their defaults.

![image](https://user-images.githubusercontent.com/44756128/113427067-07339600-939a-11eb-81ac-627aac98c454.png)

Click Review + create.

!!DO NOT click to create the VM.!!

Click the link to Download a template for automation.

![image](https://user-images.githubusercontent.com/44756128/113427104-19153900-939a-11eb-8737-1d4de09472d8.png)

# Add the Secret Key to the VM ARM Template

On the Template page, click Download to save the .zip file to your local machine.

![image](https://user-images.githubusercontent.com/44756128/113427274-5aa5e400-939a-11eb-86fc-214d6a5b9247.png)

Extract the .zip file.

Open the template folder.

Open the parameters.json file in a local text editor.

![image](https://user-images.githubusercontent.com/44756128/113427370-81fcb100-939a-11eb-8902-69ed1c7af1aa.png)

At the bottom of the file, locate the following section:
```json
"adminPassword": {
    "value": null
}
```

![image](https://user-images.githubusercontent.com/44756128/113427438-a062ac80-939a-11eb-9b96-4f8825786982.png)

Edit it to match the following, replacing KeyVaultID with the resource ID we copied earlier and KeyVaultSecret with the Key Vault secret name we created earlier (VMPass):
```json
"adminPassword": {
    "reference": {   
        "keyVault": {      
            "id": "KeyVaultID"           
        },     
        "secretName": "KeyVaultSecret"       
    }   
}
```

Note: When it's done, there should be four } brackets at the end of the file.

![image](https://user-images.githubusercontent.com/44756128/113429427-216f7300-939e-11eb-9453-4741451fe471.png)

Save the file.

In the Azure portal, click Home.

Click the resource group.

Click the storage account.

Click File shares.

Click on cloudshell.

![image](https://user-images.githubusercontent.com/44756128/113427655-123af600-939b-11eb-8c85-166a9d58c1bf.png)

Click Upload.

Upload the parameters.json and template.json files.

![image](https://user-images.githubusercontent.com/44756128/113427735-30a0f180-939b-11eb-8953-3ecee6bec6a4.png)

# Create the Virtual Machine Using the ARM Template

Click the Cloud Shell icon (>_) in the menu bar at the top of the screen.

Select Bash.

Click Show advanced settings.

![image](https://user-images.githubusercontent.com/44756128/113427920-84abd600-939b-11eb-940c-25d03a402dbd.png)

Set the following values:
 - Subscription: Select the subscription you have
 - Cloud Shell region: Select the region your resources are located in
 - Resource group: Select your resource group
 - Storage account: Use existing
 - File share: Use existing, and enter the name of the file share from earlier
 - Click Attach storage.

![image](https://user-images.githubusercontent.com/44756128/113428048-b755ce80-939b-11eb-900d-eb5ade141ec7.png)

Change to the clouddrive directory:
```sh
cd clouddrive
```

Paste in the following Azure CLI command, but do not run it:
```sh
az deployment group create --resource-group "<RESOURCE_GROUP>" --template-file template.json --parameters parameters.json
```

With the code pasted, delete the "<RESOURCE_GROUP>" and leave the cursor just behind –-resource-group.

Press Tab twice to complete the resource group name automatically.

![image](https://user-images.githubusercontent.com/44756128/113428350-3fd46f00-939c-11eb-9dae-c49b20d35c24.png)

Press Enter to execute the command.

![image](https://user-images.githubusercontent.com/44756128/113429589-64314b00-939e-11eb-9bf6-8e82d3080132.png)

OUTPUT:
```sh
cloud@Azure:~/clouddrive$ az deployment group create --resource-group 415-f980fe72-using-azure-key-vault-p53 --template-file template.json --parameters parameters.json
{- Finished ..
  "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Resources/deployments/template",
  "location": null,
  "name": "template",
  "properties": {
    "correlationId": "1338d4e4-5f69-4c40-b481-6aa0069048cb",
    "debugSetting": null,
    "dependencies": [
      {
        "dependsOn": [
          {
            "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Network/networkInterfaces/vm-fhq7g191",
            "resourceGroup": "415-f980fe72-using-azure-key-vault-p53",
            "resourceName": "vm-fhq7g191",
            "resourceType": "Microsoft.Network/networkInterfaces"
          }
        ],
        "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Compute/virtualMachines/vm-fhq7g",
        "resourceGroup": "415-f980fe72-using-azure-key-vault-p53",
        "resourceName": "vm-fhq7g",
        "resourceType": "Microsoft.Compute/virtualMachines"
      }
    ],
    "duration": "PT13.9348674S",
    "error": null,
    "mode": "Incremental",
    "onErrorDeployment": null,
    "outputResources": [
      {
        "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Compute/virtualMachines/vm-fhq7g",
        "resourceGroup": "415-f980fe72-using-azure-key-vault-p53"
      },
      {
        "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Network/networkInterfaces/vm-fhq7g191",
        "resourceGroup": "415-f980fe72-using-azure-key-vault-p53"
      }
    ],
    "outputs": {
      "adminUsername": {
        "type": "String",
        "value": "azureuser"
      }
    },
    "parameters": {
      "adminPassword": {
        "reference": {
          "keyVault": {
            "id": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.KeyVault/vaults/kv-fhq7g",
            "resourceGroup": "415-f980fe72-using-azure-key-vault-p53"
          },
          "secretName": "VMPass"
        },
        "type": "SecureString"
      },
      "adminUsername": {
        "type": "String",
        "value": "azureuser"
      },
      "location": {
        "type": "String",
        "value": "eastus"
      },
      "networkInterfaceName": {
        "type": "String",
        "value": "vm-fhq7g191"
      },
      "osDiskType": {
        "type": "String",
        "value": "Standard_LRS"
      },
      "publicIpAddressId": {
        "type": "String",
        "value": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Network/publicIPAddresses/pip-fhq7g"
      },
      "subnetName": {
        "type": "String",
        "value": "default"
      },
      "virtualMachineComputerName": {
        "type": "String",
        "value": "vm-fhq7g"
      },
      "virtualMachineName": {
        "type": "String",
        "value": "vm-fhq7g"
      },
      "virtualMachineRG": {
        "type": "String",
        "value": "415-f980fe72-using-azure-key-vault-p53"
      },
      "virtualMachineSize": {
        "type": "String",
        "value": "Standard_B1s"
      },
      "virtualNetworkId": {
        "type": "String",
        "value": "/subscriptions/0f39574d-d756-48cf-b622-0e27a6943bd2/resourceGroups/415-f980fe72-using-azure-key-vault-p53/providers/Microsoft.Network/virtualNetworks/vnet-fhq7g"
      }
    },
    "parametersLink": null,
    "providers": [
      {
        "id": null,
        "namespace": "Microsoft.Network",
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "eastus"
            ],
            "properties": null,
            "resourceType": "networkInterfaces"
          }
        ]
      },
      {
        "id": null,
        "namespace": "Microsoft.Compute",
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "eastus"
            ],
            "properties": null,
            "resourceType": "virtualMachines"
          }
        ]
      }
    ],
    "provisioningState": "Succeeded",
    "templateHash": "14740114641095705237",
    "templateLink": null,
    "timestamp": "2021-04-02T15:29:21.407791+00:00",
    "validatedResources": null
  },
  "resourceGroup": "415-f980fe72-using-azure-key-vault-p53",
  "tags": null,
  "type": "Microsoft.Resources/deployments"
}
```

When the CLI output appears on the screen, check the Azure portal to confirm the deployment of the VM by going to Home > Virtual machines to ensure the virtual machine exists.

Test Logging on to the VM with the Secret Password

Click the virtual machine we just created.

Click Connect > SSH.

Under Run the example command below to connect to your VM, copy the azureuser email address in the command (it will be similar to `azureuser@vm-l4q3r.southcentralus.cloudapp.azure.com`).

In Cloud Shell, log in to the VM via SSH (replacing <AZUREUSER_EMAIL> with the email address you just copied):

![image](https://user-images.githubusercontent.com/44756128/113430041-19fc9980-939f-11eb-9502-c8ae3910e7c2.png)
```json
ssh <AZUREUSER_EMAIL>
```

At the prompt asking if you want to continue connecting, enter y.

At the password prompt, enter the value of the password secret you created earlier (not the password created when configuring the VM template).

If the prompt becomes azureuser@<VM_NAME>, it means we've set up the secret correctly.

![image](https://user-images.githubusercontent.com/44756128/113430160-4fa18280-939f-11eb-944b-1d2c74a31f21.png)
