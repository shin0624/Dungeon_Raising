using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TitleLoginButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button loginButton;//�α��ι�ư�� ��ư ��ü
    [SerializeField] private Image loginButtonImage;//�α��� ��ư�� �̹��� ��ü
    [SerializeField] private Sprite loginButtonClickedSprite;//�α��ι�ư Ŭ�� �� ����� ��������Ʈ ��ü
    [SerializeField] private Sprite loginButtonHoveredSprite;//�α��� ��ư ȣ���� �� ���� �� ��������Ʈ ��ü
    [SerializeField] private Sprite loginButtonDefaultSprite;//�α��� ��ư�� �⺻ ��������Ʈ
    void Start()
    {
        loginButtonImage = GetComponent<Image>();//�α��ι�ư�� �̹����� �����´�.
        loginButtonImage.sprite = loginButtonDefaultSprite;//ó���� �⺻ ��������Ʈ�� ����.
        loginButton.onClick.AddListener(LoginButtonClicked);//��ư ��ü�� Ŭ�� �� �̺�Ʈ �����ʸ� ����Ѵ�.       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        loginButtonImage.sprite = loginButtonHoveredSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        loginButtonImage.sprite = loginButtonDefaultSprite;
    }

    void LoginButtonClicked()
    {
        loginButtonImage.sprite = loginButtonClickedSprite;//�α��� ��ư Ŭ�� �� ������ ��������Ʈ�� ��ü
        LoadCharacterSelectScene();//�α��� ��ư Ŭ�� �� ĳ���� ���� ������ �̵�
    }

    void LoadCharacterSelectScene()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
