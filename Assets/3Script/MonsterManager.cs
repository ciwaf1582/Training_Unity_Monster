using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Threading;
[System.Serializable]
public class MonsterCard
{
    public int id; // ���� ID
    public string name; // ���� �̸�
    public int atk; // ���� ���ݷ�
    public int hp; // ���� ü��
    public Sprite face; // ������ �̹���
    public string house;
    public bool isChecked;
    public MonsterCard(int id, string name ,int atk, int hp, Sprite face, string house, bool isChecked)
    {
        this.id = id;
        this.name = name;
        this.atk = atk;
        this.hp = hp;
        this.face = face;
        this.house = house;
        this.isChecked = isChecked;
    }
}
public class MonsterManager : MonoBehaviour
{
    public Player player;
    MonsterInfo MonsterInfo;
    public GameObject SampleMonster;
    public List<MonsterCard> allMonsters = new List<MonsterCard>();
    public MonsterCard GetMonsterById(int id) // ���� IDã��
    {
        // ���ǿ� �����ϴ� �⺻���� ��ȯ
        // MonsterCard��ü�� monster��� �����ϰ� IDȮ��
        return allMonsters.FirstOrDefault(monster => monster.id == id);
    }
    public string GetMonsterByName(int id) // ���� IDã��
    {
        // ���ǿ� �����ϴ� �⺻���� ��ȯ
        // MonsterCard��ü�� monster��� �����ϰ� IDȮ��
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.name : "Unknown";
    }
    public int GetMonsterByAtk(int id) // ���� ATKã��
    {
        // ���ǿ� �����ϴ� �⺻���� ��ȯ
        // MonsterCard��ü�� monster��� �����ϰ� IDȮ��
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.atk : 0;
    }
    public int GetMonsterByHP(int id) // ���� HPã��
    {
        // ���ǿ� �����ϴ� �⺻���� ��ȯ
        // MonsterCard��ü�� monster��� �����ϰ� IDȮ��
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.hp : 0;
    }
    public Sprite GetMonsterImageById(int id) // ���� �̹��� ã��
    {
        // ���ǿ� �����ϴ� �⺻���� ��ȯ
        MonsterCard monster = allMonsters.FirstOrDefault(m => m.id == id);
        return monster?.face; // �̹����� ������ ��ȯ, ������ null ��ȯ
    }
    public int GetIdByImage(Sprite image)
    {
        // �̹����� ��ġ�ϴ� ���͸� ã�� �ش� ������ ID�� ��ȯ
        MonsterCard monster = allMonsters.FirstOrDefault(m => m.face == image);
        return monster != null ? monster.id : -1; // ������ -1 ��ȯ
    }
    void Start()
    {
        AddMonster(0, "Dark Wolf", 10, 10, "Monster_jungle_1-removebg-preview", "Jungle", false);
        AddMonster(1, "Fire Mouse", 2, 4, "Monster_jungle_2-removebg-preview", "Jungle", false);
        AddMonster(2, "Carbuncle", 5, 5, "Monster_jungle_3-removebg-preview", "Jungle", false);
        AddMonster(3, "Hi-Gle", 4, 7, "Monster_jungle_4-removebg-preview", "Jungle", false);
        AddMonster(4, "Plant-Wolf", 4, 13, "Monster_jungle_5-removebg-preview", "Jungle", false);
        AddMonster(5, "Siro-Monoke", 12, 20, "Monster_jungle_6-removebg-preview", "Jungle", false);
        AddMonster(6, "Mandago", 5, 7, "Monster_jungle_7-removebg-preview", "Jungle", false);
        AddMonster(7, "Maka_Fox", 7, 8, "Monster_jungle_8-removebg-preview", "Jungle", false);
    }
    public void AddMonster(int id, string name, int atk, int hp, string faceName, string house, bool isChecked) // ���� �߰�
    {
        // ���� �̹��� ���ҽ�
        string monsterFaceResources = "MonsterImage/";
        Sprite face = Resources.Load<Sprite>(monsterFaceResources + faceName);
 
        MonsterCard monster = new MonsterCard(id, name, atk, hp, face, house, isChecked);
        allMonsters.Add(monster);
    }
    public GameObject SpawnMonster(int id, Transform parentTransform, bool fadeIn, GameObject sampleCard)
    {
        // ���� ī�� ���� ��������
        MonsterCard monster = GetMonsterById(id);
        Sprite monsterFace = GetMonsterImageById(id);

        // ��ȿ�� ������ ��� �ν��Ͻ� ����
        if (monster != null)
        {
            // ���� ī�带 �����Ͽ� ���ο� ���� ������Ʈ ����
            GameObject monsterObject = Instantiate(sampleCard, parentTransform.position, Quaternion.identity, parentTransform);

            // ������ �̹��� ����
            Image monsterImage = monsterObject.GetComponentInChildren<Image>(); // ���� �̹��� ������Ʈ ��������
            if (monsterImage != null)
            {
                monsterImage.sprite = monsterFace; // �̹��� ����
                //monsterImage[2].preserveAspect = true; // ���� ����
            }

            // ���� ������Ʈ�� �߰����� ���� ���� (�ʿ��)
            MonsterInfo monsterInfo = monsterObject.GetComponent<MonsterInfo>();
            if (monsterInfo != null)
            {
                monsterInfo.id = monster.id;
                monsterInfo.monsterName = monster.name;
                monsterInfo.atk = monster.atk;
                monsterInfo.hp = monster.hp;
                monsterInfo.face = monsterFace;
                monsterInfo.house = monster.house;
            }

            // ���̵� �� ȿ�� ���� (���� ����)
            if (fadeIn)
            {
                StartCoroutine(FadeIn(monsterImage)); // ���̵� �� ȿ��
            }

            return monsterObject; // ������ ���� ������Ʈ ��ȯ
        }

        // ���Ͱ� ��ȿ���� ���� ��� null ��ȯ
        return null;
    }
    public GameObject SpawnMonsterCube(int id, Transform parentTransform, GameObject sampleCard)
    {
        // ���� ī�� ���� ��������
        MonsterCard monster = GetMonsterById(id);
        Sprite monsterFace = GetMonsterImageById(id);

        // ��ȿ�� ������ ��� �ν��Ͻ� ����
        if (monster != null)
        {
            // ���� ī�带 �����Ͽ� ���ο� ���� ������Ʈ ����
            GameObject monsterObject = Instantiate(sampleCard, parentTransform.position, Quaternion.identity, parentTransform);
            MonsterInfo monsterInfo = monsterObject.GetComponent<MonsterInfo>();
            if (monsterInfo != null)
            {
                monsterInfo.id = monster.id;
                monsterInfo.monsterName = monster.name;
                monsterInfo.atk = monster.atk;
                monsterInfo.hp = monster.hp;
                monsterInfo.face = monsterFace;
                monsterInfo.house = monster.house;
            }
            return monsterObject; // ������ ���� ������Ʈ ��ȯ
        }


        // ���Ͱ� ��ȿ���� ���� ��� null ��ȯ
        return null;
    }
    public void checkedCard()
    {
        foreach (MonsterCard card in player.myChoiceCard)
        {
            card.isChecked = true;
        }
    }
    //public GameObject SpawnMonster(int id, Transform canvas, bool fadeIn, GameObject sampleCard) // �ν��Ͻ�
    //{
    //    MonsterCard card = GetMonsterById(id);
    //    GameObject gameObject = Instantiate(sampleCard, Vector3.zero, Quaternion.identity);

