using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : MonoBehaviour
{
    // skill manager, controller die alle logic regelt om skill points etc.


    private SkillData skillData;


    public void SetSkillData(MovPlayer.PlayerTypes playerType)
    {
        switch (playerType)
        {
            
        }
    }

    public int GetRemainingSkillPoints()
    {
        return skillData.unusedSkillPoints;
    }

    public bool CanSpendPointOn(SkillData.SkillTypes skillType)
    {
        return true;

        
    }

    
    public void SpendPoint(SkillData.SkillTypes skillType) // zo maken hoe je die wel gewoon kan krijgen dat je in de Onlclick, "naar een int of string"
    {
        if (skillData.unusedSkillPoints == 0)
            return;

        skillData.unusedSkillPoints--;
        switch (skillType)
        {
            case SkillData.SkillTypes.Speed:
                {
                    skillData.characterSpeed++;
                    break;
                }
            case SkillData.SkillTypes.JumpHeight:
                {
                    skillData.characterJumpHeight++;
                    break;
                }
            case SkillData.SkillTypes.Health:
                {
                    skillData.characterHealth++;
                    break;
                }
            case SkillData.SkillTypes.Damage:
                {
                    skillData.characterDamage++;
                    break;
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
