using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        // items 
        Fireballs, Feather, Fruit, BandAid, Diaper, BestFriend, BadGrade, Ressurect, Glasses, Crown,
        Tooth, Soup, Lego, PeanutButter, Socks, Slime, Poop, Fart, UniCornHorn,
    }

    public ItemType itemtype;
    public ItemBase(ItemBase toCopy) { itemtype = toCopy.itemtype;  }

    public float playerHealth;
    public float fireDamage;


    public GameObject peanut;

    public void Items()
    {
        switch (itemtype)
        {
            case ItemType.Fireballs:
                {
                    // Bullet script
                    break;
                }
            case ItemType.Feather:
                {
                    break;
                }
            case ItemType.Fruit: // health
                {
                    break;
                }
            case ItemType.BandAid:
                {
                    break;
                }
            case ItemType.Diaper:
                {
                    break;
                }
            case ItemType.BestFriend:
                {
                    break;
                }
            case ItemType.BadGrade:
                {
                    break;
                }
            case ItemType.Ressurect:
                {
                    break;
                }
            case ItemType.Glasses:
                {
                    break;
                }
            case ItemType.Crown:
                {
                    break;
                }
            case ItemType.Tooth:
                {
                    break;
                }
            case ItemType.Soup:
                {
                    break;
                }
            case ItemType.Lego:
                {
                    break;
                }
            case ItemType.PeanutButter: // bomb 
                {                  
                    // in andere scripts zit functionaliteit. Bullet 
                    break;
                }
            case ItemType.Socks:
                {
                    break;
                }
            case ItemType.Slime:
                {
                    break;
                }
            case ItemType.Poop:
                {
                    break;
                }
            case ItemType.Fart:
                {
                    break;
                }
            case ItemType.UniCornHorn:
                {
                    break;
                }

        }
    }
}
