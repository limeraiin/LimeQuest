
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Game Data/Effects/Deal Damage")]
public class Effect_DealDamage : Effect
{
    public int amount;
    public SkillTarget target;
    public override void Use()
    {
        BattleManager.Instance.GetTarget(target).ChangeAttribute('H',-amount, ChangeType.Add);
    }
}

public enum ChangeType
{
    Add,
    Multiply,
    Set
}