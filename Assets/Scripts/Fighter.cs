using System.Collections.Generic;
using UnityEngine;

public class Fighter
{
    private string name;
    private List<TypeData> types;
    private List<Skill> skills;



    private Attribute attack;

    public Attribute Attack => attack;

    public Attribute Hp => hp;

    private int maxHP;
    private Attribute hp;
    private int level;
    private bool isFainted;


    private FighterData data;
    private bool isPlayer;
    
    
    public List<Skill> Skills
    {
        get => skills;
        set => skills = value;
    }

    public void ChangeAttribute(char attr,float amount, ChangeType type)
    {
        Attribute receptorAttribute = null;
        bool hpCheck = false;
        switch (attr)
        {
            case 'H':
                receptorAttribute = hp;
                hpCheck = true;
                break;
            case 'A':
                receptorAttribute = attack;
                break;
        }

        if (receptorAttribute == null)
        {
            Debug.LogWarning("THE RECEPTOR ATTRIBUTE IS NULL!!!!!");
            return;
        }

        switch (type)
        {
            case ChangeType.Add:
                receptorAttribute.Add((int)amount);
                break;
            case ChangeType.Multiply:
                receptorAttribute.Multiply(amount);
                break;
            case ChangeType.Set:
                receptorAttribute.Set((int)amount);
                break;
        }

        if (hpCheck)
        {
            if (hp.value<1)
            {
                isFainted = true;
                BattleManager.Instance.isOver = true;
                if (isPlayer)
                {
                    
                    BattleManager.Instance.playerAnim.SetTrigger("Faint");
                }
                else
                {
                    
                    BattleManager.Instance.enemyAnim.SetTrigger("Faint");
                }
                var UInstance = UIManager.Instance;
                UInstance.WriteDelayed($"{name} has fainted.");
                UInstance.ActionLock(1);

                if (isPlayer)
                {
                    bool isLost = PlayerManager.Instance.LossCheck();
                    if (isLost)
                    {
                        UInstance.WriteDelayed($"You lost.");
                        UInstance.ActionLock(5);
                        BattleManager.Instance.EndBattle();
                        //Set TurnStatus to end?
                    }
                }
                else
                {
                        UInstance.WriteDelayed($"You won!");
                        UInstance.ActionLock(5);
                        BattleManager.Instance.EndBattle();
                        //Set TurnStatus to end?
                }
            }
        }
        
        
    }
    
    public bool IsFainted
    {
        get => isFainted;
        set => isFainted = value;
    }
    
    public string Name
    {
        get => name;
        set => name = value;
    }

    public List<TypeData> Types
    {
        get => types;
        set => types = value;
    }
    

    public int MaxHp
    {
        get => maxHP;
        set => maxHP = value;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public FighterData Data
    {
        get => data;
        set => data = value;
    }

    public Fighter(FighterData data, bool isPlayer)
    {
        this.MaxHp = data.bHealth;
        this.hp = new Attribute(this.MaxHp);
        this.attack = new Attribute(data.bAttack);
        this.name = data.name;
        this.types = data.types;
        this.level = 1;
        this.data = data;
        
        this.isPlayer = isPlayer;
        
        skills = new List<Skill>();
        foreach (var skillData in data.starterSkills)
        {
            skills.Add(new Skill(skillData));
        }
    }
}