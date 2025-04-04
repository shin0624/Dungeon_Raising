using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GoToCurrentFloor : MonoBehaviour//현재 플레이어의 층 수 정보를 받아와, 해당하는 층의 Scene으로 이동하는 스크립트. 
{   // ChoicePanelController.cs에서 호출된다.
    private string[] floors = {"DungeonScene", "10_19Floor", "20_29Floor", "30_39Floor", "40_49Floor", "50Floor"};//각 층의 던전 이름을 저장하는 배열. 1층부터 50층까지의 던전 이름을 저장.
    public void CheckCurrentFloor(int currentFloor)//플레이어의 현재 층 수를 체크하고, 해당하는 씬으로 이동하는 메서드.
    {
        if(currentFloor != PlayerInfo.Instance.GetPlayerFloor())//현재 플레이어 층 수와 플레이어 정보에 저장된 층 수가 맞지 않다면
        {
            Debug.Log("Current floor does not match.");//디버그 로그 출력.
            return;//메서드 종료.
        }
        else
        {
            switch(currentFloor / 10)//플레이어의 층 수에 따라 이동할 씬을 결정.
            {
                case 0:
                    LoadCurrentFloorScene(0);//1_9Floor 씬으로 이동.
                    break;

                case 1:
                    LoadCurrentFloorScene(1);//10_19Floor 씬으로 이동.
                    break;

                case 2:
                    LoadCurrentFloorScene(2);//20_29Floor 씬으로 이동.
                    break;

                case 3:
                    LoadCurrentFloorScene(3);//30_39Floor 씬으로 이동.
                    break;

                case 4:
                    LoadCurrentFloorScene(4);//40_49Floor 씬으로 이동.
                    break;

                case 5 :
                    LoadCurrentFloorScene(5);//50Floor 씬으로 이동.
                    break;

                default:
                    Debug.Log("Invalid floor number.");//유효하지 않은 층 수일 경우 디버그 로그 출력.
                    break;
            }
        }
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

    private void UnlockingFloor(int currentFloor, Image[] lockImages)//현재 플레이어의 층 수에 따라, 해당 층이 모두 클리어되었다면 LockImage를 비활성화하는 메서드.
    {
        if(currentFloor-1 < lockImages.Length)//현재 층 수가 LockImage 배열의 길이보다 작을 경우에만 LockImage를 비활성화.
        {
            lockImages[currentFloor/10].gameObject.SetActive(false);//LockImage를 비활성화.
            Debug.Log($"Unlocking floor: {currentFloor}");//해당 층을 해금했다는 디버그 로그 출력.
        }
        else
        {
            Debug.Log("Invalid floor index for unlocking.");//유효하지 않은 층 수일 경우 디버그 로그 출력.
        }
    }

    public void CheckLockImage(int currentFloor, Image[] lockImages)//ChoicePanel이 오픈될 때 마다 현재 플레이어의 클리어 여부를 체크하고 LockImage를 제어하는 메서드.
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

        if(TowerProgressManager.Instance.IsFloorCleared(currentFloor, dungeonIDs))//currentFloor층의 모든 던전이 클리어되었다면
        {
            UnlockingFloor(currentFloor, lockImages);//LockImage를 비활성화.
        }
        else
        {
            return;
        }
    }
}
