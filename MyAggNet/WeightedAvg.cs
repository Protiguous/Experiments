// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
//
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
//
// This source code contained in "WeightedAvg.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "MyAggNet", "WeightedAvg.cs" was last formatted by Protiguous on 2019/08/09 at 5:48 PM.

namespace MyAggNet {

    using System;
    using System.Data.SqlTypes;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using JetBrains.Annotations;
    using Microsoft.SqlServer.Server;

    public class Functions {

        [Serializable]
        [SqlUserDefinedAggregate( Format.Native, IsInvariantToDuplicates = false, IsInvariantToNulls = true, IsInvariantToOrder = true, IsNullIfEmpty = true,
            Name = "WeightedAvg" )]
        public struct WeightedAvg {

            /// <summary>
            ///     The variable that holds the intermediate sum of all weights
            /// </summary>
            private Int32 count;

            /// <summary>
            ///     The variable that holds the intermediate sum of all values multiplied by their weight
            /// </summary>
            private Int64 sum;

            /// <summary>
            ///     Accumulate the next value, not if the value is null
            /// </summary>
            /// <param name="Value">Next value to be aggregated</param>
            /// <param name="Weight">The weight of the value passed to Value parameter</param>
            public void Accumulate( SqlInt32 Value, SqlInt32 Weight ) {
                if ( !Value.IsNull && !Weight.IsNull ) {
                    this.sum += ( Int64 )Value * ( Int64 )Weight;
                    this.count += ( Int32 )Weight;
                }
            }

            /// <summary>
            ///     Initialize the internal data structures
            /// </summary>
            public void Init() {
                this.sum = 0;
                this.count = 0;
            }

            /// <summary>
            ///     Merge the partially computed aggregate with this aggregate
            /// </summary>
            /// <param name="Group">The other partial results to be merged</param>
            public void Merge( WeightedAvg Group ) {
                this.sum += Group.sum;
                this.count += Group.count;
            }

            /// <summary>
            ///     Called at the end of aggregation, to return the results of the aggregation.
            /// </summary>
            /// <returns>The weighted average of all inputed values</returns>
            public SqlInt32 Terminate() => this.count > 0 ? new SqlInt32( ( Int32 )( this.sum / this.count ) ) : SqlInt32.Null;
        }

        [Serializable]
        [SqlUserDefinedAggregate( Format.UserDefined, //use clr serialization to serialize the intermediate result
                IsInvariantToNulls = true, //optimizer property
                IsInvariantToDuplicates = false, //optimizer property
                IsInvariantToOrder = false //optimizer property
                , Name = nameof( Concatenate )
                , MaxByteSize = 8000 ) //maximum size in bytes of persisted value
        ]
        public class Concatenate : IBinarySerialize {

            /// <summary>
            ///     The variable that holds the intermediate result of the concatenation
            /// </summary>
            [NotNull]
            public StringBuilder intermediateResult { get; } = new StringBuilder();

            /// <summary>
            ///     Accumulate the next value, not if the value is null
            /// </summary>
            /// <param name="value"></param>
            public void Accumulate( SqlString value ) {
                if ( value.IsNull ) {
                    return;
                }

                if ( this.intermediateResult.Length == 0 ) {
                    this.intermediateResult.Append( value.Value );
                }
                else {
                    this.intermediateResult.Append( ',' ).Append( value.Value );
                }
            }

            /// <summary>
            ///     Initialize the internal data structures
            /// </summary>
            [MethodImpl( MethodImplOptions.AggressiveInlining )]
            public void Init() => this.intermediateResult.Clear();

            /// <summary>
            ///     Merge the partially computed aggregate with this aggregate.
            /// </summary>
            /// <param name="other"></param>
            public void Merge( Concatenate other ) => this.intermediateResult.Append( other.intermediateResult );

            public void Read( [CanBeNull] BinaryReader reader ) {
                this.Init();

                if ( reader == null ) {
                    return;
                }

                this.intermediateResult.Append( reader.ReadString() );
            }

            /// <summary>
            ///     Called at the end of aggregation, to return the results of the aggregation.
            /// </summary>
            /// <returns></returns>
            public SqlString Terminate() {
                var output = String.Empty;

                //delete the trailing comma, if any
                if ( this.intermediateResult.Length > 0 ) {
                    output = this.intermediateResult.ToString();
                }

                return new SqlString( output );
            }

            public void Write( [CanBeNull] BinaryWriter writer ) {
                writer?.Write( this.intermediateResult.ToString() );
            }
        }
    }
}