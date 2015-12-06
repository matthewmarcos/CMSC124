using System;
using Gtk;
using uLOLCODEv2;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace uLOLCODEv2
{
	public class EvalClass
	{	
		ComplexEvaluator comp = new ComplexEvaluator();
		public static string inputVALUE = " ";

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
				"TROOF",
				"YARN",
				"NUMBR",
				"NUMBAR",
				"NOOB",
				"WIN",
				"FAIL",
				"IT",
				"MKAY"
			};
			foreach(var pattern in patterns) {
				if(pattern.Equals(variable)) {
					return false;
				}
			}
			return true;
		}

		public Boolean varAssignEval(String variable, String lines, ref Hashtable symbolTable, TextView consoleText, int lineNumber) {
			// Check if variable is in symbol Table
			if(!symbolTable.ContainsKey(variable)) {
				consoleText.Buffer.Text += "Expected error at line " + lineNumber + ", undeclared variable " + variable + "!\n";
				return false;
			} else {
				if(!evaluateComplex(ref symbolTable, consoleText, variable, lines, lineNumber)) {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber +
					", invalid variable " + lines + "!\n";
					return false;
				}
				return true;
			}
		}

		public void varDecEval(String lines, Hashtable symbolTable, TextView consoleText, int lineNumber) {
			Match m;
			char[] splitToken = {' '};
			String[] expression = lines.Split (splitToken);
			if(expression.Length == 1) {

				//I HAS A VAR
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success) {
					//Fix symbol table
					if(!symbolTable.ContainsKey(expression[0])) {
						symbolTable.Add(expression[0], "NOOB");
						return;
					} else {
						consoleText.Buffer.Text += "Expected error at line " + lineNumber +
							": variable " + expression[0] + " is already used!\n";
							return;
					}
				}
			}
			// ITZ ASSIGNMENT
			if(expression.Length >= 3) {
				//Assigning boolean to variable
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success && expression[1].Equals("ITZ")) {
					//Fix symbol table
					if(!symbolTable.ContainsKey(expression[0])) {
						expression = Regex.Split(lines, @"\s+ITZ\s+");
						//Evaluate the expression[1] if expression is a string,
						// or complexExpression that is valid. Already evaluate it lang. plz.
						if(!evaluateComplex(ref symbolTable, consoleText, expression[0], expression[1], lineNumber)) {
							consoleText.Buffer.Text += "Expected error at line " + lineNumber +
							", invalid variable " + expression[1] + "!\n";
						}
						return;
					} else {
						consoleText.Buffer.Text += "Expected error at line " + lineNumber +
							", variable " + expression[0] + " is already used!\n";
						return;
					}
				}
			}
			consoleText.Buffer.Text += "Expected error at line " + lineNumber + "!\n";
		}

		public void evalVisible(String line, Hashtable symbolTable, TextView consoleText, int lineNumber) {
			Match m;
				//======Start of string literal eval ============//
				Char splitToken = ' ';
				String[] holder = line.Split (splitToken);
				line = line.Trim ();
				
				m = Regex.Match (line, @"^\s*""");
				if (m.Success) {
					String sline = line;		//<--- for clarity's sake
					String stringLiteral = "";
					int i = 1;
					//	int limit = sline.Length - 1;
						//if the string has a paired quotation marks
					while (!sline[i].Equals('"') && i!=sline.Length-1) {
						stringLiteral += sline [i];
						//line = line.Remove (i, 1);
						i++;					
					} 

					if (sline [i].Equals ('"')) {
						
					} else {
						//THROW AN ERROR IF UNPAIRED STRING "<String....> ...<end>
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + ": Variable undeclared(Visible Func)\n";
						return;
					}
					consoleText.Buffer.Text += stringLiteral;
					//something about i+1 is equal to the last character of the passed string. wala nang kadugtong
					if (i + 1 == sline.Length) {
						consoleText.Buffer.Text += "\n";
						return;
					} else {
						i = i + 2; //this will either equal to the WORD following this STRING LITERAL "<this>"[i is here]...
						//let sline.Substring be the following stuffs.
						sline = sline.Substring (i);
						//pass it again to this function:)
						evalVisible (sline, symbolTable, consoleText, lineNumber);
					}

				}
				//=======End of String Literal Eval ============//

				//==================VARIABLE==========================//
				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(line) && m.Success) {
						//If wala sa symbol table
					if(!symbolTable.ContainsKey(line)) {
						consoleText.Buffer.Text += ("Expected error at line " + lineNumber + ", Variable undeclared! (Visible Func)\n");
				}
				m = Regex.Match (holder[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(holder[0]) && m.Success) {
				String stringLine = "";
				String sline = line;
				//If wala sa symbol table
					
					if(!symbolTable.ContainsKey(holder[0])) {
						consoleText.Buffer.Text += ("Expected error at line " + lineNumber + ": Variable undeclared(Visible Func)\n");
						return;
					} else {
							//IF NASA SYMBOL TABLE NA PRINT YUNG VALUE
					var stringLit = symbolTable[holder[0]];
						String printThis = stringLit.ToString();//<------ Gawin String yung returned object;
						//Check if STRING OR NUM then PRINT
						fixVISIBLE (printThis,consoleText);
						
						//if the ARRAY holder contains only 1 element, then VISIBLE <VAR> lang. ELSE RETURN TO EVALUATE!
						if (holder.Length == 1) {
							consoleText.Buffer.Text += "\n";
							return;
						} else {
							sline = sline.Remove (0, holder [0].Length);
							sline = sline.Trim ();
							//then pass it to a this function again...
							evalVisible (sline, symbolTable, consoleText, lineNumber);
						}
						
					}
				}
			}
				//==================END OF VAR EVAL=====================//
			
				//==================Numbar Literal =====================//
				m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
				if (m.Success) {

					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}
				//===============End of Numbar Literal==============//

				//==================Numbr Literal =====================//
				m = Regex.Match (line, @"^\-?\d+\s*");
				if (m.Success) {
				
					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}
				//===============End of Numbr Literal==============//

				//=====================Expression===================//
			m = Regex.Match(line, @"^\s*(SUM\s+OF\s*|DIFF\s+OF\s*|PRODUKT\s+OF\s*|QUOSHUNT\s+OF\s*|MOD\s+OF\s*|BIGGR\s+OF\s*|SMALLR\s+OF\s*|
			BOTH\sOF\s*|EITHER\sOF\s*|WON\sOF\s*|DIFFRINT|ANY\s+OF)\s*");
			if(m.Success) {
				//Check if complexArithmetic is valid or not
				if(isValidComplexArithmetic(line, consoleText, symbolTable)) {
					consoleText.Buffer.Text += comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString()+"\n";
					return;
				} else {
					consoleText.Buffer.Text += ("Expected error at line " + lineNumber + ", Invalid Expression (Visible Func)\n");
					return;
				}
			}
		}

		public void evalGimmeh(String line, Hashtable symbolTable,TextView consoleText,int lineNumber,Gtk.ListStore symbolTree){
			//check variable "line"
			Match m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
			if(isValidVarident(line) && m.Success) {
				//If wala sa symbol table
				if(!symbolTable.ContainsKey(line)) {
					consoleText.Buffer.Text += ("Expected error at line " + lineNumber + ", Variable undeclared (Gimmeh Func)\n");

				} else {
					uLOLCODEv2.Inputtr box = new uLOLCODEv2.Inputtr ();
					//run dialog box
					//DECLARE AN INSTANCE OF Inputtr;
					box.Run ();
					//get the input from the dialog box, passed to "inputVALUE"  of type global static string
					//symbolTable [line] = inputVALUE;
					evalINPUT (line,inputVALUE,symbolTable);
					//Append it to the "console"
					consoleText.Buffer.Text += inputVALUE+"\n";
					//update the symbolTable after input.
					updateSymbolTable (symbolTable, symbolTree);
				}
			}
		}

		public Boolean hasValidStartAndEnd (String[] lines) {
			//Check if code starts and ends with HAI KTHXBYE 
			if (lines [0].Equals ("HAI") && lines [lines.Length-1].Equals ("KTHXBYE")) {
				return true;
			} else {
				return false;
			}
		}

		protected void updateSymbolTable(Hashtable symbolTable, Gtk.ListStore symbolTree) {
			symbolTree.Clear ();
			foreach(DictionaryEntry pair in symbolTable) {
				symbolTree.AppendValues (pair.Key, pair.Value);
			}
		}

		public Boolean evaluateComplex (ref Hashtable symbolTable,TextView consoleText, String key, String expression, int lineNumber) {
			//Expression is yung natira from I HAS A
			Match m;
			char[] splitToken = {' '};
			String[] words = expression.Split (splitToken);
			if (words.Length == 1) {
				//Check if word[0] valid number.
				if (validNumber (words [0])) {
					symbolTable [key] = words [0];
					return true;
				//Check if word[0] is a valid Boolean
				} else if (words[0].Equals("WIN") || words[0].Equals("FAIL")) {
					if (words [0].Equals ("WIN")) {
						symbolTable [key] = "WIN";
						return true;
					} else {
						symbolTable [key] = "FAIL";
						return true;
					}
				//Check if word[0] is a valid Variable
				} else if (Regex.IsMatch(expression, @"^\s*[a-zA-Z][a-zA-z\d]*\s*") && isValidVarident (words [0])) {
					if (symbolTable.ContainsKey (expression)) {
						symbolTable [key] = symbolTable [expression];
						return true;
					} else {
						consoleText.Buffer.Text += "Expected error at line " + lineNumber + "\n";
						return false;
					}
				} 
			}

			// Check if string assignment
			m = Regex.Match(expression, @"^\s*"".*""$");
			if(m.Success) {
				symbolTable [key] = m.Value;
				return true;
			}

			// Check if boolean
			m = Regex.Match(expression, @"^\s*(BOTH\s+OF\s*|EITHER\s+OF\s*|WON\s+OF\s*)\s*");
			if(m.Success) {
				if (isValidComplexBoolean (expression, consoleText, symbolTable)) {
					symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (invalid bool expression)\n";
				}
				return true;
			}

			// Check if arithmetic
			m = Regex.Match(expression, @"^\s*(SUM\s+OF\s*|DIFF\s+OF\s*|PRODUKT\s+OF\s*|QUOSHUNT\s+OF\s*|MOD\s+OF\s*|BIGGR\s+OF\s*|SMALLR\s+OF\s*)\s*");
			if(m.Success) {
				if (isValidComplexArithmetic (expression, consoleText, symbolTable)) {
					symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
				} else {
					consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (invalid arith expression)\n";
				}
				return true;
			}

			// Check if equality/infinite arity
			m = Regex.Match(expression, @"^\s*(BOTH\s+SAEM\s*|DIFFRINT\s*|ALL\s+OF\s*|ANY\s+OF\s*)");
			if(m.Success) {
				symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
				//consoleText.Buffer.Text += "Expected error at line " + lineNumber + "! (invalid equality expression)\n";
				return true;
			}

			m = Regex.Match(expression, @"^\s*SMOOSH\s*");
			if(m.Success) {
				symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
				return true;
			}

			m = Regex.Match(expression, @"^\s*NOT\s*");
			if(m.Success) {
				symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
				return true;
			}

			consoleText.Buffer.Text += "Error: Invalid assignment!\n";
			return false;
		}

		public Boolean isValidComplexBoolean(String expression, TextView consoleText, Hashtable symbolTable) {
			// Check for underflows and overflows
			float operators = 0;
			float operands = 0;
			while(expression != "") {
				Match m = Regex.Match(expression, @"^\s*(BOTH\s+OF\s*|EITHER\s+OF\s*|WON\s+OF\s*)");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operators += 0.5f;
					continue;
				}

				m = Regex.Match(expression, @"^\s*(WIN\s*|FAIL\s*)\s*");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operands += 1f;
					continue;
				}

				m = Regex.Match(expression, @"^\s*AN\s*");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operators += 0.5f;
					continue;
				}

				m = Regex.Match(expression, @"^[a-zA-Z][a-zA-z\d]*");
				if(m.Success) {
					if (isValidVarident (m.Value)) {
						expression = expression.Remove (0, m.Value.Length);
						expression = expression.Trim ();
						operands += 1f;
						continue;
					}
				}

				//consoleText.Buffer.Text += "Operands: " + operands + "\n";
				//consoleText.Buffer.Text += "Operators: " + operators + "\n";
				//if none of the options above are correct, error
				return false;
			}	

			if(operands - 1 == operators) {
				return true;
			} else {
				return false;	
			}
		}

		public Boolean isValidComplexArithmetic(String expression, TextView consoleText, Hashtable symbolTable) {
			String[] tokens = Regex.Split (expression, @"[ ]");
			List<String> operations = new List<String> ();

			float operators = 0;
			float operands = 0;
			for(var i = 0; i < tokens.Length ; i+=1) {
				tokens [i] = tokens [i].Trim ();
				if (!(tokens [i].Equals ("AN"))) {
					if (tokens [i].Equals ("WIN") || tokens [i].Equals ("FAIL")) {

					} else {
						operations.Add (tokens [i]);
					}
				} else {
					if(tokens[i].Equals("AN")) {
						operators += 0.5f;
					}
				}
			}

			foreach(String operation in operations) {
				if (operation.Equals ("SUM") ||
					operation.Equals ("DIFF") ||
					operation.Equals ("PRODUKT") ||
					operation.Equals ("QUOSHUNT") ||
					operation.Equals ("MOD") ||
					operation.Equals ("BIGGR") ||
					operation.Equals ("SMALLR") ||
				    operation.Equals ("OF")) {
					operators += 0.25f;
				} else {
					operands += 1f;
				}
			}

			//consoleText.Buffer.Text += "Operands: " + operands + "\n";
			//consoleText.Buffer.Text += "Operators: " + operators + "\n";
			if (operands - 1 == operators) {
				return true;
			} else {
				return false;	
			}
		}

		public Boolean validNumber(String number) {
			Match m =  Regex.Match (number, @"^\-?\d*\.?\d+\s*");
			return m.Success;
		}

		public void evalINPUT (String variable, String value, Hashtable symbolTable){
			//EVALUATE THE GIVEN VALUE;
			//if numbar literal
			Match m = Regex.Match (value, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				symbolTable[variable]=value;
				return;
			}
			
			//if numbr literal
			 m = Regex.Match (value, @"^\-?\d+\s*");
			if (m.Success) {
				symbolTable[variable]=value;
				return;
			}
			
			//if string literal
			symbolTable[variable] = "\"" + value + "\"";

		}
		public void fixVISIBLE(String value,TextView consoleText){
			String handler;
			String toPrint = "";

			Match m = Regex.Match (value, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				handler = m.Value;
				consoleText.Buffer.Text += (handler);
				return;
			}

			m = Regex.Match (value, @"^\-?\d+\s*");
			if (m.Success) {
				handler = m.Value;
				consoleText.Buffer.Text += (handler);
				return;
			}

			m = Regex.Match (value, @"^\s*""");
			if (m.Success) {
				handler = value;
				for (var i=0; i<handler.Length; i++) {
					if (i == 0 || i == handler.Length - 1) {
						continue;
					} else {
						toPrint += handler[i];
					}
				}
				consoleText.Buffer.Text += (toPrint);
				return;
			}

			//if not a string, numbar, or number, just print it
			consoleText.Buffer.Text += (value+"\n");
		}

		public void evalORLY(String[] codes,int lineNumber,Hashtable symbolTable,TextView consoleText){
			/*IF ORLY IS DETECTED
			 * CHECK VALUE OF IT
			 * IF IT IS TRUE GO TO YA RLY
			 * IF NOT
			 * CHECK IF MEBBE
			 * IF MEBBE, CHECK CONDITION
			 * IF NOT GO TO NO WAI
			 * */
			Boolean paired = false;
			int i = lineNumber;
			for (; i<codes.Length; i++) {
				Match m = Regex.Match (codes [i], @"^\s*OIC\s*$");
				if (m.Success) {
					paired = true;
					break;
				} else {
					continue;
				}

			}
			//if after the loop, paired is still false, then there was no OIC detected;
			//if no OIC dected, throw error and return;
			//if OIC detected, then gogogogo
			if (!paired) {
				consoleText.Buffer.Text += "Expected Error at line " + lineNumber + ", OIC expected\n";
				return;
			} else {
				evaluateConditions (codes,lineNumber,symbolTable,consoleText);
			}
		}

		public void evaluateConditions(String[] codes,int lineNumber,Hashtable symbolTable,TextView consoleText){
			String value = symbolTable ["IT"].ToString();
			int i = lineNumber;
			int j;
			Boolean isComment = false;
			//fix the value of IT to a boolean;
			if (value.Equals ("WIN") || value.Equals ("FAIL")) {
			
			} else {
				//===If numbar Literal ===//
				Match m = Regex.Match (value, @"^\-?\d*\.\d+\s*");
				if (m.Success) {
					if (m.Value.Equals ("0")) {//if equals to 0
						value = "FAIL";
					} else {
						value = "WIN";
					}
				}

				//===If numbr Literal ===//
				m = Regex.Match (value, @"^\-?\d+\s*");
				if (m.Success) {
					if (m.Value.Equals ("0")) {// equals to 0
						value = "FAIL";
					} else {
						value = "WIN";
					}
				}

				//===If string Literal ===//
				m = Regex.Match (value, @"^\s*""");
				if (m.Success) {
					if (value [1].Equals ("\"")) {//if empty string
						value = "FAIL";
					} else {
						value = "WIN";
					}
				}
			}

			for (; i<codes.Length; i++) {
				Match m = Regex.Match (codes[i], @"^\s*YA\s+RLY\s*");
				if (m.Success && value.Equals("WIN")) {
					j = i+1;	//new line i scan;
					for(; j < codes.Length ; j++) {

						//IF MEBBE OR NO WAI OR OIC IS DETECTED
						Match n = Regex.Match(codes[j],@"^\s*MEBBE\s*");
						Match o = Regex.Match (codes [j], @"^\s*NO\s+WAI\s*");
						Match p = Regex.Match (codes [j], @"^\s*OIC\s*$");
						Match q = Regex.Match (codes [j], @"^\s*NO\s+WAI\s*");
						Match r = Regex.Match (codes[j], @"^\s*O\s+RLY\?\s*$");
						if (n.Success || o.Success || p.Success || q.Success || r.Success) {
							//consoleText.Buffer.Text += "STOP!\n";
							i = j;
							break;
						} else {
							//ELSE, EVALUATE THE LINES!
							ParserClass parse = new ParserClass ();
							parse.parseLines(codes [j], ref isComment, i, ref symbolTable, ref MainWindow.symbolTree, consoleText, codes);
						}
						i = j;
					}
				}

				m = Regex.Match (codes[i], @"^\s*MEBBE\s*");
				if (m.Success) {

				}

				m = Regex.Match (codes[i], @"^\s*NO\s+WAI\s*");
				if (m.Success && value.Equals("FAIL")) {
					j = i+1;	//new line i scan;
					for(; j < codes.Length ; j++) {
						//IF MEBBE OR NO WAI OR OIC IS DETECTED
						Match n = Regex.Match(codes[j],@"^\s*MEBBE\s*");
						Match o = Regex.Match (codes [j], @"^\s*YA\s+RLY\s*");
						Match p = Regex.Match (codes [j], @"^\s*OIC\s*$");
						Match q = Regex.Match (codes [j], @"^\s*NO\s+WAI\s*");
						Match r = Regex.Match (codes[j], @"^\s*O\s+RLY\?\s*$");
						if (n.Success || o.Success || p.Success || q.Success || r.Success) {
						//	consoleText.Buffer.Text += "STOP!\n";
							i = j;
							break;
						} else {
							//ELSE, EVALUATE THE LINES!
							ParserClass parse = new ParserClass ();
							parse.parseLines(codes [j], ref isComment, i, ref symbolTable, ref MainWindow.symbolTree, consoleText, codes);
						}
						i = j;
					}
				}

				m = Regex.Match (codes [i], @"^\s*OIC\s*$");
				if (m.Success) {
					MainWindow.detectedOIC = true;
					MainWindow.newIndex = i;
					//consoleText.Buffer.Text += "EVERYTHING IS DONE\n";
					break;
				}
			}
		}//End of EvalClass
	}
}
