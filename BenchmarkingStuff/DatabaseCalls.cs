// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "DatabaseCalls.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "BenchmarkingStuff", "DatabaseCalls.cs" was last formatted by Protiguous on 2019/07/14 at 1:28 PM.

namespace BenchmarkingStuff {

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Mathematics;
    using JetBrains.Annotations;
    using Librainian.Database;
    using Librainian.Internet;

    [RankColumn( NumeralSystem.Arabic )]
    [EvaluateOverhead]
    [ClrJob( baseline: true )]
    public class DatabaseCalls {

        [CanBeNull]
        public String ConnectionString { get; set; }

        public IEnumerable<SqlConnectionStringBuilder> ConnectionStrings { get; set; }

        [Benchmark]
        public async Task<Int64> Output() {
            using ( var db = new Database( this.ConnectionString ) ) {

                var pam = new SqlParameter( "@when", SqlDbType.DateTime ) {
                    Direction = ParameterDirection.Output
                };

                await db.ExecuteNonQueryAsync( "[Tools].[dbo].[GetDateAndTimeOut]", CommandType.StoredProcedure, pam ).ConfigureAwait( false );
                var result = ( DateTime ) pam.Value;

                return result.Ticks;
            }
        }

        [Benchmark]
        public async Task<Int64> ScalarAsync() {
            using ( var db = new Database( this.ConnectionString ) ) {

                var result = await db.ExecuteScalarAsync<DateTime>( "[Tools].[dbo].[GetDateAndTime]", CommandType.StoredProcedure ).ConfigureAwait( false );

                return result.Ticks;
            }
        }

        [Benchmark]
        public Int64 Scalar() {
            using ( var db = new Database( this.ConnectionString ) ) {

                var result = db.ExecuteScalar<DateTime>( "[Tools].[dbo].[GetDateAndTime]", CommandType.StoredProcedure );

                return result.Ticks;
            }
        }

        [GlobalSetup]
        public void Setup() {

            this.ConnectionStrings = new Credentials( "guest", "guest" ).LookForAnyDatabases( TimeSpan.FromSeconds( 20 ) );

            this.ConnectionString = this.ConnectionStrings.FirstOrDefault()?.ConnectionString;
        }

    }

}