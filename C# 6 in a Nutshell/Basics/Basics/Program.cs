using System;

// This is an static directive
using static System.Console;

// Aliasing Types and Namespaces
using MyStringBuilder = System.Text.StringBuilder;
      
namespace Basics
{
    class Program
    {
        static void Main(string[] args) {
            //StringBasics();
            //ClassesBasics();
            //ValueTypeBasics();
            //ReferenceTypeBasics();
            //RealNumberRoundingBasics();
            //ArrayBasics();
            //VariablesAndParametersBasics();
            //PassingParamsByReferenceBasics();
            //ParamsModifierBasics();
            //OptionlParametersBasics();
            //TestNullCoalescingOperator();
            //TestNullConditionalOperator();
            TestIterationStatements();
        }

        static void StringBasics() {
            string message = "Hello world";
            string upperMessage = message.ToUpper();
            WriteLine(upperMessage);

            int x = 2015;
            message = message + x.ToString();
            WriteLine(message);

            // verbatin string literals
            string verbatin1 = @"\\server\fileshare\helloworld.cs";
            string verbatin2 = @"First 
            Second Line";
            WriteLine(verbatin1);
            WriteLine(verbatin2);

            // string interpolation: a string preceded by a $ character
            // a c# expression of any type can appear inside braces, as in { <expression> }
            int num = 4;
            string str = $"A square has { num } sides";
            WriteLine(str);

            // interpolated strings must complete on a single line, unless you 
            // also specify the verbatim string operator
            int n = 2;
            string s = $@"this spans { n
            } lines";
            WriteLine(s);
        }

        static void ClassesBasics() {
            UnitConverter feetToInchesConverter = new UnitConverter(unitRatio: 12);
            UnitConverter milesToInchesConverter = new UnitConverter(unitRatio: 5280);

            WriteLine(feetToInchesConverter.Convert(30));
            WriteLine(feetToInchesConverter.Convert(100));

            int inches = milesToInchesConverter.Convert(1);
            int feet = feetToInchesConverter.Convert(inches);
            WriteLine(feet);
        }

        static void ValueTypeBasics() {
            Point p1 = new Point();
            p1.x = 7;

            Point p2 = p1;

            WriteLine("\np1.x: " + p1.x);
            WriteLine("p2.x: " + p2.x);

            p1.x = 9;

            WriteLine("p1.x: " + p1.x);
            WriteLine("p2.x: " + p2.x);
        }

        static void ReferenceTypeBasics() {
            PointClass p1 = new PointClass();
            p1.x = 7;

            PointClass p2 = p1;

            WriteLine("\np1.x: " + p1.x);
            WriteLine("p2.x: " + p2.x);

            p1.x = 9;

            WriteLine("p1.x: " + p1.x);
            WriteLine("p2.x: " + p2.x);
        }

        static void RealNumberRoundingBasics() {
            // float and double internally represent numbers in base 2
            // only numbers that can be expresses in base 2 can be expressed
            // precisely.

            float tenth = 0.1f;  // not quite 0.1
            float one = 1f;     // not quite 0.1
            WriteLine("\none - tenth * 10f = " + (one - tenth * 10f)); // -1.49...

            // This is why float and double are bad for financial calculations.
            // In contrast "decimal" works in base 10, so it can precisely represent 
            // numbers such as 0.1

            // this leads to accumulated rounding errors
            decimal m = 1M / 6M;
            double d = 1.0 / 6.0;

            WriteLine(m);
            WriteLine(d);
        }

        static void ArrayBasics() {
            // declare an array of 5 characters
            char[] vowels = new char[5];
            vowels[0] = 'a';
            vowels[1] = 'e';
            vowels[2] = 'i';
            vowels[3] = 'o';
            vowels[4] = 'u';

            // short-hand
            char[] vowels2 = new char[] { 'a', 'e', 'e', 'o', 'u' };

            // or
            char[] vowels3 = { 'a', 'e', 'e', 'o', 'u' };

            // accesing elements
            WriteLine(vowels[1]);

            for (int i = 0; i < vowels.Length; i++) {
                Write(vowels[i]);
            }

            // default memory initialization: creating an array always preinitializes the elements with 
            // default values. The default value for a type is the result of a bitwise zeroing of memory.
            int[] a = new int[1000];
            Write("\n");
            WriteLine(a[123]); // 0

            // Rectangular Arrays

            WriteLine("\nRectangular Arrays: \n");

            // Rectangular arrays are declared using commas to separate each dimension. 
            //
            // The following declares a rectangular two-dimentional array, where the 
            // dimensions are 3x3:

            int[,] matrix = new int[3, 3];

            // initializing
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = i * 3 + j;
            WriteLine("matrix: " + matrix[1, 1]);

            // another way to initialize a rectangular array
            int[,] matrix2 = new int[,] {
                {0,1,2},
                {3,4,5},
                {6,7,8}
            };
            WriteLine("matrix2: " + matrix2[1, 0]);

            // Bounds Checking

            WriteLine("\nBounds Checking: \n");

            // All array indexing is bounds-checked by the runtime.
            // An IndexOutOfRangeException is thrown if you use an
            // invalid index:

            int[] arr = new int[3];
            arr[3] = 1; // this throws an IndexOutOfRangeException
        }

        static void VariablesAndParametersBasics() {
            // Default Values
            WriteLine("\nDefault Values: \n");

            // you can obtain the default value for any type with the "default" keyword
            decimal d = default(decimal);
            bool s = default(bool);

            WriteLine("decimal default value: " + d);
            WriteLine("bool default value: " + s);

            // The params modifier

            WriteLine("\nThe params modifier: \n");
        }

