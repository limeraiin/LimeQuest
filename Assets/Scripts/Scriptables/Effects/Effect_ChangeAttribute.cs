
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Game Data/Effects/Change Attribute")]
public class Effect_ChangeAttribute : Effect
{
    public float amount;
    public SkillTarget target;
    public ChangeType changeType;
    public char attribute;
    public override void Use()
    {
        BattleManager.Instance.GetTarget(target).ChangeAttribute(attribute,amount, changeType);
    }
}