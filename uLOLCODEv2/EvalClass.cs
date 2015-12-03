﻿using System;
using Gtk;
using uLOLCODEv2;
using System.Collections;
using System.Text.RegularExpressions;

namespace uLOLCODEv2
{
	public class EvalClass
	{

		public EvalClass ()
		{
		}

		public Boolean isValidVarident (String variable) {
			String[] patterns = {
				"HAI",
				"KTHXBYE",
				"BTW",
				"OBTW",
				"TLDR",
				"ITZ",
				"GIMMEH",
				"R",
				"NOT",
				"DIFFRINT",
				"SMOOSH",
				"MAEK",
				"A",
				"VISIBLE",
				"GIMMEH",
				"MEBBE",
				"OIC",
				"WTF?",
				"OMG",
				"OMGWTF",
				"UPPIN",
				"NERFIN",
				"YR",
				"TIL",
				"WILE",
			};
			foreach(var pattern in patterns) {
				if(pattern.Equals(variable)) {
					return false;
				}
			}
			return true;
		}

		public void varDecEval(String lines, Hashtable symbolTable, TextView consoleText, int lineNumber) {
			Match m;
			char[] splitToken = {' '};
			String[] expression = lines.Split (splitToken);
			if(expression.Length == 1) {
				//I HAS A VAR
				//consoleText.Buffer.Text += lines + "\n";
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success) {
					//Fix symbol table
					if(!symbolTable.ContainsKey(expression[0])) {
						symbolTable.Add(expression[0], "NOOB");
						return;
					} else {
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
							": variable " + expression[0] + " is already used!\n";
							return;
					}
				}
			}

			// ITZ ASSIGNMENT
			if(expression.Length >= 3) {
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success && expression[1].Equals("ITZ")) {
					//Fix symbol table
					if(!symbolTable.ContainsKey(expression[0])) {
						expression = Regex.Split(lines, @"\s+ITZ\s+");
						//Evaluate the expression[1] if expression is a string,
						// or complexExpression that is valid. Already evaluate it lang. plz.

						symbolTable.Add(expression[0], expression[1]);
						return;
					} else {
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
							": variable " + expression[0] + " is already used!\n";
						return;
					}
				}
			}

			consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "!\n";

		}

		public void evalVisible(String line, Hashtable symbolTable, TextView consoleText, int lineNumber) {
		/*valid variable?
		 * Check if string literal then print
		 * if not
		 * 
		 * check if nasa symbol table na
		 * if wala then error
		 * 
		 *  
		 * 
		 *CHECK MUNA KUNG STRING LITERAL. PRINT AGAD
		 *IF VARIABLE CHECK IF VALID THEN CHECK VALUE
		 *IF NUMBAR THEN PRINT AGAD
		 *IF EXPRESSION BANG.
		 * 
			 */
			Match m;
			char[] splitToken = {' '}; //<=== tentative
			String[] expression = line.Split (splitToken);


			if(expression.Length == 1) {
			
				//CHECK IF STRING LITERAL

				//======Start of string literal eval ============//

				// !!! ISSUE: String literal with spaces.   Ex.   "S hit"; <----------------------------------------------------------
				m = Regex.Match (expression[0], @"^\s*""");
				if (m.Success) {
					String sline = expression[0];		//<--- for clarity's sake
					String stringLiteral = "";
					sline = sline.Remove (0, m.Value.Length);
					String matchedString = m.Value;

					for(; sline.Length > 0 && sline[0] != '"';) {
						stringLiteral += sline[0];
						sline = sline.Remove(0, 1);

					}

					if(sline.Length != 0) {

						consoleText.Buffer.Text += (stringLiteral+"\n");
						sline = sline.Remove(0, 1);
					} else {

						//ERROR -> unpaired quotation mark
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "! : Unpaired quotes (Visible Func)\n";

					}
					return;

				}
				//=======End of String Literal Eval ============//

				//==================VARIABLE==========================//
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success) {
						//If wala sa symbol table
					if(!symbolTable.ContainsKey(expression[0])) {	//if wala pa sa symbol table

						consoleText.Buffer.Text += ("Syntax Error at line " + lineNumber + ": Variable undeclared(Visible Func)\n");
						return;
					} else {
							//IF NASA SYMBOL TABLE NA PRNT YUNG VALUE
						var stringLit = symbolTable[expression[0]];
						String printThis = stringLit.ToString();			//<------ Gawin String yung returned object;
						String printTlaga = "";
						printThis = printThis.Remove(0,1);
						//printThis.Remove(0,1);
						for(; printThis.Length > 0 && printThis[0] != '"';) {
							printTlaga += printThis[0];
							printThis = printThis.Remove(0, 1);

						}


						consoleText.Buffer.Text += (printTlaga+ "\n");

						return;
					}
				}
				//==================END OF VAR EVAL=====================//
			
				//==================Number Literal =====================//
				m = Regex.Match (expression[0], @"^\-?\d*\.\d+\s*");
				if (m.Success) {

					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}

				m = Regex.Match (expression[0], @"^\-?\d+\s*");
				if (m.Success) {
				
					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}


				//===============End of Number Literal==============//
			
			}


			//================//

		}

		public void evalGimmeh(String line, Hashtable symbolTable,TextView consoleText,int lineNumber,Gtk.ListStore symbolTree){

			//check variable "line"

			Match m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
			if(isValidVarident(line) && m.Success) {
				//If wala sa symbol table
				if(!symbolTable.ContainsKey(line)) {	//if wala pa sa symbol table

					consoleText.Buffer.Text += ("Syntax Error at line " + lineNumber + ": Variable undeclared(Gimmeh Func)\n");
					return;
				} else {
					//IF NASA SYMBOL TABLE NA PRNT YUNG VALUE
					//Get the value
					onGIMMEH(consoleText,line,symbolTable,symbolTree);
					//updateSymbolTable (symbolTable,symbolTree);
				
				}
			}
		
		}
		public Boolean hasValidStartAndEnd (String[] lines) {
			/*
			 *	Check if code starts and ends with HAI KTHXBYE 
			 */
			if (lines [0].Equals ("HAI") && lines [lines.Length-1].Equals ("KTHXBYE")) {
				return true;
			} else {
				return false;
			}
		}

	
		//Create input box
		public void onGIMMEH(TextView consoleText, String variable,Hashtable symbolTable, Gtk.ListStore symbolTree){

			//Create a new window to contain the Entry box and the button to send it
			Window input = new Window ("Input");
			input.Resize (350, 100);

			//VBox as a container to allow the two widgets in 1 place
			VBox boxx = new VBox ();

			//Entry widget to hold the text
			Entry inputBox = new Entry ();
			inputBox.Name = "inputBox";
			inputBox.SetSizeRequest (100, 30);

			//Button to handle the input
			Button send = new Button ();
			send.Name = "send";
			send.Label = "Input";

			//Add an event handler for a button when clicked.
			send.Clicked += (sender, e) => sendCode(sender,e,consoleText,inputBox,input,variable,symbolTable,symbolTree);

			//add the Vbox first
			input.Add (boxx);
			//Add Entry and Button to contain 
			boxx.Add (inputBox);
			boxx.Add (send);

			//show all the widgets
			input.ShowAll ();
		}
		public void sendCode(object sender, EventArgs e, TextView consoleText, Entry inputBox, Window input, String variable,Hashtable symbolTable,Gtk.ListStore symbolTree){
			/*The value input will be given to the var
			 * 
			 * 
			 * */
			symbolTable[variable] = inputBox.Text;
			consoleText.Buffer.Text += inputBox.Text+"\n";
			updateSymbolTable (symbolTable,symbolTree);
			input.Destroy ();
		}
		protected void updateSymbolTable(Hashtable symbolTable, Gtk.ListStore symbolTree) {
			symbolTree.Clear ();
			foreach(DictionaryEntry pair in symbolTable) {
				symbolTree.AppendValues (pair.Key, pair.Value);
			}
		}
		/*
		public EvalClass(Hashtable symbolTablez, Gtk.ListStore symbolTreez, Gtk.ListStore lexmodelz) {
			this.lexModel = lexmodelz;
			this.symbolTree = symbolTreez;
			this.symbolTable = symbolTablez;
			//this.consoleText = consoleText;

		}
		public void varDecNoInit(TextView consoleText, String statement) {
			//Add lexemes to lexmodel
			//Add to symbol table
			//check if variable already exists in symbolTable
			symbolTable.Add(statement, "Var dec no init");
		}
		*/

	}
}
