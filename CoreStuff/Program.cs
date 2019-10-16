
namespace CoreStuff {

    using System;

    class Program {

        static void Main( String[] args ) {

            Console.WriteLine(Person.Empty == null); 
            Console.WriteLine(Person.Empty == null); 
            Console.WriteLine(Person.Empty == null); 

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

    public class Person {

        private static EmptyPerson _emptyPerson;

        public static Person Empty => _emptyPerson ??= new EmptyPerson();

        public Person() {
            Console.WriteLine( $"ctor {nameof(Person)}." );
        }
    }

    public sealed class EmptyPerson : Person {

        public EmptyPerson() {
            Console.WriteLine( $"ctor {nameof(EmptyPerson)}." );
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

}
