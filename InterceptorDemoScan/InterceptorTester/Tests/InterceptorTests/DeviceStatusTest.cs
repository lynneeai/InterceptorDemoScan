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
	public class DeviceStatusTest
    {
		static string outputFile = "../../../logs/DeviceStatusUnitTest.txt";

		static StreamWriter results;

		static DeviceStatusJSON status;

        public static DeviceStatusJSON getStatus()
        {
            status = new DeviceStatusJSON();
            status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
            status.callHomeTimeoutData = "";
            status.callHomeTimeoutMode = "0";
            status.capture = "1";
            status.captureMode = "1";
            status.cmdChkInt = "1";
            status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
            string[] err = new string[3];
            err[0] = "asdf";
            err[1] = "wasd";
            err[2] = "qwerty";
            status.dynCodeFormat = err;
            status.errorLog = err;
            status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
            status.requestTimeoutValue = "8000";
            status.revId = "52987";
            status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
            status.seqNum = "87";
            status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";
            return status;
        }

		[TestFixtureSetUp]
		public void setup()
		{
			status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = "";
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[3];
			err[0] = "asdf";
			err[1] = "wasd";
			err[2] = "qwerty";
			status.dynCodeFormat = err;
			status.errorLog = err;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";

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

		[Test()]
		public void ValidSerial()
		{
			DeviceStatusJSON status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = "";
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[3];
            err[0] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") + "///200///pleasework";
            err[1] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") + "///200///pleasework";
            err[2] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") + "///200///pleasework";
			status.dynCodeFormat = err;
			status.errorLog = err;
            status.intSerial = TestGlobals.validSerial;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";
            status.Ssid = "43B81B4F768D0549AB4F178022DEB384";
            status.wpaPSK = "wifipassword";


			DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
			Test statusTest = new Test(operation);
			statusTest.setTestName("ValidSerial");
			statusTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + statusTest.ToString () + " " + statusTest.getTestName ());

            AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
            results.WriteLine(statusTest.getOperation().getJson().ToString());
            results.WriteLine("Server returned:");
            results.WriteLine(HTTPSCalls.result.Key.ToString());
            results.WriteLine(HTTPSCalls.result.Value.ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + statusTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + statusTest.result ());
			results.WriteLine ();

			Assert.AreEqual("201", statusCode);
		}


		[Test()]
		public void InvalidSerial()
		{
			DeviceStatusJSON status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = null;
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[3];
			err[0] = "asdf";
			err[1] = "wasd";
			err[2] = "qwerty";
			status.errorLog = err;
			status.intSerial = TestGlobals.invalidSerial;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";

			DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
			Test statusTest = new Test(operation);
			statusTest.setTestName("BadSerial");
			statusTest.setExpectedResult ("400");


			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + statusTest.ToString () + " " + statusTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (statusTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + statusTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + statusTest.result ());
			results.WriteLine ();

			Assert.AreEqual("400", statusCode);
		}

		[Test()]
		public void EmptySerial()
		{
			DeviceStatusJSON status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = "";
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[3];
			err[0] = "asdf";
			err[1] = "wasd";
			err[2] = "qwerty";
			status.errorLog = err;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";

			DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
			Test statusTest = new Test(operation);
			statusTest.setTestName("EmptySerial");
			statusTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + statusTest.ToString () + " " + statusTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (statusTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + statusTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + statusTest.result ());
			results.WriteLine ();

			Assert.AreEqual("400", statusCode);
		}

		[Test()]
		public void NullSerial()
		{
			DeviceStatusJSON status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = null;
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[3];
			err[0] = "asdf";
			err[1] = "wasd";
			err[2] = "qwerty";
			status.errorLog = err;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";

			DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
			Test statusTest = new Test(operation);
			statusTest.setTestName("NullSerial");
			statusTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + statusTest.ToString () + " " + statusTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
			results.WriteLine (statusTest.getOperation().getJson().ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + statusTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + statusTest.result ());
			results.WriteLine ();

			Assert.AreEqual("400", statusCode);
		}

		[Test()]
		public void AlertDataStore()
		{
			DeviceStatusJSON status = new DeviceStatusJSON();
			status.bkupURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceBackup";
			status.callHomeTimeoutData = null;
			status.callHomeTimeoutMode = "0";
			status.capture = "1";
			status.captureMode = "1";
			status.cmdChkInt = "1";
			status.cmdURL = "http://cozumotesttls.cloudapp.net:80/api/iCmd";
			string[] err = new string[1];
            err[0] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")+ "///900///bypassmode";
            status.Ssid = "43B81B4F768D0549AB4F178022DEB384";
            status.wpaPSK = "wifipassword";
			status.errorLog = err;
            status.dynCodeFormat = err;
			status.intSerial = TestGlobals.validSerial;
			status.reportURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceStatus";
			status.requestTimeoutValue = "8000";
			status.revId = "52987";
			status.scanURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceScan";
			status.seqNum = "87";
			status.startURL = "http://cozumotesttls.cloudapp.net:80/api/DeviceSetting";

			DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
			Test statusTest = new Test(operation);
			statusTest.setTestName("AlertDataStore");
			statusTest.setExpectedResult ("201");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + statusTest.ToString () + " " + statusTest.getTestName ());

			AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine("Json posted:");
            results.WriteLine(statusTest.getOperation().getJson().ToString());
            results.WriteLine("Server returned:");
            results.WriteLine(HTTPSCalls.result.Key.ToString());
            results.WriteLine(HTTPSCalls.result.Value.ToString());
			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + statusTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + statusTest.result ());
			results.WriteLine ();

			Assert.AreEqual("201", statusCode);

		}
		
	}
}

