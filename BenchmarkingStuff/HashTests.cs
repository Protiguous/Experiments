// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "HashTests.cs" belongs to Protiguous@Protiguous.com and
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
//     (We're always looking into other solutions.. Any ideas?)
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
// Feel free to browse any source code we make available.
// 
// Project: "BenchmarkingStuff", "HashTests.cs" was last formatted by Protiguous on 2019/10/03 at 9:16 PM.

namespace BenchmarkingStuff {

    using System;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;
    using Librainian.Extensions;
    using Librainian.OperatingSystem.FileSystem;
    using Xunit;
    using Xunit.Abstractions;

    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead]
    [SimpleJob( baseline: true )]
    public class HashTests  {

        [NotNull]
        private readonly ITestOutputHelper _testOutputHelper;

        [Params( 1 )]
        public Int32 N;

        public HashTests( [NotNull] ITestOutputHelper testOutputHelper ) {
            this._testOutputHelper = testOutputHelper ?? throw new ArgumentNullException( nameof( testOutputHelper ) );
        }

        [Fact]
        [Benchmark]
        public async Task HarkerHashInt64Async() {

            using ( var document = new Document( @"N:\Test\RSI.rar" ) ) {
                var hash = await document.CalculateHarkerHashInt64Async().ConfigureAwait(false);
                this._testOutputHelper.WriteLine( hash.ToString() );
            }
        }

        [Benchmark]
        public void ByNewbie( ) {
            var data = Types.Newby<FASTERConstructors.MyClass>();
            data.Name = this.N.ToString();

            if ( data.Name != this.N.ToString() ) {
                throw new NullReferenceException( $"{nameof( Types.Newby )} failed." );

                //return false;
            }
        }


        /*
        [GlobalSetup]
        public void Setup() { }

        public class MyClass {

            public String Name { get; set; }

        }
        */

    }

}