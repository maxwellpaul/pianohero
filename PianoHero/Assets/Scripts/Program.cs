using System.Diagnostics;
using UnityEngine;

class Program {

	string fileName;
	string argument;

	public Program(string fn, string arg) {
		fileName = fn;
		argument = arg;
	}

	public void LaunchCommandLineApp() {
		ProcessStartInfo psi = new ProcessStartInfo (fileName, argument);
		psi.CreateNoWindow = true;
		psi.UseShellExecute = false;

		Process process = Process.Start (psi);
		process.WaitForExit ();
		process.Close ();
	}
}
