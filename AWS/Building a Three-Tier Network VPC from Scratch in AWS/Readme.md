![image](https://user-images.githubusercontent.com/44756128/114276321-b94b0d80-99eb-11eb-925d-ba0a24aba068.png)

We're going to create a three-tier VPC network from scratch. We'll start by building the VPC, building and attaching an internet gateway, and building six different subnets inside our VPC:

  - A DMZ layer
  - An app layer
  - A database layer

Next, we're going to split these pairs of subnets across two different Availability Zones — the bare minimum we always want to do for highly available and fault-tolerant architecture in AWS.

Then we're going to create two different route tables:

  - A route to the internet for our public subnets, or subnets we want to have access to the open internet
  - A route to the NAT gateway so that anything placed into our private subnets will have a route to update software from the open internet

Finally, we'll add some security to our subnets with three network access control lists (NACLs), which we'll assign to our pairs of subnet layers.

# Build and Configure a VPC, Subnets, and Internet Gateway
Let's begin by creating the foundation of our VPC. We'll start by creating our VPC, creating six subnets inside the VPC, and creating and attaching an internet gateway to our VPC.

Select All services, and then click VPC under Networking & Content Delivery.

![image](https://user-images.githubusercontent.com/44756128/114276746-a33e4c80-99ed-11eb-843c-b0b606e1d2db.png)

On the Resources page, we'll see there aren't currently any VPCs created.
 - Click Your VPCs in the sidebar.
 - Click Create VPC.

![image](https://user-images.githubusercontent.com/44756128/114276759-b6511c80-99ed-11eb-86cc-9f4dbba2d2d2.png)

 - For the Name tag, enter "SysOpsVPC".
 - For the IPv4 CIDR block range, enter "10.99.0.0/16".
 - Leave the IPv6 CIDR block and Tenancy fields as their default values.
 - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114276783-cf59cd80-99ed-11eb-8965-84fab5272ede.png)

We've now created our VPC, but we still need to create the components within the VPC.

We have six subnets to create — two each for the DMZ, app, and database layers.

Let's start with the DMZ layer.
  - Click Subnets in the sidebar.
  - Click Create subnet.

![image](https://user-images.githubusercontent.com/44756128/114276815-f44e4080-99ed-11eb-9d45-12310ec15288.png)

  - For the Name tag, enter "DMZ1public".
  - Select SysOpsVPC in the VPC dropdown menu.
  - Select us-east-1a in the Availability Zone dropdown menu.
  - For the IPv4 CIDR block range, enter "10.99.1.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114276882-34152800-99ee-11eb-8d15-dde4a89e2c43.png)

  - Click Create subnet.
  - For the Name tag, enter "DMZ2public".
  - Set the VPC to SysOpsVPC.
  - Set the Availability Zone to us-east-1b.
  - For the IPv4 CIDR block range, enter "10.99.2.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114276903-5a3ac800-99ee-11eb-82b4-cb751467a8a1.png)

Next, let's make the app layers.
  - Click Create subnet.
  - For the Name tag, enter "AppLayer1private".
  - Set the VPC to SysOpsVPC.
  - Set the Availability Zone to us-east-1a.
  - For the IPv4 CIDR block range, enter "10.99.11.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114276965-aa198f00-99ee-11eb-9b45-0c827d4348fc.png)

  - Click Create subnet.
  - For the Name tag, enter "AppLayer2private".
  - Set the VPC to SysOpsVPC.
  - Set the Availability Zone to us-east-1b.
  - For the IPv4 CIDR block range, enter "10.99.12.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114276994-ce756b80-99ee-11eb-9946-73f3b6e26a73.png)

Finally, let's create our database layers.
  - Click Create subnet.
  - For the Name tag, enter "DBLayer1private".
  - Set the VPC to SysOpsVPC.
  - Set the Availability Zone to us-east-1a.
  - For the IPv4 CIDR block range, enter "10.99.21.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277006-e9e07680-99ee-11eb-911a-7116e5c237c6.png)

  - Click Create subnet.
  - For the Name tag, enter "DBLayer2private".
  - Set the VPC to SysOpsVPC.
  - Set the Availability Zone to us-east-1b.
  - For the IPv4 CIDR block range, enter "10.99.22.0/24".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277035-0a103580-99ef-11eb-84b8-e97728ad3469.png)

We've now created all six subnets. We have three subnets each in the us-east-1a Availability Zone and the us-east-1b Availability Zone.

![image](https://user-images.githubusercontent.com/44756128/114277048-1a281500-99ef-11eb-9903-a2c31d1d9af7.png)

Notice a pattern in the CIDR block ranges? Using the third octet, we categorized them by groups of 10. So for quick reference, we know that if the third octet is:
  - 1 or 2, it's part of the DMZ layer
  - In the teens, it's part of the app layer
  - In the 20s, it's part of the database layer

Note: Whether we labeled these subnets public or private doesn’t actually make them public or private — it’s just a naming construct. We’ll actually make them public or private in a bit when we route them to a public or private route table.

Now, we need to create the internet gateway.
  - Click Internet Gateways in the sidebar.
  - Click Create internet gateway.

![image](https://user-images.githubusercontent.com/44756128/114277077-4479d280-99ef-11eb-85ba-fa4c7f441d7f.png)

  - For the Name tag, enter "IGW".
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277087-55c2df00-99ef-11eb-87fb-14b83c74cbee.png)

Once it's created, you'll see its State says detached. Even though it's been created, it isn't part of the VPC yet. Let's fix that.
  - Click Actions at the top of the screen.
  - Click Attach to VPC.

![image](https://user-images.githubusercontent.com/44756128/114277101-6b380900-99ef-11eb-9bd6-25cfb555ff04.png)

  - Set the VPC to SysOpsVPC.
  - Click Attach.

![image](https://user-images.githubusercontent.com/44756128/114277111-7a1ebb80-99ef-11eb-850d-575c59664c32.png)

The state should now say attached.

![image](https://user-images.githubusercontent.com/44756128/114277123-8d318b80-99ef-11eb-8297-32095cf5cfc7.png)

# Build and Configure a NAT Gateway, Route Tables, and NACLs
Next, we need to create a NAT gateway and route tables, set up proper routing, and create and associate our NACLs.

First, let's create our NAT gateway.
  - Click NAT Gateways in the sidebar.
    - If there’s one already in your account, you can ignore it; we’re still going to create a new one.
  - Click Create NAT Gateway.

![image](https://user-images.githubusercontent.com/44756128/114277178-bbaf6680-99ef-11eb-828f-bae9a868d51f.png)

  - Set the Subnet to DMZ2public.

![image](https://user-images.githubusercontent.com/44756128/114277209-e26d9d00-99ef-11eb-8bb8-0d4448c71fbb.png)

All NAT gateways require that we create and attach an elastic IP address. We could attach our own if we previously created one, but let's have AWS do it for us. Click Create New EIP.

![image](https://user-images.githubusercontent.com/44756128/114277228-f0232280-99ef-11eb-945d-b7d41442945c.png)

Next, we need to create our route tables. (We can do this while the status of our NAT gateway is pending, since it'll take a few minutes.)

Click Route Tables in the sidebar. We can see that a route table already exists — when we created the VPC, it created a default route table. But we're going to create two new route tables.
  - Click Create route table.

![image](https://user-images.githubusercontent.com/44756128/114277243-08933d00-99f0-11eb-8ff1-265782416ced.png)

  - For the Name tag, enter "PublicRT".
  - Set the VPC to SysOpsVPC.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277269-1d6fd080-99f0-11eb-8d02-47fefee24764.png)

  - Click Create route table.
  - For the Name tag, enter "PrivateRT".
  - Set the VPC to SysOpsVPC.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277293-337d9100-99f0-11eb-8947-68e20f9b6e24.png)

On their own, route tables don't do anything — we have to give them routes to something. For the public route table, we need to provide a route to the internet gateway. For the private route table, we need to provide a route to the NAT gateway.

Navigate to the bottom of the screen, where you'll find the Summary and Routes tabs. Select PublicRT at the top, and click the Routes tab at the bottom.

![image](https://user-images.githubusercontent.com/44756128/114277334-5a3bc780-99f0-11eb-817f-138ae615546d.png)

Under Target, it says local, which means it can communicate with any of the subnets that are in the VPC. However, right now, nothing can communicate with the internet gateway, so nothing can communicate with the outside internet. Let's fix that.
  - Click Edit routes.
  - Click Add route.
  - Click into the Target field, and select Internet Gateway from the dropdown menu.
  - Enter "0.0.0.0/0" in the Destination field.
  - Click Save routes.

![image](https://user-images.githubusercontent.com/44756128/114277363-7b041d00-99f0-11eb-881f-e761ed6f4ec9.png)

We've now created a route from our public route table through the internet gateway into the open internet.

Now let's go to the private route table. Here, we need to add a route from the private route table to the NAT gateway.
  - Select PrivateRT at the top of the screen.
  - Under the Routes tab at the bottom of the screen, click Edit routes.

![image](https://user-images.githubusercontent.com/44756128/114277391-9a02af00-99f0-11eb-8a65-a2501de4538c.png)

  - Click Add route.
  - Click into the Target field, and select NAT Gateway from the dropdown menu.
  - Enter "0.0.0.0/0" in the Destination field.
  - Click Save routes.

![image](https://user-images.githubusercontent.com/44756128/114277413-b4d52380-99f0-11eb-91f3-d1cb28702972.png)

Although both of these route tables have been created, they aren't currently associated with any subnets. This is another important part of routing: We have to associate subnets with a route table in order for those subnets — or the resources provisioned inside those subnets — to be able to access them.

Now, we're going to make our public subnets, well, public. They're currently only labeled as public, but by associating them with a route table that has a path to the internet gateway, we're going to make them public for real.
  - At the top of the screen, click PublicRT.
  - Under Subnet Associations at the bottom of the screen, click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114277458-e51cc200-99f0-11eb-8cfc-90e3303e4ef2.png)

  - Select the DMZ1public and DMZ2public subnets.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114277477-f5cd3800-99f0-11eb-9267-af7ac155ad55.png)

Next, let's associate our private subnets with the private route table.
  - Select PrivateRT at the top of the screen.
  - Under Subnet Associations at the bottom of the screen, click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114277487-07164480-99f1-11eb-8f44-9deec97b56fd.png)

  - Select the four private subnets.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114277510-1eedc880-99f1-11eb-866e-3297080a61ba.png)

Now anything placed inside the public route table has a route to the internet gateway, and anything placed inside the private route table has a route to the NAT gateway.

If we have databases or EC2 instances located inside these private subnets, they can get updates from the open internet by going through the NAT gateway, which provides an extra layer of security. Essentially, it’s a one-way street: The resources in the private subnets can access the open internet, but the open internet cannot access the resources in the private subnets (unless we explicitly allow it).

We’re almost done. Before we wrap things up, let’s add another layer of security to our VPC by creating an NACL — a sort of firewall for controlling traffic in and out of one or more subnets — for each of our layers.

Click Network ACLs in the sidebar. We should see a default NACL, like we saw with the route tables. The default NACL was created when we created our VPC. But we're going to create three new ones.
  - Click Create network ACL at the top of the screen.

![image](https://user-images.githubusercontent.com/44756128/114277566-4d6ba380-99f1-11eb-9f5a-621babb3b493.png)

  - For the Name tag, enter "DMZNACL".
  - Set the VPC to SysOpsVPC.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277584-5eb4b000-99f1-11eb-8e56-45aca2542a3a.png)

  - Click Create network ACL at the top of the screen.
  - For the Name tag, enter "AppNACL".
  - Set the VPC to SysOpsVPC.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277608-6e33f900-99f1-11eb-9834-f381283e332c.png)

  - Click Create network ACL at the top of the screen.
  - For the Name tag, enter "DBNACL".
  - Set the VPC to SysOpsVPC.
  - Click Create.

![image](https://user-images.githubusercontent.com/44756128/114277624-8277f600-99f1-11eb-8209-cac0717ab87e.png)

Just like with the route tables, we need to associate subnets with our NACLs.
  - Select DMZNACL at the top of the screen.
  - At the bottom of the screen, click Subnet associations.
  - Click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114277638-97ed2000-99f1-11eb-80d0-4d904d8a4058.png)

  - Select the DMZ layer subnets.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114277655-adfae080-99f1-11eb-80a6-c8c58f491c8c.png)

Now traffic coming in and out of these subnets will be subject to the inbound and outbound rules we set up on this particular NACL. We're not going to set up any rules — right now, we're just building the infrastructure and a shell we could put resources in.

Let's finish things up with the NACLs for the remaining layers.
  - Select AppNACL at the top of the screen.
  - Under Subnet associations at the bottom of the screen, click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114277690-d97dcb00-99f1-11eb-96ff-c37904a51cfe.png)

  - Select the app layer subnets.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114277698-e8fd1400-99f1-11eb-891a-aab0643b86af.png)

  - Select DBNACL at the top of the screen.
  - Under Subnet associations at the bottom of the screen, click Edit subnet associations.

![image](https://user-images.githubusercontent.com/44756128/114277710-fca87a80-99f1-11eb-935b-37c4a4df60d2.png)

  - Select the database layer subnets.
  - Click Save.

![image](https://user-images.githubusercontent.com/44756128/114277727-0a5e0000-99f2-11eb-8cf0-6e661baee71a.png)

![image](https://user-images.githubusercontent.com/44756128/114277735-1944b280-99f2-11eb-9973-67d33990ecee.png)
