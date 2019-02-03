// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "FizzBuzzTests.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "FizzBuzz", "FizzBuzzTests.cs" was last formatted by Protiguous on 2019/02/02 at 10:28 PM.

namespace FizzBuzz {

    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using NUnit.Framework;

    /// <summary>
    ///     <para>StackOverflow asked me if I've ever written the FizzBuzz challenge program.</para>
    ///     <para>I have not.. until now!</para>
    ///     <para>My goal is to be performant, but not overly complicate.</para>
    /// </summary>
    /// <rule>Write a program that prints the numbers from 1 to 100.</rule>
    /// <rule>But for multiples of three print “Fizz” instead of the number, and for the multiples of five print “Buzz”.</rule>
    /// <rule>For numbers which are multiples of both three and five print “FizzBuzz”.</rule>
    public class FizzBuzzTests {

        private IFizzBuzzGrader Teacher { get; set; }

        [OneTimeTearDown]
        public void Done() { }

        [OneTimeSetUp]
        public Task Setup() {
            this.Teacher = new FizzBuzzTeacher();

            return this.Teacher.Populate();
        }

        [Test]
        public async Task TestClassicFizzBuzz() {
            var testTimer = Stopwatch.StartNew();

            try {
                var student = new ClassicFizzBuzzTest( 1, 100 );
                await student.TakeTest().ConfigureAwait( false );
                this.Teacher.Grade( student );
            }
            finally {
                testTimer.Stop();
                Console.WriteLine( $"{nameof( ClassicFizzBuzzTest )} took {testTimer.Elapsed.TotalMilliseconds:N} ms." );
            }
        }

    }

}