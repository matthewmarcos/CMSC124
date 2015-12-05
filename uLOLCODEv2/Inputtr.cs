using System;

namespace uLOLCODEv2
{
	public partial class Inputtr : Gtk.Dialog
	{
		public Inputtr ()
		{	this.Title = "Input";
			this.Build ();
		
		}

		protected void inputSEND (object sender, EventArgs e)
		{	
			EvalClass.inputVALUE = inputBox.Text;
			this.Destroy ();
		}
	}
}

