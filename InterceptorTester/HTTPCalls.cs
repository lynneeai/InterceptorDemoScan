using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Timers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace ConsoleApplication1
{
    //TODOIF: Add client(?)
   public  class HTTPCalls
    {
        public static KeyValuePair<JObject, string> result;

        public HTTPCalls()
        {
            
        }

       /// <summary>
       /// Runs a test with the given http operation type
       /// </summary>
       /// <param name="currentTest">Test object containing data for the http request</param>
       /// <param name="op">One of HTTPOperation's enums (GET,POST,PUT,DELETE)</param>
       /// <returns></returns>
        public async Task<double> runTest(Test currentTest, HTTPOperation op)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Test starting");
            //Do tests
            timer.Start();
            await callType(currentTest, op);
            timer.Stop();
            double time = timer.Elapsed.TotalMilliseconds;
            Console.WriteLine("Test ending");

            return time;
        }

        static async Task callType (Test currentTest, HTTPOperation op)
        {
            switch (op)
            {
                case HTTPOperation.GET:
                    result = await RunGetAsync(currentTest.getOperation().getUri());
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.POST:
                    result = await RunPostAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson());
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.PUT:
                    result = await RunPutAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson());
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.DELETE:
                    result = await RunDeleteAsync(currentTest.getOperation().getUri());
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
               default:
                    Console.WriteLine("Unrecognized HTTP Operation!");
                    Console.WriteLine(currentTest.ToString());
                    break;
            }
        }

        //GET call
        static async Task<KeyValuePair<JObject, string>> RunGetAsync(Uri qUri)
        {
            // ... Use HttpClient.
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(qUri.AbsoluteUri))
                {
                    JObject jResponse = JObject.FromObject(response);
                    string content = await response.Content.ReadAsStringAsync();
                    return new KeyValuePair<JObject, string>(jResponse, content);
                }
            }
            catch (Exception e)
            {
				Console.WriteLine("GET request failed: " + e.ToString());
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine(e);
                return new KeyValuePair<JObject, string>(null, null);
            }
        }

        //POST call
        static async Task<KeyValuePair<JObject, string>> RunPostAsync(Uri qUri, Object contentToPush)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Newtonsoft Json serialization
                    Console.WriteLine(contentToPush.ToString());
                    var upContent = JObject.FromObject(contentToPush);
                    Console.WriteLine(upContent.ToString());
                    var strContent = new System.Net.Http.StringContent(upContent.ToString(), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(qUri, strContent))
                    {
                        JObject jResponse = JObject.FromObject(response);
                        string content = await response.Content.ReadAsStringAsync();
                        return new KeyValuePair<JObject, string>(jResponse, content);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("POST request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine("Content: " + contentToPush.ToString());
                Console.WriteLine(e);
                return new KeyValuePair<JObject, string>(null, null);
            }
        }

        //PUT call
        static async Task<KeyValuePair<JObject, string>> RunPutAsync(Uri qUri, Object contentToPut)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Newtonsoft Json serialization
                    var upContent = JObject.FromObject(contentToPut);
                    Console.WriteLine(upContent.ToString());
                    var strContent = new System.Net.Http.StringContent(upContent.ToString(), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PutAsync(qUri, strContent))
                    {
                        JObject jResponse = JObject.FromObject(response);
                        string content = await response.Content.ReadAsStringAsync();
                        return new KeyValuePair<JObject, string>(jResponse, content);
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("PUT request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine("Content: " + contentToPut.ToString());
                Console.WriteLine(e);
                return new KeyValuePair<JObject, string>(null, null);
            }
        }

        //DELETE call
        static async Task<KeyValuePair<JObject, string>> RunDeleteAsync(Uri qUri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.DeleteAsync(qUri))
                {
                    JObject jResponse = JObject.FromObject(response);
                    string content = await response.Content.ReadAsStringAsync();
                    return new KeyValuePair<JObject, string>(jResponse, content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("DELETE request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine(e);
                return new KeyValuePair<JObject, string>(null,null);
            }
        }
    }
}
