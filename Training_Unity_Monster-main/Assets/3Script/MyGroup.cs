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
            // ���͸� ����
            GameObject monsterObject = monsterManager.SpawnMonster(monsterID, map.transform, false, sampleCard);
            // ��ġ
            RectTransform cardRect = monsterObject.GetComponent<RectTransform>();
            cardRect.anchoredPosition = originalPos;
            originalPos.x += 300f;
            // �̹���
            Image[] imgs = monsterObject.GetComponentsInChildren<Image>();
            if (imgs.Length > 1)
            {
                imgs[2].sprite = monsterManager.GetMonsterImageById(monsterID);
            }

            if (monsterObject != null)
            {
                Debug.Log($"{card.name}�� ���Ͱ� �����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogWarning($"{card.name}�� ���� ������ �����Ͽ����ϴ�.");
            }
        }
    }
    public void delete()
    {
        //�гο��� "Monster" �±׸� ���� ��� �ڽ� ������Ʈ�� ã��
        foreach (Transform monster in map.GetComponentsInChildren<Transform>())
        {
            if (monster.CompareTag("Monster"))
            {
                Debug.Log($"Destroying monster: {monster.name}");
                Destroy(monster.gameObject); // �ν��Ͻ� ����
            }
        }
    }

}
