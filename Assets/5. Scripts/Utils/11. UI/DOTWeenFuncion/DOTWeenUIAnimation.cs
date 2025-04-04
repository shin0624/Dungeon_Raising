using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DOTWeenUIAnimation// UI ��Ʈ�ѷ� Ŭ�������� DOTWeen ����� ������ �ʰ� �ٷ� ����� �� �ְ� �ϴ� Ŭ����. ��𼭵� ���� ȣ���ϱ� ���� ���� Ŭ������ ����.
{
    //UI�� ��Ʈ���ϴ� Ŭ�������� DOTWeen�� ����� ����ϱ� ���� ������ DG.Tweening ���ӽ����̽��� �������� �ʾƵ� �ǰ�, �ִϸ��̼� �޼��嵵 ������ �ʾƵ� ��.
    private static bool isInitialized = false;// DOTWeen�� Init()�� �� ���� ȣ��� �� �ֵ��� �����ϴ� �÷���.
    public static void Init()// Managers.cs�� Init()���� �� ���� ȣ���.
    {
        if(!isInitialized)//DOTWeen�� �ʱ�ȭ�� �� ���� ����Ǹ� ���� ���� �����Ǵ� �̱��� ����̹Ƿ�, TitleScene���� Managers�� ���� �� ���� �ʱ�ȭ�ȴ�.
        {
            DOTween.Init();
            isInitialized = true;
        }
    }

    public static void PopupAnimationInUI(GameObject obj, float beforeEndValue, float beforeDuration, float aftertAndValue, float afterDuration)// UIũ�⸦ �Ҽ��� ������ �����ϰ��� �� �� ���.
    {
        //UI �˾��� �� Ŀ���ٰ� ���� ũ��� ���ƿ��� ȿ�� ������ ���� �޼���. �������� �����ϰ�, �˾��� Ȱ��ȭ�ǰ� �� Ŀ�� ���� ũ��� ���ӽð�, �ٽ� ���ƿ� ũ��� �� �� �ȿ� ���ƿ� �������� �Ű������� ����.
        var seq = DOTween.Sequence();
        seq.Append(obj.transform.DOScale(beforeEndValue, beforeDuration));// �� Ŀ���� ȿ��
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration));// �ٽ� �����·� �ǵ��ƿ��� ȿ��.
        // �˾��� ó�� ���� �� ȿ���� ������ �ϸ� before -> after ������ �Ű������� �ְ�, �˾��� ���� ȿ��(���� ũ�⿡�� �پ��� �� �ϰ� �������)�� ������ �ϸ� after -> before������ �ֵ�, after�� ���簪, before�� �۾��� ���� �ִ´�.
    }

    public static void PopupAnimationInUI(GameObject obj, Vector3 beforeEndValue, float beforeDuration, Vector3 aftertAndValue, float afterDuration)
    {
        //UI �˾��� �� Ŀ���ٰ� ���� ũ��� ���ƿ��� ȿ�� ������ ���� �޼���. UI�� w, h���� �������� ���� ����, ui�� ������ ���� Vector3�� �Ű������� �ִ´�.
        var seq = DOTween.Sequence();
        seq.Append(obj.transform.DOScale(beforeEndValue, beforeDuration));// �� Ŀ���� ȿ��
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration));// �ٽ� �����·� �ǵ��ƿ��� ȿ��.
        //ui�� ���ý������� �ʹ� Ŀ��, �� Ŀ���� ȿ���� ���� ������ ���� ���� ��� ==> �̷� ���� beforeEndValue�� UIũ�⺸�� �۰� ���̰�, beforeDuration�� ���� ª�� �ؼ� "���������� Ŀ����"ȿ���� �ָ� ��.
    }

    public static void PopupDownAnimationInUI(GameObject obj, Vector3 aftertAndValue, float afterDuration)// UI�� �� �� ����ϴ� �޼���. UI�� ���������� �۾��� �� ��Ȱ��ȭ�ȴ�.
    {
        var seq = DOTween.Sequence();
        Vector3 originScale = obj.transform.localScale;// DOScale ���� �Ŀ��� localScale�� �����Ǿ� �����ϱ�, �̸� obj�� ũ�⸦ �����س��´�.
        seq.Append(obj.transform.DOScale(aftertAndValue, afterDuration)).OnComplete( () =>//UI�� ���� �۾����� �����.
        {
            obj.SetActive(false);//UI ��Ȱ��ȭ.
            obj.transform.localScale = originScale;//�����س��Ҵ� ���� ũ��� UI ũ�⸦ ����.
        }//���ٽ� ���·� ���ο� �������� �ʰ�, DoScale ���� �� UIũ�⸦ �����ϸ� �ǵ���� ������� �ʴ´�. DOScale�� �����⵵ ���� localScale�� ���� ũ��� ���������� ����.
        );
    }

    public static IEnumerator PopupDownCoroutineInUI(GameObject obj, Vector3 aftertAndValue, float afterDuration, string sceneName)// UI�� �� �� ����ϴ� �޼���. timeScale�̳� SceneManagement�� ����� ���� �ε����ϰ� �ڷ�ƾ���� Ÿ�̹� ������ �ʿ��ϱ� ������ ����.
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;//UI�� �۾����� ���� ������ �����ִٸ� �ٽ� ������ �����ϵ��� ����.
        }
        PopupDownAnimationInUI(obj, aftertAndValue, afterDuration);//UI�� ���� �۾����� �����. �۾��� �� �ڵ����� ��Ȱ��ȭ �� ũ�� ���� ����.
        yield return new WaitForSecondsRealtime(afterDuration -0.2f);//UI�� �۾����� ���� ���.
        SceneManager.LoadScene(sceneName);//UI�� �۾��� �� ���� �ε��Ѵ�. �� �ε� �� UI�� ��Ȱ��ȭ�Ǿ� �ֱ� ������, UI�� ������� �ʰ� ���� ��ȯ�Ǵ� ������ �����ϱ� ���� UI�� ��Ȱ��ȭ�� �� ���� �ε��Ѵ�.
    }


}
