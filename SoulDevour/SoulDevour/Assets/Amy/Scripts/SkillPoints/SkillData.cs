using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{ 

    // model voor storing data, class die je kan opslaan als json file. 
    public int unusedSkillPoints = 5;

    public int characterSpeed;
    public int characterJumpHeight;
    public int characterHealth;
    public int characterDamage;
   
    public enum SkillTypes
    {
        Speed = 0, JumpHeight = 1, Health = 2, Damage = 3
    }


}
