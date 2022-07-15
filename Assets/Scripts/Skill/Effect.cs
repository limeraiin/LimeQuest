using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string effectName;

    public abstract void Use();
}

public enum SkillTarget
{
    Player,
    Enemy
}