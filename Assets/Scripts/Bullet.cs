using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum bullet{ NORMAL_BULLET , SUBWEAPON_BULLET1, SUBWEAPON_BULLET2, SUBWEAPON_BULLET3, SUBWEAPON_BULLET4, SUBWEAPONNORMAL_BULLET }
    public bullet bulletType;
    public float speed;
    public GameObject bulletEffect;
    Rigidbody2D rigid;
    public Transform targetTrans;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10f)
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        switch (bulletType)
        {
            case bullet.NORMAL_BULLET:
            case bullet.SUBWEAPON_BULLET1:
            case bullet.SUBWEAPON_BULLET2:
            case bullet.SUBWEAPON_BULLET3:
            case bullet.SUBWEAPON_BULLET4:
                transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);
                break;
            case bullet.SUBWEAPONNORMAL_BULLET:
                var enemys = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemys.Length < 1)
                {
                    transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);
                }
                else if (enemys.Length >= 1)
                {
                    if (enemys.Length == 1)
                    {
                        targetTrans = enemys[0].transform;
                    }
                    else if (enemys.Length > 1)
                    {
                        targetTrans = GameManager.Instance.GetLowHPTarget(enemys);
                    }
                    Vector2 targetPos = (targetTrans.position - transform.position).normalized;
                    rigid.MovePosition(rigid.position + targetPos * speed * Time.fixedDeltaTime);
                }
                break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        var target = collision.gameObject.GetComponent<Enemy>();
        var pos = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        FactoryManager.instance.CreateEffect(bulletEffect,  pos);
        Destroy(gameObject);
    }

    
}
