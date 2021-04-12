![image](https://user-images.githubusercontent.com/44756128/114408744-58e4d900-9b6f-11eb-8725-508f46dee94b.png)

# Introduction
We will learn how to create and manage DNS records inside of Route 53. We'll start by creating a simple application — which will serve as a test website — from two EC2 instances and route traffic using an Application Load Balancer. After this, we will create two records inside a Route 53 hosted zone that will configure the DNS settings for a custom domain and point it to our application.

We'll also look at how DNS works in this scenario (and in general) and how a command line utility like dig can get us more information about a domain. These skills will allow us to associate many kinds of AWS web applications with recognizable web domains such as yourcoolsite.com.

# Creating EC2 Instances and an Application Load Balancer
If you haven't already done so, log in to the live environment.

The first thing we're going to do is launch two instances.

# Create the First EC2 Instance
In the AWS Management Console, navigate to EC2 via Services at the top. Click Launch Instance.

![image](https://user-images.githubusercontent.com/44756128/114409316-efb19580-9b6f-11eb-92ca-5c96e54d65a9.png)

![image](https://user-images.githubusercontent.com/44756128/114409363-ffc97500-9b6f-11eb-98f4-13309d3fd2bf.png)

On the AMI page, select Amazon Linux AMI.

Next, select General purpose t2.micro from the list of instance types, and click Next: Configure Instance Details.

We'll leave Network as the default VPC. Under Subnet, select us-east-1e. For Auto-assign Public IP, select Enable. Before we move on, we need to write a user data script that will install and start a web server on the EC2 instances. To do this, go to the Advanced Details section at the bottom, and paste the following code snippet into the User data box:
```sh
#!/bin/bash
yum update -y
yum install -y httpd
cp /usr/share/httpd/noindex/index.html /var/www/html/index.html
service httpd start
```

![image](https://user-images.githubusercontent.com/44756128/114409829-76ff0900-9b70-11eb-8fe7-a6da8343560a.png)

Click Next: Add Storage.

We can leave the default settings here — the Size (GiB) should be 8GB and the Volume Type should be General Purpose SSD (gp2). Click Next: Add Tags.

Leave all the tag settings on this page (there shouldn't be any tags set), and click Next: Configure Security Group.

On the Configure Security Group page, we can leave the default Create a new security group setting under Assign a security group. Under Security group name, type "SG". Click Add Rule below, and then click the dropdown and select HTTP. We can leave the default settings that autopopulate for Protocol (TCP), Port Range (80), and Source (0.0.0.0/0, ::/0). Click Review and Launch.

![image](https://user-images.githubusercontent.com/44756128/114410059-a9a90180-9b70-11eb-8628-54eb5ac0f091.png)

We're going to get a warning at the top that our security group is wide open to the entire internet, but that's fine for this lab environment. In a real-world setting, we'd want to tighten it up.

![image](https://user-images.githubusercontent.com/44756128/114410143-bcbbd180-9b70-11eb-92ae-e7e4ae94ae08.png)

![image](https://user-images.githubusercontent.com/44756128/114410205-cd6c4780-9b70-11eb-895d-67271883b260.png)

![image](https://user-images.githubusercontent.com/44756128/114410275-dbba6380-9b70-11eb-99b2-0c02518724ae.png)

Everything else should look good, so click Launch.

The final step is to create a key pair so we can use SSH to log in to the instance using a key pair. In the Select an existing key pair or create a new key pair pop-up, click the default Choose an existing key pair to open the dropdown, and select Create a new key pair. In Key pair name, enter "R53KP" and click Download Key Pair. Click Launch Instances.

![image](https://user-images.githubusercontent.com/44756128/114410515-0dcbc580-9b71-11eb-97ed-2a015b7f777d.png)

On the Launch Status page, click View Instances.

![image](https://user-images.githubusercontent.com/44756128/114410657-2936d080-9b71-11eb-81a9-a949732257aa.png)

Here, we'll see the status is pending. It may take a few minutes before it changes to running. While we're waiting, let's make our second instance.

# Create the Second EC2 Instance
Navigate back to EC2 via Services at the top, and click Launch Instance.

On the AMI page, select the same Amazon Linux AMI again.

Next, select General purpose t2.micro, and click Next: Configure Instance Details.

We'll leave Network as the default VPC again. But this time, for Subnet, select us-east-1d. For Auto-assign Public IP, select Enable. Go to the Advanced Details section, and paste the following code snippet into the User data box:
```sh
#!/bin/bash
yum update -y
yum install -y httpd
cp /usr/share/httpd/noindex/index.html /var/www/html/index.html
service httpd start
```

![image](https://user-images.githubusercontent.com/44756128/114411121-86cb1d00-9b71-11eb-8b13-34ba4bafd237.png)

Click Next: Add Storage.

We can leave the default settings again, so just click Next: Add Tags.

Leave all the tag settings on this page, and click Next: Configure Security Group.

This time, on the Configure Security Group page, we want to Select an existing security group under Assign a security group, and then check the box next to our SG security group we created a few minutes ago. Click Review and Launch.

![image](https://user-images.githubusercontent.com/44756128/114411265-a06c6480-9b71-11eb-8a3f-6e5a41cfe073.png)

Everything should be fine, so click Launch.

In the Select an existing key pair or create a new key pair pop-up, leave the default Choose an existing key pair, which should already have our R53KP set. Check the box to acknowledge we have access to the selected private key file, and click Launch Instances.

![image](https://user-images.githubusercontent.com/44756128/114411343-b5e18e80-9b71-11eb-9b2f-c9b02703ba8d.png)

On the Launch Status page, click View Instances.

On the instances page, by now our first instance should have a status of running, and the one we just created is most likely pending.

![image](https://user-images.githubusercontent.com/44756128/114411435-c72a9b00-9b71-11eb-8036-0efb58821903.png)

In the IPv4 Public IP column, copy the IP address listed for the instance that's running. Paste it into a new browser tab, which should bring up an Amazon Linux AMI Test Page.

![image](https://user-images.githubusercontent.com/44756128/114411539-e0334c00-9b71-11eb-8238-09202eeb1e28.png)

# Create the Application Load Balancer
Back in the EC2 console, click Load Balancers in the sidebar (under LOAD BALANCING). Click Create Load Balancer.

![image](https://user-images.githubusercontent.com/44756128/114412111-68195600-9b72-11eb-813b-23ff3e10ba35.png)

Elastic load balancing supports three types of load balancers: Application Load Balancers, Network Load Balancers, and Classic Load Balancers (which are slowly being phased out).

For this lab, we need an Application Load Balancer, so in the Application Load Balancer section, click Create.

In the Basic Configuration section, give it a Name of "ELB" (for "Elastic Load Balancer"), make sure Scheme is set to internet-facing, and set IP address type to ipv4. Leave the defaults in the Listeners section. For Availability Zones, check both us-east-1d and us-east-1e. Click Next: Configure Security Settings.

![image](https://user-images.githubusercontent.com/44756128/114412302-95660400-9b72-11eb-9ef4-3cd9134388fa.png)

We aren't using HTTPS, so we don't have to do anything on the security settings page. Click Next: Configure Security Groups.

![image](https://user-images.githubusercontent.com/44756128/114412386-a9116a80-9b72-11eb-9663-1a50e48897f0.png)

For Assign a security group, select Create a new security group. For Security group name, enter "ELBSG", and leave the defaults in the table below. Click Next: Configure Routing.

![image](https://user-images.githubusercontent.com/44756128/114412489-c34b4880-9b72-11eb-9ff5-fa0383947f6c.png)

In the Target group section, select New target group. Give it a Name of "ELBTG", and leave the default settings for Target type (Instance), Protocol (HTTP), and Port (80). Leave the Health checks settings as they are. In the Advanced health checks settings section, leave everything as it is, but change the value for Healthy threshold to 2* (instead of the default *5). This will speed things up when we're running health checks for our Application Load Balancer. Click **Next: Register Targets.

![image](https://user-images.githubusercontent.com/44756128/114412696-f2fa5080-9b72-11eb-989a-2967343c8694.png)

In the Instances section, check the box check to both of our EC2 instances. Then, click Add to registered above the instances table, which will move our EC2 instances to the Registered targets section above. Make sure they show up there before clicking Next: Review.

![image](https://user-images.githubusercontent.com/44756128/114412840-19b88700-9b73-11eb-9319-dd5eca4b8b67.png)

Everything should be good on the Review page, so click Create. Then click Close.

![image](https://user-images.githubusercontent.com/44756128/114412919-2b019380-9b73-11eb-970e-b2dc1a9085dc.png)

On the load balancer screen, under Description at the bottom, we should see a DNS name for our Application Load Balancer. Copy and paste it into a new browser tab, where we'll see we currently can't reach it. This is because our Application Load Balancer is still in the provisioning state.

![image](https://user-images.githubusercontent.com/44756128/114413007-3c4aa000-9b73-11eb-8d0d-9e02cd8a1d8e.png)

![image](https://user-images.githubusercontent.com/44756128/114413076-48cef880-9b73-11eb-9961-f67e951d0edc.png)

In the meantime, head back to the EC2 console, and click Instances in the sidebar (under INSTANCES), and let's check that both of our instances are running. Copy the IP address of each instance, and paste them both into new browser tabs. Both should properly serve web traffic over port 80 in our browser, displaying the Amazon Linux AMI Test Page.

![image](https://user-images.githubusercontent.com/44756128/114413207-64d29a00-9b73-11eb-940b-4a1e5ede3efe.png)

![image](https://user-images.githubusercontent.com/44756128/114413282-75831000-9b73-11eb-8b8c-0848780f63ff.png)

![image](https://user-images.githubusercontent.com/44756128/114413330-816ed200-9b73-11eb-9453-c8837e69fb91.png)

When our Application Load Balancer is finally finished provisioning, it should display a state of active. It might take a few minutes. If it doesn't show that state, refresh it to see if it just hasn't updated yet. Once it's active, go back to the browser tab with its DNS name, and refresh it to make sure it displays the same Amazon Linux AMI Test Page.

![image](https://user-images.githubusercontent.com/44756128/114413479-a3685480-9b73-11eb-9c52-d4d3fd956011.png)

![image](https://user-images.githubusercontent.com/44756128/114413557-b4b16100-9b73-11eb-9d8c-451d2922b076.png)

Now that this is all set up and properly configured, we're ready to use our Application Load Balancer and our instances' IP addresses for the next step.

# Configuring Route 53 DNS Record Sets
We now want to set up the Route 53 DNS record sets to point to the Application Load Balancer whenever a user navigates to a custom domain name in their browser. Our Application Load Balancer currently has a not-so-pretty domain name, and we want to set up a custom domain name we can send users to so they can interact with our application. Now, let's see how we can do this with Route 53.

Back in the AWS console, navigate to Route 53 via Services (under Networking & Content Delivery).

![image](https://user-images.githubusercontent.com/44756128/114413699-d6aae380-9b73-11eb-84b9-b636cf412109.png)

A custom domain name has already been registered for you, so you don't need to worry about registering one here.

When we're working with Route 53, the first thing we need to deal with is hosted zones. Click Hosted zones, where we'll see a domain name listed — it should be something along the lines of cmcloudlab990.info. Click the domain name, and let's take a look.

![image](https://user-images.githubusercontent.com/44756128/114413865-f9d59300-9b73-11eb-85ad-0dc6a56f2179.png)

We'll see we have two record sets: one with a Type of NS (which stands for "name server") and one with a Type of SOA (which stands for "start of authority"). These are the only ones we need.

![image](https://user-images.githubusercontent.com/44756128/114414057-1d98d900-9b74-11eb-8dda-64bbf77dcafc.png)

Copy the domain name of the NS type, and paste it into a new browser tab. We should see it can't currently be reached. So if a user were to try and access this URL, it wouldn't be found. We need to configure it so that when someone navigates to this domain name in a browser, the user is redirected to the Application Load Balancer's IP address or the IP addresses of the EC2 instances that are serving our application.

![image](https://user-images.githubusercontent.com/44756128/114414179-36a18a00-9b74-11eb-93de-2d275cf9a90d.png)

Here's a quick sidetrack to talk about how that works.

## What Happens When We Access a Custom Domain Name
When we paste this custom domain name into the browser, our browser is going to check to see if it has the IP address cached in the browser or if it's cached at the operating system level. In this case, it doesn't, so it will go over to the ISP we're using and check if the ISP's DNS resolver has information about where to go for the domain name. The ISP's DNS resolver will have a bunch of caching in place, so it will likely know both the location of the website and the locations of name servers, such as the .com top-level domain name server.

If we navigate to a certain domain name, but don't know where it is, we'll ask the ISP's DNS resolver. If it doesn't know, it will ask the root name server where to go. If it doesn't know, it will then move on to the .com name server, which is another server that has more information about the .com top-level domain. If that one doesn't know, it will move the process to the ISP's DNS resolver yet again. Once it has the name servers, it knows where to go to look for more information about the site, and the ISP's DNS resolver will ask Route 53 if it knows where to find the site. Route 53 is the service we're using to have the name servers in the background tell us where to find the website we want to find at the web address.

We're going to do a few things to make Route 53 always point to the correct IP address. Before we work on that part, let's dig a bit more into this process.

## The Command Line Tool dig
We're going to do a little more research using the command line tool dig, which allows us to run the process we just discussed without the need for a web browser.

Note: In a terminal (assuminig dig is installed), run:
```sh
dig www.linuxacademy.com
```

![image](https://user-images.githubusercontent.com/44756128/114415681-86348580-9b75-11eb-8b88-39a14dd76431.png)

We'll see some information about some of the hops we're going through when we're looking for the particular place to find www.linuxacademy.com. Essentially, the question we asked dig was, where can we find www.linuxacademy.com? It's going to give us some answers, including the IP addresses that currently host the website. If we copy one of these IP addresses and paste it into a new browser tab, we'll land on www.linuxacademy.com — it will essentially redirect and show the domain name in the window.

![image](https://user-images.githubusercontent.com/44756128/114415871-b3813380-9b75-11eb-8641-eeb66d24302f.png)

![image](https://user-images.githubusercontent.com/44756128/114415931-c136b900-9b75-11eb-964c-77e998dad0d8.png)

It will also give us information about the name servers for the domain, as well as information about the particular name servers and other information for the IPv4 and IPv6 addresses. We're not going to get too deep into this, but it's good to know we can use this.

If we wanted more detailed information, we could enter:
```sh
dig www.linuxacademy.com +trace
```

![image](https://user-images.githubusercontent.com/44756128/114416083-ee836700-9b75-11eb-9abe-cb9241176cc8.png)

This would essentially go through all the hops, so we'll see all the root servers, top-level domain servers, and name servers, which finally say where we can find the IP addresses for the domain name we're looking for.

Let's continue and head back to Route 53 in the AWS console.

## Create the Record Sets
What do we need to do to make this process happen for our own custom domain name in Route 53?

We need to add a few records to the record sets for this domain. The records will tell Route 53 where to look for the physical machine that's hosting our service to send traffic to that machine.

Click Create Record Set. For Type, set it to A – IPv4 address.

We could use an alias, or we could choose not to use an alias. Aliases allow us to point to particular things, like Application Load Balancers or Classic Load Balancers as well as S3 static websites. The reason we have these inside AWS is because the actual IP address for these different services may change. Behind the scenes, AWS has a bunch of different web servers that, on any given day, might be hosting this different information. These servers may be switching IP addresses around. So if we choose a target alias for the Application Load Balancer, whenever the record set is asked for the record for this particular non-www domain, it's going to check the actual domain that's currently set up for this Application Load Balancer and then check the IP address currently set up for the domain. It will then route us there instead of a static IP address.

Set Alias to Yes. For the Alias Target, set it to the ELB listed under ELB Application load balancers. Click Create. We should now see a new record set listed with a Type of A.

![image](https://user-images.githubusercontent.com/44756128/114416798-a7e23c80-9b76-11eb-855d-2a8af09e0175.png)

Click Create Record Set again. This time, type "www" in the Name box. Leave Type set to A – IPv4 address. Then, set Alias to Yes, and select the ELB again for Alias Target. Click Create. Now, we should have two A records, one for each version of our domain (both the non-www and the www).

![image](https://user-images.githubusercontent.com/44756128/114416946-ca745580-9b76-11eb-92e7-3f6fbf838f5c.png)

![image](https://user-images.githubusercontent.com/44756128/114417029-de1fbc00-9b76-11eb-8689-a84bff38ba24.png)

Now, go back to the browser tab with our domain and try refreshing it again. It should still prevent us from going to the domain because the information in our record sets has to get sent out to the name servers, so it needs a little while to propagate. Wait a few minutes and then refresh.

Note: They could take up to 15 minutes to finally show up, so don't panic if they take longer than you expect. The www and non-www versions might even take different amounts of time to properly load when you try to access the URLs. You could even use dig to check the records and see if they are in yet.

![image](https://user-images.githubusercontent.com/44756128/114417289-17582c00-9b77-11eb-8ba9-08d7898c4722.png)

![image](https://user-images.githubusercontent.com/44756128/114417447-38b91800-9b77-11eb-9255-1303d94ef642.png)

Once some time has passed, refresh it again, and we should finally be able to navigate to the Amazon Linux AMI Test Page from our custom domain.

# Conclusion
We've now successfully used Route 53 to create DNS record sets that point to an Application Load Balancer, which is then routing traffic between multiple EC2 instances. This allows us to have a custom domain name and a highly available, scalable load balancer application.
