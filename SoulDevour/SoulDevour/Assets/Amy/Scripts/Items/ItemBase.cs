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
        Tooth, Soup, Lego, PeanutButter, Socks, Slime, Poop, Fart, UniCornHorn, Scissor, Plant, TextBook, Radio, 
    }

    public ItemType itemtype;
    public ItemBase(ItemBase toCopy) { itemtype = toCopy.itemtype;  }
    
    
    
}
