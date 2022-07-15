using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter Data", menuName = "Game Data/Fighter Data")]
public class FighterData : ScriptableObject
{
    public string name;
    public List<TypeData> types;  //Fighter can have multiple types
    public Sprite frontSprite;
    
    [Space(20)]
    public List<SkillData> starterSkills;
    public int bAttack;  //Base Attack
    public int bHealth;  //Base hit points

    [Space(20)] 
    public MovesetData AIMoveset;
}

public enum FighterType
{
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dark,
    Dragon,
    Steel,
    Fairy
}