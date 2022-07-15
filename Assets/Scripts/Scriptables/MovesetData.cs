using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Moveset", menuName = "Game Data/Moveset Data")]
public class MovesetData : ScriptableObject
{
    public List<SkillData> lowMoves;
    public List<SkillData> mediumMoves;
    public List<SkillData> highMoves;
    public List<SkillData> defenceMoves;
}

