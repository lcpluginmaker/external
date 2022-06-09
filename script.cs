using ILeoConsole.Core;
using ILeoConsole;
using System.Diagnostics;
using System.Text.Json;

namespace LeoConsole_externalScripts {
  public class Script : ICommand {
    public string Name { get; set; }
    public string Description { get { return "external script"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public Script(string name) {
      Name = name;
    }

    public void Command() {
      AppData d = new AppData();
      d.Username = data.User.name;
      d.SavePath = data.SavePath;
      d.DownloadPath = data.DownloadPath;
      d.Version = data.Version;

      string arg = System.Convert.ToBase64String(
          System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(d))
          );
      string args = "";
      for (int i = 1; i < _InputProperties.Length; i++) {
        args = args + " " + _InputProperties[i];
      }

      if (!runProcess(
          Path.Join(data.SavePath, "share", "scripts", Name),
          $"{arg} {args}",
          data.SavePath
        )) {
        Console.WriteLine("error running " + Name);
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
