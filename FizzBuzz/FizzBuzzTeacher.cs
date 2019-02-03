// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "FizzBuzzAnswers.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "FizzBuzz", "FizzBuzzAnswers.cs" was last formatted by Protiguous on 2019/02/02 at 8:16 PM.
namespace FizzBuzz {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using NUnit.Framework;

    public class FizzBuzzTeacher : IFizzBuzzGrader {

        [NotNull]
        public IList<String> RealAnswers { get; } = new List<String>();

        [NotNull]
        public async Task Populate() {
            try {
                var filename = Path.Combine( Path.GetTempPath(), nameof( FizzBuzzTeacher ) + ".txt" );

                if ( !File.Exists( filename ) ) {
                    var webclient = new WebClient();

                    await webclient.DownloadFileTaskAsync( @"https://raw.githubusercontent.com/Keith-S-Thompson/fizzbuzz-polyglot/master/expected-output.txt", filename )
                        .ConfigureAwait( false );
                }

                var content = await File.ReadAllLinesAsync( filename ).ConfigureAwait( false );

                this.RealAnswers.Clear();
                ( this.RealAnswers as List<String> )?.AddRange( content );
            }
            catch ( Exception exception) {
                exception.Report();
                await Task.FromException( exception ).ConfigureAwait( false );
            }
        }

        public void Grade( [NotNull] IFizzBuzzTest test ) {
            if ( test == null ) {
                throw new ArgumentNullException( paramName: nameof( test ) );
            }

            try {
                Assert.Greater( test.MyAnswers.Count, 0 );
                Assert.Greater( this.RealAnswers.Count, 0 );

                //Assert.AreEqual( this.Numbers.Count(pair => pair.Value.Div3), /*unknown*/ );
                //Assert.AreEqual( this.Numbers.Count(pair => pair.Value.Div5), /*unknown*/ );

                Assert.AreEqual( this.RealAnswers.Count, test.MyAnswers.Count );

                var areSame = this.RealAnswers.SequenceEqual( test.MyAnswers, StringComparer.OrdinalIgnoreCase );

                var result = $"The {test.NumbersToCount} answers appear to be {(areSame ? "correct" : "wrong")}.";

                Console.WriteLine( result );

                if ( !areSame ) {
                    for ( var i = test.StartingNumber; i < test.EndingNumber; i++ ) {
                        var me = test.MyAnswers[ i ];
                        var them = this.RealAnswers[ i ];

                        if ( me != them ) {
                            Console.WriteLine( $"[{i}] Me={me} Them={them}." );
                            Debugger.Break();
                        }
                    }
                    Assert.Fail( result );
                }
            }
            finally{
                Assert.Pass( "Test Done." );
            }
        }

    }

}