using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    // SinglePlayScene에서 에너미 인스턴스를 스폰하는 클래스. 던전 정보 스크립터블 오브젝트를 사용한다.
    // DungeonScene에서 Trigger이벤트를 통해 SinglePlayScene으로 입장할 때, DungeonScene의 DungeonInfoDeliver.cs의 Set__Info()를 통해 전달받은 에너미, 보스 타입을 수신하여 타입에 맞는 프리팹을 인스턴싱한다.
    // 좌표 탐색 및 인스턴싱은 AutoSpawn__.cs와 동일 로직을 사용한다. 에너미들은 유저 입력을 통한 좌표 이동이 불가하므로, 좌표 이동 메서드는 구현하지 않는다.
    // 에너미 프리팹은 직렬화된 GameObject 배열에 [0]부터 차례로 프리팹을 할당. 각 번지 별 대응 Type은 Enemy,BossType 열거체에 선언된 순서에 맞게 Switch문으로 작성하여 확장성을 높인다.
    // 인스턴싱 메서드는 SinglePlayScene이 활성화될 때 호출되는 OnEnable()에서 호출된다.
    void Start()
    {
        
    }


}
