using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupUILoader : MonoBehaviour
{
    //�˾� �гο��� �������� �� �� �ִ� �˾�UI�δ� Ŭ����. �˾��� ��ư���� Ȱ��ȭ�ǰ�, �˾� �� Cloase��ư���� ��Ȱ��ȭ�ȴ�. ���� �˾��� Ȱ��ȭ�� ���¿����� �˾� ���� ��ư�� �۵����� �ʾƾ� �Ѵ�.
    //�Ʒ� �� �ٸ� UI���� �̺�Ʈ�� ���� ���ؼ�, �˾� �ڿ� Blocker��� ������ �̹����� �˾� �ڿ� �ٿ����´�. �� Blocker�� �Ʒ� �� UI�� ��� Raycast�� �ް� �ؼ� �ǵ�ġ���� Ŭ�� �̺�Ʈ�� �����Ѵ�. ���� ���� �����.
    //Blocker�� �˾� �г��� ���� ���ο� �ִ� �ڽ����� ������ ������ �־��, ���� ���� �������ǰ� ���� �������� �׷����Ƿ�, Raycast�� ���� ���� �� �ִ�.
    [SerializeField] private Button popupButton;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image blocker;//���� Panel - PopUpPanelAnchor�� ù��° �ڽ��� Blocker�̹���. �ι�° �ڽ��� �˾� �г��̴�. �˾��гκ��� ���ο� �����ϰ� �θ� ĵ���� ũ��� �����ϰ� ���߾� ���ұ⿡ �˾��г� ���� ������Ʈ�� Ŭ������ �ʴ´�.
    void Start()
    {
        BlockerCheck();
        popupButton.onClick.AddListener(PopupButtonClicked);
        popupPanel.SetActive(false);
    }

    private void Update() 
    {
        BlockerDisable();//�˾� �г��� ��ũ��Ʈ �ܺο� ������ closebutton���ε� ���� �� �� �ֱ� ������, �˾��г��� ������ ���Ŀ�� ���� �������� �����ؾ� ��.
    }

    private void BlockerCheck()
    {
        if(blocker==null)
        {
            blocker = GameObject.Find("Blocker").GetComponent<Image>();
        }
        blocker.gameObject.SetActive(false);//�˾� �г��� ������ �ʾҴٸ� ���Ŀ�� ��Ȱ��ȭ ����.
    }

    private void BlockerActive()
    {
        if(!blocker.gameObject.activeSelf)
        {
            blocker.gameObject.SetActive(true);
        }
    }

    private void PopupButtonClicked()
    {
        if(!popupPanel.activeSelf)
        {
            popupPanel.SetActive(true);
            BlockerActive();
        }
    }

    private void BlockerDisable()
    {
        if(popupPanel.activeSelf)
        {
            blocker.gameObject.SetActive(true);
        }
        else
        {
            blocker.gameObject.SetActive(false);
        }
    }

}
