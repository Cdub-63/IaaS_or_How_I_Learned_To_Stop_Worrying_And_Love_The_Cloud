![image](https://user-images.githubusercontent.com/44756128/114580208-45f00880-9c44-11eb-8602-295661844a0a.png)

# Introduction
AWS Glacier is a long-term archive storage service that provides lower-cost storage than other AWS storage options. When data has not been accessed for a certain period of time, it can be moved automatically between S3 storage classes using a lifecycle policy. We will create a basic Amazon S3 lifecycle policy.

Log in to the AWS Management Console.

Upload the pinehead and pineheadv2 files to the S3 bucket we create.

# Create an S3 Bucket and Upload an Object
  - Navigate to S3.
  - Click Create bucket.

![image](https://user-images.githubusercontent.com/44756128/114580850-d595b700-9c44-11eb-995a-0980559d5507.png)

  - In Bucket name, enter a globally unique name.
  - Deselect Block all public access.
  - Click I acknowledge that the current settings might result in the bucket and the objects within becoming public.
  - Click Create bucket.

![image](https://user-images.githubusercontent.com/44756128/114581265-2dccb900-9c45-11eb-9132-0828b61d6b5e.png)

  - Click the bucket to open it and click Upload.
  - Click Add folder.
  - Select the ccp-master folder from GitHub.
  - Click Upload > Upload.

![image](https://user-images.githubusercontent.com/44756128/114581679-88feab80-9c45-11eb-914a-0834d8ddfd74.png)

  - At the bottom of the page, click Upload.

![image](https://user-images.githubusercontent.com/44756128/114581820-a6cc1080-9c45-11eb-869d-42aad42d8736.png)

# Create a Lifecycle Policy
  - Return to the main page of the S3 bucket.
  - Select the Management tab.
  - Scroll to Lifecycle rules and click Create lifecycle rule.

![image](https://user-images.githubusercontent.com/44756128/114581951-c6633900-9c45-11eb-9dd2-ffae60b17831.png)

  - In Lifecycle rule name, enter "sample-s3-to-glacier-rule".
  - Scroll to Filter type and in Prefix enter "pinehead".

![image](https://user-images.githubusercontent.com/44756128/114582140-f0b4f680-9c45-11eb-93fa-16470dc26caa.png)

  - In Lifecycle rule actions, click Transition current versions of objects between storage classes and Transition previous versions of objects between storage classes.
  - In Transition current versions of objects between storage classes, select Glacier from the dropdown and set Days after object creation to "30".
  - Click I acknowledge that this lifecycle rule will increase the one-time lifecycle request cost if it transitions small objects.

![image](https://user-images.githubusercontent.com/44756128/114582336-1b9f4a80-9c46-11eb-99e8-86a29a72d29b.png)

  - In Transition previous versions of objects between storage classes, select Glacier Deep Archive from the dropdown and set Days after objects become noncurrent to "15".
  - Click I acknowledge that this lifecycle rule will increase the one-time lifecycle request cost if it transitions small objects.
  - Click Create rule.

![image](https://user-images.githubusercontent.com/44756128/114582441-35d92880-9c46-11eb-90c8-4695fd4ce2ae.png)

![image](https://user-images.githubusercontent.com/44756128/114582511-42f61780-9c46-11eb-9d4c-d1f33e96bcd3.png)
