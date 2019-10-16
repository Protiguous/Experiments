// Copyright © Rick@AIBrain.org and Copyright © Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "XorOrXor.cs" belongs to Protiguous@Protiguous.com and/or
// Rick@AIBrain.org unless otherwise specified or the original license has been overwritten by
// formatting. (We try to avoid that from happening, but it does accidentally happen.)
// 
// Any unmodified portions of source code gleaned from other projects still retain their original
// license and our thanks goes to those Authors. If you find your code in this source code, please
// let us know so we can properly attribute you and include the proper license and/or copyright.
// 
// If you want to use any of our code, you must contact Protiguous@Protiguous.com or
// Sales@AIBrain.org for permission and a quote.
// 
// Donation information can be found at https://Protiguous.com/Donations
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
// Our website/blog can be found at "https://Protiguous.com/"
// Our software can be found at "https://Protiguous.Software/"
// Our GitHub address is "https://github.com/Protiguous".
// Feel free to browse!
// 
// Project: "BenchmarkingStuff", "XorOrXor.cs" was last formatted by Protiguous on 2019/09/22 at 9:20 AM.

namespace BenchmarkingStuff {

    using System;
    using System.Linq;
    using System.Text;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;

    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead]
    [ClrJob( baseline: true )]
    public class XorOrXor {

        [Benchmark]
        public void PlainXorTest() {
            const String sample = "Rick Harker";
            var encrypted = XorText( sample, 12345 );
            var decrypted = XorText( encrypted, 12345 );
        }

        [Benchmark]
        public void OptimizedXorTest() {
            const String sample = "Rick Harker";
            var encrypted = XorText( sample, 12345 );
            var decrypted = XorText( encrypted, 12345 );
        }

        [NotNull]
        public static String XorText( String text, Int32 key ) {
            var newText = "";

            foreach ( var t in text ) {
                var charValue = Convert.ToInt32( t ); //get the ASCII value of the character
                charValue ^= key; //xor the value

                newText += Char.ConvertFromUtf32( charValue ); //convert back to string
            }

            return newText;
        }

        [NotNull]
        public static String XorText_SB( String text, Int32 key ) {

            var length = text.Length;
            var sb = new StringBuilder( length );

            foreach ( var charValue in text.Select( Convert.ToInt32 ).Select( charValue => charValue ^ key ) ) {
                sb.Append( Char.ConvertFromUtf32( charValue ) );
            }

            return sb.ToString();
        }

    }

}