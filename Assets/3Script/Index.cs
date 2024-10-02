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

    public void btnIndex() // �ε� ����
    {
        //myHasCard = player.myMonsterCard;
        
        if (player.myMonsterCard.Count > 0)
        {
            Debug.Log("������ ī�带 �����մϴ�...!");
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
            Debug.Log($"{card.name}�� ī���� �����͸� �����ɴϴ�...!");
            int monsterID = card.id;

            

            if (sampleCard != null)
            {
                GameObject monsterObject = monsterManager.SpawnMonster(monsterID, panel.transform, false, sampleCard);

                // ���� �̹��� ����
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
                // ī�� ��ġ ����
                RectTransform cardRect = monsterObject.GetComponent<RectTransform>();
                cardRect.anchoredPosition = originalPos;

                // ���� ��ġ ������Ʈ
                originalPos.x += 280f;

                // ī�尡 6�帶�� �� �ٲ�
                if ((i + 1) % 6 == 0) // ���� �ε����� ���� �� �ٲ�
                {
                    originalPos.y -= 450f; // y��ǥ ����
                    originalPos.x = -750; // x�� �ʱ�ȭ
                }
            }
        }
    }
    public void getButton()
    {
        Debug.Log("Ŭ����");
    }
    public void delete()
    {
        // �гο��� "Monster" �±׸� ���� ��� �ڽ� ������Ʈ�� ã��
        foreach (Transform monster in panel.GetComponentsInChildren<Transform>())
        {
            if (monster.CompareTag("Monster"))
            {
                Debug.Log($"Destroying monster: {monster.name}");
                Destroy(monster.gameObject); // �ν��Ͻ� ����
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
