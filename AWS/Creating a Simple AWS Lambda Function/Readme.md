# Introduction
We will create and customize Python3-based Lambda functions from within the console.

Log in to the live AWS environment.

All of the code used in the lesson is available for download here.

Create a Lambda Function within the AWS Lambda Console
Navigate to Lambda.
Click Create a function.
Make sure the Author from scratch option at the top is selected, and then use the following settings:
Basic information:
Name: HelloWorld
Runtime: Python3.6
Permissions:
Select Choose or create an execution role.
Execution role: Use an existing role
Existing role: lambdarole
Click Create function.
On the HelloWorld page, scroll to the Function code section.
Delete the existing code there, and enter the code from GitHub.
Click Deploy.
Create a Test Event and Manually Invoke the Function Using the Test Event
In the dropdown next to Test at the top of the Lambda console, select Configure test events.
In the dialog, select Create new test event.
Select the Hello World event template.
Give it an event name (e.g., "Test").
Replace the current code there with the provided JSON code, and then click Create.
Click Test to verify the function's success.
Verify That CloudWatch Has Captured Function Logs
Navigate to CloudWatch.
Select Logs in the left-hand menu.
Select the log group with your function name in it.
Select the log stream within the log group.
Verify the output is present and correct.
