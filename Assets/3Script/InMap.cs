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

    public GameObject ui_lookOn; // 조준선 이미지
    public GameObject map;
    public Button btn_More;
    // 몬스터 UI
    public GameObject monsterUis;
    public GameObject monster_Info_img; // 몬스터 등장 시 정보를 담은 이미지
    public GameObject monster_Info_txt; // 몬스터 등장 시 정보를 담은 텍스트

    public TextMeshProUGUI monster_Name; // 몬스터 이름 텍스트
    public TextMeshProUGUI monster_Name_Atk_value; // 몬스터 공격력 수치 텍스트
    public TextMeshProUGUI monster_Name_Health_Value; // 몬스터 체력 수치 텍스트
    // 플레이어 UI
    public TextMeshProUGUI stage; // 플레이어 스테이지, 체력, 몬스터볼 횟 수
    public TextMeshProUGUI player_Health;
    public GameObject catchMessageBax; // 메시지박스
    public GameObject centerBall; // 던질 볼 
    public GameObject[] hasBallImg;
    // 몬스터 조우 시 싸움or도망 버튼
    public Button btnFight;
    public Button btnRun;

    public Button btnAttack; // 공격 버튼 클릭
    public Button btnDefence; // 방어 버튼 클릭

    public RectTransform targetImage; // 움직일 이미지의 RectTransform
    public float shakeAmount = 10f; // 진동 강도
    public float shakeDuration = 3f; // 진동 지속 시간
    public RectTransform shakingMap; // 진동할 맵의 RectTransform
    private Vector3 originalPosition; // 원 위치

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
            Debug.Log("전진합니다...!");
            MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
            if (monsterCard != null) // 몬스터 카드가 null인지 확인
            {
                int monsterID = monsterCard.id;
                Debug.Log($"{monsterID}의 몬스터ID로 결정되었습니다...!");
                StartCoroutine(MonsterIn(monsterID));
                StartCoroutine(CameraWaking());
                StartCoroutine(Blink(0, true));
                yield return new WaitForSeconds(1f);
                StartCoroutine(showInfo(monsterID));
            }
        }
    }
    IEnumerator MonsterIn(int monsterId) // 몬스터 등장
    {
        cor_isInMonster = true;
        yield return new WaitForSeconds(2f);
        // 모든 버튼의 기능 일시 정지
        // 프리팹의 알파값 제거
        Image[] sampleImage = sampleCard.GetComponentsInChildren<Image>();
        if (sampleImage.Length > 1)
        {
            Image image_0 = sampleImage[0];
            Color color = image_0.color;
            color.a = 0;
            image_0.color = color;

            // 오브젝트의 하위 이미지 조절
            //Image image_1 = sampleImage[1];

            //RectTransform target = image_1.GetComponentInChildren<RectTransform>();
            //target.sizeDelta = new Vector2(1000, 1100);


            // 부모 캔버스 설정
            Transform mapPos = map.transform;
            GameObject obj = monsterManager.SpawnMonster(monsterId, mapPos, true, sampleCard);
            Debug.Log($"{monsterId}의 몬스터가 등장합니다...!");
        }
        cor_isInMonster = false;
    }
    IEnumerator CameraWaking() // 카메라 워킹
    {
        Vector2 originalPosition = shakingMap.anchoredPosition;
        float count = 0f;
        while (count < shakeDuration)
        {
            // 랜덤한 Y 오프셋 계산
            float randomY = Mathf.Sin(count * 5) * shakeAmount; // Sin 함수를 사용해 부드럽게 변화
            targetImage.anchoredPosition = new Vector2(originalPosition.x, originalPosition.y + randomY);

            count += Time.deltaTime; // 카운터 업데이트
            yield return null;
        }
        Debug.Log("카메라 워킹");
        shakingMap.anchoredPosition = originalPosition;
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Blink(int count, bool isLookOn)
    {
        if (count >= 7) // 최대 깜빡임 횟수 체크
            yield break;

        // UI 활성화/비활성화
        ui_lookOn.SetActive(isLookOn);

        // 잠시 대기
        yield return new WaitForSeconds(0.2f);

        // 다음 깜빡임 호출
        StartCoroutine(Blink(count + 1, !isLookOn));
    }
    IEnumerator showInfo(int monsterId)
    {
        yield return new WaitForSeconds(4f);
        Sprite monster = monsterManager.GetMonsterImageById(monsterId);
        Debug.Log("몬스터의 정보가 공개 됩니다...!");
        Image[] monsterInfoImage = monster_Info_img.GetComponentsInChildren<Image>();
        if (monsterInfoImage.Length > 1)
        {
            monsterInfoImage[2].sprite = monster;
        }
        if (monster == null)
        {
            Debug.LogError($"몬스터 이미지가 null입니다. ID: {monsterId}");
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
            Debug.Log("몬스터 정보가 갱신되었습니다...!");
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
    public void beAttack(int monsterId) // 수비 시
    {
        int myHealth = player.healt;
        int monsterDamage = int.Parse(monster_Name_Atk_value.text);

        myHealth -= monsterDamage;
        player.healt = myHealth;
        player_Health.text = myHealth.ToString();
        StartCoroutine(shakeImage(map, 0.3f, 10f));
        Debug.Log($"플레이어의 남은 체력 {myHealth}");
    }
    IEnumerator shakeImage(GameObject target, float duration, float magnitude) // 오브젝트, 시간(s), 강도
    {
        Vector3 originalPos = target.transform.localPosition; //타겟의 원래 위치 저장

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // x, y축으로 일정 범위 안에서 랜덤하게 위치 조정
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            // 흔들리는 위치로 설정
            target.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
            // 경과 시간 증가
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 원 위치로 복귀
        target.transform.localPosition = originalPos;
    }
    public void clickAttac() //공격하기, 방어하기 버튼
    {
        MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
        int currentID = monsterCard.id;
        StartCoroutine(clickAtk(currentID));
    }
    public void clickDefence() //공격하기, 방어하기 버튼
    {
        MonsterCard monsterCard = monsterManager.GetMonsterById(randomID);
        int currentID = monsterCard.id;
        StartCoroutine(clickDef(currentID));
    }
    IEnumerator clickAtk(int monsterID) //clickAttackandDefence 코루틴
    {
        yield return null;
        Debug.Log($"{isMyTurn}");

        if (isMyTurn)
        {
            btnAttack.GetComponent<Button>().interactable = false;
            Debug.Log("몬스터를 공격했습니다.");

            StartCoroutine(onDamage(monsterID));
            Debug.Log("몬스터가 공격을 당했습니다.");

            btnAttack.GetComponent<Button>().interactable = true;
            Debug.Log("상대의 차례입니다. 공격에 대비하십시오...!");
            isMyTurn = false;
        }
    }
    IEnumerator clickDef(int monsterID) // 방어 버튼 클릭 시
    {
        yield return null;
        if (!isMyTurn)
        {
            Debug.Log("몬스터의 공격이 날라옵니다...!");
            beAttack(monsterID);
            isMyTurn = true;
        }
        yield return new WaitForSeconds(2f);
    }
    IEnumerator onDamage(int randomID) // 몬스터가 공격을 받았을 때
    {
        int damage = player.attack;
        int health = int.Parse(monster_Name_Health_Value.text);
        GameObject monsterCard = GameObject.FindGameObjectWithTag("Monster");
        if (monsterCard == null)
        {
            Debug.LogError("태그가 'Monster'인 몬스터 카드가 없습니다.");
            yield break; // 몬스터 카드가 없을 경우 코루틴 종료
        }
        // 공격 처리
        Image image = monsterCard.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("해당 몬스터 카드에서 Image 컴포넌트를 찾을 수 없습니다.");
            yield break; // Image 컴포넌트가 없는 경우 코루틴 종료
        }
        if (health > damage)
        {
            yield return new WaitForSeconds(0.2f);
            image.color = Color.red;

            UpdateHealth(damage); // 체력 감소와 UI 업데이트
            yield return new WaitForSeconds(0.8f);
            Debug.Log("몬스터의 체력이 깎였습니다.");
            image.color = Color.white;
        }
        else if (health <= damage)
        {
            btnAttack.gameObject.SetActive(false);
            btnDefence.gameObject.SetActive(false);
            Debug.Log("남은 체력이 없습니다!");
            isDie(randomID);
            yield return new WaitForSeconds(0.4f);
        }
    }
    void UpdateHealth(int damage) // 몬스터 체력 업데이트
    {
        damage = player.attack;
        int health = int.Parse(monster_Name_Health_Value.text);
        if (health > 0)
        {
            health -= damage;
            monster_Name_Health_Value.text = health.ToString();
            Debug.Log($"{health}남은 체력");
        }
    }
    void isDie(int monsterId)
    {
        GameObject monsterCard = GameObject.FindGameObjectWithTag("Monster");
        Debug.Log("몬스터가 죽었습니다!");
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
            Debug.Log("소지 중인 몬스터볼이 없습니다...!");
            centerBall.SetActive(false);
            return;
        }
    }

    IEnumerator shot()
    {
        Debug.Log("몬스터를 잡을 준비가 되었습니다...!");
        yield return new WaitForSeconds(3);
        int random = Random.Range(0, 2);
        MonsterCard currentMonsterID = monsterManager.GetMonsterById(randomID);
        switch (random)
        {
            case 0:
                Debug.Log("몬스터를 잡았습니다...!");
                monsterUis.SetActive(false);
                _deleteMonsterCard();
                CanClickbtrMore();
                btn_More.gameObject.SetActive(true);
                player.myMonsterCard.Add(currentMonsterID);
                break;
            case 1:
                Debug.Log("몬스터가 아직 힘이 남아 있습니다...!");
                centerBall.gameObject.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(3);
        //Debug.Log("던전 탐험을 계속 진행합니다...!");
        //CenterBall.gameObject.SetActive(true);
        //btnAttack.gameObject.SetActive(false);
        //btnDefence.gameObject.SetActive(false);
        //StartCoroutine(FadeInOut(monsterId, 2));
    }
    IEnumerator currentImgFadeout()
    {
        yield return null;
        Debug.Log("페이드 아웃");
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
        Debug.Log($"남은 볼 갯수 : {ball}");
        hasBallImg[ball].SetActive(false);
    }
}
