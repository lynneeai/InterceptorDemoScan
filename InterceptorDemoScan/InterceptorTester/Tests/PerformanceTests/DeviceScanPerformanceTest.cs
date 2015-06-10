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
	public class DeviceScanPerformanceTest
    {
        static StreamWriter results;

        static string outputFileHTTPSSync = "../../../logs/SyncHTTPSDeviceScanPerformanceTest.csv";
        static string outputFileHTTPAsync = "../../../logs/AsyncHTTPDeviceScanPerformanceTest.csv";
        static string outputFileHTTPSync = "../../../logs/SyncHTTPDeviceScanPerformanceTest.csv";
        static string outputFileHTTPSAsync = "../../../logs/AsyncHTTPSDeviceScanPerformanceTest.csv";
        static string outputFileMultiClientScans = "../../../logs/MultiClientDeviceScan.csv";

        [TestFixtureSetUp()]
        public void setup()
        {
            TestGlobals.setup();
        }

        [Test()]
        public void SyncHTTPSDeviceScan()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSSync);
            results = new StreamWriter(stream);

            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                DeviceScanJSON testJson = new DeviceScanJSON();
                testJson.i = TestGlobals.validSerial;
                testJson.d = "1289472198573";
                testJson.b = null;
                testJson.s = 4;
                DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

                Test scanTest = new Test(testDScan);
                scanTest.setTestName("ValidSingleScanSimple");
				scanTest.setExpectedResult ("201");
				scanTest.setType ("performance");

                timer.Start();
				AsyncContext.Run(async () => await new HTTPSCalls().runTest(scanTest, HTTPOperation.POST));
                timer.Stop();
                double time = timer.Elapsed.TotalMilliseconds;
                results.WriteLine("Test Time," + time);
                System.Threading.Thread.Sleep(TestGlobals.delay);
                //Verify Server didn't throw up
            }
            results.Close();
        }

        [Test()]
        // Valid Single Scan
        public void AsyncHTTPSDeviceScan()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSAsync);
            results = new StreamWriter(stream);

            DeviceScanJSON testJson = new DeviceScanJSON();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("ValidSingleScanSimple");
			scanTest.setExpectedResult ("201");
			scanTest.setType ("performance");

            List<Test> tests = new List<Test>();
            tests.Add(scanTest);

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                // thread sleep?  

                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPSCalls().runTest(scanTest, HTTPOperation.POST);
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
        public void SyncHTTPDeviceScan()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSync);
            results = new StreamWriter(stream);

            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                DeviceScanJSON testJson = new DeviceScanJSON();
                testJson.i = TestGlobals.validSerial;
                testJson.d = "1289472198573";
                testJson.b = null;
                testJson.s = 4;
                DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

                Test scanTest = new Test(testDScan);
                scanTest.setTestName("ValidSingleScanSimple");
				scanTest.setExpectedResult ("201");
				scanTest.setType ("performance");

                timer.Start();
				AsyncContext.Run(async () => await new HTTPCalls().runTest(scanTest, HTTPOperation.POST));
                timer.Stop();
                double time = timer.Elapsed.TotalMilliseconds;
                results.WriteLine("Test Time," + time);
                System.Threading.Thread.Sleep(TestGlobals.delay);
                //Verify Server didn't throw up
            }
            results.Close();
        }

        [Test()]
        // Valid Single Scan
        public void AsyncHTTPDeviceScan()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPAsync);
            results = new StreamWriter(stream);

            DeviceScanJSON testJson = new DeviceScanJSON();
            testJson.i = TestGlobals.validSerial;
            testJson.d = "1289472198573";
            testJson.b = null;
            testJson.s = 4;
            DeviceScan testDScan = new DeviceScan(TestGlobals.testServer, testJson);

            Test scanTest = new Test(testDScan);
            scanTest.setTestName("ValidSingleScanSimple");
			scanTest.setExpectedResult ("201");
			scanTest.setType ("performance");

            List<Test> tests = new List<Test>();
            tests.Add(scanTest);

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPCalls().runTest(scanTest, HTTPOperation.POST);
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
        public void multiClientScans()
        {
            FileStream stream;
            stream = File.Create(outputFileMultiClientScans);
            results = new StreamWriter(stream);

            DeviceScanJSON testJson1 = new DeviceScanJSON();
            testJson1.i = TestGlobals.validSerial;
            testJson1.d = "1289472198573";
            testJson1.b = null;
            testJson1.s = 4;
            DeviceScan Scan1 = new DeviceScan(TestGlobals.testServer, testJson1);

            Test scanTest1 = new Test(Scan1);
            scanTest1.setTestName("ValidSingleScanSimple");
			scanTest1.setExpectedResult ("201");
			scanTest1.setType ("performance");


            DeviceScanJSON testJson2 = new DeviceScanJSON();
            testJson2.i = TestGlobals.validSerial;
            testJson2.d = "1289472198573";
            testJson2.b = null;
            testJson2.s = 4;
            DeviceScan Scan2 = new DeviceScan(TestGlobals.testServer, testJson2);

            Test scanTest2 = new Test(Scan2);
            scanTest2.setTestName("ValidSingleScanSimple");
			scanTest2.setExpectedResult ("201");
			scanTest2.setType ("performance");

            // Construct started tasks
            Task<double>[,] tasks = new Task<double>[TestGlobals.maxReps, 2];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i, 0] = new HTTPCalls().runTest(scanTest1, HTTPOperation.POST);
                tasks[i, 1] = new HTTPCalls().runTest(scanTest2, HTTPOperation.POST);
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
