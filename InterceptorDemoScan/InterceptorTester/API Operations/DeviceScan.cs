using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace ConsoleApplication1
{
    class DeviceScan : APIOperation
    {
        DeviceScanJSON json;

        public DeviceScan(Uri server, DeviceScanJSON scan)
        {
            opHost = server;
            hOp = HTTPOperation.POST;
            json = scan;
        }

        public override string getExpectedResult()
        {
            if (json.isValid())
            {
                return "201";
            }
            else
            {
                return "400";
            }
        }

        public override string ToString()
        {
            return "DeviceScan";
        }

        public override Uri getUri()
        {
            return new Uri(opHost, "/api/DeviceScan/");
        }

        public override object getJson()
        {
			var jsonSCAN = JObject.FromObject(json);
			return jsonSCAN;
        }
    }
}
