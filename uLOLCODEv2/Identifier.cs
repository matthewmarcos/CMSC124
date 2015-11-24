using System;
using Gtk;
using System.Text.RegularExpressions;

namespace uLOLCODEv2
{
	public class Identifier
	{
		public Identifier ()
		{
		}
		public String[,] getLineType(String line, TextView consoleText) {
			int currentLength = 0;

			String[,] tempString = new String[2,2];

			Match m;
			while (line.Length != 0) {
				consoleText.Buffer.Text += "Lexeme length: " + line.Length + "\n";

				/* I HAS A <varident> */
				m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s+[a-zA-Z][a-zA-Z\d]*\s*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "varDecNoInit";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* I HAS A <varident> ITZ <expression>
				 * SUGGESTION: yung sa after ng ITZ gawan ng isa pang class? for expression.
				 * REASONS:
				 * 1. Pwede pa kasi yun magamit for arithmetic/boolean ops at conditional
				 * loop/switch/function statements
				 * 2. Mas masusunod ang BNF structure natin
				 * 3. General ang pag-identify ng lexemes e.g. SUM OF <exp> AN <exp> ay kaya
				 * 	  na niyang ma-detect kung var, expression ulit, numbr/numbar, etc.
				 */
				m = Regex.Match (line, @"^\s*I\s+HAS\s+A\s+[a-zA-Z][a-zA-Z\d]*\s+ITZ\s+[""a-zA-Z\d]*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "varDecWithInit";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* <varident> R <expression> */
				m = Regex.Match (line, @"^\s*[a-zA-Z][a-zA-Z\d]*\s+R\s+[""a-zA-Z\d]*$");
				// need to improve lexical detection for strings
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "assValToVar";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* BTW <comments> */
				m = Regex.Match (line, @"^\s*BTW\s+.*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "oneLineComm";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* FOR MULTI LINE COMMENTS
				 * Hindi po pwede na may kasama si OBTW at TLDR sa line nila,
				 * kahit yung comment mismo ay hindi pwede.
				 * Okay? Okay.
				 */
				m = Regex.Match (line, @"^\s*OBTW\s*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "startOfMulComm";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* FOR MULTI LINE COMMENTS
				 * Hindi po pwede na may kasama si OBTW at TLDR sa line nila,
				 * kahit yung comment mismo ay hindi pwede.
				 * Okay? Okay. :)
				 */
				m = Regex.Match (line, @"^\s*TLDR\s*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "endOfMulComm";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* SUM OF <expression> AN <expression>
				 * SUM lang  muna ginawa ko para hindi humaba agad yung code.
				 * Okay? Okay. :)
				 */
				m = Regex.Match (line, @"^\s*SUM\s+OF\s+[""a-zA-Z\d]*\s+AN\s+[""a-zA-Z\d]*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "addOp";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* BOTH OF <expression> AN <expression>
				 * BOTH lang muna ginawa ko para hindi humaba agad yung code.
				 * Okay? Okay. :)
				 */
				m = Regex.Match (line, @"^\s*BOTH\s+OF\s+[""a-zA-Z\d]*\s+AN\s+[""a-zA-Z\d]*$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "andOp";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}

				/* SMOOSH <var1> <var2> <var3> MKAY
				 * Gagana lang ito pag 3 vars.
				 */
				m = Regex.Match (line, @"^\s*SMOOSH\s+[""a-zA-Z\d]*\s+[""a-zA-Z\d]*\s+[""a-zA-Z\d]*\s+MKAY$");
				if (m.Success) {
					tempString [currentLength, 0] = m.Value;
					tempString [currentLength, 1] = "strConcat";
					currentLength += 1;
					line = line.Remove (m.Index, m.Index + m.Length);
					consoleText.Buffer.Text += tempString[currentLength-1, 0] + "\n";
					consoleText.Buffer.Text += tempString[currentLength-1, 1] + "\n";
					consoleText.Buffer.Text += "Remaining Lexeme Length: " + line.Length;
					continue;
				}



				consoleText.Buffer.Text += "Error 404: Not Found!!";
				tempString [currentLength, 0] = "";
				tempString [currentLength, 1] = "Error!";
				return tempString;
			}
			return tempString;
		}
	}
}

