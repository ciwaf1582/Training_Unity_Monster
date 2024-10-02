using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGroup : MonoBehaviour
{
    public MonsterManager monsterManager;
    public Player player;

    public GameObject sampleCard;
    public GameObject map;

    public void selectedShow()
    {
        Vector2 originalPos = sampleCard.GetComponent<RectTransform>().anchoredPosition;
        originalPos.x = sampleCard.GetComponent<RectTransform>().anchoredPosition.x;
        for (int i = 0; i < player.myChoiceCard.Count; i++)
        {
            MonsterCard card = player.myMonsterCard[i];
            int monsterID = card.id;
            // 몬스터를 스폰
            GameObject monsterObject = monsterManager.SpawnMonster(monsterID, map.transform, false, sampleCard);
            // 위치
            RectTransform cardRect = monsterObject.GetComponent<RectTransform>();
            cardRect.anchoredPosition = originalPos;
            originalPos.x += 300f;
            // 이미지
            Image[] imgs = monsterObject.GetComponentsInChildren<Image>();
            if (imgs.Length > 1)
            {
                imgs[2].sprite = monsterManager.GetMonsterImageById(monsterID);
            }

            if (monsterObject != null)
            {
                Debug.Log($"{card.name}의 몬스터가 생성되었습니다.");
            }
            else
            {
                Debug.LogWarning($"{card.name}의 몬스터 생성에 실패하였습니다.");
            }
        }
    }
    public void delete()
    {
        //패널에서 "Monster" 태그를 가진 모든 자식 오브젝트를 찾음
        foreach (Transform monster in map.GetComponentsInChildren<Transform>())
        {
            if (monster.CompareTag("Monster"))
            {
                Debug.Log($"Destroying monster: {monster.name}");
                Destroy(monster.gameObject); // 인스턴스 삭제
            }
        }
    }

}
