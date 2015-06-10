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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using ConsoleApplication1;
using System.Security.Cryptography;

namespace InterceptorTester.Tests.AdminTests
{
	[TestFixture()]
    public class InterceptorTest
    {
        KeyValuePair<JObject, string> intStore;

		[TestFixtureSetUp()]
        public void setup()
        {
            TestGlobals.setup();
        }


		[Test()]
		public static void createInterceptor()
		{
            LocationTest.getLocId();
            string loc = TestGlobals.locIdCreated;
            Console.WriteLine("Creating intercepter w/ loc:");
            Console.WriteLine(loc);
            idPost();
            InterceptorJSON json = new InterceptorJSON(int.Parse(TestGlobals.locIdCreated), TestGlobals.intSerialCreated, "wappisk", "AYYYYLMAO");
            Interceptor newInt = new Interceptor(TestGlobals.adminServer, TestGlobals.intIdCreated, json);
			Test mTest = new Test(newInt);
            HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();
            Console.WriteLine(newInt.getJson().ToString());
            AsyncContext.Run(async () => await new HTTPSCalls().runTest(mTest, HTTPOperation.POST, client));
            Console.WriteLine(HTTPSCalls.result.Value.ToString());
		}

        private static string idPost()
        {
            string query = "/api/interceptorId/";
            string intSerial = "123456789020";
            string intId = "8675308";
            
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bArray = new byte[25463635];
            byte[] hashedPasswordInBytes = sha.ComputeHash(bArray);
            var stringPassword = string.Concat(hashedPasswordInBytes.Select(b => string.Format("{0:X2}", b)));
            InterceptorIdJSON json = new InterceptorIdJSON ("TESTSERIAL", stringPassword);
            InterceptorIdJSON[] idList = new InterceptorIdJSON[1];
            idList[0] = json;
            JObject jPass = JObject.Parse("{\"idList\":[{\"intId\":\""+intId+"\", \"intSerial\":\""+intSerial+"\", \"key\": \"IEEgQiBQIFIgVSAiIP8=\"},{\"intId\":\"8675309\", \"intSerial\":\"123456789021\", \"key\": \"IEEgQiBQIFIgVSAiIP8=\"}]}");
            GenericRequest newId = new GenericRequest(TestGlobals.adminServer, query, jPass);
            Test mTest = new Test(newId);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();

            AsyncContext.Run(async () => await new HTTPSCalls().runTest(mTest, HTTPOperation.POST, client));
            string statusCode = HTTPSCalls.result.Key.GetValue("StatusCode").ToString();
            Console.WriteLine(statusCode);
            Console.WriteLine(HTTPSCalls.result.Value.ToString());
            TestGlobals.intIdCreated = intId;
            TestGlobals.intSerialCreated = intSerial;
            return intId;
        }

		[Test()]
		public void getSingleInterceptor()
		{
			string query = "/API/Interceptor/" + TestGlobals.intSerialCreated;
            Console.WriteLine(query);
            GenericRequest getInt = new GenericRequest(TestGlobals.adminServer, query, null);
			Test mTest = new Test(getInt);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();
            AsyncContext.Run(async () => await new HTTPSCalls().runTest(mTest, HTTPOperation.GET, client));
            string statusCode = HTTPSCalls.result.Key.GetValue("StatusCode").ToString();
            Assert.AreEqual("200", statusCode);
			intStore = HTTPCalls.result;
		}

		[Test()]
		public void getMultipleInterceptors()
		{
			string query = "/API/Interceptor/?LocId=" + TestGlobals.locIdCreated;
            GenericRequest getInt = new GenericRequest(TestGlobals.adminServer, query, null);
			Test mTest = new Test (getInt);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();
			AsyncContext.Run (async() => await new HTTPSCalls ().runTest (mTest, HTTPOperation.GET, client));
            string statusCode = HTTPSCalls.result.Key.GetValue("StatusCode").ToString();
            Assert.AreEqual("200", statusCode);
			intStore = HTTPCalls.result;
		}

		[Test()]
		public void removeInterceptor()
		{
            disableId(TestGlobals.intSerialCreated);

			string query = "/api/interceptor/" + TestGlobals.intSerialCreated;
            GenericRequest intReq = new GenericRequest(TestGlobals.adminServer, query, null);
			Test intTest = new Test(intReq);
			HttpClient client;

            Console.WriteLine(intReq.getUri().ToString());

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();
            AsyncContext.Run(async () => await new HTTPSCalls().runTest(intTest, HTTPOperation.DELETE, client));
            Console.WriteLine(HTTPSCalls.result.Value);
		}

        private void disableId(string id)
        {
            string query = "/api/interceptor/" + id;
            DeviceStatusPutJSON json = new DeviceStatusPutJSON();
            GenericRequest intReq = new GenericRequest(TestGlobals.adminServer, query, json);
            Test intTest = new Test(intReq);
            HttpClient client;

            Console.WriteLine(intReq.getUri().ToString());

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticateTest.getSessionToken();
            AsyncContext.Run(async () => await new HTTPSCalls().runTest(intTest, HTTPOperation.PUT, client));
            Console.WriteLine(HTTPSCalls.result.Value);
        }

	}
}   
