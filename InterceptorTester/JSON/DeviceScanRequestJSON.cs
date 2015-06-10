using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DeviceScanRequestJSON
    {
        /// <summary>
        /// Gets or sets a, which is the Hashed Embedded ID
        /// </summary>
        private readonly string _a;

        /// <summary>
        /// Gets or sets i, which is the serial number
        /// </summary>
        private readonly string _i;

        //Data String
        private readonly string _d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Hashed Embedded id</param>
        /// <param name="i">IntSerial</param>
        /// <param name="d">Scan data</param>
        public DeviceScanRequestJSON(string a, string i, string d)
        {
            _a = a;
            _i = i;
            _d = d;
        }

        //[DataMember]
        public string i
        {
            get { return _i; }
        }

        //[DataMember]
        public string d
        {
            get { return _d; }
        }

        //[DataMember]
        public string a
        {
            get { return _a; }
        }

        public override string ToString()
        {
            return string.Format("[DeviceScanRequest I: {0}, D: {1}, A: {2} ]", _i, _d, _a);
        }

        protected bool Equals(DeviceScanRequestJSON other)
        {
            return string.Equals(_i, other._i) && string.Equals(_d, other._d) && string.Equals(_a, other._a);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceScanRequestJSON)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (_i != null ? _i.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_d != null ? _d.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_a != null ? _a.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
