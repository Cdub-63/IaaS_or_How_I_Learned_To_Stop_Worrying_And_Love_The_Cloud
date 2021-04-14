![image](https://user-images.githubusercontent.com/44756128/114725286-fec84d00-9d01-11eb-863f-31e928d5f6f8.png)

# Introduction
In this live AWS environment, we will be using the AWS Simple Notification Service (SNS). We will create an SNS topic and then subscribe to that topic using multiple endpoints (SMS, email, and AWS Lambda). This environment allows us to demonstrate successful interaction with the SNS service by creating SNS topics and adding subscribers to those topics. At the end of this activity, we will have demonstrated that we have a basic understanding of the SNS service, the components within it, and how to use the service in the AWS console.

Log into the AWS Management Console.

Use the notification_processor.py file in the directory.

Additionally, please delete your email in the SNS section before closing the activity.

# Create an SNS Topic
  - Click on the Services drop-down menu, search and select SNS.

![image](https://user-images.githubusercontent.com/44756128/114726862-5a470a80-9d03-11eb-94f5-1908856054d4.png)

  - Type mytopic for * Topic name and select Next step >> Create topic.

![image](https://user-images.githubusercontent.com/44756128/114726950-6af78080-9d03-11eb-98c6-26dc5059674a.png)

![image](https://user-images.githubusercontent.com/44756128/114727102-8bbfd600-9d03-11eb-89ef-437acde7b1fc.png)

  - Select Subscription >> Create Subscription and set:
    - Topic ARN:Select only option from drop-down menu
    - Protocol:Email
    - Endpoint:your email address

![image](https://user-images.githubusercontent.com/44756128/114727300-b6aa2a00-9d03-11eb-9881-1dd65593b1cf.png)

   Click Create subscription. This may take some time to provision. Look in your Spam email box for a confirmation email and select Confirm subscription.

![image](https://user-images.githubusercontent.com/44756128/114727390-c9bcfa00-9d03-11eb-9aa9-856066225627.png)

![image](https://user-images.githubusercontent.com/44756128/114727459-d5102580-9d03-11eb-99e9-1ec0a53ddd9d.png)

  - Create another subscription, this time for SMS. Click subscription >> Create topic and set:
    - Topic ARN:Select only option from drop-down menu
    - Protocol:SMS
    - Endpoint:your phone number

      Click Create subscription. Keep this tab open.

![image](https://user-images.githubusercontent.com/44756128/114727653-025cd380-9d04-11eb-8bf3-03f1dfd0a09c.png)

# Create a Lambda Function
  - In a new browser tab, click on the Services drop-down menu, search and select Lambda. Keep this browser tab open.

![image](https://user-images.githubusercontent.com/44756128/114727853-2f10eb00-9d04-11eb-9f4e-70403964794f.png)

  - Click Create function >> Author from scratch and set:
    - Function name:SNSProcessor
    - Runtime:Python 3.6
    - Execution role:Use existing role
    - Existing role:LambdaRoleLA

      Select Create Function.

![image](https://user-images.githubusercontent.com/44756128/114727987-4c45b980-9d04-11eb-81ce-85d646eb76ca.png)

  - Navigate back to the SNS browser tab. Click subscription >> Create subscription and set:
    - Topic ARN:Select only option from drop-down menu
    - Protocol:AWS Lambda
    - Endpoint:select only option from drop-down menu

      Click Create subscription.

![image](https://user-images.githubusercontent.com/44756128/114728089-654e6a80-9d04-11eb-9582-258081dad67d.png)

  - Navigate back to the Lambda browser tab and paste the code within notification_processor.py file into the Function code area to overwriting the existing code. 
  - After pasting in code, click Deploy.

![image](https://user-images.githubusercontent.com/44756128/114728427-b2cad780-9d04-11eb-8f45-b68c69e5546f.png)

# Send Your SNS Topic to Multiple Endpoints
  - Navigate back to the SNS browser tab. Select Topics >> select mytopic >> Publish message and set:
    - Subject:An AWS Topic
    - Message body:Hello, this is our first message
    - Select publish message.

![image](https://user-images.githubusercontent.com/44756128/114728695-ee65a180-9d04-11eb-8deb-e6927db7e3d2.png)

![image](https://user-images.githubusercontent.com/44756128/114728532-cb3af200-9d04-11eb-8dd1-7b7e584cbf49.png)

  - If successful, you'll receive a text message on your phone and in your email box.

![image](https://user-images.githubusercontent.com/44756128/114728866-194ff580-9d05-11eb-9e6f-7bdfa8ae71a0.png)

  - Navigate back to the Lambda browser tab and click on Monitoring >> View logs in CloudWatch. If successful, you will see a message an entry within the Log streams section.

![image](https://user-images.githubusercontent.com/44756128/114729064-43091c80-9d05-11eb-95f3-97b69a92b7f2.png)

![image](https://user-images.githubusercontent.com/44756128/114729143-54eabf80-9d05-11eb-93ba-3c1a49aa4b7a.png)
