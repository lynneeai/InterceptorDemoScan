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
	public class DeviceSettingsTest
    {

		static string outputFile = "../../../logs/DeviceSettingUnitTest.txt";
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
        [Test()]
		// Valid Serial
		public void ValidSerial() 
		{
			DeviceSetting dSetting1 = new DeviceSetting(TestGlobals.testServer, TestGlobals.validSerial);

			Test deviceSetting = new Test(dSetting1);
			deviceSetting.setTestName("ValidSerial");
			deviceSetting.setExpectedResult ("200");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + deviceSetting.ToString () + " " + deviceSetting.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(deviceSetting, HTTPOperation.GET));
          	string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + deviceSetting.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + deviceSetting.result ());
			results.WriteLine ();

			Assert.AreEqual("200", statusCode);
		}

		[Test()]
		// Invalid Serial
		public void InvalidSerial() 
		{
			DeviceSetting dSetting2 = new DeviceSetting(TestGlobals.testServer, TestGlobals.invalidSerial);

			Test deviceSetting = new Test(dSetting2);
			deviceSetting.setTestName("BadSerial");
			deviceSetting.setExpectedResult ("400");


			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + deviceSetting.ToString () + " " + deviceSetting.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(deviceSetting, HTTPOperation.GET));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + deviceSetting.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + deviceSetting.result ());
			results.WriteLine ();

			Assert.AreEqual("400", statusCode);
		}

		[Test()]
		// No Serial
		public void NoSerial() 
		{
			DeviceSetting dSetting3 = new DeviceSetting(TestGlobals.testServer, null);

			Test deviceSetting = new Test(dSetting3);
			deviceSetting.setTestName("NoSerial");
			deviceSetting.setExpectedResult ("400");


			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + deviceSetting.ToString () + " " + deviceSetting.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(deviceSetting, HTTPOperation.GET));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + deviceSetting.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + deviceSetting.result ());
			results.WriteLine ();

			Assert.AreEqual("400", statusCode);
		}
	}
}

