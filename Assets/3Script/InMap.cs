using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class InMap : MonoBehaviour
{
    public Player player;
    public MonsterManager monsterManager;
    public ButtonManager buttonManager;

    public GameObject sampleCard;

    public GameObject ui_lookOn; // ���ؼ� �̹���
    public GameObject map;
    public Button btn_More;
    // ���� UI
    public GameObject monsterUis;
    public GameObject monster_Info_img; // ���� ���� �� ������ ���� �̹���
    public GameObject monster_Info_txt; // ���� ���� �� ������ ���� �ؽ�Ʈ

    public TextMeshProUGUI monster_Name; // ���� �̸� �ؽ�Ʈ
    public TextMeshProUGUI monster_Name_Atk_value; // ���� ���ݷ� ��ġ �ؽ�Ʈ
    public TextMeshProUGUI monster_Name_Health_Value; // ���� ü�� ��ġ �ؽ�Ʈ
    // �÷��̾� UI
    public TextMeshProUGUI stage; // �÷��̾� ��������, ü��, ���ͺ� Ƚ ��
    public TextMeshProUGUI player_Health;
    public GameObject catchMessageBax; // �޽����ڽ�
    public GameObject centerBall; // ���� �� 
    public GameObject[] hasBallImg;
    // ���� ���� �� �ο�or���� ��ư
    public Button btnFight;
    public Button btnRun;

    public Button btnAttack; // ���� ��ư Ŭ��
    public Button btnDefence; // ��� ��ư Ŭ��

    public RectTransform targetImage; // ������ �̹����� RectTransform
    public float shakeAmount = 10f; // ���� ����
    public float shakeDuration = 3f; // ���� ���� �ð�
    public RectTransform shakingMap; // ������ ���� RectTransform
    private Vector3 originalPosition; // �� ��ġ

    public bool cor_isInMonster;
    bool isLookOn;
    public bool isMyTurn;
    int randomID;
    public int stageNum;

    void Start()
    {
        stageNum = 1;
        player_Health.text = player.healt.ToString();
        cor_isInMonster = true;
    }
    public void btnMoreClick()
    {
        stage.text = stageNum.ToString();
        stageNum++;
        randomID = Random.Range(0, 8);
        StartCoroutine(btnMore(randomID));
        btn_More.gameObject.SetActive(false);
        _showMonsterUI(randomID);
    }
    IEnumerator btnMore(int randomID)
    {
        yield return null;
        if (cor_isInMonster)
        {
            Debug.Log("�����մϴ�...!");
            MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
            if (monsterCard != null) // ���� ī�尡 null���� Ȯ��
            {
                int monsterID = monsterCard.id;
                Debug.Log($"{monsterID}�� ����ID�� �����Ǿ����ϴ�...!");
                StartCoroutine(MonsterIn(monsterID));
                StartCoroutine(CameraWaking());
                StartCoroutine(Blink(0, true));
                yield return new WaitForSeconds(1f);
                StartCoroutine(showInfo(monsterID));
            }
        }
    }
    IEnumerator MonsterIn(int monsterId) // ���� ����
    {
        cor_isInMonster = true;
        yield return new WaitForSeconds(2f);
        // ��� ��ư�� ��� �Ͻ� ����
        // �������� ���İ� ����
        Image[] sampleImage = sampleCard.GetComponentsInChildren<Image>();
        if (sampleImage.Length > 1)
        {
            Image image_0 = sampleImage[0];
            Color color = image_0.color;
            color.a = 0;
            image_0.color = color;

            // ������Ʈ�� ���� �̹��� ����
            //Image image_1 = sampleImage[1];

            //RectTransform target = image_1.GetComponentInChildren<RectTransform>();
            //target.sizeDelta = new Vector2(1000, 1100);


            // �θ� ĵ���� ����
            Transform mapPos = map.transform;
            GameObject obj = monsterManager.SpawnMonster(monsterId, mapPos, true, sampleCard);
            Debug.Log($"{monsterId}�� ���Ͱ� �����մϴ�...!");
        }
        cor_isInMonster = false;
    }
    IEnumerator CameraWaking() // ī�޶� ��ŷ
    {
        Vector2 originalPosition = shakingMap.anchoredPosition;
        float count = 0f;
        while (count < shakeDuration)
        {
            // ������ Y ������ ���
            float randomY = Mathf.Sin(count * 5) * shakeAmount; // Sin �Լ��� ����� �ε巴�� ��ȭ
            targetImage.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + randomY);

            count += Time.deltaTime; // ī���� ������Ʈ
            yield return null;
        }
        Debug.Log("ī�޶� ��ŷ");
        shakingMap.anchoredPosition = originalPosition;
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Blink(int count, bool isLookOn)
    {
        if (count >= 7) // �ִ� ������ Ƚ�� üũ
            yield break;

        // UI Ȱ��ȭ/��Ȱ��ȭ
        ui_lookOn.SetActive(isLookOn);

        // ��� ���
        yield return new WaitForSeconds(0.2f);

        // ���� ������ ȣ��
        StartCoroutine(Blink(count + 1, !isLookOn));
    }
    IEnumerator showInfo(int monsterId)
    {
        yield return new WaitForSeconds(4f);
        Sprite monster = monsterManager.GetMonsterImageById(monsterId);
        Debug.Log("������ ������ ���� �˴ϴ�...!");
        Image[] monsterInfoImage = monster_Info_img.GetComponentsInChildren<Image>();
        if (monsterInfoImage.Length > 1)
        {
            monsterInfoImage[2].sprite = monster;
        }
        if (monster == null)
        {
            Debug.LogError($"���� �̹����� null�Դϴ�. ID: {monsterId}");
            yield break;
        }
        monster_Info_img.SetActive(true);
        monster_Info_txt.SetActive(true);
        btnFight.gameObject.SetActive(true);
        btnRun.gameObject.SetActive(true);


    }
    IEnumerator showMonsterUI(int monsterId)
    {
        yield return null;
        string monsterName = monsterManager.GetMonsterByName(monsterId);
        int monsterAtk = monsterManager.GetMonsterByAtk(monsterId);
        int monsterHp = monsterManager.GetMonsterByHP(monsterId);
        if (!string.IsNullOrEmpty(monsterName))
        {
            monster_Name.text = monsterName;
            monster_Name_Atk_value.text = monsterAtk.ToString();
            monster_Name_Health_Value.text = monsterHp.ToString();
            Debug.Log("���� ������ ���ŵǾ����ϴ�...!");
        }
    }
    void _showMonsterUI(int randomID)
    {
        StartCoroutine(showMonsterUI(randomID));
    }
    public void _deleteMonsterCard()
    {
        StartCoroutine(deleteMonsterCard());
    }
    IEnumerator deleteMonsterCard()
    {
        yield return null;
        StartCoroutine(currentImgFadeout());
        yield return null;
        CanClickbtrMore();
    }
    public void CanClickbtrMore()
    {
        if (cor_isInMonster)
        {
            cor_isInMonster = false;
        }
        else
        {
            cor_isInMonster = true;
        }
    }
    public void beAttack(int monsterId) // ���� ��
    {
        int myHealth = player.healt;
        int monsterDamage = int.Parse(monster_Name_Atk_value.text);

        myHealth -= monsterDamage;
        player.healt = myHealth;
        player_Health.text = myHealth.ToString();
        StartCoroutine(shakeImage(map, 0.3f, 10f));
        Debug.Log($"�÷��̾��� ���� ü�� {myHealth}");
    }
    IEnumerator shakeImage(GameObject target, float duration, float magnitude) // ������Ʈ, �ð�(s), ����
    {
        Vector3 originalPos = target.transform.localPosition; //Ÿ���� ���� ��ġ ����

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // x, y������ ���� ���� �ȿ��� �����ϰ� ��ġ ����
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            // ��鸮�� ��ġ�� ����
            target.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
            // ��� �ð� ����
            elapsed += Time.deltaTime;
            yield return null;
        }
        // �� ��ġ�� ����
        target.transform.localPosition = originalPos;
    }
    public void clickAttac() //�����ϱ�, ����ϱ� ��ư
    {
        MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
        int currentID = monsterCard.id;
        StartCoroutine(clickAtk(currentID));
    }
    public void clickDefence() //�����ϱ�, ����ϱ� ��ư
    {
        MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
        int currentID = monsterCard.id;
        StartCoroutine(clickDef(currentID));
    }
    IEnumerator clickAtk(int monsterID) //clickAttackandDefence �ڷ�ƾ
    {
        yield return null;
        Debug.Log($"{isMyTurn}");

        if (isMyTurn)
        {
            btnAttack.GetComponent<Button>().interactable = false;
            Debug.Log("���͸� �����߽��ϴ�.");

            StartCoroutine(onDamage(monsterID));
            Debug.Log("���Ͱ� ������ ���߽��ϴ�.");

            btnAttack.GetComponent<Button>().interactable = true;
            Debug.Log("����� �����Դϴ�. ���ݿ� ����Ͻʽÿ�...!");
            isMyTurn = false;
        }
    }
    IEnumerator clickDef(int monsterID) // ��� ��ư Ŭ�� ��
    {
        yield return null;
        if (!isMyTurn)
        {
            Debug.Log("������ ������ ����ɴϴ�...!");
            beAttack(monsterID);
            isMyTurn = true;
        }
        yield return new WaitForSeconds(2f);
    }
    IEnumerator onDamage(int randomID) // ���Ͱ� ������ �޾��� ��
    {
        int damage = player.attack;
        int health = int.Parse(monster_Name_Health_Value.text);
        GameObject monsterCard = GameObject.FindGameObjectWithTag("Monster");
        if (monsterCard == null)
        {
            Debug.LogError("�±װ� 'Monster'�� ���� ī�尡 �����ϴ�.");
            yield break; // ���� ī�尡 ���� ��� �ڷ�ƾ ����
        }
        // ���� ó��
        Image image = monsterCard.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("�ش� ���� ī�忡�� Image ������Ʈ�� ã�� �� �����ϴ�.");
            yield break; // Image ������Ʈ�� ���� ��� �ڷ�ƾ ����
        }
        if (health > damage)
        {
            yield return new WaitForSeconds(0.2f);
            image.color = Color.red;

            UpdateHealth(damage); // ü�� ���ҿ� UI ������Ʈ
            yield return new WaitForSeconds(0.8f);
            Debug.Log("������ ü���� �𿴽��ϴ�.");
            image.color = Color.white;
        }
        else if (health <= damage)
        {
            btnAttack.gameObject.SetActive(false);
            btnDefence.gameObject.SetActive(false);
            Debug.Log("���� ü���� �����ϴ�!");
            isDie(randomID);
            yield return new WaitForSeconds(0.4f);
        }
    }
    void UpdateHealth(int damage) // ���� ü�� ������Ʈ
    {
        damage = player.attack;
        int health = int.Parse(monster_Name_Health_Value.text);
        if (health > 0)
        {
            health -= damage;
            monster_Name_Health_Value.text = health.ToString();
            Debug.Log($"{health}���� ü��");
        }
    }
    void isDie(int monsterId)
    {
        GameObject monsterCard = GameObject.FindGameObjectWithTag("Monster");
        Debug.Log("���Ͱ� �׾����ϴ�!");
        Image image = monsterCard.GetComponent<Image>();
        image.color = Color.red;
        catchMessageBax.SetActive(true);
    }
    public void throwCenterBall()
    {
        player.hasBall--;
        centerBall.SetActive(false);
        StartCoroutine(shot());
        if (player.hasBall > 0)
        {
            ThrowMonsterBall(player.hasBall);
        }
        else
        {
            Debug.Log("���� ���� ���ͺ��� �����ϴ�...!");
            centerBall.SetActive(false);
            return;
        }
    }

    IEnumerator shot()
    {
        Debug.Log("���͸� ���� �غ� �Ǿ����ϴ�...!");
        yield return new WaitForSeconds(3);
        int random = Random.Range(0, 2);
        MonsterCard currentMonsterID = monsterManager.GetMonsterById(randomID);
        switch (random)
        {
            case 0:
                Debug.Log("���͸� ��ҽ��ϴ�...!");
                monsterUis.SetActive(false);
                _deleteMonsterCard();
                CanClickbtrMore();
                btn_More.gameObject.SetActive(true);
                player.myMonsterCard.Add(currentMonsterID);
                break;
            case 1:
                Debug.Log("���Ͱ� ���� ���� ���� �ֽ��ϴ�...!");
                centerBall.gameObject.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(3);
        //Debug.Log("���� Ž���� ��� �����մϴ�...!");
        //CenterBall.gameObject.SetActive(true);
        //btnAttack.gameObject.SetActive(false);
        //btnDefence.gameObject.SetActive(false);
        //StartCoroutine(FadeInOut(monsterId, 2));
    }
    IEnumerator currentImgFadeout()
    {
        yield return null;
        Debug.Log("���̵� �ƿ�");
        GameObject currentMonster = GameObject.FindGameObjectWithTag("Monster");
        Image currentMonsterImg = currentMonster.GetComponent<Image>();
        Color color = currentMonsterImg.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            currentMonsterImg.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(currentMonster);
    }
    public void ThrowMonsterBall(int ball)
    {
        Debug.Log($"���� �� ���� : {ball}");
        hasBallImg[ball].SetActive(false);
    }
}
