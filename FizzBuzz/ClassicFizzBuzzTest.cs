// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
//
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
//
// This source code contained in "ClassicFizzBuzzTest.cs" belongs to Protiguous@Protiguous.com and
// Rick@AIBrain.org unless otherwise specified or the original license has
// been overwritten by formatting.
// (We try to avoid it from happening, but it does accidentally happen.)
//
// Any unmodified portions of source code gleaned from other projects still retain their original
// license and our thanks goes to those Authors. If you find your code in this source code, please
// let us know so we can properly attribute you and include the proper license and/or copyright.
//
// If you want to use any of our code, you must contact Protiguous@Protiguous.com or
// Sales@AIBrain.org for permission and a quote.
//
// Donations are accepted (for now) via
//     bitcoin:1Mad8TxTqxKnMiHuZxArFvX8BuFEB9nqX2
//     PayPal:Protiguous@Protiguous.com
//     (We're still looking into other solutions! Any ideas?)
//
// =========================================================
// Disclaimer:  Usage of the source code or binaries is AS-IS.
//    No warranties are expressed, implied, or given.
//    We are NOT responsible for Anything You Do With Our Code.
//    We are NOT responsible for Anything You Do With Our Executables.
//    We are NOT responsible for Anything You Do With Your Computer.
// =========================================================
//
// Contact us by email if you have any questions, helpful criticism, or if you would like to use our code in your project(s).
// For business inquiries, please contact me at Protiguous@Protiguous.com
//
// Our website can be found at "https://Protiguous.com/"
// Our software can be found at "https://Protiguous.Software/"
// Our GitHub address is "https://github.com/Protiguous".
// Feel free to browse any source code we *might* make available.
//
// Project: "FizzBuzz", "ClassicFizzBuzzTest.cs" was last formatted by Protiguous on 2019/07/13 at 6:36 PM.

namespace FizzBuzz {

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;
    using NUnit.Framework;

    /// <summary>
    /// </summary>
    /// <rule>Write a program that prints the numbers from 1 to 100.</rule>
    /// <rule>But for multiples of three print “Fizz” instead of the number, and for the multiples of five print “Buzz”.</rule>
    /// <rule>For numbers which are multiples of both three and five print “FizzBuzz”.</rule>
    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead( true )]
    [ClrJob( baseline: true )]
    public class ClassicFizzBuzzTest : IFizzBuzzTest {

        [NotNull]
        public ConcurrentDictionary<Int32, FooBar> MyCalculations { get; set; }

        public Int32 EndingNumber { get; }

        public IReadOnlyList<String> MyAnswers {
            get {
                if ( this._myAnswers == null ) {

                    //this._myAnswers = this.Calculations.OrderBy( pair => pair.Key ).Select( pair => pair.Value.Answer ).ToList();
                    return this._myAnswers = this.MyCalculations.Values.Select( pair => pair.Answer ).ToList();
                }

                return this._myAnswers;
            }
        }

        public Int32 NumbersToCount { get; }

        public Int32 StartingNumber { get; }

        [Benchmark]
        public void TakeTest() {

            try {
                var stopwatch = Stopwatch.StartNew();

                this.MyCalculations.Clear();

                Parallel.For( this.StartingNumber, this.EndingNumber + 1, FizzBuzzConstants.DontStarve, i => {
                    var foo = new FooBar {
                        Div3 = i.IsDiv3(), Div5 = i.IsDiv5()
                    };

                    if ( foo.Div3 && foo.Div5 ) {
                        foo.Answer = FizzBuzzConstants.FizzBuzz;
                    }
                    else if ( foo.Div3 ) {
                        foo.Answer = FizzBuzzConstants.Fizz;
                    }
                    else if ( foo.Div5 ) {
                        foo.Answer = FizzBuzzConstants.Buzz;
                    }
                    else {
                        foo.Answer = i.ToString();
                    }

                    this.MyCalculations[ i ] = foo;
                } );

                stopwatch.Stop();
                Console.WriteLine( $"{nameof( this.TakeTest )} took {stopwatch.Elapsed.TotalMilliseconds} ms." );
            }
            catch ( Exception exception ) {
                exception.Report();
            }

            Assert.AreEqual( expected: this.StartingNumber, actual: this.MyCalculations.Min( pair => pair.Key ), message: $"Min != {nameof( this.StartingNumber )}" );
            Assert.AreEqual( expected: this.EndingNumber, actual: this.MyCalculations.Max( pair => pair.Key ), message: $"Max != {nameof( this.EndingNumber )}" );
            Assert.AreEqual( expected: this.NumbersToCount, actual: this.MyCalculations.Count, message: "Numbers to count mismatch" );
        }

        private IReadOnlyList<String> _myAnswers;

        public ClassicFizzBuzzTest( Int32 startingNumber, Int32 endingNumber ) {
            this.StartingNumber = Math.Min( startingNumber, endingNumber );
            this.EndingNumber = Math.Max( startingNumber, endingNumber );
            this.MyCalculations = new ConcurrentDictionary<Int32, FooBar>();
            this.NumbersToCount = this.EndingNumber + 1 - this.StartingNumber; //because Parallel.For is max-exclusive
        }
    }
}