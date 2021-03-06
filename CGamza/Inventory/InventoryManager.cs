using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CGamza.Inventory
{
  public class InventoryManager
  {
    private const string Dir = "data";
    private const string Suffix = ".idata";

    public static void initEmptyInventory(string name)
    {
      var path = $"{Dir}/{name}/inventory{Suffix}";
      
      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();
      
      serializer.Serialize(ws, new Inventory());
      ws.Close();
    }
  }
}
