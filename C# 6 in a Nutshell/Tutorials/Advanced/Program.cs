using System;

using static System.Console;

namespace Advanced
{
	class Program
	{
		static void Main(string[] args) {
            TestDriveDelegates();
		}

        static void TestDriveDelegates() {
            //Delegates.TestDriveDelegates();
            //Delegates.TestDrivePluginMethods();
            //Delegates.TestDriveMulticastDelegate();
            //Delegates.TestDriveGenericDelegate();
            Delegates.TestDriveStandardEventPattern();
        }
	}
}
