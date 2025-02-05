using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IInventoryItem
{
    //아이템 객체가 상속받을 인터페이스.
    string ItemName{get;}
    int ItemID{get;}
    Sprite ItemSprite{get;}
    ItemType Type{get;}

}
