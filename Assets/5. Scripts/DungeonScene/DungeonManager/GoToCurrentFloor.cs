using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToCurrentFloor : MonoBehaviour//현재 플레이어의 층 수 정보를 받아와, 해당하는 층의 Scene으로 이동하는 스크립트.
{
    private string[] floors = {"1_9Floor", "10_19Floor", "20_29Floor", "30_39Floor", "40_49Floor", "50Floor"};//각 층의 던전 이름을 저장하는 배열. 1층부터 50층까지의 던전 이름을 저장.
    
    private void CheckCurrentFloor()//플레이어의 현재 층 수를 체크하고, 해당하는 씬으로 이동하는 메서드.
    {
        int currentFloor = PlayerInfo.Instance.GetPlayerFloor();//플레이어의 현재 층 수를 가져옴.
        switch(currentFloor / 10)//플레이어의 층 수에 따라 이동할 씬을 결정.
        {
            case 0:
                LoadCurrentFloorScene(1);//1층 -> 10_19Floor 씬으로 이동.
                break;

            case 1:
                LoadCurrentFloorScene(1);//2층 -> 20_29Floor 씬으로 이동.
                break;

            case 2:
                LoadCurrentFloorScene(2);//3층 -> 30_39Floor 씬으로 이동.
                break;

            case 3:
                LoadCurrentFloorScene(3);//4층 -> 40_49Floor 씬으로 이동.
                break;

            case 4:
                LoadCurrentFloorScene(4);//5층 -> 50Floor 씬으로 이동.
                break;

            case 5 :
                LoadCurrentFloorScene(5);//6층 -> 50Floor 씬으로 이동.
                break;

            default:
                Debug.Log("Invalid floor number.");//유효하지 않은 층 수일 경우 디버그 로그 출력.
                break;
        }
    }

    private void LoadCurrentFloorScene(int index)//층 수에 해당하는 씬을 로드하는 메서드.
    {
        if(index < floors.Length)//index는 0이 들어올 수도 있으므로, floors.Length보다 작을 경우에만 씬을 로드.
        {
            Debug.Log($"Loading floor: {floors[index]}");//로딩할 층 수를 디버그 로그로 출력.
            SceneManager.LoadScene(floors[index]);//index에 해당하는 씬을 로드.
        }
        else
        {
            Debug.Log("Invalid floor index.");//유효하지 않은 층 수일 경우 디버그 로그 출력.
        }   
    }
}
