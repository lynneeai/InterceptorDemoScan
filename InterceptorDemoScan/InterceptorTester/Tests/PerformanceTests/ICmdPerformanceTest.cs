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
	public class ICmdPerformanceTest
    {
        static StreamWriter results;

        static string outputFileHTTPSSync = "../../../logs/SyncHTTPSICmdPerformanceTest.csv";
        static string outputFileHTTPSAsync = "../../../logs/AsyncHTTPSICmdPerformanceTest.csv";
        static string outputFileHTTPSync = "../../../logs/SyncHTTPICmdPerformanceTest.csv";
        static string outputFileHTTPAsync = "../../../logs/AsyncHTTPICmdPerformanceTest.csv";
        static string outputFileMultiClientICmd = "../../../logs/MultiClientICmd.csv";

        [TestFixtureSetUp()]
        public void setup()
        {
            TestGlobals.setup();
        }

        [Test()]
        public void SyncHTTPSICmd()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSSync);
            results = new StreamWriter(stream);

            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                ICmd validICmd = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
                Test validTest = new Test(validICmd);
                validTest.setTestName("ValidSerial");
				validTest.setExpectedResult ("200");
				validTest.setType ("performance");
                List<Test> tests = new List<Test>();
                tests.Add(validTest);

                timer.Start();
                AsyncContext.Run(async () => await new HTTPSCalls().runTest(validTest, HTTPOperation.GET));
                timer.Stop();
                double time = timer.Elapsed.TotalMilliseconds;
                results.WriteLine("Test Time," + time);
                //Verify Server didn't throw up
            }
            results.Close();
        }

        [Test()]
        public void AsyncHTTPSICmd()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSAsync);
            results = new StreamWriter(stream);
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();


            ICmd validICmd = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
            Test validTest = new Test(validICmd);
            validTest.setTestName("ValidSerial");
			validTest.setExpectedResult ("200");
			validTest.setType ("performance");
            List<Test> tests = new List<Test>();
            tests.Add(validTest);

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPSCalls().runTest(validTest, HTTPOperation.GET);
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
        public void SyncHTTPICmd()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPSync);
            results = new StreamWriter(stream);

            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                ICmd validICmd = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
                Test validTest = new Test(validICmd);
                validTest.setTestName("ValidSerial");
				validTest.setExpectedResult ("200");
				validTest.setType ("performance");
                List<Test> tests = new List<Test>();
                tests.Add(validTest);

                timer.Start();
                AsyncContext.Run(async () => await new HTTPCalls().runTest(validTest, HTTPOperation.GET));
                timer.Stop();
                double time = timer.Elapsed.TotalMilliseconds;
                results.WriteLine("Test Time," + time);

                //Verify Server didn't throw up
            }
            results.Close();
        }

        [Test()]
        public void AsyncHTTPICmd()
        {
            FileStream stream;
            stream = File.Create(outputFileHTTPAsync);
            results = new StreamWriter(stream);
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();


            ICmd validICmd = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
            Test validTest = new Test(validICmd);
            validTest.setTestName("ValidSerial");
			validTest.setExpectedResult ("200");
			validTest.setType ("performance");
            List<Test> tests = new List<Test>();
            tests.Add(validTest);

            // Construct started tasks
            Task<double>[] tasks = new Task<double>[TestGlobals.maxReps];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i] = new HTTPCalls().runTest(validTest, HTTPOperation.GET);
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
        public void multiClientICmd()
        {
            FileStream stream;
            stream = File.Create(outputFileMultiClientICmd);
            results = new StreamWriter(stream);

            ICmd validICmd1 = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
            Test validTest1 = new Test(validICmd1);
            validTest1.setTestName("ValidSerial");
			validTest1.setExpectedResult ("200");
			validTest1.setType ("performance");


            ICmd validICmd2 = new ICmd(TestGlobals.testServer, TestGlobals.validSerial);
            Test validTest2 = new Test(validICmd2);
            validTest2.setTestName("ValidSerial");
			validTest2.setExpectedResult ("200");
			validTest2.setType ("performance");

            // Construct started tasks
            Task<double>[,] tasks = new Task<double>[TestGlobals.maxReps, 2];
            for (int i = 0; i < TestGlobals.maxReps; i++)
            {
                System.Threading.Thread.Sleep(TestGlobals.delay);
                tasks[i, 0] = new HTTPCalls().runTest(validTest1, HTTPOperation.GET);
                tasks[i, 1] = new HTTPCalls().runTest(validTest2, HTTPOperation.GET);
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
