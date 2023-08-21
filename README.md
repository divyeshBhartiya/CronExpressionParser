# CronExpressionParser
This is a command line command line application or script which parses a CRON string and expands each field to show the times at which it will run.

Code is written in C# on .NET7 platform.
A **CronExpressionParserCLI.EXE** file is attached and it can run easily via command prompt or powershell on windows env.
For environments other than windows, you can use tools like **WINE**. 
To run the EXE, go to any command prompt and navigate to the folder/directory containing the EXE and run the following command:

_**CronExpressionParserCLI.exe */15 0 1,15 * 1-5 /usr/bin/find**_

I'm also attaching the unit test cases so it makes easier to understand the functionality and capabilities of the **CronExpressionParser**.
