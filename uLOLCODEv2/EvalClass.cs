using System;
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
					} else {
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
							": variable " + expression[0] + " is already used!\n";
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
					} else {
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
							": variable " + expression[0] + " is already used!\n";
					}
				}
			}

			consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "!\n";

		}

		public void evalVisible(String line, Hashtable symbolTable, TextView consoleText, int lineNumber) {
		
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

