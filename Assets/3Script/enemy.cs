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
        // Start()에서 Castle 참조를 설정
        if (castle == null)
        {
            castle = FindObjectOfType<Castle>(); // 씬에 있는 Castle을 찾아서 참조
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
            rigid.velocity = Vector3.zero; // 이동 속도
            rigid.angularVelocity = Vector3.zero; // 회전 속도
        }
    }
    IEnumerator MoveMonster()
    {       
        
        while (monster.transform.position.z < 20f)
        {
            monster.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            // 일정 시간 대기
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator Attack()
    {
        isRun = false;
        isAttack = true;
        attackArea.gameObject.SetActive(true);

        // 캐슬에 데미지 주기
        if (castle != null)
        {
            castle.hp -= 10; // 공격으로 10 HP 감소
            Debug.Log("캐슬에 공격을 가했습니다! 남은 HP: " + castle.hp);
            StartCoroutine(castle.OnDamage());
        }
        yield return new WaitForSeconds(2f);
        attackArea.gameObject.SetActive(false);

        // 공격 후 이동 가능 상태로 변경
        

        // 캐슬의 HP가 0 이하인 경우 적이 더 이상 공격하지 않도록 함
        if (castle.hp <= 0)
        {
            Debug.Log("캐슬이 파괴되었습니다!");
            Destroy(castle.gameObject); // 캐슬 파괴
            isRun = true;
            isAttack = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Castle")) // 충돌한 객체가 Castle인지 확인
        {
            StartCoroutine(Attack());
            Debug.Log("공격을 시작합니다.");
        }
    }
    void OnTriggerStay(Collider other)
    {
        
    }
    // 충돌 중일 때 호출

    // 충돌이 종료될 때 호출
    void OnCollisionExit(Collision collision)
    {
        
    }


}
