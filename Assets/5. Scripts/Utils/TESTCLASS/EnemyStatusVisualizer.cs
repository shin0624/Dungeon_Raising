using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyStatusVisualizer : MonoBehaviour
{
//게임 중 유닛 hp 체크를 위해 Canvas에 유닛의 상태를 표시하는 클래스.
    private List<GameObject> unitStatusList = new List<GameObject>();
    private List<GameObject> statusUIList = new List<GameObject>(); // 생성된 UI 저장
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject bossParent;
    [SerializeField] private GameObject statusImage;//프리팹
    [SerializeField] private GameObject statusPanel;
    private string tempText = "100";
    public static event Action<int> OnEnemyCountChanged;// 에너미 리스트의 개수가 줄어들 때 마다 호출될 이벤트.
    void Start()
    {
        StartCoroutine(StartSetting());
        //StartCoroutine(UpdateUnitStatus()); // 코루틴 한 번만 실행  
    }

    public void StartUpdateUnitStatus()
    {
        StartCoroutine(UpdateUnitStatus()); // 코루틴 한 번만 실행
    }

    private void GetUnitLists()//활성화된 유닛을가져온다.
    {
         unitStatusList.Clear(); // 중복 방지
        foreach(Transform child in enemyParent.transform)
        {
            if (child.gameObject.activeInHierarchy && child.gameObject.scene.IsValid())
            unitStatusList.Add(child.gameObject);
        }
        foreach(Transform child in bossParent.transform)
        {
            if (child.gameObject.activeInHierarchy && child.gameObject.scene.IsValid())
            unitStatusList.Add(child.gameObject);
        }

        Debug.Log($"유닛 리스트 초기화 완료. unitStatusList.Count: {unitStatusList.Count}");
    }

     private void GetUnitStatus()//유닛의 상태를 가져온다.
    {    
        for (int i = 0; i < unitStatusList.Count; i++)
        {
            if (i < statusUIList.Count) // UI가 충분한 경우에만 업데이트
            {
                GameObject unit = unitStatusList[i];
                if(unit!=null)
                {
                    TextMeshProUGUI unitNameText = statusUIList[i].transform.Find("UnitName").GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI unitHpText = statusUIList[i].transform.Find("UnitHP").GetComponent<TextMeshProUGUI>();

                    unitNameText.text = unit.name;
                    unitHpText.text = unit.GetComponentInChildren<DamageCalculater>().currentHP.ToString();
                    if(unit.GetComponentInChildren<DamageCalculater>()==null)
                    {
                        unitHpText.text = tempText;
                    }
                }
                else{
                    // 유닛이 사라졌다면 UI도 삭제하고 리스트에서 제거
                    DestroyImmediate(statusUIList[i]);
                    statusUIList.RemoveAt(i);
                    unitStatusList.RemoveAt(i);
                    i--;
                    OnEnemyCountChanged?.Invoke(unitStatusList.Count);//유닛이 제거될 때 마다 이벤트를 호출.
                }

            }

        }
    }

    private void SetUnits()//패널에 유닛 정보 이미지를 불러온다. GridLayoutGroup을 사용하여 유닛 정보를 정렬한다.
    {
        foreach(GameObject unit in unitStatusList)
        {
            GameObject status = Instantiate(statusImage, statusPanel.transform);
            statusUIList.Add(status); // UI 리스트에 저장
        }
    }

    private IEnumerator UpdateUnitStatus()// 매 프레임 유닛의 상태 정보를 업데이트한다.
    {
        while(true)
        {
            GetUnitStatus();//매 프레임마다 유닛의 HP를 업데이트한다. 
            yield return new WaitForSeconds(1.0f);//유닛의 상태 정보를 업데이트하는 타이밍을 조절한다.
        }
    }

    private IEnumerator StartSetting()// 게임 활성화 시 차례차례 유닛 정보 세팅.
    {
        yield return new WaitForSeconds(1.0f);
        GetUnitLists();
        SetUnits();

        yield return new WaitForSeconds(0.5f);
        GetUnitStatus();
    }
}
