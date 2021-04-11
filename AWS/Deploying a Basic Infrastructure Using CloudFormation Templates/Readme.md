![image](https://user-images.githubusercontent.com/44756128/114305279-1f439d80-9a9d-11eb-8886-a899066c9e86.png)

# Introduction
We will use CloudFormation to provision a basic infrastructure environment with an EC2 instance. There are many different basic infrastructures we could build with CloudFormation, and this is just one example.

We will complete several objectives throughout this, including creating an EC2 key pair, using a CloudFormation template to deploy a basic infrastructure with an EC2 instance, and finally logging in to the instance via SSH to demonstrate the CloudFormation stack provisioned and deployed the environment correctly.

Log into the AWS Management Console.

# Create an EC2 Key Pair and Use the Provided CloudFormation Template to Provision an EC2 Instance
  - From the AWS Management Console, navigate to the EC2 dashboard.
  - In the left-hand menu, under Network & Security, click Key Pairs.

![image](https://user-images.githubusercontent.com/44756128/114305391-a55fe400-9a9d-11eb-9a54-0d6d3afb2594.png)

  - Click Create Key Pair.

![image](https://user-images.githubusercontent.com/44756128/114305410-b6a8f080-9a9d-11eb-9c47-29aeca2cb1c9.png)

  - Give the key pair a name and click Create. Remember the location of the key pair as it cannot be downloaded again.

![image](https://user-images.githubusercontent.com/44756128/114305431-cde7de00-9a9d-11eb-9c27-1ce6adf0ca8e.png)

  - In the top-left corner of the page, open the Services menu, and click CloudFormation.

![image](https://user-images.githubusercontent.com/44756128/114305492-e9eb7f80-9a9d-11eb-9ad4-620b465074dc.png)

  - Click Create stack.

![image](https://user-images.githubusercontent.com/44756128/114305511-fec81300-9a9d-11eb-9fd2-e9edaa92a36c.png)

  - Under Prepare template, select Create template in Designer.
  - In the Create template in designer box, click Create template in designer.

![image](https://user-images.githubusercontent.com/44756128/114305527-13a4a680-9a9e-11eb-8250-568c2c3cd40b.png)

  - At the bottom of the page, switch to the Template tab, and delete any pre-populated text.
  - Using the following CloudFormation template, copy and paste the contents and click Validate template.
  - Click Create stack.
  - Leave the settings as default and click Next.
  - Give the stack a name and for the KeyName, select the key pair created earlier in the lab. Click Next.
  - Continue clicking next, leaving the rest of the settings as default, and click Create stack.
  - In the top-left corner of the page, open the Services menu, and click VPC.
  - Review the resources that were created during the stack creation.

# Use the EC2 Public IP Address to Verify Connectivity to the Instance
From the EC2 dashboard, select the EC2 instance that was created with the stack, and click Connect.
Copy the Example SSH command provided at the bottom of the Connect To Your Instance modal.
Using a local terminal application, change the permissions for the downloaded key pair to read-only:
```sh
cd Downloads
chmod 400 <KEY_PAIR_NAME>.pem
```

Note: You will need to be in the same directory as the key pair to update the permissions.

Using the SSH command copied earlier, attempt to connect to the EC2 instance:
```sh
ssh -i "<KEY_PAIR_NAME>.pem" ec2-user@<EC2_INSTANCE_DNS_NAME>
```

Check the current directory to verify connectivity to the instance:
```sh
pwd
```
