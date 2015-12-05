using System;
using Gtk;
using uLOLCODEv2;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace uLOLCODEv2
{
	public class ComplexEvaluator
	{
		public ComplexEvaluator ()
		{	
		}

		public String evaluateComplexExpression(String exp, TextView consoleText, Hashtable symbolTable) {
			Match m;
			String expression = exp;
			//List<String> operations = new List<String>();
			Stack stack = new Stack ();

			//consoleText.Buffer.Text += expression + "\n";

			while (!String.IsNullOrEmpty(expression)) {
				//consoleText.Buffer.Text += expression + "\n";
				m = Regex.Match (expression, @"WIN$");
				if (m.Success) {
					expression = expression.Remove (m.Index, m.Value.Length);
					expression = expression.Trim ();
					stack.Push (true);
					continue;
				}

				m = Regex.Match (expression, @"FAIL$");
				if (m.Success) {
					expression = expression.Remove (m.Index, m.Value.Length);
					expression = expression.Trim ();
					stack.Push (true);
					continue;
				}

				m = Regex.Match (expression, @"^[a-zA-Z][a-zA-z\d]*");
				if (m.Success && symbolTable.ContainsKey(expression)) {
					// If variable that exists in table
					var myValue = (String)symbolTable[expression];
					if(myValue.Equals("WIN")) {
						stack.Push(true);						
					} else if(myValue.Equals("FAIL")){
						stack.Push(false);
					}
				}
				m = Regex.Match (expression, @"AN$");
				if (m.Success) {
					expression = expression.Remove (m.Index, m.Value.Length);
					expression = expression.Trim ();
					//consoleText.Buffer.Text += expression + "\n";
					continue;
				}

				m = Regex.Match (expression, @"SUM\s+OF$");
				if (m.Success) {
					expression = expression.Remove (m.Index, m.Value.Length);
					expression = expression.Trim ();
					//consoleText.Buffer.Text += expression + "\n";
					continue;
				}

				break;
			}
			return "Yes!";
		}



	}
}

