using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfoPanelSetting : MonoBehaviour
{
    // MainScene�� CharacterInfoPanel ������ ���� Ŭ����. �÷��̾�â�� �г���, ����, Ŭ���� �� ��, ������, ����ġ�� ���� �÷��̾ �°� �����Ѵ�.
    [SerializeField] private TextMeshProUGUI characterName; 
    private const int MaxCharacterLimit = 10;
    private const float FontSizeLarge = 34.7f;
    private const float FontSizeSmall = 30.0f;
    void Awake()
    {
        MainSceneInit();
    }

    private void MainSceneInit()
    {
        StartCoroutine(DelaySetCharacterName());
    }

    private void SetCharacterName()//�÷��̾� �� = �÷��̾� �Է� ������ �����ϴ� �޼���
    {
        if(PlayerInfo.Instance==null)//���� PlayerInfo�� �г����� ���������� �������� �ʾ��� ���� �ӽð��� ����
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            characterName.text = "Unknown Player";
            return;
        }
        characterName.text =PlayerInfo.Instance.GetPlayerNickname();//MainScene�� �÷��̾� �� = ĳ���� ���� ������ �÷��̾ �Է��ߴ� �г������� ����
    }

    private IEnumerator DelaySetCharacterName()//�г��� ������ �Ϲ� �޼���� �ۼ� ��, �� ��ȯ �� PlayerInfo�� �����Ͱ� �����Ǳ� ������ UI�� ������Ʈ�Ǿ�, �г��ӿ� �� ���� ���޵Ǵ� ��츦 Ȯ����. �̸� �����ϱ� ���� �ڷ�ƾ�� ����Ͽ� �� �ε� �� PlayerInfo �����Ͱ� ������ ������ �� ���� ��ٸ� �� UI�� �����ϵ��� ��.
    {
        yield return new WaitForSeconds(0.5f);
        SetCharacterName();
    }


}
