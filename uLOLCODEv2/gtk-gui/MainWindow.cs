
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VPaned vpaned5;
	
	private global::Gtk.VPaned vpaned6;
	
	private global::Gtk.HPaned hpaned7;
	
	private global::Gtk.Label Code;
	
	private global::Gtk.HPaned hpaned8;
	
	private global::Gtk.Button fileOpener;
	
	private global::Gtk.HPaned hpaned9;
	
	private global::Gtk.Label label2;
	
	private global::Gtk.Label label3;
	
	private global::Gtk.HBox hbox9;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TextView inputCode;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	
	private global::Gtk.TreeView treeview1;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow2;
	
	private global::Gtk.TreeView treeview2;
	
	private global::Gtk.VPaned vpaned7;
	
	private global::Gtk.Button button1;
	
	private global::Gtk.VPaned vpaned8;
	
	private global::Gtk.Label label4;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow3;
	
	private global::Gtk.TextView consoleText;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vpaned5 = new global::Gtk.VPaned ();
		this.vpaned5.CanFocus = true;
		this.vpaned5.Name = "vpaned5";
		this.vpaned5.Position = 439;
		// Container child vpaned5.Gtk.Paned+PanedChild
		this.vpaned6 = new global::Gtk.VPaned ();
		this.vpaned6.CanFocus = true;
		this.vpaned6.Name = "vpaned6";
		this.vpaned6.Position = 48;
		// Container child vpaned6.Gtk.Paned+PanedChild
		this.hpaned7 = new global::Gtk.HPaned ();
		this.hpaned7.CanFocus = true;
		this.hpaned7.Name = "hpaned7";
		this.hpaned7.Position = 149;
		// Container child hpaned7.Gtk.Paned+PanedChild
		this.Code = new global::Gtk.Label ();
		this.Code.Name = "Code";
		this.Code.LabelProp = global::Mono.Unix.Catalog.GetString ("Code");
		this.hpaned7.Add (this.Code);
		global::Gtk.Paned.PanedChild w1 = ((global::Gtk.Paned.PanedChild)(this.hpaned7 [this.Code]));
		w1.Resize = false;
		// Container child hpaned7.Gtk.Paned+PanedChild
		this.hpaned8 = new global::Gtk.HPaned ();
		this.hpaned8.CanFocus = true;
		this.hpaned8.Name = "hpaned8";
		this.hpaned8.Position = 166;
		// Container child hpaned8.Gtk.Paned+PanedChild
		this.fileOpener = new global::Gtk.Button ();
		this.fileOpener.CanFocus = true;
		this.fileOpener.Name = "fileOpener";
		this.fileOpener.UseUnderline = true;
		this.fileOpener.Label = global::Mono.Unix.Catalog.GetString ("Open File");
		this.hpaned8.Add (this.fileOpener);
		global::Gtk.Paned.PanedChild w2 = ((global::Gtk.Paned.PanedChild)(this.hpaned8 [this.fileOpener]));
		w2.Resize = false;
		// Container child hpaned8.Gtk.Paned+PanedChild
		this.hpaned9 = new global::Gtk.HPaned ();
		this.hpaned9.CanFocus = true;
		this.hpaned9.Name = "hpaned9";
		this.hpaned9.Position = 344;
		// Container child hpaned9.Gtk.Paned+PanedChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Lexemes");
		this.hpaned9.Add (this.label2);
		global::Gtk.Paned.PanedChild w3 = ((global::Gtk.Paned.PanedChild)(this.hpaned9 [this.label2]));
		w3.Resize = false;
		// Container child hpaned9.Gtk.Paned+PanedChild
		this.label3 = new global::Gtk.Label ();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Symbol Table");
		this.hpaned9.Add (this.label3);
		this.hpaned8.Add (this.hpaned9);
		this.hpaned7.Add (this.hpaned8);
		this.vpaned6.Add (this.hpaned7);
		global::Gtk.Paned.PanedChild w7 = ((global::Gtk.Paned.PanedChild)(this.vpaned6 [this.hpaned7]));
		w7.Resize = false;
		// Container child vpaned6.Gtk.Paned+PanedChild
		this.hbox9 = new global::Gtk.HBox ();
		this.hbox9.Name = "hbox9";
		this.hbox9.Spacing = 6;
		// Container child hbox9.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.inputCode = new global::Gtk.TextView ();
		this.inputCode.CanFocus = true;
		this.inputCode.Name = "inputCode";
		this.GtkScrolledWindow.Add (this.inputCode);
		this.hbox9.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.GtkScrolledWindow]));
		w9.Position = 0;
		// Container child hbox9.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.treeview1 = new global::Gtk.TreeView ();
		this.treeview1.CanFocus = true;
		this.treeview1.Name = "treeview1";
		this.GtkScrolledWindow1.Add (this.treeview1);
		this.hbox9.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.GtkScrolledWindow1]));
		w11.Position = 1;
		// Container child hbox9.Gtk.Box+BoxChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.treeview2 = new global::Gtk.TreeView ();
		this.treeview2.CanFocus = true;
		this.treeview2.Name = "treeview2";
		this.GtkScrolledWindow2.Add (this.treeview2);
		this.hbox9.Add (this.GtkScrolledWindow2);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.GtkScrolledWindow2]));
		w13.Position = 2;
		this.vpaned6.Add (this.hbox9);
		this.vpaned5.Add (this.vpaned6);
		global::Gtk.Paned.PanedChild w15 = ((global::Gtk.Paned.PanedChild)(this.vpaned5 [this.vpaned6]));
		w15.Resize = false;
		// Container child vpaned5.Gtk.Paned+PanedChild
		this.vpaned7 = new global::Gtk.VPaned ();
		this.vpaned7.CanFocus = true;
		this.vpaned7.Name = "vpaned7";
		this.vpaned7.Position = 29;
		// Container child vpaned7.Gtk.Paned+PanedChild
		this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("Execute");
		this.vpaned7.Add (this.button1);
		global::Gtk.Paned.PanedChild w16 = ((global::Gtk.Paned.PanedChild)(this.vpaned7 [this.button1]));
		w16.Resize = false;
		// Container child vpaned7.Gtk.Paned+PanedChild
		this.vpaned8 = new global::Gtk.VPaned ();
		this.vpaned8.CanFocus = true;
		this.vpaned8.Name = "vpaned8";
		this.vpaned8.Position = 30;
		// Container child vpaned8.Gtk.Paned+PanedChild
		this.label4 = new global::Gtk.Label ();
		this.label4.Name = "label4";
		this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Console");
		this.vpaned8.Add (this.label4);
		global::Gtk.Paned.PanedChild w17 = ((global::Gtk.Paned.PanedChild)(this.vpaned8 [this.label4]));
		w17.Resize = false;
		// Container child vpaned8.Gtk.Paned+PanedChild
		this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
		this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
		this.consoleText = new global::Gtk.TextView ();
		this.consoleText.CanFocus = true;
		this.consoleText.Name = "consoleText";
		this.GtkScrolledWindow3.Add (this.consoleText);
		this.vpaned8.Add (this.GtkScrolledWindow3);
		this.vpaned7.Add (this.vpaned8);
		this.vpaned5.Add (this.vpaned7);
		this.Add (this.vpaned5);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1024;
		this.DefaultHeight = 650;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.button1.Clicked += new global::System.EventHandler (this.executeCode);
	}
}
