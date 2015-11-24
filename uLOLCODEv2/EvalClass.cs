using System;
using Gtk;
using uLOLCODEv2;
using System.Collections;

namespace uLOLCODEv2
{
	public class EvalClass
	{
		public EvalClass ()
		{
		}

		public void varDecNoInit(Hashtable symbolTable, Gtk.ListStore lexmodel, String statement) {
			//Add lexemes to lexmodel
			//Add to symbol table
			//check if variable already exists in symbolTable
			symbolTable.Add("it is", "working");
		}
	}
}

