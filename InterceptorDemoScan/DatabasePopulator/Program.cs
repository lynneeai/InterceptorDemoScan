﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;
using InterceptorTester;
using System.IO;

namespace DatabasePopulator
{
    class Program
    {
		static List<ConsoleApplication1.Test> basket = new List<ConsoleApplication1.Test>();
		static int basketNum;
		static int scanNum;
		static int totalScan;
		public static string logFile = "../../../logs/scanPopulatorLog.txt";
		static StreamWriter results;
		public static int seqNum;

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting demo scan generator");
            try
            {
                ConsoleApplication1.TestGlobals.setup();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            generateScans();
        }

        private static async void generateScans()
        {
            Console.WriteLine("Generating scans");
			int[] pseudoRandDelay = {60, 120, 240, 60};
			int[] pseudoRandBasket = {10,9,8,7,6,5,4,3,2,1,5,6,7,8,9,1,2,3,4,10,6,5,4,3,2,10,9,8,7,1};

			basketNum = 0;
			totalScan = 0;
			seqNum = 1;

			string started = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffffff");
			DateTime started1 = DateTime.Now;

			FileStream stream;
            try
            {
                stream = File.Create(logFile);
                results = new StreamWriter(stream);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory("../../../logs/");
                stream = File.Create(logFile);
                results = new StreamWriter(stream);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Demo data: ");
			Console.WriteLine (ConsoleApplication1.TestGlobals.demoServer);
			Console.WriteLine (ConsoleApplication1.TestGlobals.demoSerial);

			foreach (int delay in pseudoRandDelay)
            {
				foreach (int basketType in pseudoRandBasket)
                {
					getBasket(basketType, seqNum);
					Console.WriteLine (DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));
					results.WriteLine (DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));

					scanNum = 0;

					foreach (ConsoleApplication1.Test nextScan in basket)
                    {
						Console.WriteLine("Posting Scan");
						Console.WriteLine(DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));

						results.WriteLine ("Posting Scan");
						results.WriteLine (DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));
						AsyncContext.Run(async () => await new ConsoleApplication1.HTTPSCalls().runTest(nextScan, ConsoleApplication1.HTTPOperation.POST));

						Console.WriteLine ("Scan posted:");
						Console.WriteLine(DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));
						Console.WriteLine (nextScan.getOperation().getJson().ToString ());

						results.WriteLine ("Scan posted:");
						results.WriteLine (DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff"));
						results.WriteLine (nextScan.getOperation ().getJson ().ToString ());

						scanNum++;
						seqNum++;

                        Console.WriteLine ("Waiting for next scan...");

						results.WriteLine ("Posted Scan");
						results.WriteLine ("Waiting for next scan...");

						System.Threading.Thread.Sleep (4000);

                    }
					basketNum++;
					totalScan += scanNum;

					string current = DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff");
					DateTime current1 = DateTime.Now;
					TimeSpan testTime = current1 - started1;

					Console.WriteLine ("Basket complete.");
					Console.WriteLine ("Number of items in this basket: " + scanNum);
					Console.WriteLine ("Total baskets: " + basketNum);
					Console.WriteLine ("Total scans: " + totalScan);
					Console.WriteLine ("Test started at: " + started);
					Console.WriteLine ("Current time: " + current);
					Console.WriteLine ("Test has lasted for " + testTime);
					Console.WriteLine ("Waiting for next basket...");

					results.WriteLine ("Basket complete.");
					results.WriteLine ("Number of items in this basket: " + scanNum);
					results.WriteLine ("Total baskets: " + basketNum);
					results.WriteLine ("Total scans: " + totalScan);
					results.WriteLine ("Test started at: " + started);
					results.WriteLine ("Current time: " + current);
					results.WriteLine ("Test has lasted for " + testTime);
					results.WriteLine ("Waiting for next basket...");


					System.Threading.Thread.Sleep(delay * 1000);

					Console.WriteLine ("Getting next basket");
					Console.WriteLine();

					results.WriteLine ("Getting next basket");
					results.WriteLine ();
                }
            }
            Console.WriteLine("Reached end of posts");
			results.WriteLine ("Reached end of posts");
			results.WriteLine ();

			string ended = DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss.ffffff");
			DateTime ended1 = DateTime.Now;
			TimeSpan testLast = ended1 - started1;
			results.WriteLine ("Summary:");
			results.WriteLine ("Total baskets: " + basketNum);
			results.WriteLine ("Total scans: " + totalScan);
			results.WriteLine ("Test started at " + started);
			results.WriteLine ("Test ended at " + ended);
			results.WriteLine ("Test lasted for " + testLast);

			results.Close ();
        }

		private static List<ConsoleApplication1.Test> getBasket(int basketType, int s)
        {
            basket.Clear();
			ConsoleApplication1.DemoScans scanGen = new ConsoleApplication1.DemoScans();

            switch (basketType)
            {
                case 1:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Laptop11Inch, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Headset, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.LaptopCase11Inch, s + 2));
                    break;
                case 2:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Laptop13Inch, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Mouse, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.LaptopCase13Inch, s + 2));
                    break;
                case 3:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Printer, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 1));
                    break;
                case 4:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Laptop13Inch, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.LaptopCase13Inch, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Warranty, s + 2));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.ExternalHDD, s + 3));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Headset, s + 4));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Mouse, s + 5));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Keyboard, s + 6));
                    break;
                case 5:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.ExternalHDD, s + 7));
                    break;
                case 6:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Keyboard, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 1));
                    break;
                case 7:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Speakers, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Headset, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 2));
                    break;
                case 8:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Mouse, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Earbuds, s + 2));
                    break;
                case 9:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.HdmiCable, s));
                    break;
                case 10:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.ExternalHDD, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 1));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Keyboard, s + 2));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Earbuds, s + 3));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Speakers, s + 4));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.Mouse, s + 5));
                    break;
                default:
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.ExternalHDD, s));
				basket.Add(ConsoleApplication1.DemoScans.getScan(ConsoleApplication1.UpcCode.UsbCable, s + 1));
                    break;
            }
				
            return basket;
        }
    }
}
