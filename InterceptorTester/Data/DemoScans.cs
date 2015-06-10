using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Configuration;
using Nito.AsyncEx;
using System.IO.Compression;

namespace ConsoleApplication1
{
	public class DemoScans
	{
		public static Test getScan(UpcCode item, int s)
		{
			if (item == UpcCode.Laptop13Inch) {
				DeviceScanJSON scan1 = new DeviceScanJSON ();
				scan1.i = TestGlobals.demoSerial;
				scan1.d = "416000383720";
				scan1.b = null;
				scan1.s = s;
				DeviceScan testDScan1 = new DeviceScan (TestGlobals.demoServer, scan1);

				Test scanTest1 = new Test (testDScan1);
				return scanTest1;
			} 
			else if (item == UpcCode.Laptop11Inch) {
				DeviceScanJSON scan2 = new DeviceScanJSON ();
				scan2.i = TestGlobals.demoSerial;
				scan2.d = "416000336108";
				scan2.b = null;
				scan2.s = s;
				DeviceScan testDScan2 = new DeviceScan (TestGlobals.demoServer, scan2);

				Test scanTest2 = new Test (testDScan2);
				return scanTest2;
			} 
			else if (item == UpcCode.Printer) {
				DeviceScanJSON scan3 = new DeviceScanJSON ();
				scan3.i = TestGlobals.demoSerial;
				scan3.d = "416000837315";
				scan3.b = null;
				scan3.s = s;
				DeviceScan testDScan3 = new DeviceScan (TestGlobals.demoServer, scan3);

				Test scanTest3 = new Test (testDScan3);
				return scanTest3;
			} 
			else if (item == UpcCode.Warranty) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837223";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.ExternalHDD) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837360";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.Keyboard) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000338973";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.Speakers) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837988";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.Mouse) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000336894";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.LaptopCase13Inch) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837261";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.Headset) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837018";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.LaptopCase11Inch) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000338799";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.Earbuds) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837049";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.HdmiCable) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837339";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else if (item == UpcCode.UsbCable) {
				DeviceScanJSON scan4 = new DeviceScanJSON ();
				scan4.i = TestGlobals.demoSerial;
				scan4.d = "416000837827";
				scan4.b = null;
				scan4.s = s;
				DeviceScan testDScan4 = new DeviceScan (TestGlobals.demoServer, scan4);

				Test scanTest4 = new Test (testDScan4);
				return scanTest4;
			} 
			else 
			{
				return null;
			}
		}

	}
}


