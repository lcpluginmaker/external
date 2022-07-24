using ILeoConsole.Core;
using ILeoConsole;
using System.Diagnostics;
using System.Text.Json;

namespace LeoConsole_External
{
  public class Utils
  {
    // encode IData to json and then to base64
    public static string EncodeData(IData data)
    {
      AppData d = new AppData();
      d.SavePath = data.SavePath;
      d.DownloadPath = data.DownloadPath;
      d.Version = data.Version;
      try
      {
        d.Username = data.User.name;
      }
      catch (System.NullReferenceException)
      {
        d.Username = "";
      }

      return System.Convert.ToBase64String(
          System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(d))
          );
    }

    // run a process with parameters and wait for it to finish
    public static bool RunProcess(string name, string args, string pwd)
    {
      try
      {
        Process p = new Process();
        p.StartInfo.FileName = name;
        p.StartInfo.Arguments = args;
        p.StartInfo.WorkingDirectory = pwd;
        p.Start();

        p.WaitForExit();
        if (p.ExitCode != 0)
        {
          return false;
        }
      }
      catch (Exception e)
      {
        LConsole.MessageErr0(e.Message);
        return false;
      }
      return true;
    }

    public static string GetOutput(string name, string args, string pwd)
    {
      try
      {
        Process p = new Process();
        p.StartInfo.FileName = name;
        p.StartInfo.Arguments = args;
        p.StartInfo.WorkingDirectory = pwd;
        p.StartInfo.RedirectStandardOutput = true;
        p.Start();

        string data = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        return data;
      }
      catch (Exception e)
      {
        LConsole.MessageErr0(e.Message);
        return "";
      }
      return "";
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
