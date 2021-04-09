![image](https://user-images.githubusercontent.com/44756128/114199575-0a8acc80-991a-11eb-8be9-bfae8ca669a9.png)

# Introduction
We will create a VPC with an internet gateway, as well as create subnets across multiple Availability Zones.

Log in to the live AWS environment.

## Create a VPC
  - Navigate to the VPC dashboard.
  - Click Your VPCs in the left-hand menu.
  - Click Create VPC, and set the following values:
    - Name tag: VPC1
    - IPv4 CIDR block: 172.16.0.0/16
  - Leave the IPv6 CIDR block and Tenancy fields as their default values.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114201057-6f92f200-991b-11eb-8d8f-52ac4980ef2c.png)

# Create a Public and Private Subnet in Different Availability Zones
## Create Public Subnet
  - Click Subnets in the left-hand menu.
  - Click Create subnet, and set the following values:
    - Name tag: Public1
    - VPC: VPC1
    - Availability Zone: us-east-1a
    - IPv4 CIDR block: 172.16.1.0/24
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114201512-e334ff00-991b-11eb-93ad-b19d732f972a.png)

## Create Private Subnet
  - Click Create subnet, and set the following values:
    - Name tag: Private1
    - VPC: VPC1
    - Availability Zone: us-east-1b
    - IPv4 CIDR block: 172.16.2.0/24
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114201611-019afa80-991c-11eb-8568-6f70ea440831.png)

# Create Two Network Access Control Lists (NACLs), and Associate Each With the Proper Subnet
## Create and Configure Public NACL
  - Click Network ACLs in the left-hand menu.
  - Click Create network ACL, and set the following values:
    - Name tag: Public_NACL
    - VPC: VPC1
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114201807-35762000-991c-11eb-8fbd-660808be3f23.png)

  - With Public_NACL selected, click the Inbound Rules tab below.
  - Click Edit inbound rules.

![image](https://user-images.githubusercontent.com/44756128/114201883-4aeb4a00-991c-11eb-9505-418c4905b1b3.png)

  - Click Add Rule, and set the following values:
    - Rule #: 100
    - Type: HTTP (80)
    - Port Range: 80
    - Source: 0.0.0.0/0
    - Allow / Deny: ALLOW
  - Click Add Rule, and set the following values:
    - Rule #: 110
    - Type: SSH (22)
    - Port Range: 22
    - Source: 0.0.0.0/0
    - Allow / Deny: ALLOW
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114202012-69514580-991c-11eb-8cb1-176eaacc32c0.png)

  - Click the Outbound Rules tab.
  - Click Edit outbound rules.

![image](https://user-images.githubusercontent.com/44756128/114202099-7ec66f80-991c-11eb-9250-51baf45b3a2f.png)

  - Click Add Rule, and set the following values:
    - Rule #: 100
    - Type: Custom TCP Rule
    - Port Range: 1024-65535
    - Destination: 0.0.0.0/0
    - Allow / Deny: ALLOW
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114202222-97368a00-991c-11eb-9a32-e241f0b5e3e5.png)

  - Click the Subnet associations tab.
  - Click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114202295-a87f9680-991c-11eb-8fef-587bd31169cb.png)

  - Select the Public1 subnet, and click Save.

![image](https://user-images.githubusercontent.com/44756128/114202453-cf3dcd00-991c-11eb-98ae-e031243b74d6.png)

## Create and Configure Private NACL
  - Click Create network ACL, and set the following values:
    - Name tag: Private_NACL
    - VPC: VPC1
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114202587-f1374f80-991c-11eb-95b5-3c7c87f43b2b.png)

  - With Private_NACL selected, click the Inbound Rules tab below.
  - Click Edit inbound rules.

![image](https://user-images.githubusercontent.com/44756128/114202659-04e2b600-991d-11eb-9df5-3038772e1e20.png)

  - Click Add Rule, and set the following values:
    - Rule #: 100
    - Type: SSH (22)
    - Port Range: 22
    - Source: 172.16.1.0/24
    - Allow / Deny: ALLOW
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114202742-1af07680-991d-11eb-94a6-d4b42cc20602.png)

  - Click the Outbound Rules tab.
  - Click Edit outbound rules.

![image](https://user-images.githubusercontent.com/44756128/114202781-280d6580-991d-11eb-9b01-b0ee9372560f.png)

  - Click Add Rule, and set the following values:
    - Rule #: 100
    - Type: Custom TCP Rule
    - Port Range: 1024-65535
    - Destination: 0.0.0.0/0
    - Allow / Deny: ALLOW
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114202878-41161680-991d-11eb-9578-f43bd25697aa.png)

  - Click the Subnet associations tab.
  - Click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114202939-4ffcc900-991d-11eb-9b89-aa717fc1c59c.png)

  - Select the Private1 subnet, and click Save.

![image](https://user-images.githubusercontent.com/44756128/114202999-60ad3f00-991d-11eb-8a69-ed002310cba8.png)

## Create an Internet Gateway, and Connect It to the VPC
  - Click Internet Gateways in the left-hand menu.
  - Click Create internet gateway.

![image](https://user-images.githubusercontent.com/44756128/114203105-791d5980-991d-11eb-82c5-82d07d7dca9c.png)

  - Give it a Name tag of "IGW".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114203163-863a4880-991d-11eb-952f-9af93b5c8bca.png)

  - Once it's created, click Actions > Attach to VPC.

![image](https://user-images.githubusercontent.com/44756128/114203223-93efce00-991d-11eb-8e78-8d2f08fe29ce.png)

  - In the dropdown, select our VPC1.

![image](https://user-images.githubusercontent.com/44756128/114203307-a66a0780-991d-11eb-9f54-75bbe08fb9e8.png)

  - Click Attach.

# Create Two Route Tables, and Associate Them with the Correct Subnet
## Create and Configure Public Route Table
  - Click Route Tables in the left-hand menu.

![image](https://user-images.githubusercontent.com/44756128/114203458-d0232e80-991d-11eb-9c20-df599d443030.png)

  - Click Create route table, and set the following values:
    - Name tag: PublicRT
    - VPC: VPC1
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114203576-e8934900-991d-11eb-9aa1-80229e79a043.png)

  - With PublicRT selected, click the Routes tab on the page.
  - Click Edit routes.

![image](https://user-images.githubusercontent.com/44756128/114203707-052f8100-991e-11eb-99fd-7a5053572adf.png)

  - Click Add route, and set the following values:
    - Destination: 0.0.0.0/0
    - Target: Internet Gateway > IGW
  - Click Save routes.

![image](https://user-images.githubusercontent.com/44756128/114203792-18425100-991e-11eb-9806-f15d101d7bbf.png)

  - Click the Subnet Associations tab.
  - Click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114203900-3740e300-991e-11eb-9f40-58bac2d04f2f.png)

  - Select our Public1 subnet.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114203959-458eff00-991e-11eb-81ac-60d95e9f2d5b.png)

## Create and Configure Private Route Table
  - Click Route Tables in the left-hand menu.
  - Click Create route table, and set the following values:
    - Name tag: PrivateRT
    - VPC: VPC1
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114204081-66efeb00-991e-11eb-8bbe-e9a3ba0e234a.png)

  - With PrivateRT selected, click the Subnet Associations tab.
  - Click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114204171-7cfdab80-991e-11eb-91bf-537465755f44.png)

  - Select our Private1 subnet.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114204215-8d158b00-991e-11eb-8bce-1931722914e0.png)
