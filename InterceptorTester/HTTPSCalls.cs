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

namespace ConsoleApplication1{

    public class HTTPSCalls
    {
		static string certPath = "../../Data/unittestcert.pfx";
        static string certPass = "unittest";
        // Create a collection object and populate it using the PFX file
        static X509Certificate cert;

        public static KeyValuePair<JObject, string> result;


        /// <summary>
        /// Constructor, creates client certificate from file located at: "../../Data/unittestcert.pfx"
        /// </summary>
        public HTTPSCalls()
        {
            try
            {
                cert = new X509Certificate(certPath, certPass);
                Console.WriteLine("SLL certificate created successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not initialize SLL certificate");
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Runs a test with the given http operation type
        /// </summary>
        /// <param name="currentTest">Test object containing data for the http request</param>
        /// <param name="op">One of HTTPOperation's enums (GET,POST,PUT,DELETE)</param>
        /// <returns>Number of milliseconds the request took</returns>
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

        /// <summary>
        /// Runs a test with the given http operation type
        /// </summary>
        /// <param name="currentTest">Test object containing data for the http request</param>
        /// <param name="op">One of HTTPOperation's enums (GET,POST,PUT,DELETE)</param>
        /// <param name="client">HTTP Client w/ a header - used for authorization</param>
        /// <returns>Number of milliseconds the request took</returns>
        public async Task<double> runTest(Test currentTest, HTTPOperation op, HttpClient client)
        {
			
			System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Test starting");

            //Do tests
            timer.Start();
			await callType(currentTest, op, client);
            timer.Stop();
            double time = timer.Elapsed.TotalMilliseconds;
            Console.WriteLine("Test ending");


            return time;
        }

        static async Task callType(Test currentTest, HTTPOperation op)
        {
			switch (op)
            {
			case HTTPOperation.GET:
					result = await RunGetAsync (currentTest.getOperation ().getUri ());
					currentTest.setActualResult (result.Key.GetValue ("StatusCode").ToString ());
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

        static async Task callType(Test currentTest, HTTPOperation op, HttpClient client)
        {
			switch (op)
            {
                case HTTPOperation.GET:
                    result = await RunGetAsync(currentTest.getOperation().getUri(), client);
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.POST:
                    result = await RunPostAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson(), client);
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.PUT:
                    result = await RunPutAsync(currentTest.getOperation().getUri(), currentTest.getOperation().getJson(), client);
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                case HTTPOperation.DELETE:
                    result = await RunDeleteAsync(currentTest.getOperation().getUri(), client);
                    currentTest.setActualResult(result.Key.GetValue("StatusCode").ToString());
                    break;
                default:
                    Console.WriteLine("Unrecognized HTTP Operation!");
                    Console.WriteLine(currentTest.ToString());
                    break;
            }
        }

        //GET call w/ client
        static async Task<KeyValuePair<JObject, string>> RunGetAsync(Uri qUri, HttpClient clientPass)
        {
            // ... Use HttpClient.
            try
            {
                WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Authorization = clientPass.DefaultRequestHeaders.Authorization;
                    using (HttpResponseMessage response = await client.GetAsync(qUri.AbsoluteUri))
                    {
                        JObject jResponse = JObject.FromObject(response);
                        string content = await response.Content.ReadAsStringAsync();
                        return new KeyValuePair<JObject, string>(jResponse, content);
                    }
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

        //GET call
        static async Task<KeyValuePair<JObject, string>> RunGetAsync(Uri qUri)
        {
            // ... Use HttpClient.
            try
            {
				WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(qUri.AbsoluteUri))
                    {
                        JObject jResponse = JObject.FromObject(response);
                        string content = await response.Content.ReadAsStringAsync();
                        return new KeyValuePair<JObject, string>(jResponse, content);
                    }
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
				WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //TODO: How do we get session token
                    //client.DefaultRequestHeaders.Authorization = "Token " + "1";
                    
                    // Newtonsoft Json serialization
                    var upContent = JObject.FromObject(contentToPush);
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

        //POST call w/ client
        static async Task<KeyValuePair<JObject, string>> RunPostAsync(Uri qUri, Object contentToPush, HttpClient clientPass)
        {
            try
            {
                WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //TODO: Do this in a less stupid way
                    client.DefaultRequestHeaders.Authorization = clientPass.DefaultRequestHeaders.Authorization;

                    // Newtonsoft Json serialization
                    var upContent = JObject.FromObject(contentToPush);
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
                // ... Use HttpClient.
				WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    Console.WriteLine(contentToPut.ToString());
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

        //PUT call w/ client
        static async Task<KeyValuePair<JObject, string>> RunPutAsync(Uri qUri, Object contentToPut, HttpClient clientPass)
        {
            try
            {
                // ... Use HttpClient.
                WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = clientPass.DefaultRequestHeaders.Authorization;

                    var upContent = JObject.FromObject(contentToPut);
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
                // ... Use HttpClient.
				WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
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

        //DELETE call w/ client
        static async Task<KeyValuePair<JObject, string>> RunDeleteAsync(Uri qUri, HttpClient clientPass)
        {
            try
            {
                // ... Use HttpClient.
                WebRequestHandlerWithClientcertificates handler = new WebRequestHandlerWithClientcertificates();
                handler.ClientCertificates.Add(cert);
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Authorization = clientPass.DefaultRequestHeaders.Authorization;
                    using (HttpResponseMessage response = await client.DeleteAsync(qUri))
                    {
                        JObject jResponse = JObject.FromObject(response);
                        string content = await response.Content.ReadAsStringAsync();
                        return new KeyValuePair<JObject, string>(jResponse, content);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("DELETE request failed.");
                Console.WriteLine("URL: " + qUri.ToString());
                Console.WriteLine(e);
                return new KeyValuePair<JObject, string>(null, null);
            }
        }
    }
}
