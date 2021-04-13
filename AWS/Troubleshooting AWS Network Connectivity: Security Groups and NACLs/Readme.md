![image](https://user-images.githubusercontent.com/44756128/114583205-fa8b2980-9c46-11eb-933e-354eee195e3c.png)

# Introduction
Troubleshooting basic network connectivity issues is an important skill. This troubleshooting scenario is an opportunity to assess skills in this area.

Our scenario is this: a junior administrator has deployed a VPC and instances but there are a few things wrong. Instance3 is not able to connect to the internet and the junior admin can't determine why. Being a senior administrator, it's your responsibility to troubleshoot the issue and ensure that the instance has connectivity to the internet, so that you can ping and log into the instance with SSH.

Log in to the AWS Management Console.

# Determine Why the Instance Cannot Connect to the Internet
  - In the top-left corner of the page, click Services, and navigate to the EC2 dashboard.
  - In the left-hand menu, click Instances.
  - Select Instance3 from the list and review the instance details. Notice the instance does not have a public IP address.

![image](https://user-images.githubusercontent.com/44756128/114583859-a2a0f280-9c47-11eb-909a-5bc70009a4bb.png)

  - In the Actions menu, open the Networking tab, and select Manage IP addresses.

![image](https://user-images.githubusercontent.com/44756128/114583923-b64c5900-9c47-11eb-99ba-6d735efb3ced.png)

  - In the tooltip, click allocate to be redirected to the Elastic IP addresses list.

![image](https://user-images.githubusercontent.com/44756128/114583975-c8c69280-9c47-11eb-8444-2d6636eb8bfe.png)

  - In the top-right corner, click Allocate Elastic IP address.

![image](https://user-images.githubusercontent.com/44756128/114584047-daa83580-9c47-11eb-8518-a40211402256.png)

  - Leave the settings as default and click Allocate.

![image](https://user-images.githubusercontent.com/44756128/114584083-e7c52480-9c47-11eb-972d-599eec323f79.png)

  - Select the IP address and open the Actions dropdown menu. Click Associate Elastic IP address.

![image](https://user-images.githubusercontent.com/44756128/114584149-f90e3100-9c47-11eb-9eca-a9890fcc1758.png)

  - Select Instance3 and click Associate.

![image](https://user-images.githubusercontent.com/44756128/114584250-1642ff80-9c48-11eb-853a-58ff7476c899.png)

  - In the left-hand menu, click Instances.
  - Select Instance3 from the list and review the instance details. Notice the instance now has a public IP address.

![image](https://user-images.githubusercontent.com/44756128/114584332-2c50c000-9c48-11eb-9576-578fbca7d7bb.png)

  - Copy the public IP address and attempt to ping the instance. Is it successful?

![image](https://user-images.githubusercontent.com/44756128/114584537-6621c680-9c48-11eb-890d-6aa897f4608e.png)

# Identify the Second Issue Preventing the Instance from Connecting to the Internet
  - Back in the AWS Console, under Network & Security, click Security Groups.
  - Select the security group associated with Instance3 from the list.

Note: You can find the associated security group in the instance's Security tab when reviewing the instance details.

  - Review the security group details.

![image](https://user-images.githubusercontent.com/44756128/114584904-b6008d80-9c48-11eb-8bf8-233baa8a346c.png)

![image](https://user-images.githubusercontent.com/44756128/114584957-c31d7c80-9c48-11eb-9968-c18d6ba7c1b9.png)

  - In the top-left corner of the page, click Services, and navigate to the VPC dashboard

![image](https://user-images.githubusercontent.com/44756128/114585013-d597b600-9c48-11eb-8f1b-169e8a1c6f24.png)

  - In the left-hand menu, click Subnets.
  - Select the subnet associated with Instance3 from the list. Click the subnet ID to open the subnet details.

![image](https://user-images.githubusercontent.com/44756128/114585244-0ed02600-9c49-11eb-9eb1-80353a5136c4.png)

![image](https://user-images.githubusercontent.com/44756128/114585335-26a7aa00-9c49-11eb-9719-03d52662d9ef.png)

Note: You can find the associated subnet in the instance's Networking tab when reviewing the instance details.

  - Open the Network ACL associated with the subnet by clicking the NACL ID under Network ACLs.
  - Review the Network ACL's inbound rules. Is it denying any traffic?

![image](https://user-images.githubusercontent.com/44756128/114585444-4048f180-9c49-11eb-9fa0-07e00c2b5636.png)

  - In the left-hand menu, under SECURITY, click Network ACLs.
  - Review the inbound rules associated with the other NACLs and find one that allows inbound traffic.

![image](https://user-images.githubusercontent.com/44756128/114585815-a6ce0f80-9c49-11eb-97db-79ef2e25c861.png)

  - Using the left-hand menu, return to the Subnets list, and open PublicSubnet4.
  - Select the Network ACLs tab and click Edit network ACL association.

![image](https://user-images.githubusercontent.com/44756128/114585884-b5b4c200-9c49-11eb-99a1-907683571eb4.png)

  - Select the NACL from the list and click Save.

![image](https://user-images.githubusercontent.com/44756128/114585971-c9f8bf00-9c49-11eb-985a-95cb9c18182c.png)

  - Attempt to ping the instance again. Is it successful?

![image](https://user-images.githubusercontent.com/44756128/114586141-f6acd680-9c49-11eb-8559-08f2577f7346.png)

  - Back in the Subnets list, select PublicSubnet4 and open the Route Table tab.

![image](https://user-images.githubusercontent.com/44756128/114586236-0fb58780-9c4a-11eb-975f-a5e5d794ed40.png)

  - Review the route table details. Notice the route table does not have a route to the internet gateway.

![image](https://user-images.githubusercontent.com/44756128/114586321-25c34800-9c4a-11eb-9696-84c9633ed6f3.png)

  - Click Edit route table assocation.

![image](https://user-images.githubusercontent.com/44756128/114586418-3f648f80-9c4a-11eb-815e-333f9fe3f09c.png)

  - In the Route table ID dropdown, select Public3-RT, and click Save.

![image](https://user-images.githubusercontent.com/44756128/114586449-4ab7bb00-9c4a-11eb-9e7e-bdc83310d65d.png)

  - Attempt to ping the instance again. Is it successful?
 
![image](https://user-images.githubusercontent.com/44756128/114586507-5c995e00-9c4a-11eb-983f-b9c40dacd1b9.png)
