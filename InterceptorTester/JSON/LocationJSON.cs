using System.Runtime.Serialization;

namespace ConsoleApplication1
{

	//[DataContract]

	public class LocationJSON
	{
		public LocationJSON(string orgId, string unitSuite, string street, string city, string province, string country, string postalCode)
		{
			this.orgId = orgId;
            this.unitSuite = unitSuite;
            this.street = street;
            this.city = city;
            this.State = province;
            this.country = country;
            this.postalCode = postalCode;
            this.State = province;
        }



		public bool isValid ()
		{
			if ((orgId != null) && (unitSuite != null) && (city != null) && (State != null) && (country != null) && (postalCode != null))
			{
				return true;
			}

			return false;
		}

        public string State;

		public string orgId;

        public string locDesc;

        public string unitSuite;

        public string street;

        public string city;

        public string country;

        public string postalCode;

        public decimal latitude;

        public decimal longitude;

        public string locType;

        public string locSubType;

		// ReSharper restore InconsistentNaming

		public override string ToString()
		{
			return "";
		}
	}

}


