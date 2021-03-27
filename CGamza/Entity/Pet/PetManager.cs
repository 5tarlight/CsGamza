using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.Player;
using CGamza.Util;
using Colorify;

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

    public static List<CPet> LoadPetShell(string name)
    {
      for (int i = 0; i < 5; i++)
      {
        try
        {
          var path = Path(name);
          Stream ws = new FileStream(path, FileMode.OpenOrCreate);
          var deserializer = new BinaryFormatter();

          var result = (List<CPet>) deserializer.Deserialize(ws);
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

    public static void AddPet(string name, CPet pet)
    {
      var current = LoadPetShell(name);
      var path = Path(name);

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      var serializer = new BinaryFormatter();

      current.Add(pet);
      serializer.Serialize(ws, current);
      ws.Close();
    }

    public static void DisplayPet(int row = 10)
    {
      var pets = LoadPetShell(PlayerManager.CurrentPlayer.Name);
      if (pets == null) return;

      var page = 0;
      var index = 0;

      while (true)
      {
        Console.Clear();

        var list = pets.Count - 1 >= page * row + row
          ? pets.GetRange(page * row, row)
          : pets.GetRange(page * row, pets.Count - page * row);

        for (var i = 0; i < list.Count; i++)
        {
          var msg = $"{page * row + i + 1}. {list[i].Name}";

          if (i == index)
            ConsoleUtil.WriteColor(msg);
          else
            ConsoleUtil.WriteColor(msg, Colors.txtMuted);
        }

        ConsoleUtil.WriteColor("");
        ConsoleUtil.WriteColor($"\n페이지: {page + 1}, 개수 : {page * row + index + 1}/{pets.Count}");
        ConsoleUtil.WriteColor("↑ ↓ ← →");
        ConsoleUtil.WriteColor("Enter로 선택, Esc로 종료");

        var key = Console.ReadKey().Key;

        if (pets.Count > 0)
        {
          switch(key)
          {
            case ConsoleKey.UpArrow:
              if (index != 0) index--;
              break;
            case ConsoleKey.DownArrow:
              if (index != pets.Count - 1) index++;
              break;
            case ConsoleKey.LeftArrow:
              if (page != 0)
              {
                page--;
                index = 0;
              }

              break;
            case ConsoleKey.RightArrow:
              var maxPage = (int) Math.Ceiling(((double) pets.Count / row)) - 1;
              if (page != maxPage)
              {
                page++;
                index = 0;
              }

              break;
            case ConsoleKey.Enter:
              // TODO
              break;
            case ConsoleKey.Escape:
              return;
            case ConsoleKey.D1:
              if (pets.Count > 0)
                index = 0;
              break;
            case ConsoleKey.D2:
              if (pets.Count > 1)
                index = 1;
              break;
            case ConsoleKey.D3:
              if (pets.Count > 2)
                index = 2;
              break;
            case ConsoleKey.D4:
              if (pets.Count > 3)
                index = 3;
              break;
            case ConsoleKey.D5:
              if (pets.Count > 4)
                index = 4;
              break;
            case ConsoleKey.D6:
              if (pets.Count > 5)
                index = 5;
              break;
            case ConsoleKey.D7:
              if (pets.Count > 6)
                index = 6;
              break;
            case ConsoleKey.D8:
              if (pets.Count > 7)
                index = 7;
              break;
            case ConsoleKey.D9:
              if (pets.Count > 8)
                index = 8;
              break;
            case ConsoleKey.D0:
              if (pets.Count > 9)
                index = 9;
              break;
          }
        }
        else
          return;
      }
    }
  }
}
