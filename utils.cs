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
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
