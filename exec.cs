using ILeoConsole;
using ILeoConsole.Core;

namespace LeoConsole_External
{
  public class Exec : ICommand
  {
    public string Name { get { return "exec"; } }
    public string Description { get { return "execute arbitrary command or program"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("not available"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();

    public void Command()
    {
      if (_Arguments.Length < 2)
      {
        LConsole.MessageErr0("you need to provide the command to run");
        return;
      }

      string command = _Arguments[1];
      string args = "";
      for (int i = 2; i < _Arguments.Length; i++)
      {
        args = $"{args} {_Arguments[i]}";
      }

      LConsole.MessageSuc0($"executing {command} {args}...");
      if (!Utils.RunProcess(command, args, data.SavePath))
      {
        LConsole.MessageErr0($"cannot execute {command}");
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
