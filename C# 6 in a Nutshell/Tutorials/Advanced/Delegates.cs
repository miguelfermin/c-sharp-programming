using System;
using static System.Console;
using static System.Threading.Thread;
//
// A delegate is an object that knows how to call a method.
// A delegate type defines the kind of method that delegate instances can call.
// Specifically, it defines the method's return type and its parameter types.
//

// use to test delegate signatures
public delegate int Transformer(int x);

namespace Advanced
{
    public class Delegates
    {
        // Square is compatible with any method that matches its signature
        //
        // short-hand syntax for:
        //            static int Square(int x) { get { return x * x; } }, or
        //            static int Square(int x) { return x * x; }
        //
        public static int Square(int x) => x * x;

        #region Test Dive Helpers

        public static void TestDriveDelegates() {
            // This is valid since Square has the same signature as Transformer
            // Assigning a method to a delegate variab;e creates a delegate instance
            Transformer t = Delegates.Square;

            // It can be invoked the same way as a method
            int x = t(3);
            WriteLine($"answer: {x}\n");

            // NOTE:
            // A delegate instance literally acts as a delegate for the caller: 
            // the caller invokes the delegate, and then the delegate calls the target method.
            // This indirection decouples the caller from the target method.
            //
            // A delegate is similar to a callback, a general term that captures constructs such
            // as C function pointers.
        }

        public static void TestDrivePluginMethods() {
            // Here we're passing the array to transform and the transform method delegate

            int[] values = { 1, 2, 3 };

            Util.Transform(values, Square);

            foreach (int i in values)
                Write(i + " ");     // 1 4 9
        }

        public static void TestDriveGenericDelegate() {
            int[] values = { 1, 2, 3 };

            Utils.Transform(values, Square);

            foreach (int i in values)
                Write(i + " ");     // 1 4 9
        }

        public static void TestDriveMulticastDelegate() {
            // To monitor progress (see example in #region) create a multicasr delegate
            // instance p, such that progress is monitored by two independent methods.
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
            Utils.HardWork(p);
        }

        public static void TestDriveStandardEventPattern() {
            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;
            stock.PriceChanged += StockPriceChanged; // Register with PriceChange event
			stock.Price = 31.59M;
        }

        #endregion


        #region Helpers

        static void WriteProgressToConsole(int percentComplete) => WriteLine(percentComplete);

        static void WriteProgressToFile(int percentComplete) {
            System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
        }

		static void StockPriceChanged(object sender, PriceChangedEventArgs e) {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                WriteLine("Alert, 10% stock price increase!");
		}

        #endregion
    }

    #region Plugin Methods
    // A delegate variable is assigned a method at runtime. This is usefull for writing
    // plug-in methods.
    // In this example, we have a utility method named Transform that applies a trasnform
    // to each element in an int array. The Transform has a delegate parameter, for specifying
    // a plug-in transform.

    public static class Util
    {
        public static void Transform(int[] values, Transformer t) {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
    }

    #endregion

    #region Multicast Methods
    #region NOTES
    // All delegate instances have multicast capabilities.
    // This means that a delegate instance can not just reference a single target method,
    // but a list of target methods. 
    //
    // The + and += operatos combine delegate instances. For example:
    //
    //  SomeDelegate d = SomeMethod1;
    //  d += SomeMethod2;
    //
    // Invoking "d" will now call both SomeMethod1 and SomeMethod2. Delegates are invoked
    // in the order they are added.
    //
    // The - and -= operators remove the right operand delegate from the left delegate operand
    // For example:
    //
    //  d -= SomeMethod1;
    //
    // Invoking d now only cause SomeMethod2 to be invoked.
    //
    // Calling + or += on a delegate variable with null works, and it is equivalent
    // to assigning the variable to a new value:
    //
    //  SomeDelegate d = null;
    //  d += SomeMethod1;   // Equivalent to (when d is null) d = SomeMethod1;
    //
    // Similarly, calling -= on a delegate variable with a single target is equivalent to
    // assigning null to that variable.
    //
    //  SomeDelegate d = SomeMethod1;
    //  d -= SomeMethod1; // Assings null to that variable
    //
    // NOTE: delegates are immutable, so when you call += or -=, you're in fact creating
    //  a new delegate instance and assigning it to the existing variable.
    //
    // If a multicast delegate has a nonvoid return type, the caller receives the return
    // value from the last method to be invoked.
    // The preceding methods are still called, but their return values are discarded. 
    // In most scenarios in which multicast delegates are used, they have void return types,
    // so this subtlety does not arise.
    #endregion

    public delegate void ProgressReporter(int percentComplete);

    public class Utils
    {
        // Suppose you wrote a method that took a long time to execute. 
        // That method could regularly report progress to its caller by invoking a delegate. 
        // In this example, the HardWork method has a ProgressReporter delegate parameter, which
        // it invokes to indicate progress:
        public static void HardWork(ProgressReporter p) {
            for (int i = 0; i < 10; i++) {
                p(i * 10);  // invoke delegate
                Sleep(100); // simulate hard work
            }
        }

        // Generic delegates
        public delegate T Transformer<T>(T arg);

        public static void Transform<T>(T[] values, Transformer<T> t) {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
    }

	#endregion

	#region Standard Event Pattern

	// .NET Framework has an standard pattern for writing events, with the goal
	// of consistency across the Framework and third party code. At the core of the 
	// standard event pattern is System.EventArgs class, predefined in the Framework.
	// It doesn't have any members, other than an empty static property.
	// EventArgs is a base class for conveying info from an event.
	//

	// A EventArgs subclass
	public class PriceChangedEventArgs: EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;

        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice) {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
	}

	// Once we have an EventArgs subclass, the next step is to define or choose a 
	// delegate for the event. There are three rules for a EventArgs's subclass 
	// delegate:
	//
	//  1. It must have a void return type
	//  2. It must accept 2 arguments, first of type object and second a subclass of EventArgs.
	//      2.1 The first argument indicates the event broadcaster
	//      2.2 The second argument contains the information to convey
	//  3. Its name must end with EventHandler
	//
	// For convenience the .NET Framework defines a generic delegate that conforms to these rules.
	// It's called System.EventHandler<>, defined as follows:
	//
	// public delegate void EventHanler<TEventArgs>(object source, TEventArgs e) where TEventArgs : EventArgs;
	//
	// NOTE: see "TestDriveStandardEventPattern()" above to see this in action
	//
	// NOTE: The predefined nongeneric EventHandler delegate can be used when an event doesn’t carry extra information.
    //
	public class Stock
    {
        string symbol;
        decimal price;

        public Stock(string symbol) {
            this.symbol = symbol;
        }

		// Define an event of the chosen delegate type. Using the generic EventHandler delegate:
		public event EventHandler<PriceChangedEventArgs> PriceChanged;

		// The Standard Event Pattern requires a protected virtual method that fires the event.
        // The name has to match the name of the event, prefixed with the word "On".
        // It has to accept a single EventArgs argument
        protected virtual void OnPriceChanged(PriceChangedEventArgs e) {
            // This is a thread-safe and succint way to envoke the event.
            PriceChanged?.Invoke(this, e);
        }

        // Property that tracks changes and broadcasts event
		public decimal Price {
			get { return price; }
			set {
				if (price == value) return;
				decimal oldPrice = price;
				price = value;

				var priceChangeEvent = new PriceChangedEventArgs(oldPrice, price);
				OnPriceChanged(priceChangeEvent);
			}
		}
	}
	
	#endregion
}
