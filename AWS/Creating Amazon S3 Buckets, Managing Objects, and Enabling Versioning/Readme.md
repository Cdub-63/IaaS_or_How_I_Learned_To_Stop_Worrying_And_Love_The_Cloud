![image](https://user-images.githubusercontent.com/44756128/114716843-13084c00-9cfa-11eb-9ab0-07a3c980b56b.png)

# Introduction
We will create two S3 buckets and verify public vs. non-public access to the buckets. We will also enable and validate versioning based on uploaded objects.

Log into the AWS Management Console.

Download the files we'll upload later from GitHub.

# Create an Amazon S3 Bucket with Public Access Enabled
## Create Public Bucket
  - Navigate to S3.
  - Click + Create bucket.
  - For Bucket name, type "lapublic". (Since bucket names must be globally unique, add a series of random numbers at the end.) Click Next.
  - Leave the versioning settings as-is, and click Next.
  - On the configuration options screen, ensure that Block all public access is unchecked. Click Next.
  - On the permissions screen, ensure none of the checkmarks are selected — this includes individual ones and any group check mark.
  - Click Create bucket.

## Create Private Bucket
  - Click + Create bucket.
  - For Bucket name, type "laprivate". (Since bucket names must be globally unique, add a series of random numbers at the end.) Click Next.
  - Leave the versioning settings as-is, and click Next.
  - On the configuration options screen, make sure Block all public access is checked. Click Next.
  - On the permissions screen, verify the private bucket creation fields that we just set up.
  - Click Create bucket.

## Create a Folder and Upload File in Public Bucket
  - Click the public bucket to open it.
  - Click + Create folder
  - Give the folder a name of "images", and click Save.
  - Click the folder name, and then click Upload.
  - Click Add files to upload the pinehead.jpg file you downloaded at the start of the lab. Click Next.
  - Set Manage public permissions to Grant public read access to this object(s), and click Next.
  - Click Next and Upload.

## Upload File to Private Bucket
  - Head back to the S3 dashboard.
  - Click the private bucket to open it.
  - Click Upload.
  - Click Add files to upload the same pinehead.jpg file. Click Next.
  - Click Next, Next, and Upload.
  - Once it's uploaded, click the listed Object URL, which should result in an error.

# Enable Versioning on the Public Bucket and Validate Access to Different Versions of Files with the Same Name
  - Note: Before taking the following steps, rename the pineheadv2.jpg file to pinehead.jpg to achieve the same versioning results as the lab video.
  - Head back to the S3 dashboard.
  - Click the public bucket to open it.
  - Click the Properties tab.
  - Click into the Versioning box.
  - Check the circle to Enable versioning, and click Save.
  - Back in the Overview tab, click to open the images folder.
  - Click Upload, Add files, and upload the pinehead.jpg file.
  - Click Next, Next, and Upload.
  - Once it's uploaded, click Make public.
  - Click the Latest version link at the top to see there are two versions listed: the previous one and the one we just uploaded.
