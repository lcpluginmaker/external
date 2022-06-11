using ILeoConsole;
using ILeoConsole.Core;

namespace LeoConsole_External {
  public class ListScripts : ICommand {
    public string Name { get { return "scripts-list"; } }
    public string Description { get { return "list installed scripts"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public void Command() {
      try {
        foreach (string filename in Directory.GetFiles(Path.Join(data.SavePath, "share", "scripts"))) {
          Console.WriteLine(Path.GetFileName(filename));
        }
      } catch (Exception e) {
        Console.WriteLine("error: " + e.Message);
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
