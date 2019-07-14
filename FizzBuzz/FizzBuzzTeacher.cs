// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
//
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
//
// This source code contained in "FizzBuzzTeacher.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "FizzBuzz", "FizzBuzzTeacher.cs" was last formatted by Protiguous on 2019/07/13 at 6:36 PM.

namespace FizzBuzz {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;
    using NUnit.Framework;

    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead( true )]
    [ClrJob( baseline: true )]
    public class FizzBuzzTeacher : IFizzBuzzGrader {

        [NotNull]
        public IList<String> RealAnswers { get; } = new List<String>();

        [Benchmark]
        public void Grade( [NotNull] ref ClassicFizzBuzzTest test ) {
            if ( test == null ) {
                throw new ArgumentNullException( paramName: nameof( test ) );
            }

            Assert.True( this.RealAnswers.Count >= 100, $"Missing {nameof( this.RealAnswers )}." );

            Console.Write( "Grading " );
            var stopwatch = Stopwatch.StartNew();
            var areSame = this.RealAnswers.SequenceEqual( test.MyAnswers, StringComparer.OrdinalIgnoreCase );
            stopwatch.Stop();
            Console.WriteLine( $"took {stopwatch.Elapsed.TotalMilliseconds} ms." );

            if ( !areSame ) {
                var result = $"The {test.NumbersToCount} answers appear to be wrong.";
                Console.WriteLine( result );
                var local = test.MyAnswers.ToList();

                for ( var i = test.StartingNumber; i < test.EndingNumber; i++ ) {
                    var me = local[ i ];
                    var them = this.RealAnswers[ i ];

                    if ( me != them ) {
                        Console.WriteLine( $"[{i}] Me={me} Them={them}." );
                        Debugger.Break();
                    }
                }

                Assert.Fail( result );
            }
        }

        public Boolean LoadExpectedOutputs( [NotNull] String filename ) {
            if ( String.IsNullOrWhiteSpace( value: filename ) ) {
                Assert.Fail( $"{nameof( filename )} cannot be null or whitespace." );

                return false;
            }

            try {
                filename = Path.GetFileName( filename );

                if ( String.IsNullOrWhiteSpace( value: filename ) ) {
                    Assert.Fail( $"{nameof( filename )} cannot be null or whitespace." );

                    return false;
                }

                var fileInfo = new FileInfo( fileName: filename );

                if ( !fileInfo.Exists || fileInfo.Length < 256 || File.ReadAllLines( fileInfo.FullName ).Length < 100 ) {
                    if ( !DownloadFromGithub() ) {
                        Assert.Fail( $"Unable to download the file {filename}." );
                        return false;
                    }

                    fileInfo.Refresh();
                }

                if ( !fileInfo.Exists || fileInfo.Length < 256 || File.ReadAllLines( fileInfo.FullName ).Length < 100 ) {
                    Assert.Fail( $"Unable to find or download the file {filename}." );

                    return false;
                }

                Console.Write( "Loading local file..." );
                this.RealAnswers.Clear();
                var strings = File.ReadAllLines( fileInfo.FullName );
                ( this.RealAnswers as List<String> )?.AddRange( strings );
                Console.WriteLine( "done." );

                return this.RealAnswers.Any();
            }
            catch ( Exception exception ) {
                exception.Report();
            }

            Boolean DownloadFromGithub() {
                try {
                    var url = new Uri( "https://raw.githubusercontent.com/Protiguous/Experiments/master/FizzBuzz/" + filename, UriKind.Absolute );

                    Console.Write( $"Downloading 'expected outputs' file from {url.Host}..." );
                    var webclient = new WebClient();
                    webclient.DownloadFile( url, filename );
                    Console.WriteLine( "done." );

                    return File.Exists( filename );
                }
                catch ( Exception exception ) {
                    exception.Report();

                    return false;
                }
            }

            return false;
        }
    }
}