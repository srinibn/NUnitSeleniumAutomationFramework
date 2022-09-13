# Selenium webdriver in C# with Page Object Model design pattern

This framework is implemented for web-based automation projects and developed using a selenium tool and page object model design pattern. It can be used as a template for selenium projects and flexibility to change the code per the project requirements.
It supports chrome, firefox, and IE browser for local environment testing. Also, it supports parallel execution with proper report generation.
* Note: It can run on the Cloud (Remote) also like the Browser stack etc. Currently, remote execution work is in-progress.

# Pre-requisites
* Windows OS
* Visual Studio

# Get Started
Git clone URL

# Installation

1. Visual Studio
2. Start Visual Studio0
3.  Click on File>>Open>> Project / SolutionNavigate to the project folder and select "NUnitAutomationFramework.sln"

Or 

Navigate to the project folder and click on "NUnitAutomationFramework.sln"

# Configuration setup required:
In folder structure, open app.config file, pass the browser, and run environment details.
* Browser: On which Browser do you want to execute selenium scripts
* Environment: In which Environment do you want to run your test case. Ex: QA, UAT or PROD
* RunEnvironment: Where you want to run your test suite, Local or Remote (Cloud)

In folder structure, Navigate to the Resource folder and open the #Environment.json file. Here you need to add a Website URL based on the Environments

# How to write a Testcase
1. Navigate to the TestSuites folder, and open Regression.cs file
* Add a TestCase Method starting with TestCase_id.Note: add test case method name starting with testcase_id.
* Navigate to the Pages folder, Open the HomePage.cs fileHere, as a sample, added a few page methods. 
* Once pages are added, come back to the regression class file, and refer to existing test method examples, of how to call the page class methods.

2. In order to pass test data to the test case. You can pass the test data in multiple ways: 
* Add test data in the Constant file and pass the test data in test case method
* Add test data in Resources>> Testdata.xml file as per the test case format How to read test data is shown in regression.cs file

# How to execute the TestCase 
1. Local
* Under the menu click on Build> Build Solutionb. Once the solution is built successfully. Under the menu click on View >> Test Explorer
Here you can see the list of test cases.Expand the project tree and right click on the test method and run it.

# Report
An extent report is used to generate the report.Once execution is completed, navigate to the Reports folder, right-click on index.html file and open it with the respective system browser.

# Contionus improvement to the framework is in progress

# Feedback
Do provide your feedback and report issues in the issue tracking section, happy to help.

# Contact
If you have any questions or need some help with the repo, please do contact me.




