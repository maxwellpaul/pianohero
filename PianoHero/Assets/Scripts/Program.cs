using System.Diagnostics;

class Program {

	string fileName;
	string arguments;

	public Program(string fn, string arg) {
		fileName = fn;
		arguments = arg;
	}

	public void LaunchCommandLineApp() {
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.CreateNoWindow = false;
		startInfo.UseShellExecute = false;
		startInfo.FileName = fileName;
		startInfo.WindowStyle = ProcessWindowStyle.Hidden;
		startInfo.Arguments = arguments;

		try {
			System.Console.WriteLine("Executing " + fileName + " with " + arguments);
			using (Process exeProcess = Process.Start(startInfo)) {
				exeProcess.WaitForExit();
			}
		}
		catch {
			System.Console.WriteLine ("Error Occured in exe: " + fileName + " with " + arguments);
		}
	}
}
