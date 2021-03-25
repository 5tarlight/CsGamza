using static CGamza.Entity.EntityType;

namespace CGamza.Entity
{
  public enum EntityType
  {
    NORMAL,
    FIRE,
    WATER,
    GRASS
  }

  public static class EntityTypeExtension
  {
    public static TypeCompacity CheckCompacity(this EntityType entityType, EntityType opposite)
    {
      switch (entityType)
      {
        case FIRE:
          if (opposite == GRASS)
            return TypeCompacity.VERY_EFFECTIVE;
          else if (opposite == WATER || opposite == FIRE)
            return TypeCompacity.NOT_EFFECTIVE;
          
          return TypeCompacity.NORMAL;
        case WATER:
          if (opposite == FIRE)
            return TypeCompacity.VERY_EFFECTIVE;
          else if (opposite == WATER || opposite == GRASS)
            return TypeCompacity.NOT_EFFECTIVE;

          return TypeCompacity.NORMAL;
        case GRASS:
          if (opposite == WATER)
            return TypeCompacity.VERY_EFFECTIVE;
          else if (opposite == GRASS || opposite == FIRE)
            return TypeCompacity.NOT_EFFECTIVE;
          
          return TypeCompacity.NORMAL;
        default:
          return TypeCompacity.NORMAL;
      }
    }
  }
}
