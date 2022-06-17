using ILeoConsole;
using ILeoConsole.Core;

namespace LeoConsole_External
{
  public class Exec : ICommand
  {
    public string Name { get { return "exec"; } }
    public string Description { get { return "execute arbitrary command or program"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    public Action HelpFunktion { get { return () => Console.WriteLine("haha"); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public void Command()
    {
      if (_InputProperties.Length < 2)
      {
        Console.WriteLine("error: you need to provide the command to run");
        return;
      }

      string command = _InputProperties[1];
      string args = "";
      for (int i = 2; i < _InputProperties.Length; i++)
      {
        args = $"{args} {_InputProperties[i]}";
      }

      Console.WriteLine($"executing {command} {args}...");
      if (!Utils.RunProcess(command, args, data.SavePath))
      {
        Console.WriteLine($"error executing {command}");
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
