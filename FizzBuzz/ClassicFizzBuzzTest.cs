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
//     paypal@AIBrain.Org
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
// Project: "FizzBuzz", "ClassicFizzBuzzTest.cs" was last formatted by Protiguous on 2019/02/03 at 2:39 AM.

namespace FizzBuzz {

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using NUnit.Framework;

    public class ClassicFizzBuzzTest : IFizzBuzzTest {

        [NotNull]
        public ConcurrentDictionary<Int32, FooBar> Calculations { get; set; }

        public Int32 EndingNumber { get; }

        public List<String> MyAnswers { get; } = new List<String>();

        public Int32 NumbersToCount { get; }

        public Int32 StartingNumber { get; }

        public async Task TakeTest() {
            try {
                await DoCalculations().ConfigureAwait( false );

                Assert.AreEqual( expected: this.StartingNumber, actual: this.Calculations.Min( pair => pair.Key ), message: $"Min != {nameof( this.StartingNumber )}" );
                Assert.AreEqual( expected: this.EndingNumber, actual: this.Calculations.Max( pair => pair.Key ), message: $"Max != {nameof( this.EndingNumber )}" );
                Assert.AreEqual( expected: this.NumbersToCount, actual: this.Calculations.Count, message: "Numbers to count mismatch" );

                await CreateAnswers().ConfigureAwait( false );
            }
            catch ( Exception exception ) {
                exception.Report();
            }

            Task DoCalculations() {
                return Task.Run( () => {
                    try {
                        this.Calculations.Clear();

                        Parallel.For( this.StartingNumber, this.EndingNumber + 1, FizzBuzzConstants.DontStarve, i => this.Calculations.TryAdd( i, new FooBar {
                            Div3 = i.IsDiv3(), Div5 = i.IsDiv5()
                        } ) );
                    }
                    catch ( Exception exception ) {
                        exception.Report();
                    }
                } );
            }

            Task CreateAnswers() {
                return Task.Run( () => {
                    lock ( this.MyAnswers ) {
                        this.MyAnswers.Clear();

                        foreach ( var number in this.Calculations ) {
                            if ( number.Value.Div3 && number.Value.Div5 ) {
                                this.MyAnswers.Add( FizzBuzzConstants.FizzBuzz );
                            }
                            else if ( number.Value.Div3 ) {
                                this.MyAnswers.Add( FizzBuzzConstants.Fizz );
                            }
                            else if ( number.Value.Div5 ) {
                                this.MyAnswers.Add( FizzBuzzConstants.Buzz );
                            }
                            else {
                                this.MyAnswers.Add( number.Key.ToString() );
                            }
                        }
                    }
                } );
            }
        }

        public ClassicFizzBuzzTest( Int32 startingNumber, Int32 endingNumber ) {
            this.StartingNumber = Math.Min( startingNumber, endingNumber );
            this.EndingNumber = Math.Max( startingNumber, endingNumber );
            this.Calculations = new ConcurrentDictionary<Int32, FooBar>();
            this.NumbersToCount = this.EndingNumber + 1 - this.StartingNumber; //because Parallel.For is max-exclusive
        }
    }
}