using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject monster;
    public Collider attackArea;
    public Castle castle;
    Vector3 vec3;
    Rigidbody rigid;

    public float speed;

    public bool isAttack;
    public bool isRun;
    bool isDie = false;


    void Start()
    {
        isRun = true;
        // Start()���� Castle ������ ����
        if (castle == null)
        {
            castle = FindObjectOfType<Castle>(); // ���� �ִ� Castle�� ã�Ƽ� ����
        }
    }
    void FixedUpdate()
    {
        if (isRun && !isAttack)
        {
            Debug.Log(isRun);
            StartCoroutine(MoveMonster());
        }
    }
    void FreezeVelocity()
    {
        if (!isDie)
        {
            rigid.velocity = Vector3.zero; // �̵� �ӵ�
            rigid.angularVelocity = Vector3.zero; // ȸ�� �ӵ�
        }
    }
    IEnumerator MoveMonster()
    {       
        
        while (monster.transform.position.z < 20f)
        {
            monster.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            // ���� �ð� ���
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator Attack()
    {
        isRun = false;
        isAttack = true;
        attackArea.gameObject.SetActive(true);

        // ĳ���� ������ �ֱ�
        if (castle != null)
        {
            castle.hp -= 10; // �������� 10 HP ����
            Debug.Log("ĳ���� ������ ���߽��ϴ�! ���� HP: " + castle.hp);
            StartCoroutine(castle.OnDamage());
        }
        yield return new WaitForSeconds(2f);
        attackArea.gameObject.SetActive(false);

        // ���� �� �̵� ���� ���·� ����
        

        // ĳ���� HP�� 0 ������ ��� ���� �� �̻� �������� �ʵ��� ��
        if (castle.hp <= 0)
        {
            Debug.Log("ĳ���� �ı��Ǿ����ϴ�!");
            Destroy(castle.gameObject); // ĳ�� �ı�
            isRun = true;
            isAttack = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Castle")) // �浹�� ��ü�� Castle���� Ȯ��
        {
            StartCoroutine(Attack());
            Debug.Log("������ �����մϴ�.");
        }
    }
    void OnTriggerStay(Collider other)
    {
        
    }
    // �浹 ���� �� ȣ��

    // �浹�� ����� �� ȣ��
    void OnCollisionExit(Collision collision)
    {
        
    }


}
