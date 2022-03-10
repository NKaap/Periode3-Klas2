using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{ 

    // model voor storing data, class die je kan opslaan als json file. 
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
