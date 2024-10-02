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

    public GameObject targetObject; // 토글할 대상 오브젝트
    public Button toggleButton; // UI 버튼

    public static List<int> clickCounts = new List<int>();
    private int cardIndex;
    public bool isChecked;

    void Start()
    {
        if (player == null)
        {
            Debug.Log("player가 null입니다.");
        }
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        if (player == null)
        {
            Debug.Log("Player를 찾을 수 없습니다. 태그가 올바른지 확인하세요.");
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
            Debug.Log("선택한 카드가 6장이 초과되었습니다...!");
            return;
        }
        else
        {
            if (targetObject != null)
            {
                // 현재 활성화 상태를 반전
                bool isActiveNow = !targetObject.activeSelf;
                // 활성화 상태를 반전
                targetObject.SetActive(isActiveNow);
                Debug.Log($"{targetObject.name}의 활성화 상태가 {isActiveNow}로 변경되었습니다.");
                // 활성화된 경우 클릭 수 증가
                if (isActiveNow)
                {
                    clickCounts[cardIndex]++;
                    Debug.Log($"{targetObject.name} 클릭 수: {clickCounts[cardIndex]}");
                }
                else
                {
                    // 비활성화된 경우 클릭 수를 -1 감소
                    clickCounts[cardIndex]--;
                    // 클릭 수가 0 미만으로 떨어지지 않도록 방지
                    if (clickCounts[cardIndex] < 0)
                    {
                        clickCounts[cardIndex] = 0;
                    }
                    Debug.Log($"{targetObject.name}이 비활성화되었습니다. 클릭 수 감소: {clickCounts[cardIndex]}");
                }
                isChecked = isActiveNow;
            }
            else
            {
                Debug.LogWarning("토글할 대상 오브젝트가 설정되지 않았습니다.");
            }
        }
    }
}
