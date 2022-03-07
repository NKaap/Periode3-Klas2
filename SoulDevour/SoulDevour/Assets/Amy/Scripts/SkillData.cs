using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int unusedSkillPoints;

    public int characterSpeed;
    public int characterJumpHeight;
    public int characterHealth;
    public int characterDamage;
   
    public enum SkillTypes
    {
        Speed, JumpHeight, Health, Damage
    }


}
