using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private BattleInfoBox playerInfoBox;
    [SerializeField] private BattleInfoBox enemyInfoBox;
    [Space(20)]
    [SerializeField] private Image pBattleImage;
    [SerializeField] private Image eBattleImage;

    [Space(20)] 
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typeDelay;

    [Space(20)] 
    [SerializeField] public Sprite[] healthBars;

    [Space(20)] 
    [SerializeField] private ActionsBox actionsMenu;
    [SerializeField] private GameObject skillMenu;
    [SerializeField] private SkillGridLayout skillGridLayout;

    private BattleManager _battleManager;
    private Queue<string> strings;
    private bool isWriting;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)   //Singleton check
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        _battleManager = BattleManager.Instance;
        strings = new Queue<string>();
    }

    public void Setup_HUD(Fighter player, Fighter enemy, bool firstTime)
    {
        playerInfoBox.SetupInfo(player);
        enemyInfoBox.SetupInfo(enemy);
        
        pBattleImage.enabled = true;
        eBattleImage.enabled = true;
        
        pBattleImage.sprite = player.Data.frontSprite;
        eBattleImage.sprite = enemy.Data.frontSprite;
        
        
        if (firstTime)
        {
            TypeWrite($"A wild {enemy.Name} appeared."); 
        }
        
    }

    public void PlayerTurn()
    {
        TypeWrite($"What will {BattleManager.Instance.playerFighter.Name} do?");
    }
    public void EnemyTurn()
    {
        ActionLock(5);
    }

    public void ActionLock(int i)   // 5: lock all actions
    {
        if (i == 5)
        {
            actionsMenu.LockActions();
        }
        else
        {
            actionsMenu.LockActions(i);
        }
    }

    
    public void TypeWrite(string text)
    {
        strings.Enqueue(text);
        if (strings.Count == 1 && isWriting == false)
        {
            StartCoroutine(IETypeWrite(dialogueText)); 
        }
        
    }
    
    public IEnumerator TypeWriteLate(string text)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        strings.Enqueue(text);
        if (strings.Count == 1 && isWriting == false)
        {
            StartCoroutine(IETypeWrite(dialogueText)); 
        }
        
    }

    public void WriteDelayed(string text)
    {
        StartCoroutine(TypeWriteLate(text));
    }
    
    public void TypeWrite(TMP_Text textField, string text)  // Unused for now
    {
        strings.Enqueue(text);
        if (strings.Count == 1 && isWriting == false)
        {
            StartCoroutine(IETypeWrite(textField)); 
        }
        
    }

    private IEnumerator IETypeWrite(TMP_Text textField)
    {
        isWriting = true;
        ActionLock(5);
        
        while (strings.Count>0)
        {
            string text = strings.Dequeue();    // pop the text with highest priority from the queue
            
            for (int i = 0; i <= text.Length; i++)
            {
                textField.text = text.Substring(0, i);
                yield return new WaitForSeconds(typeDelay);
            }
            yield return new WaitForSeconds(1f);

        }
        
        isWriting = false;
        actionsMenu.UnlcokActions();

        if (_battleManager.isOver)
        {
            if (!PlayerManager.Instance.LossCheck())
            {
                actionsMenu.LockActions(1);
            }
            else
            {
                actionsMenu.LockActions();
            }
        }
        


    }

    public void RefreshHUD()
    {
        
        playerInfoBox.RefreshInfo();
        enemyInfoBox.RefreshInfo();
    }

    public void FullRefresh()
    {
        Setup_HUD(_battleManager.playerFighter, _battleManager.enemyFighter, false);
    }

    public void CalculateHPBar(Image bar, Fighter fighter)
    {
        var pHPRatio = (float) fighter.Hp.value / fighter.MaxHp;
        bar.fillAmount = pHPRatio;
        
        UIManager UIManager = UIManager.Instance;
        if (0.75<=pHPRatio)
        {
            bar.sprite = UIManager.healthBars[0];
        }
        else if (0.25<=pHPRatio)
        {
            bar.sprite = UIManager.healthBars[1];
        }
        else
        {
            
            bar.sprite = UIManager.healthBars[2];
        }
    }

    public bool IsWriting()
    {
        return isWriting;
    }

    public void ShowSkillMenu()
    {
        skillMenu.SetActive(true);
        skillGridLayout.SkillGridSetup(_battleManager.playerFighter);
    }
    public void HideSkillMenu()
    {
        skillMenu.SetActive(false);
    }
}