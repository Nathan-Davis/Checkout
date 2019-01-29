# Checkout
This is a coding Kata to demonstrate a basic Checkout API

You can build and run the tests of this project from the Developer Command Prompt for Visual Studio.
You will need to have installed [Nuget.Exe](https://www.nuget.org/downloads) and [VisualStudio](https://visualstudio.microsoft.com/downloads/).

**To build the project from the Developer Command Prompt:**

cd {path to Checkout directory}

"{full path to nuget.exe}" restore checkout.sln

msbuild checkout.sln

**To run tests from the Developer Command Prompt:**

You may need to use the full path of MSTest.exe. For Visual Studio 2017 it can normally be found here: 

C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe

"{full path to MSTest}" /testcontainer:CheckOutUnitTests\bin\Debug\CheckoutUnitTests.dll 

**You can see the HTML results of the test coverage by double clicking on TestCoverageResults.html in the solution directory.**





