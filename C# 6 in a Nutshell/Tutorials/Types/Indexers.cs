﻿using System;
using static System.Console;

namespace Types
{
    // Indexers provide a natural syntax for accessing elements in a class or struct
    // that encapsulate a list or dictionary of values.
    //
    // Indexers are similar to properties but are accessed via an index argument 
    // rather than a property name.
    //
    // To write an indexer, define a property called "this", specifying the arguments
    // in square brackets.

    public class Sentence
    {
		public Sentence() {
			WriteLine("Default constructor....\n");
		}

        string[] words = "The quick brown fox".Split();

        public string this[int wordNum] {
            get { return words[wordNum]; }
            set { words[wordNum] = value; }
        }

        // if yoyu omit the set accessor, an indexer becomes read-only, and expression
        // body syntax may be used in C# 6 shorten its definition:
        //
        // public string this[int wordNum] => words[wordNum];

        // a type may declare multiple indexers
        public string this[int arg1, string arg2] {
            get { return arg2.Split()[arg1]; }
        }

		// Static constructor
		//
		// A static constructor executes once per type, rather than once per instance.
        // A type can define only one static constructor, and it must be parameterless 
        // and have the same name as the type.
        //
        // WARNING: If a static constructor throws an unhandled exception, that type
        // becomes unusable for the life of the application.
		static Sentence() {
            WriteLine("Sentence static constructor....\n");
        }
    }
}
