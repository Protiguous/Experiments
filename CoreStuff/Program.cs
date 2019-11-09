// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "Program.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "CoreStuff", "Program.cs" was last formatted by Protiguous on 2019/11/07 at 2:07 PM.

namespace CoreStuff {

    using System;
    using JetBrains.Annotations;

    internal class Program {

        private static void Main( String[] args ) {

            //Console.WriteLine(Person.Empty == null);
            //Console.WriteLine(Person.Empty == null);
            //Console.WriteLine(Person.Empty == null);

            /*
            var source = "span like types are awesome!";

            var toMem = source.AsMemory( 3 );   // source.ToMemory() converts source from string to ReadOnlyMemory<char>,

            var asMem = MemoryMarshal.AsMemory( toMem );

            asMem.Capitialize();  // MemoryMarshal.AsMemory converts ReadOnlyMemory<char> to Memory<char> so you can modify the elements.

            // You get "Span like types are awesome!";
            Console.WriteLine( source );
            */
        }

    }

    public static class ParsingExtensions {

        public static void Capitialize( this Memory<Char> memory ) {
            if ( memory.IsEmpty ) {
                return;
            }

            ref var first = ref memory.Span[ 0 ];

            if ( first >= 'a' && first <= 'z' ) {
                first = Char.ToUpper( first );
            }
        }

    }

    public interface IPerson {
        String Name { get; }
        Int32 Age { get; }
    }

    public class Person : IPerson {

        public String Name { get; }

        public Int32 Age { get; }

        public Person( String name, Int32 age ) {
            this.Name = name ?? throw new ArgumentNullException( nameof( name ) );
            this.Age = age;
        }

        private Person( [NotNull] IPerson other ) : this(other.Name, other.Age) { }
        
        public Person( [CanBeNull] Person other ) : this( PassThroughNonNull( other ) ) { }

        [NotNull]
        private static IPerson PassThroughNonNull( Person person ) => person ?? throw new ArgumentNullException( nameof( person ) );
    }


}