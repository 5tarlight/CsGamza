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

    public List<InventoryItem> items;
    public int Limit { get; }

    public Inventory()
    {
      Limit = 100;
      items = new List<InventoryItem>();
    }

    public bool Has(CItem item)
    {
      var has = from i in items
        where i.Item.Name.Equals(item.Name)
        select i;

      return has.Any();
    }

    public void addItem(CItem item, int count)
    {
      if (Has(item))
      {
        var index = items.FindIndex(item => item.Item.Equals(item));
        var current = new InventoryItem(items[index]);
        current.Count += count;

        items[index] = current;
      }
      else
      {
        var invItem = new InventoryItem(item, count);
        items.Add(invItem);
      }
    }
  }
}
