using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DOTWeenUIAnimation// UI 컨트롤러 클래스에서 DOTWeen 기능을 만들지 않고 바로 사용할 수 있게 하는 클래스. 어디서든 쉽게 호출하기 위해 정적 클래스로 선언.
{
    //UI를 컨트롤하는 클래스에서 DOTWeen의 기능을 사용하기 위해 일일히 DG.Tweening 네임스페이스를 선언하지 않아도 되고, 애니메이션 메서드도 만들지 않아도 됨.
    private static bool isInitialized = false;// DOTWeen의 Init()가 한 번만 호출될 수 있도록 제어하는 플래그.
    public static void Init()// Managers.cs의 Init()에서 한 번만 호출됨.
    {
        if(!isInitialized)//DOTWeen의 초기화는 한 번만 수행되면 게임 내내 유지되는 싱글톤 기반이므로, TitleScene에서 Managers에 의해 한 번만 초기화된다.
        {
            DOTween.Init();
            isInitialized = true;
        }
    }

    public static void PopupAnimationInUI(GameObject obj, float beforeEndValue, float beforeDuration, float aftertAndValue, float afterDuration)// UI크기를 소수점 단위로 조절하고자 할 때 사용.
    {
        //UI 팝업이 훅 커졌다가 원래 크기로 돌아오는 효과 연출을 위한 메서드. 시퀀스를 선언하고, 팝업이 활성화되고 훅 커질 때의 크기와 지속시간, 다시 돌아올 크기와 몇 초 안에 돌아올 것인지를 매개변수로 삽입.
        var seq = DOTween.Sequence();
        seq.Append(obj.transform.DOScale(beforeEndValue, beforeDuration));// 훅 커지는 효과
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration));// 다시 원상태로 되돌아오는 효과.
        // 팝업이 처음 켜질 때 효과를 내고자 하면 before -> after 순으로 매개변수를 넣고, 팝업을 끄는 효과(현재 크기에서 줄어들며 뿅 하고 사라지는)를 내고자 하면 after -> before순서로 넣되, after에 현재값, before에 작아질 값을 넣는다.
    }

    public static void PopupAnimationInUI(GameObject obj, Vector3 beforeEndValue, float beforeDuration, Vector3 aftertAndValue, float afterDuration)
    {
        //UI 팝업이 훅 커졌다가 원래 크기로 돌아오는 효과 연출을 위한 메서드. UI의 w, h값이 동일하지 않을 때는, ui의 스케일 값을 Vector3로 매개변수에 넣는다.
        var seq = DOTween.Sequence();
        seq.Append(obj.transform.DOScale(beforeEndValue, beforeDuration));// 훅 커지는 효과
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration));// 다시 원상태로 되돌아오는 효과.
        //ui의 로컬스케일이 너무 커서, 훅 커지는 효과를 쓰면 오히려 눈이 아플 경우 ==> 이럴 때는 beforeEndValue를 UI크기보다 작게 줄이고, beforeDuration을 아주 짧게 해서 "점진적으로 커지는"효과를 주면 됨.
    }

    public static void PopupDownAnimationInUI(GameObject obj, Vector3 aftertAndValue, float afterDuration)// UI를 끌 때 사용하는 메서드. UI가 점진적으로 작아진 후 비활성화된다.
    {
        var seq = DOTween.Sequence();
        Vector3 originScale = obj.transform.localScale;// DOScale 실행 후에는 localScale이 고정되어 버리니까, 미리 obj의 크기를 저장해놓는다.
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration)).OnComplete( () =>//UI를 점점 작아지게 만든다.
        {
            obj.SetActive(false);//UI 비활성화.
            obj.transform.localScale = originScale;//저장해놓았던 원래 크기로 UI 크기를 복구.
        }//람다식 형태로 내부에 선언하지 않고, DoScale 실행 후 UI크기를 복구하면 의도대로 실행되지 않는다. DOScale이 끝나기도 전에 localScale을 원래 크기로 돌려버리기 때문.
        );
    }

    public static IEnumerator PopupDownCoroutineInUI(GameObject obj, Vector3 aftertAndValue, float afterDuration, string sceneName)// UI를 끌 때 사용하는 메서드. timeScale이나 SceneManagement를 사용할 때는 부득이하게 코루틴으로 타이밍 조절이 필요하기 때문에 선언.
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;//UI가 작아지는 동안 게임이 멈춰있다면 다시 게임을 진행하도록 설정.
        }
        PopupDownAnimationInUI(obj, aftertAndValue, afterDuration);//UI를 점점 작아지게 만든다. 작아진 후 자동으로 비활성화 및 크기 복구 수행.
        yield return new WaitForSecondsRealtime(afterDuration -0.2f);//UI가 작아지는 동안 대기.
        SceneManager.LoadScene(sceneName);//UI가 작아진 후 씬을 로드한다. 씬 로드 시 UI가 비활성화되어 있기 때문에, UI가 사라지지 않고 씬이 전환되는 현상을 방지하기 위해 UI를 비활성화한 후 씬을 로드한다.
    }


}
