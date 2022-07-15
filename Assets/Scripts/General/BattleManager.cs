using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public TurnStatus TurnStatus;
    public bool turnable;
    public bool isOver;
    

    public Fighter playerFighter;
    public Fighter enemyFighter;

    public Animator playerAnim;
    public Animator enemyAnim;

    [Space(20)]
    [SerializeField] private UIManager UIManager;

    [SerializeField] private EnemyLogic enemyLogic;
    

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
        playerFighter = PlayerManager.Instance.CurrentFighter;

        var dangerZone = FindObjectOfType<DangerZone>();
        var enemyFighterData = dangerZone.encounterData.encounters[0];  //Find a better way!
        enemyFighter = new Fighter(enemyFighterData,false );
        enemyLogic.SetupLogic(enemyFighter);
        Destroy(dangerZone.gameObject);
        Starting();
    }

    private void Starting()
    {
        
        UIManager.Setup_HUD(playerFighter,enemyFighter,true);
        TurnStatus = TurnStatus.Start;
        StartCoroutine(SetTurn(TurnStatus.Player));
    }

    public IEnumerator SetTurn(TurnStatus newValue)
    {
        UIManager.Instance.RefreshHUD();
        
        while(UIManager.IsWriting()){
            yield return null;
        }
        if (isOver)
        {
            yield break;   
        }
        
        switch (newValue)
        {
            case TurnStatus.Player:
                TurnStatus = TurnStatus.Player;
                UIManager.PlayerTurn();
                break;
            case TurnStatus.Enemy:
                TurnStatus = TurnStatus.Enemy;
                enemyLogic.RunLogic();
                UIManager.EnemyTurn();
                break;
        }
    }
    public Fighter GetTarget(SkillTarget target)
    {
        Fighter newTarget = null;
        if (TurnStatus == TurnStatus.Player)
        {
            if (target == SkillTarget.Enemy)
            {
                newTarget = enemyFighter;
            }
            else
            {
                newTarget = playerFighter;
            }
 
        }
        else if (TurnStatus == TurnStatus.Enemy)
        {
            if (target == SkillTarget.Enemy)
            {
                newTarget = playerFighter;
            }
            else
            {
                newTarget = enemyFighter;
            }
        }

        return newTarget;
    }

    public void PlayAnim(SkillTarget target, string anim)
    {
        if (TurnStatus == TurnStatus.Player)
        {
            if (target == SkillTarget.Enemy)
            {
                enemyAnim.SetTrigger(anim);
            }
            else
            {
                playerAnim.SetTrigger(anim);
            }
        }
        else
        {
            if (target == SkillTarget.Enemy)
            {
                playerAnim.SetTrigger(anim);
            }
            else
            {
                enemyAnim.SetTrigger(anim);
            }
        }
        
    }

    public void EndBattle()
    {
        // Reward calculation here?

        StartCoroutine(DelayedFade());
    }

    private IEnumerator DelayedFade()
    {
        yield return new WaitForSecondsRealtime(4f);
        FindObjectOfType<FadeControl>().FadeStart(0);
    }
}

public enum TurnStatus
{
    Start,
    Player,
    Enemy,
    End,
}