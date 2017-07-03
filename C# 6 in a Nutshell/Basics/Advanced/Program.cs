using System;

using static System.Console;

namespace Types
{
	class Program
	{
        public const string Message = "Hello World";

		static void Main(string[] args) {
            //TestDriveClasses();
            //TestDriveIndexers();
            //TestDriveStaticConstructors();
            //TestDriveInheritance();
            //TestDriveNewVSOverride();
            //TestDriveObjectType();
            //TestDriveBoxingAndUnboxing();
            TestDriveEnums();
		}

        static void TestDriveClasses() {
			// Object Initializer. 
			// Note: Properties have to be public.
			// Note: There must be an empty public constructor
			Address addressOne = new Address {
				street = "5500 Laurent Drive",
				city = "Parma",
				state = "OH",
				zipCode = "44129"
			};

			Person personOne = new Person("Miguel", "Fermin", addressOne);

			Person personTwo = new Person("Noah", "Fermin", new Address {
				street = "2099 W103rd ST",
				city = "Cleveland",
				state = "OH",
				zipCode = "44102"
			});

			personOne.Description();
			personTwo.Description();

			string desc1 = personOne.MyDescription;
			string desc2 = personTwo.MyDescription;

			WriteLine($"\ndesc1: {desc1}\ndesc2: {desc2}\n");

            Stock stock = new Stock();
            stock.CurrentPrice = 350;
            stock.SharesOwned = 10;

            decimal worth1 = stock.Worth;
            decimal worth2 = stock.Worth2;

            WriteLine($"\nworth1: {worth1}");
            WriteLine($"worth2: {worth2}\n");
        }
	
        static void TestDriveIndexers() {
			Sentence s = new Sentence();
			WriteLine(s[3]); // fox

			s[3] = "kangaroo";
			WriteLine(s[3]); // kangaroo

			string str = s[1, "Hello this is miguel"]; // this
			WriteLine(str);
		}

        static void TestDriveStaticConstructors() {
            Sentence s1 = new Sentence();
            Sentence s2 = new Sentence();
            Sentence s3 = new Sentence();
            Sentence s4 = new Sentence();
        }

        static void TestDriveInheritance() {
            Stock2 msft = new Stock2 {
                Name = "MSFT",
                SharesOwned = 1000
            };

            WriteLine(msft.Name);
            WriteLine(msft.SharesOwned);

            House mansion = new House { Name = "Mansion", Mortgage = 250000 };
			WriteLine(mansion.Name);
            WriteLine(mansion.Mortgage);

            // Casting
            WriteLine("\nUpcasting:\n");
            //
            // Upcasting: an upcast operation creates a base class reference from 
            // a subclass reference. An upcast always succeeds.
            Asset a = msft;
            // After the upcast, variable "a" still references the same Stock object as variable "msft"
            // The object being referenced is not itself altered or converted.
            WriteLine(a == msft); // True
            // Although "a" and "msft" refer to the identical object, "a" has a more restrictive
            // view on that object
            //
            WriteLine(a.Name);  // OK
            //WriteLine(a.SharesOwned);  // Error: SharesOwned undefined
            //
            // The second statement generates a compile-time error because the variable "a"
            // is of type Asset, even though it refers to an object of the type Stock2.
            // To get to its "SharesOwned" field, you must downcast the Asset to a Stock.

            // Downcasting
            WriteLine("\nDowncasting:\n");
            // A downcast operation creates a subclass reference from base class reference.
            // For example:
            //
            Stock2 s = (Stock2)a;
			WriteLine(s.SharesOwned);   // No error
			WriteLine(s == a);          // True
            WriteLine(s == msft);       // True

            // NOTE: downcasting may fail, throwing an InvalidCastException.
            House h = new House();
            Asset aa = h;           // upcast always succeeds
            //Stock2 ss = (Stock2)aa; // downcast fails: "aa" is not a Stock2. throws a InvalidCastException

			// NOTE: you can us the "as" operator to perform a downcast that when 
			// fails evaluates to null, rather than throwing an exception.
            Asset aaa = new Asset();
            Stock2 sss = aaa as Stock2; // sss is null; no exception is thrown

            // The "is" operator tests whether a reference conversion would succeed.
            // It is often used to test before downcasting:

            if (aaa is Stock2)
                WriteLine("SharesOwned: " + ((Stock2)aaa).SharesOwned);
            
            // Virtual Function Members
            WriteLine("\nVirtual Function Members:\n");
            WriteLine($"msft\'s Liability is {msft.Liability}");
            WriteLine($"mansion\'s Liability is {mansion.Liability}");
		}
    
        static void TestDriveNewVSOverride() {
            // The differences in behavior between Overrider and Hider
            // are demostrated in the following code:

            Overrider over = new Overrider();
            BaseClass b1 = over;
            over.Foo(); // Overrider.Foo
			b1.Foo();   // Overrider.Foo

            Hider h = new Hider();
            BaseClass b2 = h;
			h.Foo();    // Hider.Foo
			b2.Foo();   // BaseClass.Foo
		}
    
        static void TestDriveObjectType() {
            // Because Stack works with the object Type, we can Push and Pop instances of any type
            // to and from the Stack.
            Stack stack = new Stack();
            stack.Push("sausage");
            string s = (string)stack.Pop(); // downcast, so explicit cast is needed
            WriteLine(s);   // sausage

            // NOTE: when you cast between a value type and object, the CLR must perform
            // some special work to bridge the difference in semantics between value and 
            // reference types.
            // This process is called "boxing" and "unboxing"
		}

        static void TestDriveBoxingAndUnboxing() {
            // Boxing is the act of converting a value-type instance to a reference-type instance.
            // The reference type may be either the "object" class or an interface.
            //
            // When you cast between a value type and object, the CLR must perform
            // some special work to bridge the difference in semantics between value and 
            // reference types.
            // This process is called "boxing" and "unboxing"
            //

            // in this example, we box an int into an object
            int x = 9;
            object obj = x;     // Box the int

            // unboxing reverses the operation by casting the object back to the original value type
            int y = (int)obj;   // Unbox the int

            // Unboxing requires an explicit cast. The runtime checks that the stated value
            // type matches the actual object type and throws an InvalidCastException if the
            // check fails.
            //
            // Boxing copies the value-type instance into the new object, and unboxing copies
            // the contents of the object back into a value-type instance.
            //
            // In the following example, changing the value if i doesn't change its previously
            // boxed copy:
            int i = 3;
            object boxed = i;
            i = 5;
            WriteLine(boxed);   // 3

		}
    
        static void TestDriveEnums() {
            BorderSide topSide = BorderSide.Top;
            bool isTop = (topSide == BorderSide.Top);
            WriteLine($"isTop: {isTop}");
        }
    }
}



