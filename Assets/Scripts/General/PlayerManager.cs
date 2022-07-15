using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private List<Fighter> fighter_list;

    private Fighter _currentFighter;

    [SerializeField] private FighterData[] datas;       // For debugging only 
    
    public List<Fighter> FighterList => fighter_list;

    public Fighter CurrentFighter
    {
        get => _currentFighter;
        set => _currentFighter = value;
    }

    private void Start()
    {
        AddFromData();
    }

    private void Update()           //FOR DEBUGGING
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddFromData();
        }
    }

    private void AddFromData()
    {
            foreach (var data in datas)
            { 
                Fighter newFighter = new Fighter(data,true);
                AddFighter(newFighter);
            }
    }


    public void AddFighter(Fighter newFighter)
    {
        fighter_list.Add(newFighter);

        if (fighter_list.Count == 1)
        {
            _currentFighter = newFighter;
        }
    }

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
        
        DontDestroyOnLoad(this.gameObject);

        if (fighter_list == null)
        {
            fighter_list = new List<Fighter>();
        }
    }
    
    
    public void UseSkill(int i)
    {
        
        SkillData usedSkill = _currentFighter.Skills[i].data;
        usedSkill.Execute();
        StartCoroutine(BattleManager.Instance.SetTurn(TurnStatus.Enemy));
    }
    
    public void SelectFighter(Fighter fighter)
    {
        CurrentFighter = fighter;
        BattleManager battleManager = BattleManager.Instance;
        battleManager.playerFighter = _currentFighter;
        battleManager.isOver = false;
        battleManager.playerAnim.Play("Idle");
        
        var uiManager = UIManager.Instance;
        uiManager.FullRefresh();
        uiManager.TypeWrite($"{fighter.Name} enters the battle.");
        
        
        StartCoroutine(battleManager.SetTurn(TurnStatus.Enemy));
    }

    public bool LossCheck()
    {
        foreach (var fighter in fighter_list)
        {
            if (!fighter.IsFainted)
            {
                return false;
            }
        }

        return true;

    }
    
}