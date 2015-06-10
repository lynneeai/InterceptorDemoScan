using System.Runtime.Serialization;

namespace ConsoleApplication1
{

	//[DataContract]

	public class OrganizationJSON
	{

		public OrganizationJSON(int ownerID, string name)
		{
			this.ownerID = ownerID;
			this.name = name;
		}

		public int ownerID;

		public string name;

		// ReSharper restore InconsistentNaming

		public override string ToString()
		{
			return "";
		}
	}

}

