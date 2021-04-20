![image](https://user-images.githubusercontent.com/44756128/115412492-f799b700-a1b9-11eb-82a9-b05fcf89a8d8.png)

Welcome. Come right in. In this lab, we're going to set up a a VPC scheme where a couple of application servers are going to each be sitting in their own private subnets, serving out an application. They'll be sitting behind two public subnets, protected by the NAT gateway and bastion host that will reside out there.

# Create the VPC Skeleton
Create a VPC
Right away, we need to get into the VPC Dashboard. In the Services menu, in the Networking & Content Delivery section, click VPC. Once we're in there, we'll have another menu on our left. We're not using the wizard to create a VPC, so click Your VPCs in that menu, and then the Create VPC button on the next screen. Set the form values in the next screen to these:
  - Name tag: ATD_VPC
  - IPv4 CIDR block: 192.168.0.0/24

Click Create, then (on the next screen) Close, and then we'll land back on the Your VPCs page. We can see ours got created, so we can move on.

![image](https://user-images.githubusercontent.com/44756128/115417440-3c275180-a1be-11eb-8539-4d6d01659b25.png)

## Create Our Subnets
In that same left-hand menu, click Subnets. To make a new one, we can click Create subnet. We've got to do this four times: twice to create public subnets, and twice to create private ones. The settings are coming right up — just remember you need to close one before you can create the next one.

### Public
#### Public #1
  - Name tag: ATD_Public1
  - VPC: ATD_VPC
  - Availability Zone: us-east-1a
  - IPv4 CIDR block: 192.168.0.0/26

![image](https://user-images.githubusercontent.com/44756128/115418460-1d758a80-a1bf-11eb-8b38-6bf9ea4f674f.png)

#### Public #2
  - Name tag: ATD_Public2
  - VPC: ATD_VPC
  - Availability Zone: us-east-1b
  - IPv4 CIDR block: 192.168.0.64/26

![image](https://user-images.githubusercontent.com/44756128/115418616-3e3de000-a1bf-11eb-88f6-ec56da5705ef.png)

### Private
#### Private #3
  - Name tag: ATD_Private3
  - VPC: ATD_VPC
  - Availability Zone: us-east-1a
  - IPv4 CIDR block: 192.168.0.128/26

![image](https://user-images.githubusercontent.com/44756128/115419678-2450cd00-a1c0-11eb-99a4-8038f0ef5f4d.png)

#### Private #4
  - Name tag: ATD_Private4
  - VPC: ATD_VPC
  - Availability Zone: us-east-1b
  - IPv4 CIDR block: 192.168.0.192/26

![image](https://user-images.githubusercontent.com/44756128/115418904-80ffb800-a1bf-11eb-8527-14ed6b70f16a.png)

# Create the Internet Gateway
Now, in the left-hand menu, click Internet Gateways, and then Create internet gateway. We've just got a Name tag field to fill in here, and we'll call this ATD_IGW, then click Create, and Close. It's got a status of detached, so we need to rectify that. In the Actions menu, click Attach to VPC. Choose our VPC from the VPC dropdown there, and click Attach.

![image](https://user-images.githubusercontent.com/44756128/115420139-80b3ec80-a1c0-11eb-9fa1-386074b6e0b6.png)

![image](https://user-images.githubusercontent.com/44756128/115420197-8e697200-a1c0-11eb-8cd6-d3b6736b2cfd.png)

![image](https://user-images.githubusercontent.com/44756128/115420232-975a4380-a1c0-11eb-8962-82aad029dc02.png)

# Create Public and Private Route Tables
## The Public Route Table
Let's click Route Tables in the left menu, leave the default one (that AWS created for us) alone, and click Create route table. In the form we land on, we're going to set the Name tag to ATD_PublicRT, select our VPC from the VPC dropdown, and then click Create and Close.

![image](https://user-images.githubusercontent.com/44756128/115420389-b5c03f00-a1c0-11eb-80f2-ce970d6c8428.png)

Now that it's created, we'll make sure it's selected, and then head to the Routes tab. After we click the Edit routes button, we'll land on the Edit routes page, where we'll click Add route. This will add one in addition to the one that's already sitting there. Our Destination is going to be 0.0.0.0/0, and set the Target as our ATD_IGW internet gateway (which will appear in the dropdown once we click Internet Gateway). Now, we can click Save routes and Close.

![image](https://user-images.githubusercontent.com/44756128/115420589-e2745680-a1c0-11eb-8311-ee19efae2afe.png)

### Subnet Associations
In the Subnet Associations tab, we need to associate our public subnets with this route table. Let's click Edit subnet associations. Here, we'll select our two public subnets, and then click Save.

![image](https://user-images.githubusercontent.com/44756128/115420732-fe77f800-a1c0-11eb-80c3-f7c9b5b7d2f3.png)

## The Private Route Table
Just like we did a few minutes ago, let's create a route table, but we'll call this one ATD_PrivateRT. Once we've saved and close it out, we're back in the main Route tables screen. With our ATD_PrivateRT selected in the list, let's get down into the Routes tab. We won't do anything here, since we don't want anything coming in from or going out to the public for now.

![image](https://user-images.githubusercontent.com/44756128/115421349-7ba36d00-a1c1-11eb-8000-08d248972680.png)

### Subnet Associations
Associate subnets with this route table just like we did a little while ago, but we want to associate the private subnets with this one. Remember, we can do that in the Subnet Associations tab.

![image](https://user-images.githubusercontent.com/44756128/115421498-9fff4980-a1c1-11eb-86ca-b2811c0c9aee.png)

## Configure a NACL
### Create It
Click Network ACLs down in the Security section of the left-hand menu. Once we click on Create network ACL here, we'll land at a new page that needs some information. We're going to set the Name tag as ATD_Public1, select our VPC from the VPC dropdown, and then click Create.

![image](https://user-images.githubusercontent.com/44756128/115421644-bdccae80-a1c1-11eb-9ac6-9fe175dce65d.png)

### Make Some Rules
Back out in the list of NACLs, make sure our new one is selected.

#### Inbound
In the Inbound Rules tab, on the lower part of the screen, click Edit inbound rules, and then click Add Rule. This one will allow SSH access. Set the form values like this:
  - Rule #: 110
  - Type: SSH
  - Protocol: TCP(6)
  - Port Range: 22
  - Source: x.x.x.x/32 (Substitute x.x.x.x with your own public IP)
  - Allow / Deny: ALLOW

![image](https://user-images.githubusercontent.com/44756128/115422070-2ae04400-a1c2-11eb-92a6-ee8845ac1be7.png)

Click Save here, and then get into the Outbound Rules tab.

#### Outbound
Click Edit outbound rules, Add Rule, and set these form values:
  - Rule #: 110
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Destination: x.x.x.x/32 (Substitute x.x.x.x with your own public IP)
  - Allow / Deny: ALLOW

![image](https://user-images.githubusercontent.com/44756128/115422536-9b876080-a1c2-11eb-912b-fb171a473988.png)

#### Subnet Associations
Get into the Subnet associations tab, and click Edit subnet associations. Select the Public1 subnet, and click Save in order to associate this NACL with Public1.

![image](https://user-images.githubusercontent.com/44756128/115422963-02a51500-a1c3-11eb-89b1-b9d7b3ab460c.png)

# Set up a Security Group
In Security Groups, over in the left-hand menu, we need a rule that allows SSH traffic into the bastion host that we'll be creating shortly. Click Create security group, use ATD_Bastion-SG as both a Security group name and a Description. Pick our VPC from the VPC dropdown, then click Create and Close.

![image](https://user-images.githubusercontent.com/44756128/115423123-2bc5a580-a1c3-11eb-9428-b2280288c0fe.png)

## Make Some More Rules
On the main Security Groups page, make sure our new one is selected. Then, we'll head down to the lower part of the page to make some rules.

In the Inbound Rules tab, on the lower part of the screen, click Edit rules and then Add Rule. Set the form values like this:
  - Type: SSH
  - Protocol: TCP
  - Port Range: 22
  - Source: Custom x.x.x.x/32 (Substitute x.x.x.x with your own public IP)
  - Description: RemoteAdmin

Click Save rules here, and then click Close. Security groups are "stateful," so we don't need outbound rules. Once a connection (like an SSH session) is established, traffic can go both ways with just the one rule. NACLs are stateless, though, which is why we had to make both inbound and outbound rules.

![image](https://user-images.githubusercontent.com/44756128/115423394-6596ac00-a1c3-11eb-9b04-5a9c9d9ea5b4.png)

We're done here. Let's go launch an EC2 instance, shall we?

# Configure the Bastion Host
In our main Services menu (up at the top of the screen), find EC2, and open it up in a new browser tab. In the EC2 dashboard, click Launch Instance. Select the Amazon Linux 2 AMI from the list of AMIs, leave t2.micro selected, and click the Next: Configure Instance Details button.

On this page, we're greeted with several dropdowns. We're only concerned with two though. We need to select our ATD_Public1 from the Subnet list, and we need to make sure we set Auto-assign Public IP to Enable.

![image](https://user-images.githubusercontent.com/44756128/115423638-9c6cc200-a1c3-11eb-9a7f-110274ed6d89.png)

Click Next: Add Storage. Everything is fine here, so let's keep moving along by clicking Next: Add Tags. In this screen, click Add tag, and let's set a Key of Name with a Value of BastionHost. Click Next: Configure Security Group.

![image](https://user-images.githubusercontent.com/44756128/115423742-b1e1ec00-a1c3-11eb-8caa-662556cb3396.png)

Here, we'll choose Select an existing security group from the Assign a security group list, select our ATD_Bastion-SG, and then click Review and Launch. Then, click Launch on the next screen.

![image](https://user-images.githubusercontent.com/44756128/115423889-ce7e2400-a1c3-11eb-863f-08075076d987.png)

We'll get a popup about key pairs, where we'll set the dropdown to Create a new key pair, and use atd_keypair as a Key pair name. Click the Download Key Pair button, then the Launch Instances button. Click View Instances, where we'll be taken to the screen that shows us all of our instances and what state they're in.

![image](https://user-images.githubusercontent.com/44756128/115424024-e786d500-a1c3-11eb-991c-4a078a3575c1.png)

Once ours has a status of running, click the Connect button up top. There will be a window popping up with an example SSH command. This will get us in, but remember to take the location of the .pem file into account when you run it. Also remember the file permissions need to be changed. AWS wants it to be read-only and only by the file's owner (you). In a terminal, run these:
```sh
chmod 400 Downloads/atd_keypair.pem
ssh -i "Downloads/atd_keypair.pem" ec2-user@x.x.x.x
```

We'll answer yes to the prompt asking if we're sure we want to connect, and should land at a command prompt in our bastion host.

![image](https://user-images.githubusercontent.com/44756128/115426582-40f00380-a1c6-11eb-8312-eecba1465c4e.png)

# Allow Traffic from the Bastion Host to the Application Servers
In order to do this, we've got to edit an existing security group, as well as add a new one. We'll have to do the same with NACLs.

## Public1 Setup
We'll start off by getting traffic going from the public end.

### Security Group
#### Outbound Rules
In the Security Groups dashboard, let's select our ATD_Bastion-SG and get into the Outbound tab. Click Edit. We want to allow outbound traffic out to the CIDR range of our VPC, but only SSH traffic on port 22. We're going to click the Add Rule button, and set the form values like this:
  - Type: SSH
  - Protocol: TCP
  - Port Range: 22
  - Destination: 192.168.0.0/24
  - Description: SSH to AppServers

![image](https://user-images.githubusercontent.com/44756128/115426921-94625180-a1c6-11eb-9ff5-ac0cc3040a6d.png)

### NACL
#### Inbound Rules
In the Network ACLs dashboard, we'll select ATD_Public1 from the list, get into the Inbound Rules tab, and click the Edit inbound rules button. Here, we'll click Add Rule, and set the form with these values:
  - Rule #: 120
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Source: 192.168.0.0/24
  - Allow / Deny: ALLOW

We're done here, and we can click Save.

![image](https://user-images.githubusercontent.com/44756128/115427131-cc699480-a1c6-11eb-968d-17376ba05c94.png)

### Outbound Rules
But we're not done with the NACL entirely. Let's get into the Outbound Rules tab, click Edit outbound rules, and click Add Rule on the next page.
  - Rule #: 120
  - Type: SSH(22)
  - Protocol: TCP(6)
  - Port Range: 22
  - Destination: 192.168.0.0/24
  - Allow / Deny: ALLOW

We can click Save and move on to the private subnets.

![image](https://user-images.githubusercontent.com/44756128/115427268-f0c57100-a1c6-11eb-8e62-ad4064aab924.png)

## Private3 Setup
Now, we'll set up things in the private end of our layout.

### Create a New NACL
In the Network ACLs dashboard, click Create network ACL. We're going to name it ATD_Private3, select our VPC from the dropdown list, and then click Create.

![image](https://user-images.githubusercontent.com/44756128/115427459-1bafc500-a1c7-11eb-8f91-758523927ffe.png)

### Inbound Rules
Back out in the dashboard, we're going to make sure our new one is selected, get to the Inbound Rules tab, and click Edit inbound rules. We're going to make a few here, and here they are:

#### SSH
  - Rule #: 110
  - Type: SSH(22)
  - Protocol: TCP(6)
  - Port Range: 22
  - Source: 192.168.0.0/26
  - Allow / Deny: ALLOW

#### ICMP
  - Rule #: 120
  - Type: All ICMP - IPv4
  - Protocol: ICMP(1)
  - Port Range: ALL
  - Source: 0.0.0.0/0
  - Allow / Deny: ALLOW

#### Ephemeral
  - Rule #: 130
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Source: 0.0.0.0/0
  - Allow / Deny: ALLOW

Now, we can click Save.

![image](https://user-images.githubusercontent.com/44756128/115427891-78ab7b00-a1c7-11eb-9556-769ef0e6fe94.png)

### Outbound Rules
Let's now get into the Outbound Rules tab, click Edit inbound rules, and add a couple in that next window. Set them like this:

#### HTTPS
  - Rule #: 110
  - Type: HTTPS(443)
  - Protocol: TCP(6)
  - Port Range: 443
  - Destination: 0.0.0.0/0
  - Allow / Deny: ALLOW

#### ICMP
  - Rule #: 120
  - Type: All ICMP - IPv4
  - Protocol: ICMP(1)
  - Port Range: ALL
  - Source: 0.0.0.0/0
  - Allow / Deny: ALLOW

We're done with the rules, so let's click Save.


### Subnet Associations
Let's get into the Subnet associations tab, and click Edit subnet associations. Once we're in there, select our two private subnets and click Edit.

### Create a New Security Group
In the Security Groups dashboard, click on Create security group, and give it these values:
  - Security group name: ATD_Private34_SecGrp
  - Description: ATD_Private34_SecGrp
  - VPC: Pick ours

Now, we can click Create and Close.

### Create Some Rules
On the main Security Groups page, make sure our new one is selected. Let's head down to the lower part of the page again.

### Inbound
In the Inbound Rules tab, on the lower part of the screen, click Edit Rules, and then click Add Rule. Set the form values like this:
  - Type: SSH
  - Protocol: TCP
  - Port Range: 22
  - Source: Custom 192.168.0.0/26 (Our bastion host's subnet)
  - Description: SSH from Bastion Host

We just need the one rule, so we can click Save.

### Outbound
Click Edit rules in the Outbound rules tab, and add a couple with these values:

#### HTTPS
  - Type: HTTPS
  - Protocol: TCP
  - Port Range: 443
  - Destination: Custom 0.0.0.0/0
  - Description: Whatever you like here

#### ICMP
  - Type: All ICMP IPv4
  - Protocol: ICMP
  - Port Range: ALL
  - Destination: Custom 0.0.0.0/0
  - Description: Whatever you like here

We can click Save here.

# Create an EC2 Instance in the Private Subnet
Back in our main Services menu (up at the top of the screen), find EC2 and open it up in a new browser tab. In the EC2 dashboard, click Launch Instance. Select the Amazon Linux 2 AMI from the list of AMIs, leave t2.micro selected, and click the Next: Configure Instance Details button.

On this page, we're not setting much. Select our ATD_Private3 from the Subnet list, and make sure Auto-assign Public IP is set to Disable. We don't want this instance getting a public IP.

Let's click Next: Add Storage, and breeze right through by clicking Next: Add Tags. In this screen, click Add tag, and we'll set a Key of Name with a Value of PrivateAppServer. Click Next: Configure Security Group.

Here, we'll choose Select an existing security group from the Assign a security group list, select our ATD_Private34_SecGrp, and then click Review and Launch. Then, click Launch on the screen after that.

We'll get another popup about key pairs, and this time we'll set the dropdown to Choose an existing key pair, and select atd_keypair from the Select a key pair dropdown. Check the I acknowledge... box, click Launch Instances, and then click View Instances. Just like last time we did this, we'll be taken to the screen that shows us all of our instances and what state they're in.

# Log in to the Instance
Windows Users: Please use the instructions below when prompted during the video lessons:

For instructions on connecting to a Amazon Linux instance using Putty on a Windows computer, please use the following video lesson:

https://linuxacademy.com/cp/courses/lesson/course/2748/lesson/8/module/241

As an alternative to PuTTY, WSL2 can be used. You can find setup steps here: https://docs.microsoft.com/en-us/windows/wsl/install-win10

If you are using a Windows computer and need instructions on configuring the ssh-agent, please use these instructions:

https://aws.amazon.com/blogs/security/securely-connect-to-linux-instances-running-in-a-private-amazon-vpc/

Once our PrivateAppServer has a status of running, we can try logging in. This time, it's a bit trickier though. We need to utilize our shared key, but without storing it on an EC2 (like our bastion host) instance. From our own terminal (the one on our own workstation — we'll exit out of the bastion host if we're still connected), we'll run:
```sh
ssh-add -K "~/Downloads/atd_keypair.pem"
```

We'll enter our passphrase if we're prompted. To verify what we've got set up for SSH keys, run this:
```sh
ssh-add -L
```

It will list our our keys, and the corresponding pem files.

Now, log in to the bastion host again (but without having to specify a keyfile this time):
```sh
ssh -A ec2-user@x.x.x.x
```

And now try to log in to our application server. Grab its private IP address by selecting it in the Instances page of the EC2 dashboard (it will be down in the Description in the lower portion of the screen), then back in the terminal run this, making sure to substitute that last x for our server's real bit:
```sh
ssh ec2-user@192.168.0.x
```

Oops. It's not working. What'd we miss?

## Troubleshoot the Login Failure
Over in the VPC Dashboard, let's double-check our Network ACLs. Select our ATD_Private3. The Inbound Rules tab looks fine, but we forgot to add a rule for ephemeral port traffic in the Outbound Rules tab. Let's do that now. Click Add Rule and set the form like this:
  - Rule #: 130
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Destination: 0.0.0.0/0
  - Allow / Deny: ALLOW

Let's click Save and try logging in again. In the terminal, we should still see it hung (at the ssh command). Hit the Ctrl + c combination to kill it. Run ssh ec2-user@192.168.0.x again, and we should get the
```sh
chmod 400 Downloads/atd_keypair.pem
ssh -i "Downloads/atd_keypair.pem" ec2-user@x.x.x.x
```

We'll answer yes to the prompt and should now land at a command prompt in our app server, the one in our private subnet. In other words, we're in!

# Set up the NAT Gateway
We need to set up a NAT gateway so the app servers can communicate through it and out to the internet.

# NACL Setup
In our main left-hand menu, click Network ACLs, and then Create network ACL. We're going to name this one ATD_Public2-NACL and select our VPC from the list below that. Click Create, and we'll get dumped back to the main NACL page. Select our newest one, then click Edit inbound rules in the Inbound Rules tab. We've got to allow HTTPS traffic, ephemeral ports, and ping traffic. We've got to allow all that traffic out as well. Here's a list of settings:

## Inbound Rules
### HTTPS
  - Rule #: 110
  - Type: HTTPS (443)
  - Protocol: TCP(6)
  - Port Range: 443
  - Source: 192.168.0.0/24
  - Allow / Deny: ALLOW

### Ephemeral
  - Rule #: 120
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Source: 0.0.0.0/0
  - Allow / Deny: ALLOW

### Ping
  - Rule #: 130
  - Type: All ICMP - IPv4
  - Protocol: ICMP(1)
  - Port Range: ALL
  - Source: 0.0.0.0/0
  - Allow / Deny: ALLOW

## Outbound Rules
### HTTPS
  - Rule #: 110
  - Type: HTTPS (443)
  - Protocol: TCP(6)
  - Port Range: 443
  - Destination: 0.0.0.0/0
  - Allow / Deny: ALLOW

### Ephemeral
  - Rule #: 120
  - Type: Custom TCP Rule
  - Protocol: TCP(6)
  - Port Range: 1024-65535
  - Destination: 192.168.0.0/24
  - Allow / Deny: ALLOW

### Ping
  - Rule #: 130
  - Type: All ICMP - IPv4
  - Protocol: ICMP(1)
  - Port Range: ALL
  - Destination: 0.0.0.0/0
  - Allow / Deny: ALLOW

# Subnet Associations
In the Subnet Associations tab, we'll click Edit subnet associations, and select our ATD_Public2 subnet. Click Edit.

Our NACL is done!

# The NAT Gateway Itself
Get into the NAT Gateways screen (it's in the menu on the left) and click Create NAT Gateway. On the next screen, pick our ATD_Public2 from the Subnet dropdown, and since we don't have an Elastic IP yet, click Create New EIP. That form field will populate with eipalloc-xxxx... where xxx and everything afterward is something random that AWS generates. We can click Create NAT Gateway.

Normally in windows like this next one, we just click Close. This time, however, there's a warning. We can't use this NAT Gateway until we edit our route tables. Lucky for us, AWS stuck a handy Edit route tables button in here for us. Let's click it.

# Fix the Route Table
Back in the Route Tables screen, let's select our ATD_PrivateRT, get into the Routes tab, and click Edit routes. We need to add a route. In the form, we're going to set Destination to 0.0.0.0/0, and then in the Target, we'll get into the dropdown and click NAT Gateway. Ours should be in there, so select it. Once we click Save routes, then Close, we should be ready to test.

# Testing
To test this setting, we're going to ping from an app server through the NAT gateway out to the internet. Sound fun? Let's do it.

Hopefully we're still logged in to the app server. If not, we can get logged in again by hopping through the bastion host. Once we're there, try the pinging test:
```sh
ping google.com
```

When we see our reply, we know we set everything up properly.

# Conclusion
We set up a VPC layout, allowing for a couple of application servers to each be sitting in their own private subnets, serving out an application. We created two public subnets for them to sit behind and protected them from the general public using a NAT gateway and bastion host living out on two public subnets we also created. Between that and the routing setup, with the accompanying NACL and security group configuration, this was quite a project. But we did it! Congratulations!
