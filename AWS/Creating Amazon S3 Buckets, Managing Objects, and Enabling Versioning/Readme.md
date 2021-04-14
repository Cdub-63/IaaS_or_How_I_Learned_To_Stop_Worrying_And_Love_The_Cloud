![image](https://user-images.githubusercontent.com/44756128/114716843-13084c00-9cfa-11eb-9ab0-07a3c980b56b.png)

# Introduction
We will create two S3 buckets and verify public vs. non-public access to the buckets. We will also enable and validate versioning based on uploaded objects.

Log into the AWS Management Console.

Download the ccp-master.zip file that we'll upload later.

# Create an Amazon S3 Bucket with Public Access Enabled
## Create Public Bucket
  - Navigate to S3.
  - Click + Create bucket.
  - For Bucket name, type "lapublic". (Since bucket names must be globally unique, add a series of random numbers at the end.)

![image](https://user-images.githubusercontent.com/44756128/114717939-2c5dc800-9cfb-11eb-8b32-61a118a662d4.png)

  - Leave the versioning settings as-is.
  - On the configuration options screen, ensure that Block all public access is unchecked.

![image](https://user-images.githubusercontent.com/44756128/114718091-4f887780-9cfb-11eb-9415-7e5d7e9ff945.png)

  - Click Create bucket.

![image](https://user-images.githubusercontent.com/44756128/114718237-6cbd4600-9cfb-11eb-829e-23422b8433a5.png)

## Create Private Bucket
  - Click + Create bucket.
  - For Bucket name, type "laprivate". (Since bucket names must be globally unique, add a series of random numbers at the end.)

![image](https://user-images.githubusercontent.com/44756128/114718332-865e8d80-9cfb-11eb-809b-34d8aef4c3f4.png)

  - Leave the versioning settings as-is.
  - On the configuration options screen, make sure Block all public access is checked.

![image](https://user-images.githubusercontent.com/44756128/114718638-cd4c8300-9cfb-11eb-86a4-b40d537af649.png)

  - Click Create bucket.

## Create a Folder and Upload File in Public Bucket
  - Click the public bucket to open it.
  - Click + Create folder
  - Give the folder a name of "images", and click Create.

![image](https://user-images.githubusercontent.com/44756128/114718741-ec4b1500-9cfb-11eb-98fb-fc7e763f62f9.png)

  - Click the folder name, and then click Upload.
  - Click Add files to upload the pinehead.jpg file you downloaded at the start.

![image](https://user-images.githubusercontent.com/44756128/114719015-359b6480-9cfc-11eb-8e89-204a9ea53a51.png)

  - Set Manage public permissions to Grant public read access to this object(s).
  - 
![image](https://user-images.githubusercontent.com/44756128/114719084-4ea41580-9cfc-11eb-80eb-e88d4c672c25.png)

  - Click Upload.

## Upload File to Private Bucket
  - Head back to the S3 dashboard.
  - Click the private bucket to open it.
  - Click Upload.
  - Click Add files to upload the same pinehead.jpg file.
  - Click Upload.

![image](https://user-images.githubusercontent.com/44756128/114719286-890db280-9cfc-11eb-86e6-f46252799407.png)

  - Once it's uploaded, click the listed Object URL, which should result in an error.

![image](https://user-images.githubusercontent.com/44756128/114719359-9c208280-9cfc-11eb-9155-447ce2cff826.png)

# Enable Versioning on the Public Bucket and Validate Access to Different Versions of Files with the Same Name
  - Note: Before taking the following steps, rename the pineheadv2.jpg file to pinehead.jpg to achieve the same versioning results.
  - Head back to the S3 dashboard.
  - Click the public bucket to open it.
  - Click the Properties tab.
  - Click Edit in the Bucket Versioning box.

![image](https://user-images.githubusercontent.com/44756128/114719614-e1dd4b00-9cfc-11eb-809f-490b64c4efd6.png)

  - Check the circle to Enable versioning, and click Save.

![image](https://user-images.githubusercontent.com/44756128/114719656-ebff4980-9cfc-11eb-8e5a-32c9e59d59bc.png)

  - Back in the Objects tab, click to open the images folder.
  - Click Upload, Add files, and upload the pinehead.jpg file.
  - Click Upload.
  - Once it's uploaded, click Make public.

![image](https://user-images.githubusercontent.com/44756128/114719999-3bde1080-9cfd-11eb-9b5e-438625708f62.png)

![image](https://user-images.githubusercontent.com/44756128/114720027-44364b80-9cfd-11eb-90e3-1e70066a3495.png)

  - Click the Latest version link at the top to see there are two versions listed: the previous one and the one we just uploaded.

![image](https://user-images.githubusercontent.com/44756128/114720171-662fce00-9cfd-11eb-8d20-cce8ff6d6d49.png)
