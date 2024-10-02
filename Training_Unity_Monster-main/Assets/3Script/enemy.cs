using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    MonsterManager monsterManager;
    public GameObject monster;

    Vector3 vec3;

    public float speed;

    Rigidbody rigid;   

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (monster.transform.position.z < 30f)
        {
            StartCoroutine(MoveMonster());
        }
    }
    IEnumerator MoveMonster()
    {
        Debug.Log($"{monster}�� �����մϴ�.");
        while (monster.transform.position.z < 30f)
        {
            monster.transform.position += new Vector3(0, 0, speed * Time.deltaTime);

            // ���� �ð� ���
            yield return new WaitForSeconds(0.1f);
        }
    }


}
