using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class FightMap : MonoBehaviour
{
    public Camera mainCamera;
    public Camera fightCamera;
    private bool isInSceneView = false;

    public Transform monster_map;
    public GameObject sampleCube;
    MonsterInfo monsterInfo;

    public ButtonManager buttonManager;
    public Player player;
    public GameManager gameManager;
    public MonsterManager monsterManager;
    public GameObject monsterObj;
    public float speed;

    public GameObject sampleCard;
    public Transform map;
    public Index index;
    public Image userFace;
    public TextMeshProUGUI UserName;
    public Time Time;

    public TextMeshProUGUI ID;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI faceName;
    public TextMeshProUGUI house;

    void Start()
    {
        UserInfo();
    }

    void Update()
    {
        
    }
    void UserInfo()
    {
        Image playerImage = player.userImage.GetComponent<Image>();
        TextMeshProUGUI userName = player.userName.GetComponent<TextMeshProUGUI>();
        
        if (player != null) // 유저 이미지
        {
            userFace.sprite = playerImage.sprite;
            Debug.Log("유저 이미지의 데이터를 가져옵니다...!");
        }
        if (player != null && player.userName != null) // 유저 이름
        {
            UserName.text = userName.text;
            Debug.Log("유저 이름의 데이터를 가져옵니다...!");
        }
        if (player.myChoiceCard != null) // 선택한 몬스터
        {
            // 위치
            Vector2 originalPos = sampleCard.GetComponent<RectTransform>().anchoredPosition;
            originalPos.x = sampleCard.GetComponent<RectTransform>().anchoredPosition.x;
            originalPos.y = sampleCard.GetComponent<RectTransform>().anchoredPosition.y;
            // 이미지
            for (int i = 0; i < player.myChoiceCard.Count; i++)
            {
                MonsterCard card = player.myChoiceCard[i];
                int id = card.id;
                GameObject cardObj = monsterManager.SpawnMonster(id, map, false, sampleCard);
                Debug.Log($"{id}번 몬스터의 얼굴 데이터를 가져옵니다...!");
                
                RectTransform cardRect = cardObj.GetComponent<RectTransform>();
                cardRect.anchoredPosition = originalPos;
                originalPos.x += 140f;
                if ((i+1) % 2 == 0)
                {
                    originalPos.x = -70f;
                    originalPos.y -= 140f;
                }
            }
        }
    }
    public void ChangeCameraView()
    {
        isInSceneView = !isInSceneView;

        // 카메라 상태 전환
        mainCamera.enabled = !isInSceneView; // UI 카메라 활성화/비활성화
        fightCamera.enabled = isInSceneView; // 3D 카메라 활성화/비활성화

        Debug.Log($"Main Camera Enabled: {mainCamera.enabled}, Scene Camera Enabled: {fightCamera.enabled}");
    }

    public void btn_inMonster()
    {
        
        MonsterCard monster = monsterManager.allMonsters[2];
        MonsterCard monsterID = monsterManager.GetMonsterById(monster.id);

        GameObject objCube = monsterManager.SpawnMonsterCube(monsterID.id, monster_map, sampleCube);
        clickMonsterInfo(monsterID);
        Transform recPos = objCube.GetComponent<Transform>();

        // 위치 조정
        Vector3 vec3 = recPos.position;
        vec3.x = 910f; // X 좌표 설정
        vec3.y = 530f;   // Y 좌표 설정
        vec3.z = -150;   // z 좌표 설정
        recPos.position = vec3; // 변경된 위치 적용



        Debug.Log("소환 성공!");
    }
    public void clickMonsterInfo(MonsterCard monster)
    {
        ID.text = "ID : "+ monster.id.ToString();
        monsterName.text = "Name : " + monster.name.ToString();
        atk.text = "ATK : " + monster.atk.ToString();
        hp.text = "HP : "+ monster.hp.ToString();
        faceName.text = "FaceName : "+ monster.face.ToString();
        house.text = "House : "+ monster.house;
    }
    
}
