using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface IInventoryItem
{
    //������ ��ü�� ��ӹ��� �������̽�.
    string ItemName{get;}
    int ItemID{get;}
    Sprite ItemSprite{get;}
    ItemType Type{get;}

}
