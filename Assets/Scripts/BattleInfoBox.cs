using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoBox : MonoBehaviour
{
    private Fighter _fighter;
    
    [SerializeField] private Image HPBar;
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text HPField;
    [SerializeField] private TMP_Text levelField;

    public void SetupInfo(Fighter fighter)
    {
        _fighter = fighter;
        nameField.text = fighter.Name;
        HPField.text = $"{fighter.Hp.value}/{fighter.MaxHp}";
        levelField.text = _fighter.Level.ToString();
    }

    public void RefreshInfo()
    {
        
        HPField.text = $"{_fighter.Hp.value}/{_fighter.MaxHp}";

        UIManager.Instance.CalculateHPBar(HPBar,_fighter);
    }
    
}
