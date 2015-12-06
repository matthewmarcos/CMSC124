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
		ComplexEvaluator comp = new ComplexEvaluator();
		public ParserClass ()
		{
		}

		public void parseLines (String line,ref Boolean isComment, int lineNumber, ref Hashtable symbolTable, ref Gtk.ListStore symbolTree, TextView consoleText,String[] codes) {
			lineNumber += 1;
			Match m;
			String matchedString;
			String it = "";

			updateSymbolTable (ref symbolTable, ref symbolTree);
			if(isComment) {
				m = Regex.Match (line, @"^TLDR$");
				if (m.Success) {
					isComment = false;
				}
			}

			m = Regex.Match (line, @"^KTHXBYE$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*BTW");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				line = line.Remove (0, line.Length);
			}

			m = Regex.Match (line, @"^OBTW$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				isComment = true;
			}

			m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();

				eval.varDecEval (line, symbolTable, consoleText, lineNumber);
				return;
			}

			m = Regex.Match (line, @"^\s*SUM\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if (eval.isValidComplexArithmetic (line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression (line, consoleText, symbolTable).ToString ();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
					consoleText.Buffer.Text += "IT: " + it + "\n";
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (SUM OF)\n";
				}
				return;
			}

			m = Regex.Match (line, @"^\s*DIFF\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if (eval.isValidComplexArithmetic (line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression (line, consoleText, symbolTable).ToString ();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*PRODUKT\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*QUOSHUNT\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;				
			}

			m = Regex.Match (line, @"^\s*MOD\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*BIGGR\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*SMALLR\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexArithmetic(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
				}
				consoleText.Buffer.Text += "IT: " + it + "\n";
				return;
			}

			m = Regex.Match (line, @"^\s*BOTH\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if (eval.isValidComplexBoolean (line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression (line, consoleText, symbolTable).ToString ();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
					consoleText.Buffer.Text += "IT: " + it + "\n";
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (BOTH OF)\n";
				}
			}

			m = Regex.Match (line, @"^\s*EITHER\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
					consoleText.Buffer.Text += "IT: " + it + "\n";
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (EITHER OF)\n";
				}
			}

			m = Regex.Match (line, @"^\s*WON\s+OF\s*");
			if (m.Success) {
				line = line.Trim ();
				if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
					consoleText.Buffer.Text += "IT: " + it + "\n";
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (WON OF)\n";
				}
			}

			m = Regex.Match (line, @"^\s*NOT\s*");
			if (m.Success) {
				it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();		
				eval.evaluateComplex (ref symbolTable, consoleText, "IT", it, lineNumber);
			}

			m = Regex.Match (line, @"^\s*ALL\s+OF\s*");
			if (m.Success) {
				
			}

			m = Regex.Match (line, @"^\s*ANY\s+OF\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^\s*BOTH\s+SAEM\s*");
			if (m.Success) {
				line = line.Trim ();

				//line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				//if(eval.isValidComplexBoolean(line, consoleText, symbolTable)) {
					//it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();
					eval.evaluateComplex (ref symbolTable, consoleText, "IT", line,lineNumber);
				//}

				consoleText.Buffer.Text += "IT: " + it + "\n";
		
			}

			m = Regex.Match (line, @"^\s*DIFFRINT\s*");
			if (m.Success) {
								
			}

			m = Regex.Match (line, @"^\s*SMOOSH\s*");
			if (m.Success) {
				it = comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString();	
			}

			m = Regex.Match (line, @"^\s*MAEK\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				line = line.Trim();
				//MAEK <expression> [A] <type>
				//get the type and the variable to put it in
				// evaluate expression
				// typecast with eval.evalMAEK(input, type, lineNumber, ref symbolTable, consoleText);
				// update source with the value

				String[] inputs = Regex.Split(line, @"\s+A\s+");
				m = Regex.Match (inputs[0], @"[a-zA-Z][a-zA-z\d]*$");
				if (m.Success && symbolTable.ContainsKey(m.Value)) {
					inputs [0] = symbolTable [m.Value].ToString ();
					symbolTable [m.Value] = eval.evalMAEK(inputs[0].Trim(), inputs[1].Trim(), lineNumber, ref symbolTable, consoleText);
				}
					
			}

			m = Regex.Match (line, @"^A\s*");
			if (m.Success) {

			}

			m = Regex.Match (line, @"^\s*IS\s+NOW\s+A\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*VISIBLE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				eval.evalVisible (line, symbolTable, consoleText, lineNumber);
				
			}

			m = Regex.Match (line, @"^\s*GIMMEH\s*");
			if (m.Success) {
				line = line.Remove (0,m.Value.Length);
				eval.evalGimmeh (line, symbolTable, consoleText, lineNumber, symbolTree);
			}

			m = Regex.Match (line, @"^\s*O\s+RLY\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();

				eval.evalORLY (codes,lineNumber, symbolTable,consoleText);

			}

			m = Regex.Match (line, @"^\s*YA\s+RLY\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*MEBBE\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*NO\s+WAI\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*OIC\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*WTF\?\s*$");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
				eval.evalWTF (codes, lineNumber, symbolTable, consoleText);
			}

			m = Regex.Match (line, @"^\s*OMG\s*");
			if (m.Success) {
				return;
			}

			m = Regex.Match (line, @"^\s*GTFO\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
			}

			m = Regex.Match (line, @"^\s*OMGWTF\s*");
			if (m.Success) {
				line = line.Remove (0, m.Value.Length);
				matchedString = m.Value;
				matchedString = matchedString.Trim ();
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
					eval.varAssignEval (variable, line, ref symbolTable, consoleText, lineNumber);
				}
			}
			m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*");
			if (m.Success) {
				String something = m.Value;
				if (symbolTable.ContainsKey (something)) {
					symbolTable ["IT"] = symbolTable[something];
				}
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
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + ", Unpaired quotes!\n";
				}
			}

			//Numbar Literal
			m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				return;
			}

			//Numbr Literal
			m = Regex.Match (line, @"^\-?\d+\s*");
			if (m.Success) {
				return;
			}		
		}

		protected void updateSymbolTable(ref Hashtable symbolTable, ref Gtk.ListStore symbolTree) {
			symbolTree.Clear ();
			foreach(DictionaryEntry pair in symbolTable) {
				symbolTree.AppendValues (pair.Key, pair.Value);
			}
		}
	}//End of ParserClass
}

