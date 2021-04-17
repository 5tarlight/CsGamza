namespace CGamza.Item
{
  public interface IUsableItem
  {
    public bool SelfUsable
    {
      get { return false; }
    }

    public bool OnUse(IItemTarget user, IItemTarget target);
  }
}
