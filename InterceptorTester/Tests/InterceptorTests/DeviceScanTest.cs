using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Configuration;
using Nito.AsyncEx;
using System.IO.Compression;
using ConsoleApplication1;

namespace InterceptorTester.Tests.InterceptorTests
{
	
	[TestFixture()]
    public class DeviceScanTest
    {
		static string outputFile = "../../../logs/DeviceScanUnitTest.txt";

		static StreamWriter results;

        [TestFixtureSetUp()]
        public void setup()
        {
            TestGlobals.setup();
            FileStream stream;
			stream = File.OpenWrite(outputFile);
            results = new StreamWriter(stream);
        }

        [TestFixtureTearDown()]
        public void tearDown()
        {
            results.Close();
        }

		// Simple Scan Data

        [Test()]
        // Valid Single Scan
        public void ValidSingleScanSimple()
        {

            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("ValidSingleScanSimple");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());


            AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();


            Assert.AreEqual("201", statusCode);
        }

        [Test()]
        public void UTF8ScanCode()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "¿ÀÁÂÆÐ123òü";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("UTF8ScanCode");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

            AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        [Test()]
        public void ASCIIScanCode()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "!\"#&)(";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("ASCIIScanCode");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

            AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();


			Assert.AreEqual("201", statusCode);
        }

        [Test()]
        // Bad Serial
        public void InvalidSerialSimple()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = "BAD SERIAL";
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("InvalidSerialSimple");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }

        [Test()]
        // No Serial(Empty String)
        public void EmptySerialSimple()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = "";
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("EmptySerialSimple");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }



        [Test()]
        // No Serial(Null)
        public void NullSerialSimple()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = null;
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("NullSerialSimple");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }

        [Test()]
        // List of Valid Scans
        public void LOValidScansSimple()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = null;
            string[] scanData = new string[4];
            scanData [0] = "0";
            scanData [1] = "1";
            scanData [2] = "2";
            scanData [3] = "3";
            testJson.b = scanData;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("LOValidScansSimple");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        //Dynamic

        [Test()]
        // Valid Single Scan
        public void ValidSingleScanDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "~20/90210|";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("ValidSingleScanDyn");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        [Test()]
        // Valid Call Home Scan
        public void ValidCH()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "~21/*CH*055577520928";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("ValidCH");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        //TODO: Identify invalid possible scans
        /*
        [Test()]
        // Invalid Scan Data
        public void InvalidSingleScanDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = validSerial;
            testJson.d = "~20|noendingbar";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("InvalidSingleScanDyn");


            List<Test> tests = new List<Test> ();
            tests.Add (scanTest);

            AsyncContext.Run(async() => await Program.buildTests(tests));

            foreach (Test nextTest in Program.getTests()) {
                Assert.AreEqual (nextTest.getExpectedResult (), nextTest.getActualResult ());
            }
        }
        */

        [Test()]
        // Bad Serial
        public void InvalidSerialDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.invalidSerial;
            testJson.d = "~20/90210|";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("InvalidSerialDyn");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }


        [Test()]
        // No Serial (Empty Sting)
        public void EmptySerialDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = "";
            testJson.d = "~20/90210|";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("EmptySerialDyn");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }

        [Test()]
        // No Serial (Null)
        public void NullSerialDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = null;
            testJson.d = "~20/90210|";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("NullSerialDyn");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }

        [Test()]
        // List of Valid Scans
        public void LOValidScansDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = null;
            string[] scanData = new string[4];
            scanData[0] = "~20/0|";
            scanData[1] = "~20/1|";
            scanData[2] = "~20/2|";
            scanData[3] = "~20/3|";
            testJson.b = scanData;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("LOValidScansDyn");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        //TODO: Identify invalid scan
        
		/*
        [Test()]
        // Mixed of Valid/Invalid Scans
        public void ValInvalScansDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = validSerial;
            testJson.d = null;
            string[] scanData = new string[4];
            scanData [0] = "~20/0|";
            scanData [1] = "~20/noendingbar";
            scanData [2] = "~20/2|";
            scanData [3] = "~20/3|";
            testJson.b = scanData;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("ValInvalScansDyn");


            results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

            AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();
			
			Assert.AreEqual("201", statusCode);
			
        }

        */

        // Combined

        [Test()]
        // List of Valid Simple and Dynamic Code Scans 
        public void ValidScansSimDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = null;
            string[] scanData = new string[4];
            scanData [0] = "~20/0|";
            scanData [1] = "123456789";
            scanData [2] = "~20/2|";
            scanData [3] = "987654321";
            testJson.b = scanData;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("ValidScansSimDyn");
			scanTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("201", statusCode);
        }

        //TODO: Need dynamic invalid scan
        /*
        [Test()]
        // Mixed of Valid and Invalid Scans
        public void ValInvalScansSimDyn()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = validSerial;
            testJson.d = null;
            string[] scanData = new string[4];
            scanData [0] = "~20/0|";
            scanData [1] = "123456789";
            scanData [2] = "~20/noendingbar";
            scanData [3] = "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm";;
            testJson.b = scanData;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("ValInvalScansSimDyn");


            List<Test> tests = new List<Test> ();
            tests.Add (scanTest);

            AsyncContext.Run(async() => await Program.buildTests(tests));

            foreach (Test nextTest in Program.getTests()) {
                Assert.AreEqual (nextTest.getExpectedResult (), nextTest.getActualResult ());
            }
        }
        */

        [Test()]
        // No scan data
        public void NoScanData()
        {
            DeviceScanJSON testJson = new DeviceScanJSON ();
            testJson.i = TestGlobals.validSerial;
            testJson.d = null;
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan (TestGlobals.testServer, testJson);

            Test scanTest = new Test (testDScan);
            scanTest.setTestName("NoScanData");
			scanTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + scanTest.ToString () + " " + scanTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (scanTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + scanTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + scanTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
        }
    }
}
