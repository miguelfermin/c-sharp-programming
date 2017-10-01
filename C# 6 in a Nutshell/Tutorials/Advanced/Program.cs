using System;

namespace Advanced
{
	class Program
	{
		static void Main(string[] args) {
            //TestDriveDelegates();
            TestDriveEvents();
		}

        static void TestDriveDelegates() {
            //Delegates.TestDriveDelegates();
            //Delegates.TestDrivePluginMethods();
            //Delegates.TestDriveMulticastDelegate();
            //Delegates.TestDriveGenericDelegate();
        }

        static void TestDriveEvents() {
            Events.TestDriveStandardEventPattern();
        }
	}
}
