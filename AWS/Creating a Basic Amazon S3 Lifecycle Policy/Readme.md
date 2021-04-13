![image](https://user-images.githubusercontent.com/44756128/114580208-45f00880-9c44-11eb-8602-295661844a0a.png)

# Introduction
AWS Glacier is a long-term archive storage service that provides lower-cost storage than other AWS storage options. When data has not been accessed for a certain period of time, it can be moved automatically between S3 storage classes using a lifecycle policy. We will create a basic Amazon S3 lifecycle policy.

Log in to the AWS Management Console.

Download the ccp-master folder to upload to the S3 bucket we create.

# Create an S3 Bucket and Upload an Object
  - Navigate to S3.
  - Click Create bucket.
  - In Bucket name, enter a globally unique name.
  - Deselect Block all public access.
  - Click I acknowledge that the current settings might result in the bucket and the objects within becoming public.
  - Click Create bucket.
  - Click the bucket to open it and click Upload.
  - Click Add folder.
  - Select the ccp-master folder downloaded from GitHub.
  - Click Upload > Upload.
  - At the bottom of the page, click Upload.

# Create a Lifecycle Policy
  - Return to the main page of the S3 bucket.
  - Select the Management tab.
  - Scroll to Lifecycle rules and click Create lifecycle rule.
  - In Lifecycle rule name, enter "sample-s3-to-glacier-rule".
  - Scroll to Filter type and in Prefix enter "pinehead".
  - In Lifecycle rule actions, click Transition current versions of objects between storage classes and Transition previous versions of objects between storage classes.
  - In Transition current versions of objects between storage classes, select Glacier from the dropdown and set Days after object creation to "30".
  - Click I acknowledge that this lifecycle rule will increase the one-time lifecycle request cost if it transitions small objects.
  - In Transition previous versions of objects between storage classes, select Glacier Deep Archive from the dropdown and set Days after objects become noncurrent to "15".
  - Click I acknowledge that this lifecycle rule will increase the one-time lifecycle request cost if it transitions small objects.
  - Click Create rule.
