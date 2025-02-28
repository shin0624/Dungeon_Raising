using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceUIUpperPanelController : MonoBehaviour
{
    //SinglePlayScene에 현재 배치된 플레이어블 유닛 개수를 출력하는 UI를 제어한다. 플레이어 (배치 수/ 최대치 1) | 영웅 (배치 수 / 최대치 1) | 병사 (배치 수 / 최대치 10)
    
    [SerializeField] private TextMeshProUGUI[] amountText = new TextMeshProUGUI[3];//[0] : 플레이어 / [1] : 영웅 / [2] 병사
    
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
