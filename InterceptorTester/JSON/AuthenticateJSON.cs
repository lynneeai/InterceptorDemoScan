using System.Runtime.Serialization;

namespace ConsoleApplication1
{

	//[DataContract]

	public class AuthenticateJSON
	{

		public bool isValid ()
		{
			if ((userID != null) && (password != null))
			{
				return true;
			}

			return false;
		}

		public string userID;

		public string password;

		// ReSharper restore InconsistentNaming

		public override string ToString()
		{
			return "";
		}
	}
		
}
