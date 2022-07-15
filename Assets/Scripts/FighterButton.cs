using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FighterButton : MonoBehaviour
{
    private Fighter _fighter;
    private FighterSelection _fighterSelection;
    
    
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text levelField;
    [SerializeField] private TMP_Text currentHPField;
    [SerializeField] private TMP_Text maxHPField;
    [SerializeField] private Image HPBar;

    public void ButtonSetup(Fighter fighter)
    {
        _fighter = fighter;
        nameField.text = _fighter.Name;
        levelField.text = _fighter.Level.ToString();
        currentHPField.text = _fighter.Hp.value.ToString();
        maxHPField.text = _fighter.MaxHp.ToString();
        
        UIManager.Instance.CalculateHPBar(HPBar, _fighter);

        if (_fighter.Hp.value<=0)
        {
            
            var nameColor = nameField.faceColor;
            nameColor.a = 120;
            nameField.faceColor = nameColor;
            
            var levelColor = levelField.faceColor;
            levelColor.a = 120;
            levelField.faceColor = levelColor;

            var currentHPColor = currentHPField.faceColor;
            currentHPColor.a = 120;
            currentHPField.faceColor = currentHPColor;

            var maxHPColor = maxHPField.faceColor;
            maxHPColor.a = 120;
            maxHPField.faceColor = maxHPColor;

            GetComponent<Button>().enabled = false;
        }

        _fighterSelection = FindObjectOfType<FighterSelection>();
    }

    public void OnSelect()
    {
        var playerManager = PlayerManager.Instance;
        
        if (_fighter == playerManager.CurrentFighter)
        {
            return;
        }
        playerManager.SelectFighter(_fighter);
        _fighterSelection.HideWindow();
    }

    
}
