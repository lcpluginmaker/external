using ILeoConsole.Core;
using ILeoConsole;

namespace LeoConsole_External
{
  public class Script : ICommand
  {
    public string Name { get; set; }
    public string Description { get { return "external script"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public Script(string name)
    {
      Name = name;
    }

    public void Command()
    {
      string args = "";
      for (int i = 1; i < _InputProperties.Length; i++) {
        args = $"{args} {_InputProperties[i]}";
      }

      if (!Utils.RunProcess(
          Path.Join(data.SavePath, "share", "scripts", Name),
          $"{Utils.EncodeData(data)} {args}",
          data.SavePath))
      {
        Console.WriteLine($"error running {Name}");
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
