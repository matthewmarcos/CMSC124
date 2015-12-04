using System;
using Gtk;
using uLOLCODEv2;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace uLOLCODEv2
{
	public class ParserClass
	{
		public ParserClass ()
		{
		}

		public void parseLines (String line,ref Boolean isComment, int lineNumber, ref Hashtable symbolTable, ref Gtk.ListStore symbolTree, TextView consoleText) {
			lineNumber += 1;
			Match m;
			String matchedString;

			while (line.Length != 0) {
				//consoleText.Buffer.Text += ("Value of Flag: " + isComment + "\n");
				//updateSymbolTable ();
				if(isComment) {
					m = Regex.Match (line, @"^TLDR$");
					if (m.Success) {
						isComment = false;
			//			lexModel.AppendValues (m.Value, "End of Multi-Comment");
						break;
					}
			//		lexModel.AppendValues (line, "Multi Line Comment");
					break;
				}

				m = Regex.Match (line, @"^HAI\s*$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Start of Program");
					continue;
				} 

				m = Regex.Match (line, @"^KTHXBYE$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "End of Program");
					continue;
				}

				m = Regex.Match (line, @"^\s*BTW");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Start of Single Comment");
			//		lexModel.AppendValues (line, "Single Line Comment");
					line = line.Remove (0, line.Length);
					continue;
				}

				m = Regex.Match (line, @"^OBTW$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Start of Multi-Comment");
					isComment = true;
					continue;
				}

				m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Var Declaration");

					//	eval.varDecEval (line, symbolTable, consoleText, lineNumber);
					continue;
				}

				m = Regex.Match (line, @"^\s*ITZ\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Var Initializer");
					continue;
				}

				m = Regex.Match (line, @"^\s*R\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Var Assignment");
					continue;
				}

				m = Regex.Match (line, @"^\s*SUM\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Addition Op");
					// evaluateComplex(ref symbolTable, consoleText, "IT", line);
					continue;
				}

				m = Regex.Match (line, @"^\s*DIFF\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Subtraction Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*PRODUKT\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Multiplication Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*QUOSHUNT\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Division Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*MOD\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Modulo Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*BIGGR\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Max Value Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*SMALLR\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Min Value Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*BOTH\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "AND Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*EITHER\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "OR Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*WON\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "XOR Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*NOT\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Negation Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*ALL\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Infinite Arity AND");
					continue;
				}

				m = Regex.Match (line, @"^\s*ANY\s+OF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Infinite Arity OR");
					continue;
				}

				m = Regex.Match (line, @"^\s*AN\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Linking Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*BOTH\s+SAEM\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Equality Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*DIFFRINT\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Inequality Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*SMOOSH\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "String Concat Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*MAEK\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Casting Op");
					continue;
				}

				m = Regex.Match (line, @"^A\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Linking Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*IS\s+NOW\s+A\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Casting Op");
					continue;
				}

				m = Regex.Match (line, @"^\s*VISIBLE\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Print Op");

					//	eval.evalVisible (line, symbolTable, consoleText, lineNumber);
					continue;
				}

				m = Regex.Match (line, @"^\s*GIMMEH\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Scan Op");
					//	eval.evalGimmeh (line, symbolTable,consoleText,lineNumber,symbolTree);
					continue;
				}

				m = Regex.Match (line, @"^\s*O\s+RLY\?\s*$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Start of If-Then");
					continue;
				}

				m = Regex.Match (line, @"^\s*YA\s+RLY\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Equivalent to IF");
					continue;
				}

				m = Regex.Match (line, @"^\s*MEBBE\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Equivalent to ELSEIF");
					continue;
				}

				m = Regex.Match (line, @"^\s*NO\s+WAI\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Equivalent to ELSE");
					continue;
				}

				m = Regex.Match (line, @"^\s*OIC\s*$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "End of Selection Statement");
					continue;
				}

				m = Regex.Match (line, @"^\s*WTF\?\s*$");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Start of Switch");
					continue;
				}

				m = Regex.Match (line, @"^\s*OMG\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Case");
					continue;
				}

				m = Regex.Match (line, @"^\s*GTFO\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Break");
					continue;
				}

				m = Regex.Match (line, @"^\s*OMGWTF\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Default Case");
					continue;
				}

				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Var Identifier");
					//Check if matchedString matches a reserved word
					//	if (eval.isValidVarident (matchedString)) {
					//		matchedString = matchedString.Trim ();
					//		lexModel.AppendValues (matchedString, "Var Identifier");
					//	} else {
					//		consoleText.Buffer.Text += "Error: Invalid variable identifier " +
					//			matchedString + " at line " + lineNumber + "\n";
					//	}

					continue;
				}

				m = Regex.Match (line, @"^\s*""");
				if (m.Success) {
					String stringLiteral = "";
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
			//		lexModel.AppendValues (matchedString, "Start string Delimiter");

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
			//			lexModel.AppendValues (stringLiteral, "String Literal");
			//			lexModel.AppendValues (line[0] + "", "End string Delimiter");
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
			//		lexModel.AppendValues (matchedString, "Numbar Literal");
					continue;
				}

				m = Regex.Match (line, @"^\-?\d+\s*");
				if (m.Success) {
					line = line.Remove (0, m.Value.Length);
					matchedString = m.Value;
					matchedString = matchedString.Trim ();
			//		lexModel.AppendValues (matchedString, "Numbr Literal");
					continue;
				}


				consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "!\n";
				break;


			} //End of main loop
		}
	}
}

