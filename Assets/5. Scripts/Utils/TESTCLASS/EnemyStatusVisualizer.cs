using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyStatusVisualizer : MonoBehaviour
{
//���� �� ���� hp üũ�� ���� Canvas�� ������ ���¸� ǥ���ϴ� Ŭ����.
    private List<GameObject> unitStatusList = new List<GameObject>();
    private List<GameObject> statusUIList = new List<GameObject>(); // ������ UI ����
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject bossParent;
    [SerializeField] private GameObject statusImage;//������
    [SerializeField] private GameObject statusPanel;
    private string tempText = "100";
    public static event Action<int> OnEnemyCountChanged;// ���ʹ� ����Ʈ�� ������ �پ�� �� ���� ȣ��� �̺�Ʈ.
    void Start()
    {
        StartCoroutine(StartSetting());
        //StartCoroutine(UpdateUnitStatus()); // �ڷ�ƾ �� ���� ����  
    }

    public void StartUpdateUnitStatus()
    {
        StartCoroutine(UpdateUnitStatus()); // �ڷ�ƾ �� ���� ����
    }

    private void GetUnitLists()//Ȱ��ȭ�� �����������´�.
    {
         unitStatusList.Clear(); // �ߺ� ����
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

        Debug.Log($"���� ����Ʈ �ʱ�ȭ �Ϸ�. unitStatusList.Count: {unitStatusList.Count}");
    }

     private void GetUnitStatus()//������ ���¸� �����´�.
    {    
        for (int i = 0; i < unitStatusList.Count; i++)
        {
            if (i < statusUIList.Count) // UI�� ����� ��쿡�� ������Ʈ
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
                    // ������ ������ٸ� UI�� �����ϰ� ����Ʈ���� ����
                    DestroyImmediate(statusUIList[i]);
                    statusUIList.RemoveAt(i);
                    unitStatusList.RemoveAt(i);
                    i--;
                    OnEnemyCountChanged?.Invoke(unitStatusList.Count);//������ ���ŵ� �� ���� �̺�Ʈ�� ȣ��.
                }

            }

        }
    }

    private void SetUnits()//�гο� ���� ���� �̹����� �ҷ��´�. GridLayoutGroup�� ����Ͽ� ���� ������ �����Ѵ�.
    {
        foreach(GameObject unit in unitStatusList)
        {
            GameObject status = Instantiate(statusImage, statusPanel.transform);
            statusUIList.Add(status); // UI ����Ʈ�� ����
        }
    }

    private IEnumerator UpdateUnitStatus()// �� ������ ������ ���� ������ ������Ʈ�Ѵ�.
    {
        while(true)
        {
            GetUnitStatus();//�� �����Ӹ��� ������ HP�� ������Ʈ�Ѵ�. 
            yield return new WaitForSeconds(1.0f);//������ ���� ������ ������Ʈ�ϴ� Ÿ�̹��� �����Ѵ�.
        }
    }

    private IEnumerator StartSetting()// ���� Ȱ��ȭ �� �������� ���� ���� ����.
    {
        yield return new WaitForSeconds(1.0f);
        GetUnitLists();
        SetUnits();

        yield return new WaitForSeconds(0.5f);
        GetUnitStatus();
    }
}
