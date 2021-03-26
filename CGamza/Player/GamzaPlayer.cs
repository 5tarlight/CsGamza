using System;
using CGamza.Entity.Pet;
using CGamza.Terrain;
using CGamza.Terrain.Town;

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
      Pets = new CPet[6] { null, null, null, null, null, null };
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
      return GetPetsCount() >= 6;
    }

    public void AddPet(CPet pet)
    {
      if (!IsPetFull())
        Pets[GetPetsCount()] = pet;
    }
  }
}
