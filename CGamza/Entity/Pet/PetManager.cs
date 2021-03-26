using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.Player;

namespace CGamza.Entity.Pet
{
  #pragma warning disable SYSLIB0011
  public class PetManger
  {
    private const string Dir = "data";
    private const string Suffix = ".pet";

    private static string Path(string name)
    {
      return $"{Dir}/{name}/pet{Suffix}";
    }

    public static void InitEmptyFile(string name)
    {
      PlayerManager.CheckPlayerDir(name);
      var path = Path(name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      var serializer = new BinaryFormatter();

      serializer.Serialize(ws, new List<CPet>());
      ws.Close();
    }
  }
}
