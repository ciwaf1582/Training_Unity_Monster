using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JungleMap : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] UnityEngine.UI.Button inButton;

    public Button stage1;
    public Button stage2;

    public GameObject jungle_in_map;
    public GameObject mapInpo_1;

    private int selectedButton = -1;
    private void Update()
    {
        
    }
    public void Start() // �������� ��ư Ŭ�� �� ���
    {
        stage1.onClick.AddListener(() => OnSelectButton(1));
        stage2.onClick.AddListener(() => OnSelectButton(2));

        inButton.onClick.AddListener(OninMapBtnClick);
    }
    public void OnSelectButton(int buttonNumber) // ���
    {
        selectedButton = buttonNumber;
    }
    public void OninMapBtnClick()
    {
        if (selectedButton == 1)
        {
            UnityEngine.Debug.Log("����1�� ��ư ���� �� ������ �����մϴ�.");
            mapInpo_1.SetActive(true);
        }
        else if (selectedButton == 2)
        {
            UnityEngine.Debug.Log("����2�� ��ư ���� �� ������ �����մϴ�.");
            mapInpo_1.SetActive(true);
        }

    }
    public void inMap() // �ش� �������� ����
    {
        if (selectedButton == 1)
        {
            jungle_in_map.SetActive(true);
        }
        else if (selectedButton == 2)
        {
            jungle_in_map.SetActive(true);
        }
    }
    //public void showInfoImg(GameObject gameobject) // �� ���� �̹���
    //{
    //    gameObject.SetActive(true);
    //}
    public void Exit()
    {
        gameManager.AdventureCanvars.SetActive(true);
        gameManager.Jungle_Stage.SetActive(false);
    }
}
