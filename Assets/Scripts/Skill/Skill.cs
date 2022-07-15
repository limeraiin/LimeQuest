using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillData data;
    public int currentUse;

    public Skill(SkillData data)
    {
        this.data = data;
        this.currentUse = data.maxUse;
    }

}