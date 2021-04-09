![image](https://user-images.githubusercontent.com/44756128/114194132-ccd77500-9914-11eb-8a9f-03374e202fcb.png)

# Introduction
AWS Identity and Access Management (IAM), is a service that allows AWS customers to manage user access and permissions for their accounts, and available APIs/services within AWS. IAM can manage users, security credentials (such as API access keys), and allow users to access AWS resources.

We will walk through the foundations of IAM. We'll focus on user and group management as well as how to assign access to specific resources using IAM managed policies. We'll learn how to find the login URL where AWS users can log in to their account and explore this from a real-world use case perspective.

Log in to the AWS Management Console using your credentials.

# Add the Users to the Proper Groups
  - From the AWS Management Console, navigate to the IAM dashboard.

![image](https://user-images.githubusercontent.com/44756128/114196665-32c4fc00-9917-11eb-964f-158d2c5f94a4.png)

  - In the left-hand menu, click Groups and open the S3-Support group.
 
![image](https://user-images.githubusercontent.com/44756128/114195360-0361bf80-9916-11eb-905c-e8239e9ed6a1.png)
  
  - Select the Users tab, and click Add Users to Group.

![image](https://user-images.githubusercontent.com/44756128/114195444-14aacc00-9916-11eb-9294-1517853595a7.png)

  - From the list, select user-1 and click Add Users.
  
![image](https://user-images.githubusercontent.com/44756128/114195519-23917e80-9916-11eb-9f1d-bac283ce1f5e.png)

  - Back in the Groups list, select the EC2-Support group.

![image](https://user-images.githubusercontent.com/44756128/114195732-576ca400-9916-11eb-9222-cb2c52ca7c81.png)

  - Select the Users tab, and click Add Users to Group.

![image](https://user-images.githubusercontent.com/44756128/114195787-65222980-9916-11eb-9644-b8fb52388b12.png)

  - From the list, select user-2 and click Add Users.

![image](https://user-images.githubusercontent.com/44756128/114195856-766b3600-9916-11eb-9114-8b0c2518c4ae.png)

  - Back in the Groups list, select the EC2-Admin group.

![image](https://user-images.githubusercontent.com/44756128/114195975-8d118d00-9916-11eb-9f9c-8d180e5cad50.png)

  - Select the Users tab, and click Add Users to Group.

![image](https://user-images.githubusercontent.com/44756128/114196022-9864b880-9916-11eb-9615-d240263d65b5.png)

  - From the list, select user-3 and click Add Users.

![image](https://user-images.githubusercontent.com/44756128/114196075-a581a780-9916-11eb-8712-31c9b62b5b88.png)

  - In the left-hand menu, click Users and select user-3 from the list.

![image](https://user-images.githubusercontent.com/44756128/114196279-d4981900-9916-11eb-88b2-28c2179613c9.png)

  - Click the Permissions tab, and expand the ec2-admin policy.

![image](https://user-images.githubusercontent.com/44756128/114196432-f5f90500-9916-11eb-839f-1ce422cf8ff8.png)

  - Click Edit policy and under Inline Policies, click Edit policy again.

![image](https://user-images.githubusercontent.com/44756128/114196487-07421180-9917-11eb-8129-22fbf6ddce53.png)

  - Review the policy but do not make any changes.

![image](https://user-images.githubusercontent.com/44756128/114196550-1628c400-9917-11eb-97d4-8e20d12cfa52.png)

# Use the IAM Sign-In Link to Sign in as a User
  - Return to the IAM Dashboard.
  - At the top of the page, copy the Sign-in URL for IAM users.

![image](https://user-images.githubusercontent.com/44756128/114196794-512af780-9917-11eb-829d-8cc439547657.png)

  - In a new tab, navigate to the URL.
  - Log in to the user-1 account.
  - Navigate to the S3 dashboard.
  - Click Create bucket.

![image](https://user-images.githubusercontent.com/44756128/114197001-88010d80-9917-11eb-9606-3cdf233f9a0d.png)

  - Enter a unique name for the bucket and click Create bucket. Do you receive an error?

![image](https://user-images.githubusercontent.com/44756128/114197161-aebf4400-9917-11eb-8bb2-54a92e77f9bc.png)

  - Navigate to the EC2 dashboard. Can you view any EC2 instances?

![image](https://user-images.githubusercontent.com/44756128/114197252-c4cd0480-9917-11eb-8cb7-b12543c03562.png)

  - Click on the username in the top-right corner of the page and logout of the user-1 account.

![image](https://user-images.githubusercontent.com/44756128/114197297-d0b8c680-9917-11eb-858d-75800bd97225.png)

  - Log in to the user-2 account.
  - Navigate to EC2 and click Instances (running). Can you view the running instance?

![image](https://user-images.githubusercontent.com/44756128/114197504-0958a000-9918-11eb-9192-c8eda8b55cf6.png)

  - Select the running instance and using the Instance State dropdown, click Stop instance. Do you receive an error?

![image](https://user-images.githubusercontent.com/44756128/114197590-1ffef700-9918-11eb-8c7f-b17465d0dd0d.png)

  - Navigate to the S3 dashboard. Do you receive any errors?

![image](https://user-images.githubusercontent.com/44756128/114197760-4755c400-9918-11eb-84e0-1f69a32883e5.png)

  - Click on the username in the top-right corner of the page and logout of the user-2 account.
  - Log in to the user-3 account.
  - Navigate to the EC2 dashboard and click Instances (running).
  - Select the running instance and using the Instance State dropdown, click Stop instance. Do you receive an error?

![image](https://user-images.githubusercontent.com/44756128/114198098-8f74e680-9918-11eb-9950-84ea68f281cb.png)

  - Once the instance has stopped successfully, open the Instance State dropdown again and click Start instance.

![image](https://user-images.githubusercontent.com/44756128/114198172-a61b3d80-9918-11eb-9cc3-c882c2f3c2f8.png)

![image](https://user-images.githubusercontent.com/44756128/114198252-b6cbb380-9918-11eb-8c05-55fadd19ef92.png)
