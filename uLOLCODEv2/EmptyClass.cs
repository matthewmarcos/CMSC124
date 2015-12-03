using System;
using Gtk;
using uLOLCODEv2;
using System.Collections;
using System.Text.RegularExpressions;
namespace uLOLCODEv2
{
	public class EmptyClass
	{

		Gtk.ListStore lexModel = new Gtk.ListStore(typeof(string),typeof(string));
		Gtk.ListStore symbolTree = new Gtk.ListStore(typeof(string),typeof(string));
		Hashtable symbolTable = new Hashtable ();
		Identifier ident = new Identifier();
		EvalClass eval = new EvalClass ();
		public EmptyClass ()
		{
		}
		public String printHello() {
			return "I like to hintai!";
		}
		public void accessTextView(TextView nigga,String stuff){

			nigga.Buffer.Text += stuff;

		}


		public void parseLines (String line) {
			Match m;
			while (line.Length != 0) {
				String matchedString;

				m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s*");
				if (m.Success) {
					//I HAS A 
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "vardec");
					continue;
				}


				m = Regex.Match (line, @"^ITZ\s+");
				if (m.Success) {
					//I HAS A 
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "Varible Assignment");

					continue;
				}



				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*");
				if (m.Success) {
					//I HAS A 
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "varident");
					continue;
				}

				m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
				if (m.Success) {
					//I HAS A 
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "Numbar Literal");
					continue;
				}

				m = Regex.Match (line, @"^\-?\d+\s*");
				if (m.Success) {
					//I HAS A 
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "Numbr Literal");
					continue;
				}

			}
		}
	}
}

/*

 */