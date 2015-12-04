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
		EvalClass eval = new EvalClass ();
		public ParserClass ()
		{
		}

		public void parseLines (String line,ref Boolean isComment, int lineNumber, ref Hashtable symbolTable, ref Gtk.ListStore symbolTree, TextView consoleText) {
			lineNumber += 1;
			Match m;
			String matchedString;
			String it = "";

			//consoleText.Buffer.Text += ("Value of Flag: " + isComment + "\n");
			updateSymbolTable (ref symbolTable, ref symbolTree);
			if(isComment) {
				m = Regex.Match (line, @"^TLDR$");
				if (m.Success) {
					isComment = false;
		//			lexModel.AppendValues (m.Value, "End of Multi-Comment");
					
				}
		//		lexModel.AppendValues (line, "Multi Line Comment");
				
			}

			m = Regex.Match (line, @"^KTHXBYE$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "End of Program");
				
			}

			m = Regex.Match (line, @"^\s*BTW");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Start of Single Comment");
		//		lexModel.AppendValues (line, "Single Line Comment");
				line = line.Remove (0, line.Length);
				
			}

			m = Regex.Match (line, @"^OBTW$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Start of Multi-Comment");
				isComment = true;
				
			}

			m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Var Declaration");

				eval.varDecEval (line, symbolTable, consoleText, lineNumber);

				return;

			}

			m = Regex.Match (line, @"^\s*SUM\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*DIFF\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
				
			}

			m = Regex.Match (line, @"^\s*PRODUKT\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
				
			}

			m = Regex.Match (line, @"^\s*QUOSHUNT\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;				
			}

			m = Regex.Match (line, @"^\s*MOD\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
				
			}

			m = Regex.Match (line, @"^\s*BIGGR\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
				
			}

			m = Regex.Match (line, @"^\s*SMALLR\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString();
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
				
			}

			m = Regex.Match (line, @"^\s*BOTH\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexBoolean(line, consoleText, symbolTable).ToString();
				}
		//		lexModel.AppendValues (matchedString, "AND Op");
				
			}

			m = Regex.Match (line, @"^\s*EITHER\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexBoolean(line, consoleText, symbolTable).ToString();
				}
				
			}

			m = Regex.Match (line, @"^\s*WON\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					// consoleText.Buffer.Text += eval.evalComplexArithmetic(line, consoleText, symbolTable).ToString() + "\n";
					it = eval.evalComplexBoolean(line, consoleText, symbolTable).ToString();
				}
				
			}

			m = Regex.Match (line, @"^\s*NOT\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^\s*ALL\s+OF\s*");
			if (m.Success) {
				
			}

			m = Regex.Match (line, @"^\s*ANY\s+OF\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^\s*BOTH\s+SAEM\s*");
			if (m.Success) {
				
			}

			m = Regex.Match (line, @"^\s*DIFFRINT\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^\s*SMOOSH\s*");
			if (m.Success) {
				consoleText.Buffer.Text += "Nope!\n";
				
			}

			m = Regex.Match (line, @"^\s*MAEK\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^A\s*");
			if (m.Success) {
				
			}

			m = Regex.Match (line, @"^\s*IS\s+NOW\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Casting Op");
				
			}

			m = Regex.Match (line, @"^\s*VISIBLE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Print Op");

				//	eval.evalVisible (line, symbolTable, consoleText, lineNumber);
				
			}

			m = Regex.Match (line, @"^\s*GIMMEH\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				
			}

			m = Regex.Match (line, @"^\s*O\s+RLY\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Start of If-Then");
				
			}

			m = Regex.Match (line, @"^\s*YA\s+RLY\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Equivalent to IF");
				
			}

			m = Regex.Match (line, @"^\s*MEBBE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Equivalent to ELSEIF");
				
			}

			m = Regex.Match (line, @"^\s*NO\s+WAI\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Equivalent to ELSE");
				
			}

			m = Regex.Match (line, @"^\s*OIC\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "End of Selection Statement");
				
			}

			m = Regex.Match (line, @"^\s*WTF\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Start of Switch");
				
			}

			m = Regex.Match (line, @"^\s*OMG\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Case");
				
			}

			m = Regex.Match (line, @"^\s*GTFO\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Break");
				
			}

			m = Regex.Match (line, @"^\s*OMGWTF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
		//		lexModel.AppendValues (matchedString, "Default Case");
				
			}

			m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s+R\s+");
			if (m.Success) {
				// VAR R...
				String varident = m.Value;
				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*");
				String variable = m.Value;
				variable = variable.Trim();
				line = line.Remove(0, varident.Length);

				if (eval.isValidVarident(variable)) {
					// consoleText.Buffer.Text += "Variable: " + variable + "\n";
					// consoleText.Buffer.Text += "Line: " + line + "\n";
					eval.varAssignEval (variable, line, ref symbolTable, consoleText, lineNumber);
				}
				
				// evalVarR(sdgviudsv)
				
			}

			m = Regex.Match (line, @"^\s*""");
			if (m.Success) {
				String stringLiteral = "";
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;

				for(; line.Length > 0 && line[0] != '"';) {
					stringLiteral += line[0];
					line = line.Remove(0, 1);
				}
				if(line.Length != 0) {
					line = line.Remove(0, 1);
				} else {
					//ERROR -> unpaired quotation mark
					consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "! (unpaired quotes)\n";
					
				}

				
			}

			m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				return;
		//		lexModel.AppendValues (matchedString, "Numbar Literal");
				
			}

			m = Regex.Match (line, @"^\-?\d+\s*");
			if (m.Success) {
				return;
		//		lexModel.AppendValues (matchedString, "Numbr Literal");
				
			}		


		}

		protected void updateSymbolTable(ref Hashtable symbolTable, ref Gtk.ListStore symbolTree) {
			symbolTree.Clear ();
			foreach(DictionaryEntry pair in symbolTable) {
				symbolTree.AppendValues (pair.Key, pair.Value);
			}
		}
	}
}

