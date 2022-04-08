using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointManager : MonoBehaviour
{
    // skill manager, controller die alle logic regelt om skill points etc.


    private SkillData skillData;// skillData is never set.

    public SkillData.SkillTypes skillType;

    public GameObject ModelParent;

    public Text speedText, jumpText, healthText, damageText;

    public Text skillPoints;

    public GameObject player;

    private void Update()
    {
        Debug.Log(skillData.unusedSkillPoints);

        skillPoints.text = (GetRemainingSkillPoints() + ("  Skillpoints"));
        if (GetRemainingSkillPoints() == 1)
        {
            skillPoints.text = (GetRemainingSkillPoints() + ("  Skillpoint"));
        }
        else
        {
            skillPoints.text = (GetRemainingSkillPoints() + ("  Skillpoints"));
        }


        //Debug.Log(GetAllocatedPointsOf(0).ToString() + "---");
        //Debug.Log(GetAllocatedPointsOf(1).ToString() + "---");
        //Debug.Log(GetAllocatedPointsOf(2).ToString() + "---");
        //Debug.Log(GetAllocatedPointsOf(3).ToString() + "---");
    }

    private void Start()
    {
        //SetSkillData(MovPlayer.PlayerTypes.TeddyBear);
      



    }

   

    public void SetSkillData(MovPlayer.PlayerTypes playerType)
    {
        string fileName = Application.dataPath + "/" + playerType.ToString() + ".txt";

       // Debug.Log("New Data: " + fileName);
        
        if (File.Exists(fileName))
        {
            //Debug.Log("File Exists!");
            
            string data = File.ReadAllText(fileName);

            //Debug.Log(data);

            skillData = JsonUtility.FromJson<SkillData>(data);
            
           // Debug.Log(skillData.unusedSkillPoints);
          
        }
        else
        {
            //Debug.Log("Creating new data");
            skillData = new SkillData();
            string json = JsonUtility.ToJson(skillData);
            File.WriteAllText(fileName, json);
        }

        RefreshSkillInfo();
    }

    public int GetRemainingSkillPoints()
    {
        return skillData.unusedSkillPoints;
    }

    public void AddUnusedSkillPoint()
    {
        // add that it only adds with the playertype that is active.\

        skillData.unusedSkillPoints++;

        //int activeIndex = 0;
        //for (int i = 0; i < ModelParent.transform.childCount; i++)
        //{
        //    if (ModelParent.transform.GetChild(i).gameObject.activeInHierarchy)
        //    {
        //        activeIndex = i;
        //        break;
        //    }

        //}
        string fileName = Application.dataPath + "/" + player.GetComponent<MovPlayer>().playerTypes.ToString() + ".txt";
        string data = JsonUtility.ToJson(skillData);
        //Debug.Log("Spent: " + fileName);
        File.WriteAllText(fileName, data);
        
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
        int activeIndex = 0;

        for (int i = 0; i < ModelParent.transform.childCount; i++)
        {
            if (ModelParent.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                activeIndex = i;
                break;
            }
        }
        string fileName = Application.dataPath + "/" + ((MovPlayer.PlayerTypes)activeIndex).ToString() + ".txt";
        string data = JsonUtility.ToJson(skillData);
        //Debug.Log("Spent: " + fileName);
        File.WriteAllText(fileName,data);

       // Debug.Log("Spent a point");
        RefreshSkillInfo();
    }

    public void RefreshSkillInfo()
    {
        speedText.text = "Speed: " + GetAllocatedPointsOf(0).ToString();
        jumpText.text = "Jump: " + GetAllocatedPointsOf(1).ToString();
        healthText.text = "Health: " + GetAllocatedPointsOf(2).ToString();
        damageText.text = "Damage: " + GetAllocatedPointsOf(3).ToString();
    }

    public int GetAllocatedPointsOf(int typeValue)
    {
        switch ((SkillData.SkillTypes)typeValue)
        {
            case SkillData.SkillTypes.Speed: // value 0
                {
                    return skillData.characterSpeed; 

                }
            case SkillData.SkillTypes.JumpHeight: // value 1
                {
                    return skillData.characterJumpHeight;

                }
            case SkillData.SkillTypes.Health: // value 2
                {
                    return skillData.characterHealth;

                }
            case SkillData.SkillTypes.Damage: // value 3
                {
                    return skillData.characterDamage;

                }
        }
        return 0;
    }

}
