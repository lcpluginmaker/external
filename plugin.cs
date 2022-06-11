using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_External {

  public class ConsoleData : IData {
    public static User _User;
    public User User { get { return _User; } set { _User = value; } }
    public static string _SavePath;
    public string SavePath { get { return _SavePath; } set { _SavePath = value; } }
    public static string _DownloadPath;
    public string DownloadPath { get { return _DownloadPath; } set { _DownloadPath = value; } }
    public static string _Version;
    public string Version { get { return _Version; } set { _Version = value; } }
    public static string _CurrentWorkingPath;
    public string CurrentWorkingPath { get { return _CurrentWorkingPath; } set { _CurrentWorkingPath = value; } }
  }
  
  public class ExternalScripts : IPlugin {
    public string Name { get { return "external"; } }
    public string Explanation { get { return "run go plugins, external scripts or programs"; } }
    
    private IData _data;
    public IData data { get { return _data; } set { _data = value; } }
    
    private List<ICommand> _Commands;
    public List<ICommand> Commands { get { return _Commands; } set { _Commands = value; } }

    private List<string> GoPlugins;

    public void PluginInit() {
      _data = new ConsoleData();
      _Commands = new List<ICommand>();
      GoPlugins = new List<string>();
    }

    public void RegisterCommands() {
      _Commands.Add(new ListScripts());
      _Commands.Add(new Exec());

      if (!Directory.Exists(Path.Join(_data.SavePath, "share", "scripts"))) {
        Console.WriteLine("error: scripts folder does not exist!");
        return;
      }
      if (!Directory.Exists(Path.Join(_data.SavePath, "share", "go-plugin"))) {
        Console.WriteLine("error: go plugins folder does not exist!");
        return;
      }

      foreach (string s in Directory.GetFiles(Path.Join(_data.SavePath, "share", "scripts"))) {
        _Commands.Add(new Script(Path.GetFileName(s)));
      }
      foreach (string s in Directory.GetFiles(Path.Join(_data.SavePath, "share", "go-plugin"))) {
        _Commands.Add(new GoPlugin(Path.GetFileName(s)));
        GoPlugins.Add(Path.GetFileName(s));
      }
    }
    
    public void PluginMain() {
      RunGoPlugins("main");
    }

    public void PluginShutdown() {
      RunGoPlugins("shutdown");
    }

    private void RunGoPlugins(string inducement) {
      foreach (string gp in GoPlugins) {
        string fullPath = Path.Join(_data.SavePath, "share", "go-plugin", gp);
        string pwd;
        if (string.IsNullOrEmpty(_data.CurrentWorkingPath)) {
          pwd = _data.SavePath;
        } else {
          pwd = _data.CurrentWorkingPath;
        }
        string args = $"{inducement} {Utils.EncodeData(_data)}";
        Utils.RunProcess(fullPath, args, pwd);
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
