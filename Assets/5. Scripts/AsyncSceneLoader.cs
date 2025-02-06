using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    //�񵿱� �� �ε带 �̿��Ͽ� MainScene���� �Ѿ������ Ŭ����.
    public void LoadMainSceneAsync()//�� ��ȯ�� �񵿱� ������� �ε��ϴ� �޼���
    {
        StartCoroutine(LoadMainSceneCoroutine());
    }

    private IEnumerator LoadMainSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");
        while (!asyncLoad.isDone)//�� �ε尡 �Ϸ�� �� ���� ����
        {
            yield return null;
        }
        InitUIObject();//�� �ε� �Ϸ� �� �ʱ�ȭ�۾�.
    }

    private void InitUIObject()//���� �ε�� �� �ʿ��� UI ��ü�� �ʱ�ȭ
    {
        var panelParent = GameObject.FindWithTag("InventoryPanel");
        if(panelParent!=null)
        {
            panelParent.SetActive(true);
        }
        else
        {
            Debug.LogError("inventoryPanel not found.");
        }
    }
}
