using System;
using System.Collections.Generic;
using CGamza.Item;
using System.Linq;

namespace CGamza.Inventory
{
  [Serializable]
  public class Inventory
  {
    [Serializable]
    public class InventoryItem
    {
      public CItem Item { get; }
      public int Count { get; set; }

      public InventoryItem(CItem item, int count)
      {
        Item = item;
        Count = count;
      }

      public InventoryItem(InventoryItem item) : this(item.Item, item.Count)
      {
      }
    }

    public List<InventoryItem> Items;
    public int Limit { get; }

    public Inventory()
    {
      Limit = 100;
      Items = new List<InventoryItem>();
    }

    public bool Has(CItem item)
    {
      var has = from i in Items
        where i.Item.Name.Equals(item.Name)
        select i;

      return has.Any();
    }

    public void AddItem(CItem item, int count)
    {
      if (Has(item))
      {
        var index = Items.FindIndex(pi => pi.Item.Equals(item));
        var current = new InventoryItem(Items[index]);
        current.Count += count;

        Items[index] = current;
      }
      else
      {
        var invItem = new InventoryItem(item, count);
        Items.Add(invItem);
      }
    }

    public int GetCount(CItem item)
    {
      if (!Has(item)) return 0;

      var index = Items.FindIndex(i => i.Item.Name == item.Name);

      return Items[index].Count;
    }
    
    public bool MinusItem(CItem item, int count)
    {
      if (!Has(item)) return false;

      var limit = GetCount(item);
      if (count > limit) return false;
      
      AddItem(item, -count);
      TrimInv();
      return true;
    }

    private void TrimInv()
    {
      foreach (var v in Items)
      {
        if (v.Count == 0)
          Items.Remove(v);
      }
    }
  }
}
