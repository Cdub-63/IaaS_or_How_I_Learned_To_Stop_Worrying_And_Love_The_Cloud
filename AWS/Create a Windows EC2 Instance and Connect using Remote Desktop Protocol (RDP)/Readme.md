![image](https://user-images.githubusercontent.com/44756128/114720600-c3c41a80-9cfd-11eb-9620-9291f2e20970.png)

# Introduction
Typically, you will connect to a Linux instance via SSH. But when connecting to an EC2 instance running a Windows operating system, you usually connect with RDP (Remote Desktop Protocol).

We'll create a Windows EC2 instance and connect using RDP. We will configure a security group to allow the RDP protocol and associate the security group with a Windows EC2 instance we will create. Once the instance is created, we'll use Remote Desktop to connect to the instance.

If you're using a Windows machine, you already have an RDP client.

If you're using a Mac computer, download and install the RDP client here:

https://itunes.apple.com/us/app/microsoft-remote-desktop/id1295203466?mt=12

Linux users can use the following program for RDP here:

https://remmina.org/

# Verify the NACL and Security Group Configuration
## Verify RDP Traffic Is Allowed Out to the Internet
First, we'll verify our VPC, internet gateway, route table, and Network Access Control List are configured correctly to allow RDP traffic out to the internet.
  - In the AWS Management Console, navigate to VPC.
  - Click Your VPCs in the left-hand menu.

![image](https://user-images.githubusercontent.com/44756128/114721849-edca0c80-9cfe-11eb-9daf-f825fac0cbd0.png)

  - Click Subnets in the left-hand menu, and we'll see there are two subnets listed.

![image](https://user-images.githubusercontent.com/44756128/114721903-f884a180-9cfe-11eb-9e38-b85c2fe49c5a.png)

  - Click Internet Gateways in the left-hand menu, and we'll see one is listed and attached to the VPC.

![image](https://user-images.githubusercontent.com/44756128/114721970-05a19080-9cff-11eb-9e97-e7efbd17f414.png)

  - Click Route Tables in the left-hand menu, and select the route table that's associated with two subnets.
  - Click the Routes tab at the bottom, and we'll see the internet gateway is attached to the route.

![image](https://user-images.githubusercontent.com/44756128/114722062-194cf700-9cff-11eb-9233-3cc4cf4fee43.png)

  - Click Network ACLs in the left-hand menu, and select the NACL that's associated with two subnets.
  - Click the Inbound Rules tab, and we'll see RDP traffic is allowed through our NACL.

![image](https://user-images.githubusercontent.com/44756128/114722139-2a960380-9cff-11eb-9bb9-720acfa61462.png)

  - Click the Outbound Rules tab, and we'll see all TCP ports are allowed.

![image](https://user-images.githubusercontent.com/44756128/114722180-32ee3e80-9cff-11eb-81b9-f9bd84a2eae7.png)

  - Click Security Groups in the left-hand menu, and select the one listed.
  - Click the Inbound Rules and Outbound Rules tab, and notice all traffic is allowed in and out.

![image](https://user-images.githubusercontent.com/44756128/114722241-41d4f100-9cff-11eb-878b-1b032391e7d3.png)

![image](https://user-images.githubusercontent.com/44756128/114722284-49949580-9cff-11eb-8b2b-b02ead45f55c.png)

Those all look good. Let's move on.

## Create New Security Group and Allow Inbound RDP Traffic into It
Now, we'll create a new security group and allow inbound RDP traffic (port 3389) into our security group.
  - Navigate to EC2 via the Services menu at the top.
  - Click Security Groups in the left-hand menu, and then click Create Security Group.
  - In the Create Security Group popup, use the following values:
    - Security group name: EssentialsSG
    - Description: EssentialsSG
    - VPC: Leave default listed.
    - Inbound: Click Add Rule and use the following values:
      - Type: RDP
      - Protocol: TCP
      - Port Range: 3389
      - Source: Custom 0.0.0.0/0
      - Description: RDP ACCESS
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114722563-83fe3280-9cff-11eb-8ffd-2cdbb43b5779.png)

![image](https://user-images.githubusercontent.com/44756128/114722604-8cef0400-9cff-11eb-8cf7-85e56d8f4690.png)

# Create a Windows EC2 Instance
  - We're ready to create our instance.
  - Navigate to the EC2 dashboard, and click Launch Instance.
  - On the AMI page, scroll to find and select the free-tier Windows server.

![image](https://user-images.githubusercontent.com/44756128/114722786-b445d100-9cff-11eb-96bb-5597912b985f.png)

  - Leave t2.micro selected, and click Next: Configure Instance Details.
  - On the Configure Instance Details page:
    - Leave the default Network and Subnet selected.
    - Auto-assign Public IP: Enable

![image](https://user-images.githubusercontent.com/44756128/114722876-caec2800-9cff-11eb-907e-99fc7bc50232.png)

  - Click Next: Add Storage, and then click Next: Add Tags.
  - On the Add Tags page, add the following tag:
    - Key: Name
    - Value: WinRDP

![image](https://user-images.githubusercontent.com/44756128/114722971-e0f9e880-9cff-11eb-8c07-b410902e5b2d.png)

  - Click Next: Configure Security Group.
  - Click to Select an existing security group, and then select EssentialsSG from the table.

![image](https://user-images.githubusercontent.com/44756128/114723055-f40cb880-9cff-11eb-8b70-f37d53aee8cc.png)

  - Click Review and Launch, and then Launch.
  - In the key pair popup, select Create a new key pair and give it a Key pair name of "windowsrdp". Click Download Key Pair, and then Launch Instances.

![image](https://user-images.githubusercontent.com/44756128/114723194-17376800-9d00-11eb-90bb-f604c13b69a3.png)

  - Click View Instances, and give it a few minutes to enter the running state.

![image](https://user-images.githubusercontent.com/44756128/114723304-2f0eec00-9d00-11eb-9b4a-826893f047bc.png)

  - Once it's running, click Connect at the top.

![image](https://user-images.githubusercontent.com/44756128/114723408-4057f880-9d00-11eb-884f-d3c58073c734.png)

  - Click Download Remote Desktop File, then Save File, and OK.

![image](https://user-images.githubusercontent.com/44756128/114723456-48b03380-9d00-11eb-98a7-6a7caff9555a.png)

  - Click Get Password.

![image](https://user-images.githubusercontent.com/44756128/114723556-5cf43080-9d00-11eb-9550-8c28d0e4018d.png)

  - Click Browse..., and then open your downloaded key pair .pem file.
  - Click Decrypt Password.
  - Copy the password, and then click Close.

# Connect Using RDP
Finally, we'll connect to our RDP instance.
  - Open your Downloads directory, and open the .rdp shortcut file that was downloaded as part of the instance setup.

![image](https://user-images.githubusercontent.com/44756128/114724682-6f229e80-9d01-11eb-8a2c-2f4f8406e204.png)

  - You might get a message saying the connection may not be secure. Click Continue.
  - In the User Account popup, paste in the password we just copied, and click Done and the Continue.
  - The RDP connection should pop up.

![image](https://user-images.githubusercontent.com/44756128/114724726-7c3f8d80-9d01-11eb-9e2b-0c05ccfedec7.png)
