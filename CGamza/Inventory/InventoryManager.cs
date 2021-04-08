using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.Battle;
using CGamza.Item;
using CGamza.Player;
using CGamza.Util;
using Colorify;

namespace CGamza.Inventory
{
  #pragma warning disable SYSLIB0011
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
      var serializer = new BinaryFormatter();

      serializer.Serialize(ws, new Inventory());
      ws.Close();
    }

    public static Inventory LoadInventory(string name)
    {
      for (int i = 0; i < 5; i++)
      {
        try
        {
          var path = Path(name);
          Stream ws = new FileStream(path, FileMode.OpenOrCreate);
          var deserializer = new BinaryFormatter();

          var result = (Inventory) deserializer.Deserialize(ws);
          ws.Close();

          return result;
        }
        catch (Exception)
        {
          ConsoleUtil.WriteColor("Retrying...");
        }
      }

      return null;
    }

    public static void SaveCurrentInventory()
    {
      if (PlayerManager.CurrentInventory == null) return;

      var path = Path(PlayerManager.CurrentPlayer.Name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();

      serializer.Serialize(ws, PlayerManager.CurrentInventory);
      ws.Close();
    }

    public static void DisplayCurrentInventory(int row = 10)
    {
      var inv = PlayerManager.CurrentInventory;
      if (inv == null) return;
      var page = 0;
      var index = 0;

      while (true)
      {
        refresh:
        {
        }
        Console.Clear();

        var list = inv.Items.Count - 1 >= page * row + row
          ? inv.Items.GetRange(page * row, row)
          : inv.Items.GetRange(page * row, inv.Items.Count - page * row);

        for (var i = 0; i < list.Count; i++)
        {
          var msg = $"{page * row + i + 1}. {list[i].Item.Name} x{list[i].Count}";

          if (i == index)
            ConsoleUtil.WriteColor(msg);
          else
            ConsoleUtil.WriteColor(msg, Colors.txtMuted);
        }

        ConsoleUtil.WriteColor("");
        ConsoleUtil.WriteColor($"\n페이지: {page + 1}, 개수 : {page * row + index + 1}/{inv.Items.Count}");
        ConsoleUtil.WriteColor("↑ ↓ ← →");
        ConsoleUtil.WriteColor("Enter로 선택, Esc로 종료");

        var key = Console.ReadKey().Key;

        if (inv.Items.Count > 0)
        {
          switch (key)
          {
            case ConsoleKey.UpArrow:
              if (index != 0) index--;
              break;
            case ConsoleKey.DownArrow:
              if (index != inv.Items.Count - 1) index++;
              break;
            case ConsoleKey.LeftArrow:
              if (page != 0)
              {
                page--;
                index = 0;
              }

              break;
            case ConsoleKey.RightArrow:
              var maxPage = (int) Math.Ceiling(((double) inv.Items.Count / row)) - 1;
              if (page != maxPage)
              {
                page++;
                index = 0;
              }

              break;
            case ConsoleKey.Enter:
              var result = UseItem(inv.Items[index]);

              if (result)
              {
                ConsoleUtil.WriteColor("아이템을 사용했습니다.");
                ConsoleUtil.Pause();
                goto refresh;
              }
              else
                ConsoleUtil.WriteColor("사용할 수 없습니다.");

              ConsoleUtil.Pause();

              break;
            case ConsoleKey.Escape:
              return;
            case ConsoleKey.D1:
              if (inv.Items.Count > 0)
                index = 0;
              break;
            case ConsoleKey.D2:
              if (inv.Items.Count > 1)
                index = 1;
              break;
            case ConsoleKey.D3:
              if (inv.Items.Count > 2)
                index = 2;
              break;
            case ConsoleKey.D4:
              if (inv.Items.Count > 3)
                index = 3;
              break;
            case ConsoleKey.D5:
              if (inv.Items.Count > 4)
                index = 4;
              break;
            case ConsoleKey.D6:
              if (inv.Items.Count > 5)
                index = 5;
              break;
            case ConsoleKey.D7:
              if (inv.Items.Count > 6)
                index = 6;
              break;
            case ConsoleKey.D8:
              if (inv.Items.Count > 7)
                index = 7;
              break;
            case ConsoleKey.D9:
              if (inv.Items.Count > 8)
                index = 8;
              break;
            case ConsoleKey.D0:
              if (inv.Items.Count > 9)
                index = 9;
              break;
          }
        }
        else
          return;
      }
    }

    public static bool UseItem(Inventory.InventoryItem item, int count = 1)
    {
      if (item.Item is IUsableItem)
      {
        var result = true;
        var usable = item.Item as IUsableItem;

        if (usable == null) return false;

        var target = BattleManager.SelectPet();
        if (target == -1) return false;  

        for (var i = 0; i < count; i++)
          result = result && usable.OnUse(PlayerManager.CurrentPlayer.Pets[target]);
        
        PlayerManager.CurrentInventory.MinusItem(item.Item, count);
        return result;
      }

      return false;
    }
  }
}
