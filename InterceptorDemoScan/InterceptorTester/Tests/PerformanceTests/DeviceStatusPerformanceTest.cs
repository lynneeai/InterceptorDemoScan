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

namespace InterceptorTester.Tests.PerformanceTests
{
	[TestFixture()]
	public class DeviceStatusPerformanceTest
    {
        static StreamWriter results;
        static string outputFileHTTPSAsync = "../../../logs/AsyncHTTPSDeviceStatusPerformanceTest.csv";
        static string outputFileHTTPAsync = "../../../logs/AsyncHTTPDeviceStatusPerformanceTest.csv";
		static string outputFileHTTPSSync = "../../../logs/SyncHTTPSDeviceStatusPerformanceTest.csv";
		static string outputFileHTTPSync = "../../../logs/SyncHTTPDeviceStatusPerformanceTest.csv";
		static string outputFileMultiClientStatus = "../../../logs/MultiClientDeviceStatus.csv";

        DeviceStatusJSON status;

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
        }

		[Test()]
		public void SyncHTTPSDeviceStatus()
		{
			FileStream stream;
			stream = File.Create(outputFileHTTPSSync);
			results = new StreamWriter(stream);

			for (int i = 0; i < TestGlobals.maxReps; i++)
			{
				System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

				DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);

				Test statusTest = new Test(operation);
				statusTest.setTestName("ValidSerial");
				statusTest.setExpectedResult ("201");
				statusTest.setType ("performance");

				timer.Start();
				AsyncContext.Run(async () => await new HTTPSCalls().runTest(statusTest, HTTPOperation.POST));
				timer.Stop();
				double time = timer.Elapsed.TotalMilliseconds;
				results.WriteLine("Test Time," + time);
				System.Threading.Thread.Sleep(TestGlobals.delay);
				//Verify Server didn't throw up
			}
			results.Close();
		}


        [Test()]
        public void AsyncHTTPSDeviceStatus()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSAsync);
            results = new StreamWriter(stream);

            DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
            Test statusTest = new Test(operation);
            statusTest.setTestName("ValidSerial");
			statusTest.setExpectedResult ("201");
			statusTest.setType ("performance");


            List<Test> tests = new List<Test>();
            tests.Add(statusTest);

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPSCalls().runTest(statusTest, HTTPOperation.POST);
                Console.WriteLine("Test starting:" + i.ToString());
            }
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("All tests initialized, waiting on them to run as async");
            Console.WriteLine("------------------------------------------------------");
            Task.WaitAll(tasks);

            foreach (Task<double> nextResult in tasks)
            {
                results.WriteLine("Test Time," + nextResult.Result);
            }

            results.Close();
        }

		[Test()]
		public void SyncHTTPDeviceStatus()
		{
			FileStream stream;
			stream = File.Create(outputFileHTTPSync);
			results = new StreamWriter(stream);

			for (int i = 0; i < TestGlobals.maxReps; i++)
			{
				System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

				DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);

				Test statusTest = new Test(operation);
				statusTest.setTestName("ValidSerial");
				statusTest.setExpectedResult ("201");
				statusTest.setType ("performance");

				timer.Start();
				AsyncContext.Run(async () => await new HTTPCalls().runTest(statusTest, HTTPOperation.POST));
				timer.Stop();
				double time = timer.Elapsed.TotalMilliseconds;
				results.WriteLine("Test Time," + time);
				System.Threading.Thread.Sleep(TestGlobals.delay);
				//Verify Server didn't throw up
			}
			results.Close();
		}

        [Test()]
        public void AsyncHTTPDeviceStatus()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPAsync);
            results = new StreamWriter(stream);

            DeviceStatus operation = new DeviceStatus(TestGlobals.testServer, status);
            Test statusTest = new Test(operation);
            statusTest.setTestName("ValidSerial");
			statusTest.setExpectedResult ("201");
			statusTest.setType ("performance");


            List<Test> tests = new List<Test>();
            tests.Add(statusTest);

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPCalls().runTest(statusTest, HTTPOperation.POST);
                Console.WriteLine("Test starting:" + i.ToString());
            }
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("All tests initialized, waiting on them to run as async");
            Console.WriteLine("------------------------------------------------------");
            Task.WaitAll(tasks);

            foreach (Task<double> nextResult in tasks)
            {
                results.WriteLine("Test Time," + nextResult.Result);
            }

            results.Close();
        }

		[Test()]
		//Multi-client simultaneious scans
		public void multiClientStatus()
		{
			FileStream stream;
			stream = File.Create(outputFileMultiClientStatus);
			results = new StreamWriter(stream);

			DeviceStatus operation1 = new DeviceStatus(TestGlobals.testServer, status);

			Test statusTest1 = new Test(operation1);
			statusTest1.setTestName("ValidSerial");
			statusTest1.setExpectedResult ("201");
			statusTest1.setType ("performance");


			DeviceStatus operation2 = new DeviceStatus(TestGlobals.testServer, status);

			Test statusTest2 = new Test(operation2);
			statusTest2.setTestName("ValidSerial");
			statusTest2.setExpectedResult ("201");
			statusTest2.setType ("performance");

			// Construct started tasks
			Task<double>[,] tasks = new Task<double>[TestGlobals.maxReps, 2];
			for (int i = 0; i < TestGlobals.maxReps; i++)
			{
				System.Threading.Thread.Sleep(TestGlobals.delay);
				tasks[i, 0] = new HTTPCalls().runTest(statusTest1, HTTPOperation.POST);
				tasks[i, 1] = new HTTPCalls().runTest(statusTest2, HTTPOperation.POST);
				Console.WriteLine("Test starting:" + i.ToString());
				Task.WaitAll(tasks[i, 0], tasks[i, 1]);
			}

			foreach (Task<double> nextResult in tasks)
			{
				results.WriteLine("Test Time," + nextResult.Result);
			}

			results.Close();
		}

    }
}
