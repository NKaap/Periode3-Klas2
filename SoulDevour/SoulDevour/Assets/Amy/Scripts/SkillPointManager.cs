using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : MonoBehaviour
{
    // skill manager, controller die alle logic regelt om skill points etc.


    private SkillData skillData;// skillData is never set.

    public SkillData.SkillTypes skillType;

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
        // when a teacher dies, add a point 

        return true;

        
    }

    
    public void SpendPoint(int typeValue)  // werkt met onclick
    {
        if (skillData.unusedSkillPoints == 0)
            return;

        skillData.unusedSkillPoints--;
        switch ((SkillData.SkillTypes)typeValue)
        {
            case SkillData.SkillTypes.Speed: // value 0
                {
                    skillData.characterSpeed++; // hoe zet je deze naar de player speed?
                    break;
                }
            case SkillData.SkillTypes.JumpHeight: // value 1
                {
                    skillData.characterJumpHeight++;
                    break;
                }
            case SkillData.SkillTypes.Health: // value 2
                {
                    skillData.characterHealth++;
                    break;
                }
            case SkillData.SkillTypes.Damage: // value 3
                {
                    skillData.characterDamage++;
                    break;
                }
        }
    }

  
}
