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
		consoleText.Buffer.Text += shizz.printHello();
		lexModel.Clear ();
		symbolTable.Clear ();
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
