using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    //비동기 씬 로드를 이용하여 MainScene으로 넘어가기위한 클래스.
    public void LoadMainSceneAsync()//씬 전환을 비동기 방식으로 로드하는 메서드
    {
        StartCoroutine(LoadMainSceneCoroutine());
    }

    private IEnumerator LoadMainSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");
        while (!asyncLoad.isDone)//씬 로드가 완료될 때 까지 대기기
        {
            yield return null;
        }
        InitUIObject();//씬 로드 완료 후 초기화작업.
    }

    private void InitUIObject()//씬이 로드된 후 필요한 UI 객체를 초기화
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
