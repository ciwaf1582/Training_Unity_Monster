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
    public int id; // 몬스터 ID
    public string name; // 몬스터 이름
    public int atk; // 몬스터 공격력
    public int hp; // 몬스터 체력
    public Sprite face; // 몬스터의 이미지
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
    public MonsterCard GetMonsterById(int id) // 몬스터 ID찾기
    {
        // 조건에 충족하는 기본값을 반환
        // MonsterCard객체를 monster라고 가정하고 ID확인
        return allMonsters.FirstOrDefault(monster => monster.id == id);
    }
    public string GetMonsterByName(int id) // 몬스터 ID찾기
    {
        // 조건에 충족하는 기본값을 반환
        // MonsterCard객체를 monster라고 가정하고 ID확인
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.name : "Unknown";
    }
    public int GetMonsterByAtk(int id) // 몬스터 ATK찾기
    {
        // 조건에 충족하는 기본값을 반환
        // MonsterCard객체를 monster라고 가정하고 ID확인
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.atk : 0;
    }
    public int GetMonsterByHP(int id) // 몬스터 HP찾기
    {
        // 조건에 충족하는 기본값을 반환
        // MonsterCard객체를 monster라고 가정하고 ID확인
        MonsterCard monster = GetMonsterById(id);
        return monster != null ? monster.hp : 0;
    }
    public Sprite GetMonsterImageById(int id) // 몬스터 이미지 찾기
    {
        // 조건에 충족하는 기본값을 반환
        MonsterCard monster = allMonsters.FirstOrDefault(m => m.id == id);
        return monster?.face; // 이미지가 있으면 반환, 없으면 null 반환
    }
    public int GetIdByImage(Sprite image)
    {
        // 이미지가 일치하는 몬스터를 찾고 해당 몬스터의 ID를 반환
        MonsterCard monster = allMonsters.FirstOrDefault(m => m.face == image);
        return monster != null ? monster.id : -1; // 없으면 -1 반환
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
    public void AddMonster(int id, string name, int atk, int hp, string faceName, string house, bool isChecked) // 몬스터 추가
    {
        // 몬스터 이미지 리소스
        string monsterFaceResources = "MonsterImage/";
        Sprite face = Resources.Load<Sprite>(monsterFaceResources + faceName);
 
        MonsterCard monster = new MonsterCard(id, name, atk, hp, face, house, isChecked);
        allMonsters.Add(monster);
    }
    public GameObject SpawnMonster(int id, Transform parentTransform, bool fadeIn, GameObject sampleCard)
    {
        // 몬스터 카드 정보 가져오기
        MonsterCard monster = GetMonsterById(id);
        Sprite monsterFace = GetMonsterImageById(id);

        // 유효한 몬스터일 경우 인스턴스 생성
        if (monster != null)
        {
            // 샘플 카드를 복제하여 새로운 몬스터 오브젝트 생성
            GameObject monsterObject = Instantiate(sampleCard, parentTransform.position, Quaternion.identity, parentTransform);

            // 몬스터의 이미지 설정
            Image monsterImage = monsterObject.GetComponentInChildren<Image>(); // 하위 이미지 컴포넌트 가져오기
            if (monsterImage != null)
            {
                monsterImage.sprite = monsterFace; // 이미지 설정
                //monsterImage[2].preserveAspect = true; // 비율 유지
            }

            // 몬스터 오브젝트에 추가적인 정보 설정 (필요시)
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

            // 페이드 인 효과 적용 (선택 사항)
            if (fadeIn)
            {
                StartCoroutine(FadeIn(monsterImage)); // 페이드 인 효과
            }

            return monsterObject; // 생성된 몬스터 오브젝트 반환
        }

        // 몬스터가 유효하지 않을 경우 null 반환
        return null;
    }
    public GameObject SpawnMonsterCube(int id, Transform parentTransform, GameObject sampleCard)
    {
        // 몬스터 카드 정보 가져오기
        MonsterCard monster = GetMonsterById(id);
        Sprite monsterFace = GetMonsterImageById(id);

        // 유효한 몬스터일 경우 인스턴스 생성
        if (monster != null)
        {
            // 샘플 카드를 복제하여 새로운 몬스터 오브젝트 생성
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
            return monsterObject; // 생성된 몬스터 오브젝트 반환
        }


        // 몬스터가 유효하지 않을 경우 null 반환
        return null;
    }
    public void checkedCard()
    {
        foreach (MonsterCard card in player.myChoiceCard)
        {
            card.isChecked = true;
        }
    }
    //public GameObject SpawnMonster(int id, Transform canvas, bool fadeIn, GameObject sampleCard) // 인스턴스
    //{
    //    MonsterCard card = GetMonsterById(id);
    //    GameObject gameObject = Instantiate(sampleCard, Vector3.zero, Quaternion.identity);

    //    return gameObject;
    //MonsterCard monster = GetMonsterById(id);
    //Sprite monsterFace = GetMonsterImageById(id);
    //if (monster != null)
    //{
    //    // 캔버스 위치
    //    Transform newParent = canvas.transform;
    //    // 인스턴스
    //    GameObject monsterObject = Instantiate(sampleCard, Vector3.zero, Quaternion.identity);

    //    monsterObject.tag = "Monster";
    //    monsterObject.transform.SetParent(newParent, false);

    //    // 오브젝트 위치
    //    RectTransform rectTransform = monsterObject.GetComponent<RectTransform>();
    //    rectTransform.anchoredPosition = Vector2.zero; // 원하는 위치로 설정 (0, 0)

    //    Image monsterImage = monsterObject.GetComponentInChildren<Image>(); // 자식 이미지 컴포넌트 찾기
    //    if (monsterImage != null)
    //    {
    //        // 몬스터 이미지
    //        monsterImage.sprite = monsterFace; // 몬스터 카드에서 로드된 스프라이트 설정
    //        if (fadeIn)
    //        StartCoroutine(FadeIn(monsterImage)); // 페이드 인 효과 적용
    //    }
    //    return monsterObject;
    //}
    //return null;
    // 페이드 효과
    private IEnumerator FadeIn(Image image)
    {
        Color color = image.color;
        color.a = 0; // 초기 알파값을 0으로 설정
        image.color = color;

        float duration = 1f; // 페이드 인 지속 시간
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration); // 알파값 증가
            image.color = color;
            yield return null; // 다음 프레임까지 대기
        }

        color.a = 1; // 최종 알파값 설정
        image.color = color;
    }
}
