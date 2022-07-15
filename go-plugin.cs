using ILeoConsole.Core;
using ILeoConsole;

namespace LeoConsole_External
{
  public class GoPlugin : ICommand
  {
    public string Name { get; set; }
    public string Description { get { return "go plugin"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("not available"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();

    public GoPlugin(string name)
    {
      Name = name;
    }

    public void Command()
    {
      string args = "";
      for (int i = 1; i < _Arguments.Length; i++)
      {
        args = $"{args} {_Arguments[i]}";
      }

      bool exitSuccessful = Utils.RunProcess(
          Path.Join(data.SavePath, "share", "go-plugin", Name),
          $"command {Utils.EncodeData(data)} {args}",
          data.SavePath
          );
      if (!exitSuccessful)
      {
        LConsole.MessageErr0($"cannot execute {Name}");
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
