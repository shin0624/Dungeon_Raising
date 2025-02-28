using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceUIUpperPanelController : MonoBehaviour
{
    //SinglePlayScene�� ���� ��ġ�� �÷��̾�� ���� ������ ����ϴ� UI�� �����Ѵ�. �÷��̾� (��ġ ��/ �ִ�ġ 1) | ���� (��ġ �� / �ִ�ġ 1) | ���� (��ġ �� / �ִ�ġ 10)
    
    [SerializeField] private TextMeshProUGUI[] amountText = new TextMeshProUGUI[3];//[0] : �÷��̾� / [1] : ���� / [2] ����
    
    private AutoSpawnCharacter autoSpawnCharacter;
    private AutoSpawnerHero autoSpawnerHero;
    private AutoSpawnerSoldier autoSpawnerSoldier;
    
    

    void Start()
    {
        autoSpawnCharacter = gameObject.GetComponent<AutoSpawnCharacter>();
        autoSpawnerHero = gameObject.GetComponent<AutoSpawnerHero>();
        autoSpawnerSoldier = gameObject.GetComponent<AutoSpawnerSoldier>();

        SetUIUpperText();
    }
    private void SetUIUpperText()
    {
        amountText[0].text = $"{autoSpawnCharacter.GetSpawnedCount()} / {autoSpawnCharacter.GetMaxAmount()}";
        amountText[1].text = $"{autoSpawnerHero.GetSpawnedCount()} / {autoSpawnerHero.GetMaxAmount()}";
        amountText[2].text = $"{autoSpawnerSoldier.GetSpawnedCount()} / {autoSpawnerSoldier.GetMaxAmount()}";
    }
}
