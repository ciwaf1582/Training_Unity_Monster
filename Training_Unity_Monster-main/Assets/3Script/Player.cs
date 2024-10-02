using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
    public MonsterManager monsterManager;
    public ButtonManager buttonManager;
    MonsterCard monsterCard;

    public int attack = 3;
    public int healt = 100;
    public int hasBall = 5;

    public GameObject userName;
    public GameObject userImage;
    public List<Image> tempImages = new List<Image>();

    public List<MonsterCard> myMonsterCard = new List<MonsterCard>();
    public List<MonsterCard> myChoiceCard = new List<MonsterCard>();

    void Start()
    {
        
    }
    public void test()
    {
        // TEST
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, monsterManager.allMonsters.Count); 
            MonsterCard selectedMonster = monsterManager.allMonsters[randomIndex];
            myMonsterCard.Add(selectedMonster);
            //myChoiceCard.Add(selectedMonster);
            
        }
        Debug.Log("������ ���� ī�� 5���� �߰��Ǿ����ϴ�.");
    }
    public void addMyHand()
    {
        GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Monster");
        Debug.Log(cardObjs.Length);
        int a = 1;
        foreach (GameObject cardObj in cardObjs)
        {
            // ButtonManager���� üũ ���� ��������
            ButtonManager buttonManager = cardObj.GetComponent<ButtonManager>();
            if (buttonManager != null)
            {
                bool _isChecked = buttonManager.isChecked;

                // ���� ID ��������
                MonsterInfo card = cardObj.GetComponent<MonsterInfo>();
                if (card != null)
                {
                    int monsterID = card.id;
                    
                    Debug.Log(monsterID + $": {a} ���� ID �� {_isChecked}");
                    a++;
                    MonsterCard findID = monsterManager.GetMonsterById(monsterID);
                    if (_isChecked && findID != null && findID.isChecked == false)
                    {
                        myChoiceCard.Add(findID); // üũ�� ���� ī�� �߰�
                        Debug.Log($"����ID - {findID} �� ���з� �߰��߽��ϴ�...!");
                    }
                }
            }
            monsterManager.checkedCard();
        }
    }
}
