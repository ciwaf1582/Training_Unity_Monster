using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Color = UnityEngine.Color;
using Image = UnityEngine.UI.Image;
using System.Threading;

public class Index : MonoBehaviour
{
    public GameObject panel;
    public MonsterManager monsterManager;
    public ButtonManager buttonManager;
    public Player player;

    public GameObject sampleCard;
    public Sprite img2;
    public Sprite img3;
    public TextMeshProUGUI Num;

    MonsterInfo monsterinfo;

    bool isClicked;

    public void btnIndex() // 로드 손패
    {
        //myHasCard = player.myMonsterCard;
        
        if (player.myMonsterCard.Count > 0)
        {
            Debug.Log("소지한 카드를 열람합니다...!");
            showCard(player.myMonsterCard.Count);
        }
    }
    void showCard(int cardNum)
    {
        Num.text = player.myMonsterCard.Count.ToString();
        Vector2 originalPos = sampleCard.GetComponent<RectTransform>().anchoredPosition;

        originalPos.x = sampleCard.GetComponent<RectTransform>().anchoredPosition.x;
        originalPos.y = sampleCard.GetComponent<RectTransform>().anchoredPosition.y;

        for (int i = 0; i < player.myMonsterCard.Count; i++)
        {
            MonsterCard card = player.myMonsterCard[i];
            Debug.Log($"{card.name}의 카드의 데이터를 가져옵니다...!");
            int monsterID = card.id;

            

            if (sampleCard != null)
            {
                GameObject monsterObject = monsterManager.SpawnMonster(monsterID, panel.transform, false, sampleCard);

                // 몬스터 이미지 조정
                monsterManager.GetMonsterImageById(monsterID);
                Image[] imgs = monsterObject.GetComponentsInChildren<Image>();
                if (imgs.Length > 1)
                {
                    imgs[0].sprite = null;
                    imgs[1].gameObject.SetActive(false);
                    imgs[2].sprite = monsterManager.GetMonsterImageById(monsterID);
                    imgs[3].sprite = img2;
                    imgs[4].sprite = img3;
                    if (card.isChecked) imgs[5].gameObject.SetActive(true);
                    else { imgs[5].gameObject.SetActive(false); }
                }
                // 카드 위치 조정
                RectTransform cardRect = monsterObject.GetComponent<RectTransform>();
                cardRect.anchoredPosition = originalPos;

                // 원래 위치 업데이트
                originalPos.x += 280f;

                // 카드가 6장마다 줄 바꿈
                if ((i + 1) % 6 == 0) // 현재 인덱스에 따라 줄 바꿈
                {
                    originalPos.y -= 450f; // y좌표 감소
                    originalPos.x = -750; // x값 초기화
                }
            }
        }
    }
    public void getButton()
    {
        Debug.Log("클릭됨");
    }
    public void delete()
    {
        // 패널에서 "Monster" 태그를 가진 모든 자식 오브젝트를 찾음
        foreach (Transform monster in panel.GetComponentsInChildren<Transform>())
        {
            if (monster.CompareTag("Monster"))
            {
                Debug.Log($"Destroying monster: {monster.name}");
                Destroy(monster.gameObject); // 인스턴스 삭제
            }
        }
    }
    public void Clicked(GameObject gameObject)
    {
        gameObject = sampleCard;
        Button btn = sampleCard.GetComponentInChildren<Button>();
        btn.onClick.AddListener(savedBtn);
    }
    private void savedBtn()
    {
        Debug.Log("SAVEBTN");
    }
}
