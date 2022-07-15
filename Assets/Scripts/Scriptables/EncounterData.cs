using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter Data", menuName = "Game Data/Encounter Data")]
public class EncounterData : ScriptableObject
{
    public FighterData[] encounters;
    //public RewardData rewards;                   FOR LATER
}
