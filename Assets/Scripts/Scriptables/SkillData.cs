using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Game Data/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string skillText;
    public List<Effect> effects;
    public int maxUse;
    public SkillTarget targetAnimator;
    public string animation;
    

    public void Execute()
    {
        BattleManager battleManager =  BattleManager.Instance;
        
        foreach (var effect in effects)
        {
            effect.Use();
        }
        
        battleManager.PlayAnim(targetAnimator,animation);
        UIManager.Instance.TypeWrite($"{battleManager.GetTarget(SkillTarget.Player).Data.name} {skillText}");
    }

}