/*
 * Created by: Mon, Matt, and James
 * 
 */
using System;
using Gtk;
using uLOLCODEv2;

public partial class MainWindow: Gtk.Window
{		
	Gtk.ListStore lexModel = new Gtk.ListStore(typeof(string),typeof(string));
	Gtk.ListStore symbolTable = new Gtk.ListStore(typeof(string),typeof(string));
	EmptyClass shizz = new EmptyClass ();
	Identifier ident = new Identifier();
	//EvalClass eval = new EvalClass (symbolTable);

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		populateTreeView ();

	}

	public void populateTreeView(){
		//instantiate 2 columns

		Gtk.TreeViewColumn lexemeCol = new Gtk.TreeViewColumn ();
		lexemeCol.Title = "Lexeme";

		Gtk.TreeViewColumn classificationCol = new Gtk.TreeViewColumn ();
		classificationCol.Title = "Classification";

		//add the 2 columns to the treeview
		treeview1.AppendColumn (lexemeCol);
		treeview1.AppendColumn (classificationCol);

		treeview1.Model = lexModel;
		Gtk.CellRendererText lexemeCell = new Gtk.CellRendererText ();
		lexemeCol.PackStart (lexemeCell, true);
		Gtk.CellRendererText classCell = new Gtk.CellRendererText ();
		classificationCol.PackStart (classCell, true);

		lexemeCol.AddAttribute (lexemeCell, "text", 0);
		classificationCol.AddAttribute (classCell, "text", 1);


		//===================================================//

		Gtk.TreeViewColumn identifierCol = new Gtk.TreeViewColumn ();
		identifierCol.Title = "Identifier";

		Gtk.TreeViewColumn valueCol = new Gtk.TreeViewColumn ();
		valueCol.Title = "Value";

		//add the 2 columns to the treeview
		treeview2.AppendColumn (identifierCol);
		treeview2.AppendColumn (valueCol);

		treeview2.Model = symbolTable;
		Gtk.CellRendererText identifierCell = new Gtk.CellRendererText ();
		identifierCol.PackStart (identifierCell, true);
		Gtk.CellRendererText valueCell = new Gtk.CellRendererText ();
		valueCol.PackStart (valueCell, true);

		identifierCol.AddAttribute (identifierCell, "text", 0);
		valueCol.AddAttribute (valueCell, "text", 1);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void executeCode (object sender, EventArgs e)
	{
		consoleText.Buffer.Text = "";
		String code = inputCode.Buffer.Text;
		String[,] codeLabels;
		lexModel.Clear ();
		symbolTable.Clear ();
		//shizz.accessTextView(inputCode, "Hello");
		char[] splitToken = {'\n'};
		String[] lines = code.Split (splitToken);
		for(var i = 0 ;i < lines.Length ; i++) {
			codeLabels = ident.getLineType (lines[i], consoleText);

			resolveLabels (codeLabels);
			//if (codeLabels [codeLabels.Rank - 1, 1].Equals ("Error!")) {
	//			continue;
	//		}
			//resolveLabels (codeLabels);
			//drawSymbolTree()
		}
	}

	protected void resolveLabels(String[,] codeLabels) {
		for(var j = 0 ; j < codeLabels.Rank ; j++) {
			consoleText.Buffer.Text += "Statement: " +
				codeLabels [j, 0] + " | TYPE: " + codeLabels[j, 1] +  "\n";
		}
	}

	protected void openCODE (object sender, EventArgs e)
	{
		FileChooserDialog fileOpen = new FileChooserDialog (
			"Open File",
			this,
			FileChooserAction.Open,
			"Cancel", ResponseType.Cancel,		//for cancel button in file dialog
			"Open", ResponseType.Accept);		//for open button 

		if( fileOpen.Run() == (int)ResponseType.Accept){
			//open file for reading

			System.IO.StreamReader file = System.IO.File.OpenText(fileOpen.Filename);	//open the file.

			//display in the textbox
			inputCode.Buffer.Text = file.ReadToEnd();

		}
		fileOpen.Destroy();
	}
}
