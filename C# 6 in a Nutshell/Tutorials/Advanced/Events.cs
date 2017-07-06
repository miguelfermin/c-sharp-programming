using System;
using static System.Console;

namespace Advanced
{
    // .NET Framework has an standard pattern for writing events, with the goal
	// of consistency across the Framework and third party code. At the core of the 
	// standard event pattern is System.EventArgs class, predefined in the Framework.
	// It doesn't have any members, other than an empty static property.
	// EventArgs is a base class for conveying info from an event.
	
	public static class Events
    {
		public static void TestDriveStandardEventPattern() {
			Stock stock = new Stock("THPW");
			stock.Price = 27.10M;
			stock.PriceChanged += StockPriceChanged; // Register with PriceChange event
			stock.Price = 31.59M;
		}

		#region Helpers
		
        static void StockPriceChanged(object sender, PriceChangedEventArgs e) {
			if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
				WriteLine("Alert, 10% stock price increase!");
		}

		#endregion

		#region Standard Event Pattern

		// A EventArgs subclass
		public class PriceChangedEventArgs : EventArgs
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
}
