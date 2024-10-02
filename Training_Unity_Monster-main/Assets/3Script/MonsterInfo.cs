using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    MonsterManager monsterManager;

    public int id;
    public string monsterName;
    public int atk;
    public int hp;
    public Sprite face;
    public string house;
    public bool isChecked;

    void Start()
    {
        // MonsterManager¸¦ Ã£±â
        monsterManager = FindObjectOfType<MonsterManager>();

        MonsterCard monster = monsterManager.GetMonsterById(id);
    }
    void infoUpdate(GameObject gameObject)
    {

    }
}
