using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UnitStatusVisualizer : MonoBehaviour
{
    //���� �� ���� hp üũ�� ���� Canvas�� ������ ���¸� ǥ���ϴ� Ŭ����.
    private List<GameObject> unitStatusList = new List<GameObject>();
    private List<GameObject> statusUIList = new List<GameObject>(); // ������ UI ����
    [SerializeField] private GameObject heroParent;
    [SerializeField] private GameObject characterParent;
    [SerializeField] private GameObject soliderParent;
    [SerializeField] private GameObject statusImage;//������
    [SerializeField] private GameObject statusPanel;
    void Start()
    {
        StartCoroutine(StartSetting());
        StartCoroutine(UpdateUnitStatus()); // �ڷ�ƾ �� ���� ����
        
    }

    private void GetUnitLists()//Ȱ��ȭ�� �����������´�.
    {
         unitStatusList.Clear(); // �ߺ� ����
        foreach(Transform child in heroParent.transform)
        {
            unitStatusList.Add(child.gameObject);
        }
        foreach(Transform child in characterParent.transform)
        {
            unitStatusList.Add(child.gameObject);
        }
        foreach(Transform child in soliderParent.transform)
        {
            unitStatusList.Add(child.gameObject);
        }
        Debug.Log("unitStatusList.Count : " + unitStatusList.Count);
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
                }
                else{
                    // ������ ������ٸ� UI�� �����ϰ� ����Ʈ���� ����
                    Destroy(statusUIList[i]);
                    statusUIList.RemoveAt(i);
                    unitStatusList.RemoveAt(i);
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
