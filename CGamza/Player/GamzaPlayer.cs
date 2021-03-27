using System;
using CGamza.Entity.Pet;
using CGamza.Terrain;
using CGamza.Terrain.Town;
using CGamza.Util;

namespace CGamza.Player
{
  [Serializable]
  public class GamzaPlayer : IPlayer
  {
    public string Name { get; set; }
    public string Profile { get; set; }
    public long Money { get; set; }
    public Location Location { get; }
    public CPet[] Pets { get; }

    public GamzaPlayer() : this("unknown-user", "invalid-user")
    {
    }
    
    public GamzaPlayer(string name, string profile)
    {
      Name = name;
      Profile = profile;
      Location = new Location(new BeginningVillage());
      Pets = new CPet[4] { null, null, null, null };
    }

    public int GetPetsCount()
    {
      var count = 0;
      foreach (var pet in Pets)
      {
        if (pet != null) count++;
      }

      return count;
    }

    public bool IsPetFull()
    {
      return GetPetsCount() >= 4;
    }

    public void AddPet(CPet pet)
    {
      if (!IsPetFull())
        Pets[GetPetsCount()] = pet;
      else
      {
        PetManger.AddPet(PlayerManager.CurrentPlayer.Name, pet);
        ConsoleUtil.WriteColor("펫쉘로 보내졌습니다.");
        ConsoleUtil.Pause();
      }
    }
  }
}
