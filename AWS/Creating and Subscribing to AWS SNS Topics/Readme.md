![image](https://user-images.githubusercontent.com/44756128/114725286-fec84d00-9d01-11eb-863f-31e928d5f6f8.png)

# Introduction
In this live AWS environment, we will be using the AWS Simple Notification Service (SNS). We will create an SNS topic and then subscribe to that topic using multiple endpoints (SMS, email, and AWS Lambda). This environment allows us to demonstrate successful interaction with the SNS service by creating SNS topics and adding subscribers to those topics. At the end of this activity, we will have demonstrated that we have a basic understanding of the SNS service, the components within it, and how to use the service in the AWS console.

Log into the AWS Management Console.

Use the notification_processor.py file in the directory.

NOTE: When creating the lambda subscription, you will have to close the permission errors that appear. Afterward, you will be able to continue and finish the lab.

Additionally, please delete your email in the SNS section before closing the lab.

Create an SNS Topic
Click on the Services drop-down menu, search and select SNS.
Type mytopic for * Topic name and select Next step >> Create topic.
Select Subscription >> Create Subscription and set:

Topic ARN:Select only option from drop-down menu
Protocol:Email
Endpoint:your email address

Click Create subscription. This may take some time to provision. Look in your Spam email box for a confirmation email and select Confirm subscription.

Create another subscription, this time for SMS. Click subscription >> Create topic and set:

Topic ARN:Select only option from drop-down menu
Protocol:SMS
Endpoint:your phone number

Click Create subscription. Keep this tab open.

Create a Lambda Function
In a new browser tab, click on the Services drop-down menu, search and select Lambda. Keep this browser tab open.
Click Create function >> Author from scratch and set:

Function name:SNSProcessor
Runtime:Python 3.6
Execution role:Use existing role
Existing role:LambdaRoleLA

Select Create Function.

Navigate back to the SNS browser tab. Click subscription >> Create subscription and set:

Topic ARN:Select only option from drop-down menu
Protocol:AWS Lambda
Endpoint:select only option from drop-down menu

Click Create subscription.

Navigate back to the Lambda browser tab and paste the code within GitHub's notification_processor.py file into the Function code area to overwriting the existing code. (Click on the Python file and click the Raw button to copy its contents.)

After pasting in code, click Deploy.

Send Your SNS Topic to Multiple Endpoints
Navigate back to the SNS browser tab. Select Topics >> select mytopic >> Publish message and set:

Subject:An AWS Topic
Message body:Hello, this is our first message
Select publish message.

If successful, you'll receive a text message on your phone and in your email box.
Navigate back to the Lambda browser tab and click on Monitoring >> View logs in CloudWatch. If successful, you will see a message an entry within the Log streams section.
