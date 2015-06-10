using System.Runtime.Serialization;

namespace ConsoleApplication1
{

	//[DataContract]

	public class InterceptorJSON
	{
		public InterceptorJSON(int locId, string intSerial, string ssid, string wpaPSK)
        {
            this.locId = locId;
            this.intSerial = intSerial;
            //this.orgId = orgId;
            this.ssid = ssid;
            this.wpaPSK = wpaPSK;
        }

		public bool isValid ()
		{
			if ((locId != null) && (intSerial != null))
			{
				return true;
			}

			return false;
		}

		public int locId;

        public string intSerial;

        //string orgId;

        public string ssid;

        public string wpaPSK;


		// ReSharper restore InconsistentNaming

		public override string ToString()
		{
			return locId.ToString() + " " + intSerial + " " + ssid + " " + wpaPSK;
		}
	}

}



