using System.IO;
using System.Diagnostics;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_externalScripts {
  public class Script : ICommand {
    public string Name { get { return "script"; } }
    public string Description { get { return "run script"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public void Command() {
      if (_InputProperties.Length < 2){
        Console.WriteLine("you need to provide the script name to run");
        return;
      }
      Console.WriteLine("running " + _InputProperties[1] + "...");
      if (!runProcess(
            Path.Join(data.SavePath, "scripts", _InputProperties[1]),
            "",
            data.SavePath)
          ) {
        Console.WriteLine("error running " + _InputProperties[1]);
      }
    }

    // run a process with parameters and wait for it to finish
    private bool runProcess(string name, string args, string pwd) {
      try {
        Process p = new Process();
        p.StartInfo.FileName = name;
        p.StartInfo.Arguments = args;
        p.StartInfo.WorkingDirectory = pwd;
        p.StartInfo.RedirectStandardInput = true;
        p.Start();

        // send all the information to the script via stdin
        StreamWriter dataWriter = p.StandardInput;
        dataWriter.WriteLine(data.User.name);
        dataWriter.WriteLine(data.SavePath);
        dataWriter.WriteLine(data.DownloadPath);
        foreach (string arg in _InputProperties) {
          dataWriter.Write(arg);
          dataWriter.Write(" ");
        }
        dataWriter.WriteLine("");
        dataWriter.Close();

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
