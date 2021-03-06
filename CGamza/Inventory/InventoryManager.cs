using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.Player;
using CGamza.util;

namespace CGamza.Inventory
{
  public class InventoryManager
  {
    private const string Dir = "data";
    private const string Suffix = ".idata";

    private static string Path(string name)
    {
      return $"{Dir}/{name}/inventory{Suffix}";
    }
    
    public static void initEmptyInventory(string name)
    {
      var path = Path(name);
      
      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();
      
      serializer.Serialize(ws, new Inventory());
      ws.Close();
    }

    public static Inventory loadInventory(string name)
    {
      for (int i = 0; i < 5; i++)
      {
        try
        {
          var path = Path(name);
          Stream ws = new FileStream(path, FileMode.OpenOrCreate);
          BinaryFormatter deserializer = new BinaryFormatter();

          var result = (Inventory) deserializer.Deserialize(ws);
          ws.Close();

          return result;
        }
        catch (Exception)
        {
          Util.WriteColor("Retrying...");
        }
      }

      return null;
    }

    public static void saveCurrentInventory()
    {
      if (PlayerManager.CurrentInventory == null) return;
      
      var path = Path(PlayerManager.CurrentPlayer.Name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();
      
      serializer.Serialize(ws, PlayerManager.CurrentInventory);
      ws.Close();
    }
  }
}
