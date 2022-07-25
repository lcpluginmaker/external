using ILeoConsole.Core;
using ILeoConsole;
using System.Text.Json;

namespace LeoConsole_External
{
  public class PluginDataJson {
    public string Description { get; set; }
  }

  public class GoPlugin : ICommand
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("not available"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();

    public GoPlugin(string name, string savePath)
    {
      Name = name;
      Description = JsonSerializer.Deserialize<PluginDataJson>(
          Processes.CheckOutput(Path.Join(savePath, "share", "go-plugin", Name), $"init {Utils.EncodeData(data)}", savePath)
          ).Description;
    }

    public void Command()
    {
      string args = "";
      for (int i = 1; i < _Arguments.Length; i++)
      {
        args = $"{args} {_Arguments[i]}";
      }

      int exitCode = Processes.Run(
          Path.Join(data.SavePath, "share", "go-plugin", Name),
          $"command {Utils.EncodeData(data)} {args}",
          data.SavePath
          );
      if (exitCode != 0)
      {
        LConsole.MessageErr0($"cannot execute {Name}");
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
