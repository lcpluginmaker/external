using System.IO;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_externalScripts
{

  public class ConsoleData : IData
  {
    public static User _User;
    public User User { get { return _User; } set { _User = value; } }
    public static string _SavePath;
    public string SavePath { get { return _SavePath; } set { _SavePath = value; } }
    public static string _DownloadPath;
    public string DownloadPath { get { return _DownloadPath; } set { _DownloadPath = value; } }
  }
  
  public class ExternalScripts : IPlugin
  {
    public string Name { get { return "external-scripts"; } }
    public string Explanation { get { return "run external scripts or programs"; } }
    
    private IData _data;
    public IData data { get { return _data; } set { _data = value; } }
    
    private List<ICommand> _Commands;
    public List<ICommand> Commands { get { return _Commands; } set { _Commands = value; } }
    
    public void PluginMain()
    {
      _data = new ConsoleData();
      
      _Commands = new List<ICommand>();
      _Commands.Add(new Script());
      _Commands.Add(new ListScripts());
      _Commands.Add(new Exec());

      // TODO SavePath is not set yet, see https://github.com/BoettcherDasOriginal/LeoConsole/issues/12
      if (!Directory.Exists(Path.Join(_data.SavePath, "scripts"))) {
        Console.WriteLine("creating scripts directory...");
        try {
          Directory.CreateDirectory(Path.Join(_data.SavePath, "scripts"));
        } catch (Exception e) {
          Console.WriteLine("WARNING: cannot create scripts dir: " + e.Message);
        }
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
