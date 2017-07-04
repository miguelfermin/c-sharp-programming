using System;

using static System.Console;

namespace Types {

	#region Main

	public class Address {
		public string street;
		public string city;
		public string state;
		public string zipCode;

		public Address() { } // for Object Initializer syntax

		public Address(string street, string city, string state, string zipCode) {
			this.street = street;
			this.city = city;
			this.state = state;
			this.zipCode = zipCode;
		}

		public string Description() {
			return $"{street}, {city}, {state} {zipCode}";
		}
	}

	public class Stock {
		// MARK: - Properties

		// the private "backing" fields
		decimal currentPrice;

		// the public properties
		public decimal CurrentPrice {
			get { return currentPrice; }
			set { currentPrice = value; }
		}

		// automatic properties
		public decimal SharesOwned { get; set; }

		// calculated properties
		public decimal Worth {
			get { return currentPrice * SharesOwned; }
		}

		// expression-bodies property. The arrow syntax replaces all the braces, get, and return keyword.
		public decimal Worth2 => currentPrice * SharesOwned;
	}

	public class Person {
		// Fields
		Guid guid;
		string firstName;
		string lastName;
		Address address;

		// Properties

		public string MyDescription {
			get {
				return $"ID: {guid}\nName: {firstName} {lastName}.\nAddress: {address.Description()}\n";
			}
		}

		// Constructor
		public Person(string firstName, string lastName, Address address) {
			guid = Guid.NewGuid();
			this.firstName = firstName;
			this.lastName = lastName;
			this.address = address;
		}

		// Method
		public void Description() {
			WriteLine($"ID: {guid}\nName: {firstName} {lastName}.\nAddress: {address.Description()}\n");
		}
	}

	#endregion

	#region Inheritance


	public class Asset {
		public string Name;

		// A function marked as "virtual" can be overridden by subclasses wanting to provide a
		// specialized implememtation. Methods, properties, indexers, and events can all be 
		// declared "virtual"

		public virtual decimal Liability => 0; // Expression-bodied property, same as { get { return 0; } }
	}

	public class Stock2 : Asset {
		public long SharesOwned;
	}

	public class House : Asset {
		public decimal Mortgage;

		// By default, the Liability of an asset is 0. The House specializes the Liability
		// property to return the value of the Mortgage.
		public override decimal Liability => Mortgage;
	}
	#endregion

	#region new VS. override

	public class BaseClass {
		public virtual void Foo() { WriteLine("BaseClass.Foo"); }
	}

	public class Overrider : BaseClass {
		public override void Foo() { WriteLine("Overrider.Foo"); }
	}


	public class Hider : BaseClass {
		public new void Foo() { WriteLine("Hider.Foo"); }
	}

	#endregion

	#region The object Type
	// "object is the ultimate base class for all types. Any type can be upcast to object.
	//
	// To illustrate how this is useful, consider a general-purpose stack.
	// A stack is a data structure based on the prociples if LIFO -"last in, first out".
	// A stack has two operations:
	//  push an object on the stack, and
	//  pop an object from the stack.
	//
	// Because Stack works with the object Type, we can Push and Pop instances of any type
	// to and from the Stack.
	//
	public class ObjectStack {
		int position;
		object[] data = new object[10];
		public void Push(object obj) { data[position++] = obj; }
		public object Pop() { return data[--position]; }
	}

	#endregion

	#region Enums
	// An enum is a special value type that lets you specify a griup of named numeric constants
	public enum BorderSide { Left, Right, Top, Bottom }
    // Each enum member has an underlying integral value. By default:
    //  - Underlying values are of type int
    //  - The constants 0,1,2... are automatically assigned in the declaration order of enum members
    #endregion

    #region Generics

    public class Stack<T> {
        int position;
        T[] data;

        public int Count => data.Length;
        public T[] Items => data;

        public Stack(int capacity = 10) {
            data = new T[capacity];
        }

		public void Push(T obj) {
			data[position++] = obj;
		}

		public T Pop() {
			return data[--position];
		}

		// Helper
		public void PrintData() {
			foreach (T d in data) { WriteLine($"data: {d}"); }
			WriteLine("\n");
		}
	}

	public class Generics {
		public static void Swap<T>(ref T a, ref T b) {
			T temp = a;
			a = b;
			b = temp;
		}

        // Generic constraints
        public static T Combine<T> (T s1, T s2)  where T : Stack<int> {
            var count = s1.Count + s2.Count;
            var result = new Stack<int>(count);

            foreach (int val in s1.Items) { result.Push(val); }
            foreach (int val in s2.Items) { result.Push(val); }

            return (T)result;
        }
    }

	#endregion
}

