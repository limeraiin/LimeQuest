using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLogic : MonoBehaviour
{
    private MovesetData _moveset;
    private Fighter _fighter;

    public void SetupLogic(Fighter fighter)
    {
        _fighter = fighter;
        _moveset = _fighter.Data.AIMoveset;
    }
    
    public void RunLogic()
    {
        SkillData move = null;
        float hpPercentage = (float)_fighter.Hp.value / _fighter.MaxHp;

        float decider = Random.Range(0.0f, 1.0f);

        if (decider<=0.25f)
        {
            if (_moveset.defenceMoves.Count>0)
            {
                if (decider<=0.05 || (hpPercentage< 0.25 && decider < 0.15))
                {
                    move = _moveset.defenceMoves[Random.Range(0, _moveset.defenceMoves.Count)];
                }
                else
                {
                    move = _moveset.lowMoves[Random.Range(0, _moveset.lowMoves.Count)];
                }
            }
        }
        else if (decider is > 0.25f and < 0.75f)
        {
            if (decider<0.5)
            {
                move = _moveset.lowMoves[Random.Range(0, _moveset.lowMoves.Count)];
            }
            else
            {
                move = _moveset.mediumMoves[Random.Range(0, _moveset.mediumMoves.Count)];
            }
        }
        else
        {
            move = _moveset.highMoves[Random.Range(0, _moveset.highMoves.Count)];
        }
        
        UseSkill(move);
    }
    
    private void UseSkill(SkillData move)
    {
        move.Execute();
        StartCoroutine(BattleManager.Instance.SetTurn(TurnStatus.Player));
    }
}
