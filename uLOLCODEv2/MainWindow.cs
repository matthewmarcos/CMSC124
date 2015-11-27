/*
 * Created by: Mon, Matt, and James
 * 
 */
using System;
using Gtk;
using uLOLCODEv2;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{		
	public static Gtk.ListStore lexModel = new Gtk.ListStore(typeof(string),typeof(string));
	public static Gtk.ListStore symbolTree = new Gtk.ListStore(typeof(string),typeof(string));
	public static Hashtable symbolTable = new Hashtable ();
	Identifier ident = new Identifier();
	EvalClass eval = new EvalClass ();

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		populateTreeView ();

	}

	public void populateTreeView(){
		//instantiate 2 columns

		Gtk.TreeViewColumn lexemeCol = new Gtk.TreeViewColumn ();
		lexemeCol.Title = "Lexeme";

		Gtk.TreeViewColumn classificationCol = new Gtk.TreeViewColumn ();
		classificationCol.Title = "Classification";

		//add the 2 columns to the treeview
		treeview1.AppendColumn (lexemeCol);
		treeview1.AppendColumn (classificationCol);

		treeview1.Model = lexModel;
		Gtk.CellRendererText lexemeCell = new Gtk.CellRendererText ();
		lexemeCol.PackStart (lexemeCell, true);
		Gtk.CellRendererText classCell = new Gtk.CellRendererText ();
		classificationCol.PackStart (classCell, true);

		lexemeCol.AddAttribute (lexemeCell, "text", 0);
		classificationCol.AddAttribute (classCell, "text", 1);


		//===================================================//

		Gtk.TreeViewColumn identifierCol = new Gtk.TreeViewColumn ();
		identifierCol.Title = "Identifier";

		Gtk.TreeViewColumn valueCol = new Gtk.TreeViewColumn ();
		valueCol.Title = "Value";

		//add the 2 columns to the treeview
		treeview2.AppendColumn (identifierCol);
		treeview2.AppendColumn (valueCol);

		treeview2.Model = symbolTree;
		Gtk.CellRendererText identifierCell = new Gtk.CellRendererText ();
		identifierCol.PackStart (identifierCell, true);
		Gtk.CellRendererText valueCell = new Gtk.CellRendererText ();
		valueCol.PackStart (valueCell, true);

		identifierCol.AddAttribute (identifierCell, "text", 0);
		valueCol.AddAttribute (valueCell, "text", 1);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void executeCode (object sender, EventArgs e)
	{
		consoleText.Buffer.Text = "";
		String code = inputCode.Buffer.Text;
		Boolean isComment = false;
	//	String[,] codeLabels;
		lexModel.Clear ();
		symbolTree.Clear ();
		symbolTable.Clear ();

		char[] splitToken = {'\n'};
		String[] lines = code.Split (splitToken);

		if(!eval.hasValidStartAndEnd(lines)) {
			consoleText.Buffer.Text += "Syntax Error at program delimiter\n";
		} 

		for(var i = 0 ; i < lines.Length ; i++) {
			parseLines(lines[i], ref isComment, i);
		}

		//parseLines(code);
	}

	public void parseLines (String line,ref Boolean isComment, int lineNumber) {
		lineNumber += 1;
		Match m;
		String matchedString;

		while (line.Length != 0) {
			//consoleText.Buffer.Text += ("Value of Flag: " + isComment + "\n");
			updateSymbolTable ();
			if(isComment) {
				m = Regex.Match (line, @"^TLDR$");
				if (m.Success) {
					isComment = false;
					lexModel.AppendValues (m.Value, "End of Multi-Comment");
					break;
				}
				lexModel.AppendValues (line, "Multi Line Comment");
				break;
			}

			m = Regex.Match (line, @"^HAI\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Start of Program");
				continue;
			} 

			m = Regex.Match (line, @"^KTHXBYE$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "End of Program");
				continue;
			}

			m = Regex.Match (line, @"^\s*BTW");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Start of Single Comment");
				lexModel.AppendValues (line, "Single Line Comment");
				line = line.Remove (0, line.Length);
				continue;
			}

			m = Regex.Match (line, @"^OBTW$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Start of Multi-Comment");
				isComment = true;
				continue;
			}

			m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Var Declaration");

				eval.varDecEval (line, symbolTable, consoleText, lineNumber);
				continue;
			}

			m = Regex.Match (line, @"^\s*ITZ\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Var Initializer");
				continue;
			}

			m = Regex.Match (line, @"^\s*R\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Var Assignment");
				continue;
			}

			m = Regex.Match (line, @"^\s*SUM\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Addition Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*DIFF\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Subtraction Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*PRODUKT\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Multiplication Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*QUOSHUNT\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Division Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*MOD\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Modulo Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*BIGGR\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Max Value Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*SMALLR\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Min Value Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*BOTH\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "AND Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*EITHER\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "OR Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*WON\s+OF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "XOR Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*NOT\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Negation Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*AN\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Linking Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*BOTH\s+SAEM\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Equality Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*DIFFRINT\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Inequality Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*SMOOSH\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "String Concat Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*MAEK\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Casting Op");
				continue;
			}

			m = Regex.Match (line, @"^A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Linking Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*IS\s+NOW\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Casting Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*VISIBLE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Print Op");

				eval.evalVisible (line, symbolTable, consoleText, lineNumber);
				continue;
			}

			m = Regex.Match (line, @"^\s*GIMMEH\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Scan Op");
				continue;
			}

			m = Regex.Match (line, @"^\s*O\s+RLY\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Start of If-Then");
				continue;
			}

			m = Regex.Match (line, @"^\s*YA\s+RLY\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Equivalent to IF");
				continue;
			}

			m = Regex.Match (line, @"^\s*MEBBE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Equivalent to ELSEIF");
				continue;
			}

			m = Regex.Match (line, @"^\s*NO\s+WAI\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Equivalent to ELSE");
				continue;
			}

			m = Regex.Match (line, @"^\s*OIC\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "End of Selection Statement");
				continue;
			}

			m = Regex.Match (line, @"^\s*WTF\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Start of Switch");
				continue;
			}

			m = Regex.Match (line, @"^\s*OMG\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Case");
				continue;
			}

			m = Regex.Match (line, @"^\s*GTFO\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Break");
				continue;
			}

			m = Regex.Match (line, @"^\s*OMGWTF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Default Case");
				continue;
			}

			m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;

				//Check if matchedString matches a reserved word
				if (eval.isValidVarident (matchedString)) {
					matchedString = matchedString.Trim ();
					lexModel.AppendValues (matchedString, "Var Identifier");
				} else {
					consoleText.Buffer.Text += "Error: Invalid variable identifier " +
						matchedString + " at line " + lineNumber + "\n";
				}

				continue;
			}

			m = Regex.Match (line, @"^\s*""");
			if (m.Success) {
				String stringLiteral = "";
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				lexModel.AppendValues (matchedString, "Start string Delimiter");

				for(; line.Length > 0 && line[0] != '"';) {
					stringLiteral += line[0];
					line = line.Remove(0, 1);
					//consoleText.Buffer.Text += "stringLiteral: " + stringLiteral + 
					//	" line: " + line + lineNumber
					//	" length: " + line.Length + "\n";
				}
				//lexModel.AppendValues (stringLiteral, "String Literal");
				//lexModel.AppendValues (line[0] + "", "End string Delimiter");
				//line = line.Remove(0, 1);

				if(line.Length != 0) {
					lexModel.AppendValues (stringLiteral, "String Literal");
					lexModel.AppendValues (line[0] + "", "End string Delimiter");
					line = line.Remove(0, 1);
				} else {
					//ERROR -> unpaired quotation mark
					consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "! (unpaired quotes)\n";
					break;
				}

				continue;
			}

			m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Numbar Literal");
				continue;
			}

			m = Regex.Match (line, @"^\-?\d+\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				lexModel.AppendValues (matchedString, "Numbr Literal");
				continue;
			}


			consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "!\n";
			break;

				
		} //End of main loop
	}

	/*
	 * 
	 * Implementation which returns the type of statement so we know how to eval
	void parseLines (String code){
		char[] splitToken = {'\n'};
		String[] lines = code.Split (splitToken);

		for(var i = 0 ; i < lines.Length ; i++) {
			//String[,] statements = ident.getLineType(lines[i], consoleText);
			List<List<String>> statements = ident.getLineType(lines[i], consoleText);
			//foreach(List<String> myList in )
			//consoleText.Buffer.Text +=  statements.Count;
			//for(var j = 0 ; j < statements.Count ; j++) {
				//if(statements[j, 1].Equals("varDecNoInit")) {
					//eval.varDecNoInit(consoleText, statements[j, 0]);
					//consoleText.Buffer.Text +=  statements[j, 0];
				//}
				//consoleText.Buffer.Text +=  statements.ElementAt(0);
			//	lexModel.AppendValues("WWW", "WWWE");
			//}
		}
	}*/

	protected void updateSymbolTable() {
		symbolTree.Clear ();
		foreach(DictionaryEntry pair in symbolTable) {
			symbolTree.AppendValues (pair.Key, pair.Value);
		}
	}


	protected void openCODE (object sender, EventArgs e)
	{
		FileChooserDialog fileOpen = new FileChooserDialog ("Open File",this,FileChooserAction.Open,
		                                                    "Cancel", ResponseType.Cancel,"Open", ResponseType.Accept);		//for open button 

		if( fileOpen.Run() == (int)ResponseType.Accept){
			//open file for reading

			System.IO.StreamReader file = System.IO.File.OpenText(fileOpen.Filename);	//open the file.

			//display in the textbox
			inputCode.Buffer.Text = file.ReadToEnd();

		}
		fileOpen.Destroy();
	}
}
