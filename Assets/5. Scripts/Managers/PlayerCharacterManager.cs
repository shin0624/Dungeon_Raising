using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterManager : MonoBehaviour
{

    //playerInfoDefine에 저장된 playerGender, playerJob에 해당하는 캐릭터 프로필을 어느 씬에서든 참조할 수 있도록 플레이어 캐릭터의 정보를 저장하는 클래스. Managers에 추가한다.
    [SerializeField] private GameObject[] maleCharacterPrefabs;//남자 캐릭터 [0] : 검사, [1] : 궁수
    [SerializeField] private GameObject[] femaleCharacterPrefabs;//여자 캐릭터 [0] : 궁수, [1] : 마법사

    private string genderCheck()// 플레이어 성별 체크
    {
        string gender="";
        if(PlayerInfo.Instance!=null)//인스턴스가 존재하면
        {
            gender = PlayerInfo.Instance.GetPlayerGender();//플레이어 성별 값을 가져와서 리턴
        }
        return gender;
    }

    private string jobCheck()//플레이어 직업체크
    {
        string job = "";
        if(PlayerInfo.Instance!=null)//인스턴스가 존재하면
        {
            job = PlayerInfo.Instance.GetPlayerJob();//플레이어 직업 값을 가져와서 리턴
        }
        return job;
    }

    public GameObject SetCharacterProfile()//캐릭터 프리팹팹 설정 메서드. 성별과 직업 값을 가져오고, 성별 값에 따른 프리팹 배열을 선택, 이후 직업 값에 따라 배열의 인덱스를 결정한다.
    {
        if(PlayerInfo.Instance==null)
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            return null;
        }

        string gender = genderCheck();//위에서 만들어두었던 체크 메서드들의 리턴값을 변수에 각각 저장
        string job = jobCheck();

        if(string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(job))//각 값들의 유효성 체크 먼저 수행
        {
            Debug.Log($"Gender or Job is invalid. Gender : {gender}, Job : {job}");
        }

        GameObject [] selectedPrefabArray = null;//성별에 따른 캐릭터 프리팹팹 배열을 선택한다.
        if(gender == "Male")
        {
            selectedPrefabArray = maleCharacterPrefabs;//남성 캐릭터 배열
        }
        else if(gender == "Female")
        {
            selectedPrefabArray = femaleCharacterPrefabs;//여성 캐릭터 배열
        }
        else//예외처리
        {
            Debug.LogError($"Unknown Gender. Gender : {gender}");
        }

        int jobIndex = -1;
        switch(job)
        {
            case "Knight" :
                jobIndex = 0;//knight는 남성 캐릭터 배열의 인덱스 0번이므로 겹칠 수 없는 문자열임.
                break;

            case "Archer" :
                if(selectedPrefabArray==maleCharacterPrefabs) 
                {
                    jobIndex = 1;//선택된 배열이 남성 배열일 경우 인덱스는 1번
                }
                else 
                {
                    jobIndex = 0;//선택된 배열이 여성 배열일 경우 인덱스는 0번
                }
                break;

            case "Magician" :
                jobIndex = 1;//Magician은 여성 캐릭터 배열의 인덱스 1번이므로, 겹칠 수 없는 문자열임. 
                break;
                
            default :
                Debug.LogError($"Unknown Job. Job is {job}");
                return null;
        }

        if(selectedPrefabArray!=null && jobIndex >=0 && jobIndex < selectedPrefabArray.Length)
        {
            return selectedPrefabArray[jobIndex];//선택한 성별과 직업에 맞는 프리팹을을 가져온다.
        }
        else
        {
            Debug.LogError($"Invalid Image index or array. JobIndex is {jobIndex}");
            return null;
        }
    }

}
