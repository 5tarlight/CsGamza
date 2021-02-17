using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CGamza.util;
using Colorify;

namespace CGamza.Player
{
  public class PlayerManager
  {
    public static GamzaPlayer CurrentPlayer { get; private set; }
    private const string Dir = "data";
    private const string Suffix = ".pdata";

    private static void CheckDir()
    {
      try
      {
        if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
      }
      catch (Exception e)
      {
        Util.WriteColor(e.Message, Colors.txtDanger);
      }
    }

    private static void CheckPlayerDir(string name)
    {
      CheckDir();

      try
      {
        string root = $"{Dir}/{name}";
        if (!Directory.Exists(root)) Directory.CreateDirectory(root);
      }
      catch (Exception e)
      {
        Util.WriteColor(e.Message, Colors.txtDanger);
      }
    }
    
    public static List<string> LoadPlayerList()
    {
      CheckDir();
      var directories = Directory.GetDirectories(Dir);
      var list = new List<string>();

      foreach (var d in directories)
      {
        const string sep = "[FileSep]";
        var v = String.Join(sep, d.Split("/"));
        var r = String.Join(sep, v.Split("\\"));

        var splitted = r.Split(sep);
        var name = splitted[splitted.Length - 1];
        
        list.Add(name);
      }

      return list;
    }
    
    public static void LoadPlayer(string name)
    {
      for (int i = 0; i < 5; i++)
      {
        try
        {
          CheckPlayerDir(name);

          var path = $"{Dir}/{name}/profile{Suffix}";
          Stream ws = new FileStream(path, FileMode.OpenOrCreate);
          BinaryFormatter deserializer = new BinaryFormatter();

          CurrentPlayer = (GamzaPlayer) deserializer.Deserialize(ws);
          ws.Close();

          break;
        }
        catch (Exception)
        {
          Util.WriteColor("Retrying...");
        }
      }
      
      // var formatter = new XmlSerializer(typeof(GamzaPlayer));
      // var fs = new FileStream(path, FileMode.OpenOrCreate);
      // var buffer = new byte[fs.Length];
      // fs.Read(buffer, 0, (int) fs.Length);
      // var ms = new MemoryStream(buffer);
      //
      // CurrentPlayer = (GamzaPlayer) formatter.Deserialize(ms);
      // ms.Close();
    }

    public static void CreatePlayerFile(GamzaPlayer player)
    {
      CheckPlayerDir(player.Name);

      var path = $"{Dir}/{player.Name}/profile{Suffix}";

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();
      
      serializer.Serialize(ws, player);
      ws.Close();

      // FileStream file = new FileStream(path, FileMode.OpenOrCreate);
      // XmlSerializer formatter = new XmlSerializer(typeof(GamzaPlayer));
      // formatter.Serialize(file, player);
      //
      // file.Close();
    }
    
    public static void SaveCurrentPlayer()
    {
      if (CurrentPlayer == null) return;
      
      CheckPlayerDir(CurrentPlayer.Name);

      var path = $"{Dir}/{CurrentPlayer.Name}/profile{Suffix}";

      Stream ws = new FileStream(path, FileMode.OpenOrCreate);
      BinaryFormatter serializer = new BinaryFormatter();
      
      serializer.Serialize(ws, CurrentPlayer);
      ws.Close();
      // FileStream file = new FileStream(path, FileMode.OpenOrCreate);
      // XmlSerializer formatter = new XmlSerializer(typeof(GamzaPlayer));
      // formatter.Serialize(file, CurrentPlayer);
      //
      // file.Close();
    }
    
    public static void PrintCurrnetPlayerInfo()
    {
      Util.WriteColor($"이름 : {CurrentPlayer.Name}");
      Util.WriteColor(CurrentPlayer.Profile);
      Console.WriteLine();
      Util.WriteColor($"체력 : {CurrentPlayer.Health} / {CurrentPlayer.MaxHealth}");
      Util.WriteColor($"레벨 : {CurrentPlayer.Level}");
      Util.WriteColor($"경험치 : {CurrentPlayer.Exp}");
      Util.WriteColor($"다음 레벨까지 {CurrentPlayer.GetNeedExpForNextLvl()} 남음");
    }
  }
}
