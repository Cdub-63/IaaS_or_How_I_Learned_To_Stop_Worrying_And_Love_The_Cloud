![image](https://user-images.githubusercontent.com/44756128/117021368-e920c480-acbc-11eb-9a83-8db6a79e2f7d.png)

# Introduction
We will create a transit gateway and attach two VPCs. We will review the propagated routes on the Transit Gateway, create the appropriate routes in our VPCs, and validate the connectivity.

# Create the Transit Gateway
  - Navigate to Services.
  - Under Networking & Content Delivery, click VPC.
  - Click VPCs.
  - In the left sidebar menu, click Transit Gateways.
  - Click Create Transit Gateway.
  - Set the following values:
    - Name tag: MyTGW
    - Description: MyTGW
    - Amazon side ASN: 65065
  - Leave the rest as their defaults and click Create Transit Gateway.
  - Click Close.
Note: It may take a few minutes for the transit gateway to change from a pending state to an available state.

![image](https://user-images.githubusercontent.com/44756128/117023060-5f71f680-acbe-11eb-9832-6c7e61f08a15.png)

![image](https://user-images.githubusercontent.com/44756128/117023136-71539980-acbe-11eb-8ed4-0bdc18c0331a.png)

# Transit Gateway Attachments
  - In the left sidebar menu, click Transit Gateway Attachments.
  - Click Create Transit Gateway Attachment.
  - Set the following values:
    - Transit Gateway ID: MyTGW
    - Attachment type: VPC
    - Attachment name tag: VPC1
    - DNS support: enable
    - VPC ID: VPC1
  - Under Subnet IDs, select us-east-1a as the Availability Zone and PublicSubnet1 as the Subnet ID.
  - Click Create attachment.

![image](https://user-images.githubusercontent.com/44756128/117023734-fb036700-acbe-11eb-9166-2539e6f3f050.png)

  - Click Close.
  - Click Create Transit Gateway Attachment, and set the following values for VPC2:
    - Transit Gateway ID: MyTGW
    - Attachment type: VPC
    - Attachment name tag: VPC2
    - DNS support: enable
    - VPC ID: VPC2
  - Under Subnet IDs, select us-east-1a as the Availability Zone and PublicSubnet2 as the Subnet ID.
  - Click Create attachment.

![image](https://user-images.githubusercontent.com/44756128/117024050-43228980-acbf-11eb-85e4-512bdaf6c334.png)

  - Click Close.
  - Click Create Transit Gateway Attachment, and set the following values for VPC3:
    - Transit Gateway ID: MyTGW
    - Attachment type: VPC
    - Attachment name tag: VPC3
    - DNS support: enable
    - VPC ID: VPC3
  - Under Subnet IDs, select us-east-1a as the Availability Zone and PublicSubnet3 as the Subnet ID.
  - Click Create attachment.

![image](https://user-images.githubusercontent.com/44756128/117024164-5f262b00-acbf-11eb-9b83-918e05f426fd.png)

  - Click Close.

![image](https://user-images.githubusercontent.com/44756128/117024381-8ed53300-acbf-11eb-89da-e9645ae4e47e.png)

# Routes
  - In the left sidebar menu, click Route Tables.
  - Select the Public1-RT route table.
  - Select the Routes tab and click Edit routes.

![image](https://user-images.githubusercontent.com/44756128/117024872-01461300-acc0-11eb-84b0-07d1e617e109.png)

  - Click Add route and set the following values:
    - Destination: 10.2.0.0/16
    - Target: Transit Gateway
  - Click Save routes.

![image](https://user-images.githubusercontent.com/44756128/117025016-22a6ff00-acc0-11eb-8caf-dbf06f92bd62.png)

  - Click Close.
  - Select the Public2-RT route table.
  - Under the Routes tab, click Edit routes.

![image](https://user-images.githubusercontent.com/44756128/117025158-49653580-acc0-11eb-88f8-1fe770db57e3.png)

  - Click Add route and set the following values:
    - Destination: 10.1.0.0/16
    - Target: Transit Gateway
  - Click Save routes.
  - Click Close.

![image](https://user-images.githubusercontent.com/44756128/117025419-83ced280-acc0-11eb-98d3-9adf414aecec.png)

# Testing
Open a terminal window.
Log in to INSTANCE1 via SSH:
```sh
ssh cloud_user@<INSTANCE1_PUBLIC_IP_ADDRESS>
```

Ping the public IP address of INSTANCE2:
```sh
ping <INSTANCE2_PUBLIC_IP_ADDRESS>
```

Ping the private IP address of INSTANCE2:
```sh
ping <INSTANCE2_PRIVATE_IP_ADDRESS>
```

Ping the private IP address of INSTANCE3:
```sh
ping <INSTANCE3_PRIVATE_IP_ADDRESS>
```

![image](https://user-images.githubusercontent.com/44756128/117025725-c690aa80-acc0-11eb-91e3-8f05aa3bb81b.png)
