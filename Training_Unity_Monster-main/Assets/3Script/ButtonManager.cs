using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using System;

public class ButtonManager : MonoBehaviour
{
    public MonsterManager monsterManager;
    public Player player;
    Index index;

    public GameObject targetObject; // ����� ��� ������Ʈ
    public Button toggleButton; // UI ��ư

    public static List<int> clickCounts = new List<int>();
    private int cardIndex;
    public bool isChecked;

    void Start()
    {
        if (player == null)
        {
            Debug.Log("player�� null�Դϴ�.");
        }
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        if (player == null)
        {
            Debug.Log("Player�� ã�� �� �����ϴ�. �±װ� �ùٸ��� Ȯ���ϼ���.");
        }
        if (targetObject != null && targetObject != null)
        {
            toggleButton.onClick.AddListener(ToggleTargetObject);
            clickCounts.Add(0);
        }
    }
    void Update()
    {

    }
    private void ToggleTargetObject()
    {
        if (clickCounts[cardIndex] >= 6 && isChecked == false)
        {
            Debug.Log("������ ī�尡 6���� �ʰ��Ǿ����ϴ�...!");
            return;
        }
        else
        {
            if (targetObject != null)
            {
                // ���� Ȱ��ȭ ���¸� ����
                bool isActiveNow = !targetObject.activeSelf;
                // Ȱ��ȭ ���¸� ����
                targetObject.SetActive(isActiveNow);
                Debug.Log($"{targetObject.name}�� Ȱ��ȭ ���°� {isActiveNow}�� ����Ǿ����ϴ�.");
                // Ȱ��ȭ�� ��� Ŭ�� �� ����
                if (isActiveNow)
                {
                    clickCounts[cardIndex]++;
                    Debug.Log($"{targetObject.name} Ŭ�� ��: {clickCounts[cardIndex]}");
                }
                else
                {
                    // ��Ȱ��ȭ�� ��� Ŭ�� ���� -1 ����
                    clickCounts[cardIndex]--;
                    // Ŭ�� ���� 0 �̸����� �������� �ʵ��� ����
                    if (clickCounts[cardIndex] < 0)
                    {
                        clickCounts[cardIndex] = 0;
                    }
                    Debug.Log($"{targetObject.name}�� ��Ȱ��ȭ�Ǿ����ϴ�. Ŭ�� �� ����: {clickCounts[cardIndex]}");
                }
                isChecked = isActiveNow;
            }
            else
            {
                Debug.LogWarning("����� ��� ������Ʈ�� �������� �ʾҽ��ϴ�.");
            }
        }
    }
}
