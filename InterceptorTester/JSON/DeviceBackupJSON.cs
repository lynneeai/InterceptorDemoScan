using System;

namespace ConsoleApplication1
{

	/// <summary>
	/// Used to parse the Json of the b element of the DeviceBackup class
	/// </summary>
	public class BackupItem
    {
        public String d;
        public int s;

        public DateTime t;

        public bool? c;

        public bool isValid()
        {
            if (d != null && s != null && t != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("[BackupItem d: {0}, s: {1}, t: {2}, c: {3}]", d,s,t,c);
        }
	}

	public class DeviceBackupJSON
	{
        public bool isValid()
        {
			throw new NotImplementedException();
        }

		/// <summary>
		/// Gets or sets i
		/// </summary>
		public string i { get; set; }

		/// <summary>
		/// Gets or sets b
		/// </summary>
		public object[] b { get; set; }

		public int? s { get; set; }

		public override string ToString()
		{
			return string.Format("[DeviceBackup s: {0}, i: {1}, b: {2}]", s, i, b.ToString());
		}
	}


}

