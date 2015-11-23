using System;
using Gtk;

namespace uLOLCODEv2
{
	public class EmptyClass
	{
		public EmptyClass ()
		{
		}
		public String printHello() {
			return "I like to hintai!";
		}
		public void accessTextView(TextView nigga,String stuff){

			nigga.Buffer.Text += stuff;

		}
	}
}

