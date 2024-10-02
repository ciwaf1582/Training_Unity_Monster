using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int hp;
    public TextMeshProUGUI hpTxt;
    public Material material;
    enemy enemy;

    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        
    }
    void UpdateHPText()
    {
        hpTxt.text = "HP : " + hp.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHPText();
    }
    void destroy()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("캐슬이 파괴되었습니다.");
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("EnemyAttack"))
    //    {
    //        StartCoroutine(OnDamage());
    //        Debug.Log("공격을 받았습니다.");
    //    }
    //}
    public IEnumerator OnDamage()
    {
        // HP 감소
        hp -= 10; // 공격당한 만큼 HP 감소 (예: 10)
        UpdateHPText(); // HP 텍스트 업데이트

        // 피격 시 색상 변경
        material.color = Color.red;
        yield return new WaitForSeconds(1f);
        material.color = Color.white;

        destroy(); // HP가 0 이하인지 확인하고 파괴
    }

}
