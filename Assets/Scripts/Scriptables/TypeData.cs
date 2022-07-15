using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Type Data", menuName = "Game Data/Type Data")]
public class TypeData : ScriptableObject
{
    public string typeName;
    public FighterType type;
    public Sprite sprite;
}