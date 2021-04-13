![image](https://user-images.githubusercontent.com/44756128/114587881-b189a400-9c4b-11eb-9f1d-77dfa23ef1d8.png)

# Introduction
In this live AWS environment, we will create and configure a simple static website. Then, we will configure that static website with a custom domain, using Route 53 Alias record sets. This demonstrates how to create cost-efficient website hosting for sites that consist of files like HTML, CSS, JavaScript, fonts, and images.

The code we use for the static site is here.

Log in to the live AWS environment.

# Create an S3 Bucket and a Static Website
## Create an S3 Bucket
  - Use the Services menu to navigate to Route 53.
  - On the Route 53 dashboard, select the Hosted zone link. There is one domain name assigned to us, which we'll use to set up our static website.

![image](https://user-images.githubusercontent.com/44756128/114588087-e72e8d00-9c4b-11eb-9777-ec2404b7aae2.png)

  - Download the provided files for our upcoming S3 bucket upload.
  - Navigate back to the Hosted zones tab and select the domain name link, then copy the domain name.
  - Use the Services menu to navigate to S3, then click Create bucket.

![image](https://user-images.githubusercontent.com/44756128/114588528-658b2f00-9c4c-11eb-968e-fb8e33f9a9cc.png)

  - Create a new S3 bucket:
    - In the Bucket name field, paste your copied domain name. Your bucket name must match your domain name exactly when setting up a static website using S3 and Route 53.
    - Ensure the Region is set to US East (N. Virginia) us-east-1.
    - In Bucket settings for Block Public Access, uncheck the Block all public access checkbox.

![image](https://user-images.githubusercontent.com/44756128/114588479-57d5a980-9c4c-11eb-8871-85ff6bc35744.png)

   - In the warning box, check I acknowledge that the current settings might result in this bucket and the objects within becoming public.
   - Leave all other default settings and click Create bucket. The bucket is created and the bucket's access shows objects can be public.

![image](https://user-images.githubusercontent.com/44756128/114588713-99665480-9c4c-11eb-9278-0086bcffcb11.png)

## Create a Static Website
  - On the Buckets page, select the domain name link. In the Objects section, click Upload.

![image](https://user-images.githubusercontent.com/44756128/114589144-01b53600-9c4d-11eb-9f5f-fbf1ae18d463.png)

  - On the Upload page, click Add files and navigate to the penguinsite folder in your extracted resources. This is where the HTML files are stored.
  - Select all the HTML files and click Open to add them to the domain files in AWS.

![image](https://user-images.githubusercontent.com/44756128/114589313-2b6e5d00-9c4d-11eb-9ed8-351a55b5f248.png)

  - Leave all other default settings and click Upload.

![image](https://user-images.githubusercontent.com/44756128/114589362-36c18880-9c4d-11eb-9b7e-052f1aa3e9fe.png)

  - After the confirmation message displays, select the Destination link to open the main page of the S3 bucket.

![image](https://user-images.githubusercontent.com/44756128/114589447-48a32b80-9c4d-11eb-96b0-54705e2e2897.png)

  - Select the Properties tab, then scroll down to Static website hosting and click Edit.
  
![image](https://user-images.githubusercontent.com/44756128/114589556-66709080-9c4d-11eb-8a49-c12b4de237f6.png)

  - Set the hosting properties:
    - Select Enable to enable static website hosting, then ensure the Hosting type is set to Host to a static website.
    - Below Index document, enter index.html, and below Error document, enter error.html.
    - Click Save changes.

![image](https://user-images.githubusercontent.com/44756128/114589718-8acc6d00-9c4d-11eb-967f-64aeb9e4df01.png)

  - On the Properties tab, scroll back down to Static website hosting and select the Bucket website endpoint link. We should receive an access denied error, which indicates either that there is an error within our S3 bucket or our HTML files are not public.

![image](https://user-images.githubusercontent.com/44756128/114589835-ab94c280-9c4d-11eb-885b-77b0c3d4a324.png)

![image](https://user-images.githubusercontent.com/44756128/114589876-b8191b00-9c4d-11eb-9cf7-4a683f79b09d.png)

  - Go back to the Objects tab and select all the HTML files.
  - Use the Actions dropdown to select Make public, then click Make public again to confirm.

![image](https://user-images.githubusercontent.com/44756128/114589977-d0893580-9c4d-11eb-898e-1bbe06846c37.png)

![image](https://user-images.githubusercontent.com/44756128/114590028-dda62480-9c4d-11eb-8bfc-55d9182792c3.png)

  - After the confirmation message displays, go back to the S3 bucket and check the Properties tab again. When you select the Bucket website endpoint, your static website should open.

![image](https://user-images.githubusercontent.com/44756128/114590135-fc0c2000-9c4d-11eb-9f22-3416546d9bbd.png)

## Configure a DNS Record for the S3 Bucket
  - Navigate to Route 53 so we can create an A name, or alias record.
  - Select the Hosted zone link, then select the domain name.
  - Click Create record and select Switch to wizard.

![image](https://user-images.githubusercontent.com/44756128/114590249-21992980-9c4e-11eb-9fc6-4959539040a2.png)

![image](https://user-images.githubusercontent.com/44756128/114590292-2fe74580-9c4e-11eb-93b3-7179c90d894e.png)

  - On the Choose routing policy page, select Simple routing and click Next.

![image](https://user-images.githubusercontent.com/44756128/114590329-3d9ccb00-9c4e-11eb-9ddf-b2d4f2f7ee81.png)

  - On the Configure records page, click Define simple record.
  - In the Value/Route traffic to section, configure the record settings:
    - Use the Choose endpoint dropdown to select Alias to S3 website endpoint.
    - Use the Choose Region dropdown to select US East (N. Virginia) [us-east-1].
    - Click into the Choose S3 bucket field and select the bucket you created. The bucket name should display automatically.
    - Leave all other default settings and click Define simple record.

![image](https://user-images.githubusercontent.com/44756128/114590470-6f159680-9c4e-11eb-8a6a-295202799c01.png)

  - After the record is defined, click Create records. The type A record is created and can route files from the bucket to the website.

![image](https://user-images.githubusercontent.com/44756128/114590500-78066800-9c4e-11eb-84f8-005d6bd15f23.png)

  - In the Records section, copy the bucket name and paste it into a browser to test the alias record. The alias should display the website (this may take a few moments).

![image](https://user-images.githubusercontent.com/44756128/114590574-8eacbf00-9c4e-11eb-94a2-ce6acb692cf7.png)

# Configure a Custom Domain in Route 53
## Create a Redirect S3 Bucket
  - Navigate to S3 and copy your existing static bucket name, then click Create bucket.
  - Create a redirect S3 bucket:
    - In the Bucket name field, type "www." and paste your copied bucket name.
    - Ensure the Region is set to US East (N. Virginia) us-east-1.
    - In Bucket settings for Block Public Access, uncheck the Block all public access checkbox.

![image](https://user-images.githubusercontent.com/44756128/114590741-c3207b00-9c4e-11eb-853b-3d3dba61dc2f.png)

   - In the warning box, check I acknowledge that the current settings might result in this bucket and the objects within becoming public.
   - Leave all other default settings and click Create bucket. The bucket is created and the bucket's access shows objects can be public.

![image](https://user-images.githubusercontent.com/44756128/114590818-d7647800-9c4e-11eb-847b-a3dc0174ac22.png)

  - Select the domain name link for the new redirect bucket, then copy the redirect domain name.
  - Select the Properties tab, then scroll down to Static website hosting and click Edit.
  - Set the redirect hosting properties:
    - Select Enable to enable static website hosting, then ensure the Hosting type is set to Redirect requests for an object.
    - In the Host name field, paste the static domain name.
    - Click Save changes.

![image](https://user-images.githubusercontent.com/44756128/114591852-ed266d00-9c4f-11eb-8eb5-92902bd3466a.png)

## Configure a DNS Record for the Redirect S3 Bucket
  - Navigate to Route 53 so we can create an A name, or alias record, for the redirect bucket.
  - Select the Hosted zone link, then select the domain name.
  - Click Create record and select Switch to wizard.
  - On the Choose routing policy page, select Simple routing and click Next.

![image](https://user-images.githubusercontent.com/44756128/114591215-46da6780-9c4f-11eb-93e1-38b1d59fc75e.png)

  - On the Configure records page, click Define simple record.
  - In the Value/Route traffic to section, configure the record settings:
    - In the Record name field, enter www.
    - Set the same endpoint and region as the static bucket.
    - Click into the Choose S3 bucket field and select the redirect bucket you created. The bucket name should display automatically.
    - Leave all other default settings and click Define simple record.

![image](https://user-images.githubusercontent.com/44756128/114591376-6d000780-9c4f-11eb-9485-2a695d1a7a49.png)

  - After the record is defined, click Create records.

![image](https://user-images.githubusercontent.com/44756128/114591401-74bfac00-9c4f-11eb-887f-bd75d432a842.png)

  - In the Records section, copy the redirect bucket name and paste it into a browser to test the alias record. The alias should display the website (this may take a few moments).

![image](https://user-images.githubusercontent.com/44756128/114591512-9325a780-9c4f-11eb-9856-05ba17f069c4.png)

![image](https://user-images.githubusercontent.com/44756128/114591924-029b9700-9c50-11eb-9c0a-88c7592cdd7e.png)

# Test the Static Website with dig and nslookup
  - To verify the website is properly routed, run the command:
```sh
 dig <DOMAIN NAME> any
```

  - For troubleshooting, run the command:
```sh
nslookup <DOMAIN NAME>
```

![image](https://user-images.githubusercontent.com/44756128/114592531-a9803300-9c50-11eb-97f7-9e35bc294ec3.png)

You know the website is properly routed if the route traffic for the name server (NS) record in Route 53 matches the Answer Section data in the terminal.