    //    return gameObject;
    //MonsterCard monster = GetMonsterById(id);
    //Sprite monsterFace = GetMonsterImageById(id);
    //if (monster != null)
    //{
    //    // ĵ���� ��ġ
    //    Transform newParent = canvas.transform;
    //    // �ν��Ͻ�
    //    GameObject monsterObject = Instantiate(sampleCard, Vector3.zero, Quaternion.identity);

    //    monsterObject.tag = "Monster";
    //    monsterObject.transform.SetParent(newParent, false);

    //    // ������Ʈ ��ġ
    //    RectTransform rectTransform = monsterObject.GetComponent<RectTransform>();
    //    rectTransform.anchoredPosition = Vector2.zero; // ���ϴ� ��ġ�� ���� (0, 0)

    //    Image monsterImage = monsterObject.GetComponentInChildren<Image>(); // �ڽ� �̹��� ������Ʈ ã��
    //    if (monsterImage != null)
    //    {
    //        // ���� �̹���
    //        monsterImage.sprite = monsterFace; // ���� ī�忡�� �ε�� ��������Ʈ ����
    //        if (fadeIn)
    //        StartCoroutine(FadeIn(monsterImage)); // ���̵� �� ȿ�� ����
    //    }
    //    return monsterObject;
    //}
    //return null;
    // ���̵� ȿ��
    private IEnumerator FadeIn(Image image)
    {
        Color color = image.color;
        color.a = 0; // �ʱ� ���İ��� 0���� ����
        image.color = color;

        float duration = 1f; // ���̵� �� ���� �ð�
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration); // ���İ� ����
            image.color = color;
            yield return null; // ���� �����ӱ��� ���
        }

        color.a = 1; // ���� ���İ� ����
        image.color = color;
    }
}
