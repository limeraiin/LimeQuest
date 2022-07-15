using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillGridLayout : MonoBehaviour
{
    [SerializeField] private Skill[] skills;
    [SerializeField] private TMP_Text[] skillNameTexts;


    public void SkillGridSetup(Fighter fighter)
    {
        skills = fighter.Skills.ToArray();

        foreach (var skillName in skillNameTexts)
        {
            skillName.text = "---";
            skillName.GetComponentInParent<Button>().interactable = false;
        }

        for (int i = 0; i < skills.Length; i++)
        {
            skillNameTexts[i].text = skills[i].data.skillName;
            skillNameTexts[i].GetComponentInParent<Button>().interactable = true;
        }
    }

    public void CastSkill(int i)
    {
        UIManager.Instance.HideSkillMenu();
        if (skills[i] == null)
        {
            return;
        }
        
        PlayerManager.Instance.UseSkill(i);
    }
}
