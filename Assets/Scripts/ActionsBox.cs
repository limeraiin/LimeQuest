using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsBox : MonoBehaviour
{
    public Button[] _buttons;     // 0: Skill, 1: Fighter, 2: Items, 3 Run

    public void LockActions(int excluded)
    {
        foreach (var button in _buttons)
        {
            if (button != _buttons[excluded])
            {
                button.interactable = false;
            }
        }
    }
    public void LockActions()
    {
        foreach (var button in _buttons)
        {
            
            button.interactable = false;

        }
    }

    public void UnlcokActions()
    {
        foreach (var button in _buttons)
        {
            button.interactable = true;
        }
    }
}