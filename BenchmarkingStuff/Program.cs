// Copyright © Rick@AIBrain.org and Protiguous. All Rights Reserved.
//
// This entire copyright notice and license must be retained and must be kept visible
// in any binaries, libraries, repositories, and source code (directly or derived) from
// our binaries, libraries, projects, or solutions.
//
// This source code contained in "ProgramFASTER.cs" belongs to Protiguous@Protiguous.com and
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
// Project: "BenchmarkingStuff", "ProgramFASTER.cs" was last formatted by Protiguous on 2019/07/13 at 6:35 PM.

namespace BenchmarkingStuff {

    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using BenchmarkDotNet.Running;

    public static class Program {

        public static void Main( String[] args ) {

            /*
            using ( var document = new Document( @"N:\Test\RSI.rar" ) ) {
                var start = StartNew();
                var task = document.CalculateHarkerHashDecimalAsync();
                task.Start();
                var hash = await document.CalculateHarkerHashDecimalAsync().ConfigureAwait( false );
                start.Stop(); //hehe
                Console.WriteLine( $"Took {start.Elapsed.Simpler()} to calculate {hash.ToString().DoubleQuote()}." );
            }
            */

            //var summary = BenchmarkRunner.Run<UriVsFile>();
            //var summary = BenchmarkRunner.Run<FASTERTests>();
            //var summary = BenchmarkRunner.Run<ConcatTests>();
            //var summary = BenchmarkRunner.Run<DatabaseCalls>();
            //var summary = BenchmarkRunner.Run<XorOrXor>();
            //var summary = BenchmarkRunner.Run<FASTERConstructors>();
            //var summary = BenchmarkRunner.Run<HashTests>();
            var summary = BenchmarkRunner.Run<CapitalizeTests>();

            foreach ( var report in summary.Reports ) {
                Console.WriteLine( report.ResultStatistics );
            }

            Console.WriteLine();
            Console.WriteLine( "Press enter to exit..." );
            Console.ReadLine();
        }


        public static void CreateMyForm() {
            // Create a new instance of the form.
            var form1 = new Form();
            // Create two buttons to use as the accept and cancel buttons.
            var button1 = new Button();
            var button2 = new Button();

            // Set the text of button1 to "OK".
            button1.Text = "OK";
            // Set the position of the button on the form.
            button1.Location = new Point( 10, 10 );
            // Set the text of button2 to "Cancel".
            button2.Text = "Cancel";
            // Set the position of the button based on the location of button1.
            button2.Location
                = new Point( button1.Left, button1.Height + button1.Top + 10 );
            // Set the caption bar text of the form.   
            form1.Text = "My Dialog Box";
            // Display a help button on the form.
            form1.HelpButton = true;

            // Define the border style of the form to a dialog box.
            form1.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the accept button of the form to button1.
            form1.AcceptButton = button1;
            // Set the cancel button of the form to button2.
            form1.CancelButton = button2;
            // Set the start position of the form to the center of the screen.
            form1.StartPosition = FormStartPosition.CenterScreen;

            // Add button1 to the form.
            form1.Controls.Add( button1 );
            // Add button2 to the form.
            form1.Controls.Add( button2 );

            // Display the form as a modal dialog box.
            form1.ShowDialog();
        }
    }


}