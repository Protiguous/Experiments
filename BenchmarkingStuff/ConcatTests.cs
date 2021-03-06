﻿// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "ConcatTests.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "BenchmarkingStuff", "ConcatTests.cs" was last formatted by Protiguous on 2019/07/21 at 5:39 AM.
// 
// #pragma warning disable RCS1138 // Add summary to documentation comment.
namespace BenchmarkingStuff {

    using System;
    using System.Text;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;
    using Librainian.Maths;

    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead]
    [ClrJob( baseline: true )]
    public class ConcatTests {

        private String data1;
        private String data2;
        private String data3;

        [Params( 256, 1048576, 536870912 )]
        public Int32 N;

        private const String spacer = ", ";

        [ Benchmark]
        [NotNull]
        public String Concat() => this.data1 + spacer + this.data2 + spacer + this.data3;

        [ Benchmark]
        [NotNull]
        public String StringBuilder() {
            var sb = new StringBuilder();
            sb.Append(this.data1);
            sb.Append(spacer);
            sb.Append(this.data2);
            sb.Append(spacer);
            sb.Append(this.data3);

            return sb.ToString();
        }

        [Benchmark]
        [NotNull]
        public String Interpolate() => $"{this.data1}{spacer}{this.data2}{spacer}{this.data3}";

        [Benchmark]
        [NotNull]
        // ReSharper disable once UseStringInterpolation
        public String Format() => String.Format( "{0}{1}{2}{3}{4}", this.data1,spacer,this.data2, spacer, this.data3);

        [GlobalSetup]
        public void Setup() {
            this.data1 = Randem.NextString( this.N,  lowers: true, uppers: true );
            this.data2 = Randem.NextString( this.N,  lowers: true, uppers: true );
            this.data3 = Randem.NextString( this.N,  lowers: true, uppers: true );
        }

    }

}