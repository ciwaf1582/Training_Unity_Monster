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
            Debug.Log("ĳ���� �ı��Ǿ����ϴ�.");
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("EnemyAttack"))
    //    {
    //        StartCoroutine(OnDamage());
    //        Debug.Log("������ �޾ҽ��ϴ�.");
    //    }
    //}
    public IEnumerator OnDamage()
    {
        // HP ����
        hp -= 10; // ���ݴ��� ��ŭ HP ���� (��: 10)
        UpdateHPText(); // HP �ؽ�Ʈ ������Ʈ

        // �ǰ� �� ���� ����
        material.color = Color.red;
        yield return new WaitForSeconds(1f);
        material.color = Color.white;

        destroy(); // HP�� 0 �������� Ȯ���ϰ� �ı�
    }

}
