![image](https://user-images.githubusercontent.com/44756128/114406201-f7236f80-9b6c-11eb-9525-2de73cd6bd6f.png)

# Introduction
We will create and customize Python3-based Lambda functions from within the console.

Log in to the live AWS environment.

All of the code used in the lesson is in the lambda_function.py and test_event.json files.

# Create a Lambda Function within the AWS Lambda Console
  - Navigate to Lambda.

![image](https://user-images.githubusercontent.com/44756128/114406651-53868f00-9b6d-11eb-87d9-f00b87927d82.png)

  - Click Create a function.

![image](https://user-images.githubusercontent.com/44756128/114406727-6305d800-9b6d-11eb-8805-2221ce259eb9.png)

  - Make sure the Author from scratch option at the top is selected, and then use the following settings:
    - Basic information:
      - Name: HelloWorld
      - Runtime: Python3.6
    - Permissions:
      - Select Choose or create an execution role.
      - Execution role: Use an existing role
      - Existing role: lambdarole
  - Click Create function.

![image](https://user-images.githubusercontent.com/44756128/114407034-9f393880-9b6d-11eb-83bd-ba463e35c8c4.png)

  - On the HelloWorld page, scroll to the Function code section.
  - Delete the existing code there, and enter the lambda_function.py code.

![image](https://user-images.githubusercontent.com/44756128/114407231-cd1e7d00-9b6d-11eb-8f8a-ea9269e3342b.png)

![image](https://user-images.githubusercontent.com/44756128/114407345-e8898800-9b6d-11eb-8620-0c6072cf94a4.png)

  - Click Deploy.

# Create a Test Event and Manually Invoke the Function Using the Test Event
  - In the dropdown next to Test at the top of the Lambda console, select Configure test events.
  - In the dialog, select Create new test event.
  - Select the Hello World event template.
  - Give it an event name (e.g., "Test").

![image](https://user-images.githubusercontent.com/44756128/114407641-3a321280-9b6e-11eb-9dc2-4335b02321fc.png)

  - Replace the current code there with the provided test_event.json code, and then click Create.

![image](https://user-images.githubusercontent.com/44756128/114407707-4cac4c00-9b6e-11eb-9dd2-5e209a311578.png)

  - Click Test to verify the function's success.

![image](https://user-images.githubusercontent.com/44756128/114407778-5fbf1c00-9b6e-11eb-818a-42622bf5bd4f.png)

# Verify That CloudWatch Has Captured Function Logs
  - Navigate to CloudWatch.

![image](https://user-images.githubusercontent.com/44756128/114407975-95fc9b80-9b6e-11eb-801c-642fe46deb89.png)

  - Select Logs in the left-hand menu.
  - Select the log group with your function name in it.
  - Select the log stream within the log group.
  - Verify the output is present and correct.

![image](https://user-images.githubusercontent.com/44756128/114408124-b9bfe180-9b6e-11eb-8a18-0c7a1a37d7d3.png)
