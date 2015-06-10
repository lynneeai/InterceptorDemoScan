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
	public class ICmdTest
	{
        
		static string outputFile = "../../../logs/ICmdUnitTest.txt";
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
		public void ValidSerial()
		{
			ICmd validICmd = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
			Test icmdTest = new Test(validICmd);
			icmdTest.setTestName("ValidSerial");
			icmdTest.setExpectedResult ("200");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + icmdTest.ToString () + " " + icmdTest.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(icmdTest, HTTPOperation.GET));
            string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + icmdTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + icmdTest.result ());
			results.WriteLine ();

            Assert.AreEqual("200", statusCode);
		}

		[Test()]
		public void InvalidSerial()
		{
			ICmd invalidICmd = new ICmd(TestGlobals.testServer, TestGlobals.invalidSerial);
			Test icmdTest = new Test(invalidICmd);
			icmdTest.setTestName("BadSerial");
			icmdTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + icmdTest.ToString () + " " + icmdTest.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(icmdTest, HTTPOperation.GET));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + icmdTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + icmdTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);

		}

		[Test()]
		public void MissingSerial()
		{
			ICmd missingICmd = new ICmd(TestGlobals.testServer, null);
			Test icmdTest = new Test(missingICmd);
			icmdTest.setTestName("EmptySerial");
			icmdTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + icmdTest.ToString () + " " + icmdTest.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(icmdTest, HTTPOperation.GET));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + icmdTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + icmdTest.result ());
			results.WriteLine ();

            Assert.AreEqual("400", statusCode);
		}

		[Test()]
		public void NoQuery()
		{
			ICmd missingICmd = new ICmd(TestGlobals.testServer, null);
			missingICmd.noQuery = true;
			Test icmdTest = new Test(missingICmd);
			icmdTest.setTestName("NoQuery");
			icmdTest.setExpectedResult ("400");

			results.WriteLine (DateTime.Now);
			results.WriteLine ("current test: " + icmdTest.ToString () + " " + icmdTest.getTestName ());


			AsyncContext.Run(async () => await new HTTPSCalls().runTest(icmdTest, HTTPOperation.GET));
			string statusCode = HTTPSCalls.result.Key.Property("StatusCode").Value.ToString();

			results.WriteLine ("Server: " + TestGlobals.testServer);
			results.WriteLine ("Expected result: " + icmdTest.getActualResult());
			results.WriteLine ("Actual result: " + statusCode);
			results.WriteLine ("Test result: " + icmdTest.result ());
			results.WriteLine ();

            Assert.AreEqual("404", statusCode);
		}
	}
}