        static void PassingParamsByReferenceBasics() {
            // Foo takes a "p" passed by value, thus it gets a copy that gets modified 
            // inside the function, but the original "p" remains intact

            WriteLine("The \"ref\" modifier");
            int x = 8;

            Foo(x);
            WriteLine("Foo(x) = " + x);

			Bar(ref x);
			WriteLine("Bar(x) = " + x);


            WriteLine("\nThe \"out\" modifier");
            string a, b;

            Split("Stevie Ray Vaughan", out a, out b);
            WriteLine("a: " + a); // Stevie Ray
            WriteLine("b: " + b); // Vaughan
		}

		static void ParamsModifierBasics() {
            int total = sum(1, 2, 3, 4);
            WriteLine("Total1: " + total);

            // You can also supply a params argument as an array
            int total2 = sum(new int[] { 1, 2, 3, 4 });
            WriteLine("Total2: " + total2);
        }

        static void OptionlParametersBasics() {
            FooOpt();
            FooOpt(1);
            FooOpt(3,6);
        }

		#region Implementation Details

		#region  The "ref" modifier
		// To pass by reference, C# provides the "ref" parameter modifier.
		// In the following example, p and x refer to the same memory locations.

		// this func gets "p" passed by value
		static void Foo(int p) {
			p = p + 1;
		}

		// this func gets "p" passed by reference
		static void Bar(ref int p) {
			p = p + 1;
		}
		#endregion

		#region  The "out" modifier
		// The "out" modifier is like a "ref" argument, except it:
		//      * Need no assigned before going into the function
		//      * Must be assigned before it comes out of the function
		// The "out" modifier is most commonly used to get multiple return values
		// back from a method.

		static void Split(string name, out string firstNames, out string lastName) {
			int i = name.LastIndexOf(' ');
			firstNames = name.Substring(0, i);
			lastName = name.Substring(i + 1);
		}
		#endregion

		#region The params modifier
		// The params modifier may be specified on the last parameter of a 
		// method so that the method accepts any number of arguments of a
		// particular type. The parameter type must be declared as an array.
        static int sum(params int[] ints) {
            int sum = 0;
            for (int i = 0; i < ints.Length; i++)
                sum += ints[i];
            return sum;
        }
		#endregion

		#region Optional parameters
        // Methods, constructors, and indexers can declare optional parameters.
        // A parameter is optional if it specifies a default value in its declaration
        //
        // Warning: Adding an optional parameter to a public method that's called from
        // another assembly requires recompilation of both assemblies - just as though the 
        // parameter was mandatory.
        //
        // The default value of an optional parameter must be specified by a constant expression,
        // or a parameterless constructor of a value type.
        // Optional parameters cannot be marked with "ref" or "out".
        //
        // Mandatory parameters must occur before optional parameters in both the method
        // declaration and the method call. The exception is with "params" arguments,
        // which still always come last.
        //
        static void FooOpt(int x = 0, int y = 0) {
            WriteLine(x + ", " + y);
        }
		#endregion

		#region Null Operators
		
		// Null-Coalescing Operator
        //
		// The ?? operator is the null-coalescing operator.
		// It says "If the operand is non-null, give it to me; otherwise,
		// give me a default value. For example:
		static void TestNullCoalescingOperator() {
            string s1 = null;
            string s2 = s1 ?? "nothing";    // s2 evaluates to "nothing"
            WriteLine("s2 = " + s2);

            // If the left-hand expression is non-null, the right-hand expression
            // is never evaluated.

            string s3 = "hello world";
            string s4 = s3 ?? "nothing";    // s4 evaluates to "hello world
            WriteLine("s4 = " + s4);
        }

        // Null-Conditional Operator
        //
        // The ?. operator is the null-conditional or "Elvis" operator, and is new
        // to C# 6. It allows you to call methods and access members just like the
        // standard dot operator, except that if the operand on the left is null,
        // the expression evaluates to null instead of throwing a NullReferenceException
        static void TestNullConditionalOperator() {
			// Note: "MyStringBuilder" is a Type alias for "System.Text.StringBuilder"
			MyStringBuilder sb = null;

            string s1 = sb?.ToString(); // no error; s instead evaluates to null
            string s2 = (sb == null ? null : sb.ToString()); // equivalent code
			
            WriteLine("s1 = " + s1);
            WriteLine("s2 = " + s2);
        }

		#endregion

		#region Iteration Statements

        static void TestIterationStatements() {
			// while loops repeatedly execute a body of code while a bool
			// expression is true
			int i = 0;
            while (i < 3) {
                WriteLine("while i: " + i);
                i++;
            }

			// do-while loops differ in functionality from while loops only in that
			// they test the expression after the statement block has executed, ensuring
			// that the block is always executed at least once.
			WriteLine("\n");

            int j = 0;
            do {
                WriteLine("do-while j: " + j);
                j++;
            } while (j < 3);

            // for loops are like while loops with special clauses for initialization
            // and iteration of a loop variable.
            WriteLine("\n");

            for (int l = 0; l < 3; l++)
                WriteLine("for l: " + l);
            
            // the foreach statement iterates over each element in an enumerable object.
            // Most of the types in C# and the .NET Framework that represent a set or list
            // of elements are enumerable.
            WriteLine("\n");

            foreach (char c in "beer") // c is the iteration variable
                WriteLine("foreach c: " + c);
        }
		#endregion
		
        #endregion
	}
}

#region Helpers
public class UnitConverter {
	int ratio;
	public UnitConverter(int unitRatio) { ratio = unitRatio; }
	public int Convert(int unit) { return unit * ratio; }
}
public class PointClass { public int x, y; }
public struct Point { public int x,y; }
#endregion
