using System;

namespace CGamza.Item
{
  [Serializable]
  public class CItem
  {
    public string Name { get; }

    public CItem(string name)
    {
      Name = name;
    }
    
    public bool Equals(CItem item)
    {
      return Name == item.Name;
    }
  }
}
