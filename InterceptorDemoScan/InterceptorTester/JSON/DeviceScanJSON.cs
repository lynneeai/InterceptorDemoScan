using System;
using System.Linq;
using System.Runtime.Serialization;

namespace ConsoleApplication1 {

    //[DataContract]
    public class DeviceScanJSON
    {
        public bool isValid()
        {
			throw new NotImplementedException();
        }

        /// <summary>
        /// Interceptor Serial number
        /// </summary>
        //[DataMember]
        public string i { get; set; }

        /// <summary>
        /// List of static device scans. Only one of this or "d" is not null
        /// </summary>
        //[DataMember]
        public string[] b { get; set; }
        /// <summary>
        /// A dynamic code device scan. Only one of this or "b" is not null.
        /// </summary>
        //[DataMember]
        public string d { get; set; }
        /// <summary>
        /// request sequence number - monotonically increasing until it resets at max value + 1
        /// </summary>
        //[DataMember]
        public int s { get; set; }

        public override string ToString()
        {
            return string.Format("[SequenceDeviceScan s: {0}, i: {1}, d: {2}, b: {3} ]", s, i, d, b);
        }
        /*
        protected bool Equals(SequenceDeviceScan other)
        {
            return s == other.s && string.Equals(i, other.i) && string.Equals(d, other.d) && b.SequenceEqual( other.b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SequenceDeviceScan) obj);
        }
        */
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) s;
                hashCode = (hashCode*397) ^ (i != null ? i.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (d != null ? d.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (b != null ? b.GetHashCode() : 0);
                return hashCode;
            }
        }
    }



}