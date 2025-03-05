using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="HeroInformation", menuName ="Hero/HeroInformation")]
public class HeroInformation : ScriptableObject
{
    //���� ������ ������ ������ ��ũ���ͺ� ������Ʈ.
    //������ �ʿ��� ������ [���� �̸�, ���� ����, ���� ����, ���� ��� ���ݷ�, ü��, ����]
    [Header("Hero Info")]
    public string heroName;
    public HeroType heroType;
    public HeroRaise heroRaise;
    public GameObject heroPrefab;
    public Sprite heroHalfImage;
    
    
    [Header("Hero Status")]
    public int heroLevel;//����
    public Grade heroGrade;//���
    public float attackPoint;//���ݷ�
    public float defensePoint;//����
    public float healthPoint;//ü��
    public float attackSpeed;//���ݼӵ�
    public float moveSpeed;//�̵��ӵ�
    public float skillDamage;//��ų ���ط�

}
