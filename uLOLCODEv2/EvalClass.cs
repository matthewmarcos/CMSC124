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
				consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + ": " + variable + " not declared!\n";
				return false;
			} else {
				if(!evaluateComplex(ref symbolTable, consoleText, variable, lines)) {
					consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
					": variable " + lines + " is invalid!\n";
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
				//Assigning boolean to variable
				m = Regex.Match (expression[0], @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(expression[0]) && m.Success && expression[1].Equals("ITZ")) {
					//Fix symbol table
					if(!symbolTable.ContainsKey(expression[0])) {
						expression = Regex.Split(lines, @"\s+ITZ\s+");
						//Evaluate the expression[1] if expression is a string,
						// or complexExpression that is valid. Already evaluate it lang. plz.
						if(!evaluateComplex(ref symbolTable, consoleText, expression[0], expression[1])) {
							consoleText.Buffer.Text += "Syntax Error at line " + lineNumber +
							": variable " + expression[1] + " is invalid!\n";
						}
//						symbolTable.Add(expression[0], expression[1]);
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
			//char[] splitToken = {' '}; //<=== tentative
			//String[] expression;
				//CHECK IF STRING LITERAL

				//======Start of string literal eval ============//

				// !!! ISSUE: String literal with spaces.   Ex.   "S hit"; <----------------------------------------------------------
				m = Regex.Match (line, @"^\s*""");
				if (m.Success) {
					String sline = line;		//<--- for clarity's sake
					String stringLiteral = "";
						//if the string has a paired quotation marks
					if(sline.EndsWith("\"")){
						for(var i = 0; i < sline.Length;i++){
							if (i==0 || i==(sline.Length-1) ){
								continue;
							}
							else{
								stringLiteral += sline [i];
								}
							}
							consoleText.Buffer.Text += stringLiteral+"\n";

					}
					else{
						consoleText.Buffer.Text += "Syntax Error at line " + lineNumber + "! : Unpaired quotes (Visible Func)\n";
					}

					return;
				}
				//=======End of String Literal Eval ============//

				//==================VARIABLE==========================//
				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
				if(isValidVarident(line) && m.Success) {
						//If wala sa symbol table
					if(!symbolTable.ContainsKey(line)) {	//if wala pa sa symbol table

						consoleText.Buffer.Text += ("Syntax Error at line " + lineNumber + ": Variable undeclared(Visible Func)\n");
						return;
					} else {
							//IF NASA SYMBOL TABLE NA PRNT YUNG VALUE
						var stringLit = symbolTable[line];
						String printThis = stringLit.ToString();			//<------ Gawin String yung returned object;
						//Check if STRING OR NUM
						fixVISIBLE (printThis,consoleText);
						return;
					}
				}
				//==================END OF VAR EVAL=====================//
			
				//==================Number Literal =====================//
				m = Regex.Match (line, @"^\-?\d*\.\d+\s*");
				if (m.Success) {

					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}

				m = Regex.Match (line, @"^\-?\d+\s*");
				if (m.Success) {
				
					String matchedString = m.Value;
					matchedString = matchedString.Trim ();
					consoleText.Buffer.Text += matchedString+"\n";
					return;
				}


				//===============End of Number Literal==============//
				//===============Expression=========================//
			m = Regex.Match(line, @"^\s*(SUM\s+OF\s*|DIFF\s+OF\s*|PRODUKT\s+OF\s*|QUOSHUNT\s+OF\s*|MOD\s+OF\s*|BIGGR\s+OF\s*|SMALLR\s+OF\s*|
			BOTH\sOF\s*|EITHER\sOF\s*|WON\sOF\s*|DIFFRINT)\s*");
			if(m.Success) {
				//Check if complexArithmetic is valid or not
				if(isValidComplexArithmetic(line, consoleText, symbolTable)) {
					//symbolTable [key] = evalComplexArithmetic(expression, consoleText, symbolTable).ToString();
					consoleText.Buffer.Text += comp.evaluateComplexExpression(line, consoleText, symbolTable).ToString()+"\n";
					return;
				} else {
					consoleText.Buffer.Text += ("Syntax Error at line " + lineNumber + ": Invalid Expression(Visible Func)\n");
					return;
				}
			}

				
				//==================================================//
			//================//

		}

		public void evalGimmeh(String line, Hashtable symbolTable,TextView consoleText,int lineNumber,Gtk.ListStore symbolTree){

			//check variable "line"

			Match m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-z\d]*\s*$");
			if(isValidVarident(line) && m.Success) {
				//If wala sa symbol table
				if(!symbolTable.ContainsKey(line)) {	//if wala pa sa symbol table
					consoleText.Buffer.Text += ("Syntax Error at line " + lineNumber + ": Variable undeclared(Gimmeh Func)\n");

				} else {
					//IF NASA SYMBOL TABLE NA PRNT YUNG VALUE

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
			/*
			 *	Check if code starts and ends with HAI KTHXBYE 
			 */
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

		public Boolean evaluateComplex (ref Hashtable symbolTable,TextView consoleText, String key, String expression) {
			//Expression is yung natira from I HAS A

			//consoleText.Buffer.Text += "complex says: " + comp.ReturnHello() + "\n";
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
						consoleText.Buffer.Text += "Error: undeclared variable!\n";
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

			// Check if arithmetic, boolean, infinite arity operation.
			m = Regex.Match(expression, @"^\s*(DIFFRINT\s*|SUM\s+OF\s*|DIFF\s+OF\s*|PRODUKT\s+OF\s*|QUOSHUNT\s+OF\s*|MOD\s+OF\s*|BIGGR\s+OF\s*|SMALLR\s+OF\s*|BOTH\s+OF\s*|EITHER\s+OF\s*|WON\s+OF\s*|BOTH\s+SAEM\s*|ALL\s+OF\s*)\s*");
			if(m.Success) {
				//consoleText.Buffer.Text += "Tascascasc\n";
				symbolTable [key] = comp.evaluateComplexExpression(expression, consoleText, symbolTable).ToString();
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

		public String evalComplexBoolean (String expression, TextView consoleText, Hashtable symbolTable) {			
			String splitToken = "[ ]";
			String[] temp = Regex.Split (expression, splitToken);
			List<String> operations = new List<String>();
			var stack = new Stack<Boolean> ();
		
			for(var i = 0; i < temp.Length ; i++) {
				temp[i] = temp[i].Trim();
				if(!(temp[i].Equals("AN") || temp[i].Equals("OF"))) {
					operations.Add(temp[i]);
				}
			}         
            // Solving the postfix expression
			for(int i = operations.Count-1 ; i >= 0 ; i--) {
				
				if(Regex.IsMatch(operations[i], @"^WIN$")) {
					stack.Push(true);
				} else if (Regex.IsMatch(operations[i],@"^FAIL$")) {
					// If variable that exists in table
					stack.Push(false);
				} else if (Regex.IsMatch(operations[i], @"^[a-zA-Z][a-zA-z\d]*") && 
					symbolTable.ContainsKey(operations[i])) {
					// If variable that exists in table
					var myValue = (String)symbolTable[operations[i]];
					if(myValue.Equals("WIN")) {
						stack.Push(true);						
					} else if(myValue.Equals("FAIL")){
						stack.Push(false);
					}
				} else if (operations[i].Equals("BOTH")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a && b);
				} else if (operations[i].Equals("EITHER")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a || b);
				} else if (operations[i].Equals("WON")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push((a || b) && !(a && b));
				} else {
					// May error. Consult Mon how to handle.
					break;
				}
			}
				

			if(stack.Pop()) {
				return "WIN";
			} else {
				return "FAIL";
			}

		} 


		// Check for underflows and overflows
		public Boolean isValidComplexBoolean(String expression, TextView consoleText, Hashtable symbolTable) {
			var operators = 0;
			var operands = 0;
			while(expression != "") {
				Match m = Regex.Match(expression, @"^\s*(BOTH\s+OF\s*|EITHER\s+OF\s*|WON\s+OF\s*)");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operators+=1;
					continue;
				}

				m = Regex.Match(expression, @"^\s*(WIN\s*|FAIL\s*)\s*");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operands+=1;
					continue;
				}

				//disregard AN
				m = Regex.Match(expression, @"^\s*AN\s*");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					continue;
				}

				m = Regex.Match(expression, @"^[a-zA-Z][a-zA-z\d]*");
				if(m.Success) {
					expression = expression.Remove(0, m.Value.Length);
					expression = expression.Trim();
					operands+=1;
					continue;
				}
			}	

			if(operands - 1 == operators) {
				return true;
			} else {
				return false;	
			}
		}

		public Boolean isValidComplexArithmetic(String expression, TextView consoleText, Hashtable symbolTable) {
			String splitToken = "[ ]";
			String[] temp = Regex.Split (expression, splitToken);
			List<String> operations = new List<String>();

			var operators = 0;
			var operands = 0;
			for(var i = 0; i < temp.Length ; i++) {
				temp[i] = temp[i].Trim();
				if(!(temp[i].Equals("AN") || temp[i].Equals("OF"))) {
					operations.Add(temp[i]);
				}
			}  

			foreach(String operation in operations) {
				if(operation.Equals("DIFF") ||
					operation.Equals("SUM") ||
					operation.Equals("PRODUKT") ||
					operation.Equals("QUOSHUNT") ||
					operation.Equals("MOD") ||
					operation.Equals("BIGGR") ||
					operation.Equals("SMALLR")) {
					operators++;
				} else {
					operands++;
				}
			}
			if(operands - 1 == operators) {
				return true;
			} else {
				return false;	
			}
		}

		public int evalComplexArithmetic (String expression, TextView consoleText, Hashtable symbolTable) {			
			String splitToken = "[ ]";
			String[] temp = Regex.Split (expression, splitToken);
			List<String> operations = new List<String>();
			var stack = new Stack<int> ();
		
			for(var i = 0; i < temp.Length ; i++) {
				temp[i] = temp[i].Trim();
				if(!(temp[i].Equals("AN") || temp[i].Equals("OF"))) {
					operations.Add(temp[i]);
				}
			}         
            // Solving the postfix expression
			for(int i = operations.Count-1 ; i >= 0 ; i--) {
				int number;
				Boolean isNumeric = int.TryParse(operations[i], out number);
				if(isNumeric) {
					stack.Push(number);
				} else if (Regex.IsMatch(operations[i], @"^[a-zA-Z][a-zA-z\d]*") && 
					symbolTable.ContainsKey(operations[i])) {
					// If variable that exists in table
					stack.Push(Int32.Parse((String)symbolTable[operations[i]]));
				} else if (operations[i].Equals("SUM")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a + b);
				} else if (operations[i].Equals("DIFF")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a - b);
				} else if (operations[i].Equals("PRODUKT")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a * b);
				} else if (operations[i].Equals("QUOSHUNT")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					// if b == 0, exit
					stack.Push(a / b);
				} else if (operations[i].Equals("MOD")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push(a % b);
				} else if (operations[i].Equals("BIGGR")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push((a > b) ? a : b);
				} else if (operations[i].Equals("SMALLR")) {
					var a = stack.Pop ();
					var b = stack.Pop ();
					// consoleText.Buffer.Text += (a+b) + '\n';
					stack.Push((a > b) ? b : a);
				} else {
					// May error. Consult Mon how to handle.
					break;
				}
			}

			var res = stack.Pop();
			// consoleText.Buffer.Text += res + "";

			return res;

		}


		public Boolean validNumber(String number) {
			Match m =  Regex.Match (number, @"^\-?\d*\.?\d+\s*");
			return m.Success;
		}

		public void evalINPUT (String variable, String value, Hashtable symbolTable){
			//EVALUATE THE GIVEN VALUE;
			// IF NUMBAR,NUMBR,STRING
			Match m = Regex.Match (value, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				symbolTable[variable]=value;
				return;
			}

			 m = Regex.Match (value, @"^\-?\d+\s*");
			if (m.Success) {
				symbolTable[variable]=value;
				return;
			}

			symbolTable[variable] = "\"" + value + "\"";

		}
		public void fixVISIBLE(String value,TextView consoleText){
			String handler;
			String toPrint = "";
			Match m = Regex.Match (value, @"^\-?\d*\.\d+\s*");
			if (m.Success) {
				handler = m.Value;
				consoleText.Buffer.Text += (handler+"\n");
				return;
			}

			m = Regex.Match (value, @"^\-?\d+\s*");
			if (m.Success) {
				handler = m.Value;
				consoleText.Buffer.Text += (handler+"\n");
				return;
			}

			m = Regex.Match (value, @"^\s*""");
			if (m.Success) {
				handler = m.Value;

				for (var i=0; i<handler.Length; i++) {
					if (i == 0 || i == handler.Length - 1) {
						continue;
					} else {
						toPrint += handler[i];
					}
				}
				consoleText.Buffer.Text += (toPrint + "\n");
				return;
			}

			//IF NOT A STRING OR A NUMBR
			//JUST PRINT;

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
			//codes [lineNumber];	<=== current location of ORLY
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

				consoleText.Buffer.Text += "Syntax Error: OIC expected\n";
				return;
			} else {

				evaluateConditions (codes,lineNumber,symbolTable,consoleText);
			}


		}

		public void evaluateConditions(String[] codes,int lineNumber,Hashtable symbolTable,TextView consoleText){


		}
		/*
		public EvalClass(Hashtable symbolTablez, Gtk.ListStore symbolTreez,		 Gtk.ListStore lexmodelz) {
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
