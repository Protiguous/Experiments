// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
// 
// This source code contained in "UnitTest1.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "TestTheLite", "UnitTest1.cs" was last formatted by Protiguous on 2019/09/26 at 9:06 PM.

namespace TestTheLite {

    using System;
    using System.Data.SQLite;
    using System.Diagnostics;
    using JetBrains.Annotations;
    using Xunit;

    public class UnitTest1 {

        public class LiteDatabase {

            private SQLiteConnectionStringBuilder ConnectionStringBuilder;

            public LiteDatabase( [CanBeNull] String connectionString = null ) {
                connectionString = connectionString.Trim();
                this.ConnectionStringBuilder = new SQLiteConnectionStringBuilder( connectionString );
            }

        }

        private static SQLiteConnection CreateConnection() {

            try {
                var sqlite_conn = new SQLiteConnection( "Data Source=database.db;" );
                sqlite_conn.Open();

                return sqlite_conn;
            }
            catch ( Exception exception ) {
                Debug.WriteLine( exception.ToString() );
            }

            return default;
        }

        private static void CreateTable( SQLiteConnection conn ) {

            using var sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "CREATE TABLE if not exists SampleTable( Col1 VARCHAR( 20 ), Col2 INT );";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE if not exists SampleTable1( Col1 VARCHAR( 20 ), col2 INT );";
            sqlite_cmd.ExecuteNonQuery();
        }

        private static void InsertData( SQLiteConnection conn ) {
            using var sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "INSERT INTO SampleTable( Col1, Col2 ) VALUES( 'Test Text ', 1 ); ";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO SampleTable( Col1, Col2 ) VALUES( 'Test1 Text1 ', 2 ); ";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO SampleTable( Col1, Col2 ) VALUES( 'Test2 Text2 ', 3 ); ";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO SampleTable1( Col1, Col2 ) VALUES( 'Test3 Text3 ', 3 ); ";
            sqlite_cmd.ExecuteNonQuery();
        }

        private static void ReadData( SQLiteConnection conn ) {
            using var sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable;";

            var sqlite_datareader = sqlite_cmd.ExecuteReader();

            while ( sqlite_datareader.Read() ) {
                var myreader = sqlite_datareader.GetString( 0 );
                Console.WriteLine( myreader );
            }

            conn.Close();
        }

        [Fact]
        public void Test1() {
            using ( var sqlite_conn = CreateConnection() ) {
                CreateTable( sqlite_conn );
                InsertData( sqlite_conn );
                ReadData( sqlite_conn );
            }
        }

        
        public class TemperatureSensor {

            private Boolean _isInitialized;

            public void Initialize() {
                // Initialize hardware interface
                this._isInitialized = true;
            }

            public Int32 ReadCurrentTemperature() {
                if ( !this._isInitialized ) {
                    throw new InvalidOperationException( "Cannot read temperature before initializing." );
                }

                // Read hardware temp
                return 42; // Simulate for demo code purposes
            }
        }

        [Fact]
        public void ReadTemperature() {
            var sut = new TemperatureSensor();

            sut.Initialize();

            var temperature = sut.ReadCurrentTemperature();

            Assert.StrictEqual( 42, temperature );
        }

        [Fact]
        public void ErrorIfReadingBeforeInitialized() {
            var sut = new TemperatureSensor();

            Assert.Throws<InvalidOperationException>( () => sut.ReadCurrentTemperature() );
        }

        [Fact]
        public void ErrorIfReadingBeforeInitialized_CaptureExDemo() {
            var sut = new TemperatureSensor();

            var ex = Assert.Throws<InvalidOperationException>( () => sut.ReadCurrentTemperature() );

            Assert.Equal( "Cannot read temperature before initializing.", ex.Message );
            // or:
            //Assert.True( ex.Message, Is.EqualTo( "Cannot read temperature before initializing." ) );
        }

        

    }

}