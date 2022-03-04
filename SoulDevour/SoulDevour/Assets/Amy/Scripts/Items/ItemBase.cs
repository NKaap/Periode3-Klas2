using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        // items 
        PropjePapier, Fireballs, Feather, Fruit, BandAid, Diaper, BestFriend, BadGrade, Ressurect, Glasses, Crown,
        Tooth, Soup, Lego, PeanutButter, Socks, Slime, Poop, Fart, UniCornHorn, Scissor, Plant, TextBook,
    }

    public ItemType itemtype;
    public ItemBase(ItemBase toCopy) { itemtype = toCopy.itemtype;  }

    public int itemCost; // prices of items, for in ShopRoom
    

    public void Items()
    {
        switch (itemtype)
        {
            case ItemType.PropjePapier:
                {
                    // bullet script
                    itemCost = 1;
                    break;
                }
            case ItemType.Fireballs:
                {
                    itemCost = 1;
                    // Bullet script
                    break;
                }
            case ItemType.Feather:
                {
                    itemCost = 1;
                    // Playermov
                    break;
                }
            case ItemType.Fruit: // health
                {
                    itemCost = 1;
                    // Playermov
                    break;
                }
            case ItemType.BandAid:
                {
                    itemCost = 1;
                    // Playermov
                    break;
                }
            case ItemType.Diaper:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.BestFriend:
                {
                    itemCost = 1;
                    // een friend die de player volgd.  in playerMov???
                    break;
                }
            case ItemType.BadGrade:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Ressurect:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Glasses:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Crown:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Tooth:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Soup:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Lego:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.PeanutButter: // bomb 
                {
                    itemCost = 1;
                    // in andere scripts zit functionaliteit. Bomb 
                    break;
                }
            case ItemType.Socks:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Slime:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Poop:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Fart:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.UniCornHorn:
                {
                    itemCost = 1;
                    break;
                }
            case ItemType.Scissor:
                {
                    itemCost = 1;
                    // bullet script
                    break;
                }
            case ItemType.Plant:
                {
                    itemCost = 1;
                    // Shoot Script
                    break;
                }
            case ItemType.TextBook:
                {
                    itemCost = 1;
                    // playermov
                    break;


                }
        }
    }
}
