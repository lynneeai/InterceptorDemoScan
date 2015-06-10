using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
	class DeviceStatus : APIOperation
	{
        DeviceStatusJSON json;
		public DeviceStatus(Uri server, DeviceStatusJSON status)
		{
			opHost = server;
			hOp = HTTPOperation.POST;
            json = status;
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
            return "DeviceStatus";
		}

        public override Uri getUri()
        {
            return new Uri(opHost, "/api/DeviceStatus/");
        }

        public override object getJson()
        {
			var jsonSTATUS = JObject.FromObject(json);
			return jsonSTATUS;
        }
    }
}


