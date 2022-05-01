using System.IO;
using System.Diagnostics;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_externalScripts {
  public class Exec : ICommand {
    public string Name { get { return "exec"; } }
    public string Description { get { return "execute arbitrary command or program"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public void Command() {
      if (_InputProperties.Length < 2){
        Console.WriteLine("you need to provide the command to run");
        return;
      }
      string command = _InputProperties[1];
      string args = "";
      for (int i = 2; i < _InputProperties.Length; i++) {
        args = args + " " + _InputProperties[i];
      }
      Console.WriteLine("executing " + command + " " + args + "...");
      if (!runProcess(command, args, data.SavePath)) {
        Console.WriteLine("error executing " + command);
      }
    }

    // run a process with parameters and wait for it to finish
    private bool runProcess(string name, string args, string pwd) {
      try {
        Process p = new Process();
        p.StartInfo.FileName = name;
        p.StartInfo.Arguments = args;
        p.StartInfo.WorkingDirectory = pwd;
        p.Start();
        p.WaitForExit();
        if (p.ExitCode != 0) {
          return false;
        }
      } catch (Exception e) {
        Console.WriteLine("error: " + e.Message);
        return false;
      }
      return true;
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
