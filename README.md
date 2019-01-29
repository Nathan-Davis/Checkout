# Checkout
This is a coding Kata to demonstrate a basic Checkout API

You can build and run the tests of this project from the Command Line.
You will need to have installed [Nuget.Exe](https://www.nuget.org/downloads) and [VisualStudio](https://visualstudio.microsoft.com/downloads/).

**To build the project from the Command Line:**

cd {path to Checkout directory}

"{full path to nuget.exe}" restore checkout.sln

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild checkout.sln

**To run tests from the Command Line:**

"{full path to MSTest}" /testcontainer:CheckOutUnitTests\bin\Debug\CheckoutUnitTests.dll 

The full path for MSTest.exe can normally be found here: 

**2017** - C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\MSTest.exe (replace Enterprise with your version of Visual Studio)

**2015** - C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe

**You can see the HTML results of the test coverage by double clicking on TestCoverageResults.html in the solution directory.**





