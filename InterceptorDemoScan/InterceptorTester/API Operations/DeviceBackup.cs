using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
	class DeviceBackup : APIOperation
	{
        DeviceBackupJSON json;

		public DeviceBackup(Uri server, DeviceBackupJSON backup)
		{
			opHost = server;
			hOp = HTTPOperation.POST;
            json = backup;
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
            return "DeviceBackup";
		}

        public override Uri getUri()
        {
            return new Uri(opHost, "/api/DeviceBackup/");
        }

        public override object getJson()
        {
			var jsonBK = JObject.FromObject(json);
			return jsonBK;
        }
    }
}

