using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class AdventuerButton : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] GameObject btnExit;

    [SerializeField] UnityEngine.UI.Button btnStart;
    public UnityEngine.UI.Button b1;
    public UnityEngine.UI.Button b2;
    public UnityEngine.UI.Button b3;
    public UnityEngine.UI.Button b4;

    public GameObject Img1;
    public GameObject Img2;
    public GameObject Img3;
    public GameObject Img4;

    private int selectedButton = -1;


    void Start()
    {
        // b?��ư�� Ŭ�� �� AddLisstener(Ŭ�� �� ������ �޼��带 ���) => OnSelectButton()�޼ҵ� ȣ��
        b1.onClick.AddListener(() => OnSelectButton(1));
        b2.onClick.AddListener(() => OnSelectButton(2));
        b3.onClick.AddListener(() => OnSelectButton(3));
        b4.onClick.AddListener(() => OnSelectButton(4));

        btnStart.onClick.AddListener(OnStartButtonClicked);
    }
    public void OnSelectButton(int buttonNumber)
    {
        selectedButton = buttonNumber;
    }
    void OnStartButtonClicked()
    {
        if (selectedButton == 1)
        {
            UnityEngine.Debug.Log("1�� ��ư ���� �� ������ �����մϴ�.");
            EnterDungeon();  // ���� ���� �޼��� ȣ��
        }
        else if (selectedButton == 2)
        {
            UnityEngine.Debug.Log("2�� ��ư ���� �� ������ �����մϴ�.");
            EnterDungeon();  // ���� ���� �޼��� ȣ��
        }
        else if (selectedButton == 3)
        {
            UnityEngine.Debug.Log("3�� ��ư ���� �� ������ �����մϴ�.");
            EnterDungeon();  // ���� ���� �޼��� ȣ��
        }
        else if (selectedButton == 4)
        {
            UnityEngine.Debug.Log("4�� ��ư ���� �� ������ �����մϴ�.");
            EnterDungeon();  // ���� ���� �޼��� ȣ��
        }

    }
    public void EnterDungeon()
    {
        if (selectedButton == 1)
        {
            UnityEngine.Debug.Log($"{selectedButton}�������� �̵��մϴ�.");
            manager.Ground_Stage.SetActive(true);
            closeImg();
        }
        if (selectedButton == 2)
        {
            UnityEngine.Debug.Log($"{selectedButton}�������� �̵��մϴ�.");
            manager.Jungle_Stage.SetActive(true);
            closeImg();
        }
        if (selectedButton == 3)
        {
            UnityEngine.Debug.Log($"{selectedButton}�������� �̵��մϴ�.");
            manager.Mountain_Stage.SetActive(true);
            closeImg();
        }
        if (selectedButton == 4)
        {
            UnityEngine.Debug.Log($"{selectedButton}�������� �̵��մϴ�.");
            manager.Snow_Stage.SetActive(true);
            closeImg();
        }

    }
    public void clickExit()
    {
        manager.MainCanvars.SetActive(true);
        manager.AdventureCanvars.SetActive(false);
    }
    public void closeImg()
    {
        Img1.SetActive(false);
        Img2.SetActive(false);
        Img3.SetActive(false);
        Img4.SetActive(false);
    }

    

}
