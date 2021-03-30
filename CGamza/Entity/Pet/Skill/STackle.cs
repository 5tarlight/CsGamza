using System;
using CGamza.Battle;
using CGamza.Entity.Monster;
using CGamza.Pet;

namespace CGamza.Entity.Pet.Skill
{
  [Serializable]
  public class STackle : SSkill
  {
    public STackle() : base ("태클", 30, SkillType.PHYSICAL, EntityType.NORMAL, 45)
    {}

    public override void OnUse(CPet user, CMonster opponent)
    {
      var dmg = BattleManager.CalDamage(
        this.Damage,
        EntityTypeExtension.CheckCompacity(user.Type, opponent.Type),
        user.Info.AdAtk,
        user.Info.ApAtk,
        DmgType.ATTACK_DAMAGE
      );

      opponent.Info.DealDmg(dmg);
      this.Point--;
    }
  }
}
