using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GoToCurrentFloor : MonoBehaviour//현재 플레이어의 층 수 정보를 받아와, 해당하는 층의 Scene으로 이동하는 스크립트. 
{   // ChoicePanelController.cs에서 호출된다.
    private string[] floors = {"DungeonScene", "10_19Floor", "20_29Floor", "30_39Floor", "40_49Floor", "50Floor"};//각 층의 던전 이름을 저장하는 배열. 1층부터 50층까지의 던전 이름을 저장.

    public void CheckCurrentFloor(int targetFloor)//플레이어의 현재 층 수를 체크하고, 해당하는 씬으로 이동하는 메서드. targetFloor는 입장하려는 층의 대표 층(10,20,30.40,50)
    {
        int playerFloor = PlayerInfo.Instance.GetPlayerFloor();//현재 플레이어의 층 수를 가져옴.
        if(targetFloor > playerFloor)
        {
            return;//플레이어의 층 수보다 높은 층으로 이동할 수 없도록 설정.
        }
        int index = targetFloor / 10;
        LoadCurrentFloorScene(index);//층 수에 해당하는 씬을 로드하는 메서드 호출.
    }

    private void LoadCurrentFloorScene(int index)//층 수에 해당하는 씬을 로드하는 메서드.
    {
        if(index < floors.Length)//index는 0이 들어올 수도 있으므로, floors.Length보다 작을 경우에만 씬을 로드.
        {
            Debug.Log($"Loading floor: {floors[index]}");//로딩할 층 수를 디버그 로그로 출력.
            StartCoroutine(LateLoadScene(index));//LateLoadScene 메서드를 호출하여 씬을 로드.
        }
        else
        {
            Debug.Log("Invalid floor index.");//유효하지 않은 층 수일 경우 디버그 로그 출력.
        }   
    }

    private IEnumerator LateLoadScene(int index)//씬을 로드하는 코루틴 메서드. 씬 전환 시 UI 애니메이션을 적용하기 위해 코루틴으로 작성.
    {
        DOTWeenUIAnimation.PopupDownAnimationInUI(gameObject, Vector3.zero, 0.2f);//씬 전환 시 UI 애니메이션을 적용.
        yield return new WaitForSecondsRealtime(0.15f);//애니메이션 길이보다 살짝 짧게 대기 후 다음 씬을 로드한다. 팝업이 꺼진 후 바로 비활성화되어버리기 때문에, LoadScene()이 동작하지 않을 수 있기 떄문.
        
        SceneManager.LoadScene(floors[index]);//index에 해당하는 씬을 로드.
    }

    public void UnlockingFloor(int currentFloor, Image[] lockImages)//현재 플레이어의 층 수에 따라, 해당 층이 모두 클리어되었다면 LockImage를 비활성화하는 메서드.
    {
        int maxIndex = currentFloor / 10;
        for(int i = 0; i <= maxIndex && i < lockImages.Length; i++)
        {
            lockImages[i].gameObject.SetActive(false);//LockImage를 비활성화.
            Debug.Log($"Unlocking floor: {i}");//해당 층을 해금했다는 디버그 로그 출력. 
        }
    }

    public void ClearProcess(int currentFloor)//WinPanelController.cs에서 호출되며, 현재 던전을 클리어처리 한 후 현재 층 클리어 여부를 체크하는 메서드. 클리어 여부에 따라 "다음 던전"클릭 시 이동하는 층이 변경된다.
    {
        //dungeonInfo.floorNumber는 1부터 6까지. currentFloor는 1부터 50까지이기 때문에, 두 변수의 범위가 다르다. 왜? => 실제 플레이어가 플레이해야 하는 층은 50층인데, 게임 아키텍처는 한 씬 당 10개의 던전을 표현하기로 했기 때문.
        int realCurrentFloor = 0;//현재 층 수를 저장할 변수. 1층부터 50층까지의 층 수를 저장. currentFloor와 floorNumber의 범위가 다르기 때문에, floorNumber에 맞게 변환해주어야 한다.

        switch(currentFloor/10)
        {
            case 0 : 
                realCurrentFloor = 1;//1층 ~ 9층.
                break;
            case 1 : 
                realCurrentFloor = 2;//10층 ~ 19층.
                break;
            case 2 : 
                realCurrentFloor = 3;//20층 ~ 29층.
                break;
            case 3 : 
                realCurrentFloor = 4;//30층 ~ 39층.
                break;
            case 4 : 
                realCurrentFloor = 5;//40층 ~ 49층.
                break;
            case 5 : 
                realCurrentFloor = 6;//50층.
                break;
        }

        List<string> dungeonIDs = TowerProgressManager.Instance.allDungeons.FindAll(dungeonInfo =>dungeonInfo.floorNumber == realCurrentFloor)
                                                                            .ConvertAll(dungeonInfo => dungeonInfo.dungeonID);
        //allDungeon리스트에서 현재 층에 해당하는 DungeonInformation객체들을 추려낸다.
        //그 객체들에서 dungeonID만 추출하여 리스트로 반환한다.
        //ConvertAll은 람다식으로 새 리스트를 만들 수 있음. Select()로도 구현할 수 있으나, 유니티에서 LINQ사용은 지양하는 편이 좋기에 C#의 내장메서드를 사용.

        SingleDungeonInfoRepository singleDungeonInfoRepository = GameObject.Find("Managers").GetComponent<SingleDungeonInfoRepository>();

        TowerProgressManager.Instance.SetDungeonClear(currentFloor, singleDungeonInfoRepository.dungeonInformation.dungeonID);//현재 저장된 던전id에 해당하는 던전의 클리어 여부를 true로 설정.
        
        if(TowerProgressManager.Instance.IsFloorCleared(currentFloor, dungeonIDs))//currentFloor에 존재하는 던전리스트를 불러와서 모두 클리어되었는지 체크.
        {
            PlayerInfo.Instance.SetPlayerFloor();//true이면 현재 층 수를 +1한 값을 리턴.
        }
        else//아직 currentFloor의 모든 던전이 클리어되지 않았다면
        {
            Debug.Log($"Number of Remaining dungeon is {dungeonIDs.Count}");
            return;
        }
    }

    public string GoToNextDungeon(int currentFloor)//WinPanelController.cs에서 호출될 다음 던전 이동 메서드.
    {
        int nextFloorSceneNum = PlayerInfo.Instance.GetPlayerFloor() / 10;// 현재 층 수를 가져온다.ResultUIController.cs에서 이미 towerFloor값 변경 작업을 마쳤기에, 클리어 여부에 따라 업데이트되어있을 것.
        
        return floors[nextFloorSceneNum];//현재 층 수 /10의 값 (ex : 32층이면 3 반환 -> floors[3] = 30_39Floor씬)을 리턴턴
    }
}
