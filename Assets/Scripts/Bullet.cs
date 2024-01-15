using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class Bullet : MonoBehaviour
{
    public enum bullet{ NORMAL_BULLET , SUBWEAPON_BULLET1, SUBWEAPON_BULLET2, SUBWEAPON_BULLET3, 
        SUBWEAPON_BULLET4, SUBWEAPONNORMAL_BULLET, ULT_BULLET,ENEMY_BULLET }
    public bullet bulletType;
    public float speed;
    public float damage;
    float saveSpeed;
    public GameObject bulletEffect;
    public int bulletHitCount;
    float hitTimer;
    float ultBulletTimer;
    bool isLive;
    Rigidbody2D rigid;
    Vector2 currentPos;
    Vector2 targetDir;

    public Transform targetTrans;
    Animator anime;
    Vector3 pos;
    Enemy target;
    bool bulletTrigger;
    private void Start()
    {
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        isLive = true;
        saveSpeed = speed;
        currentPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 9.35f)
            Destroy(gameObject);

    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive || !isLive)
            return;


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
                    if (targetTrans.position.x >= GameManager.Instance.player.transform.position.x)
                    {
                        rigid.MovePosition(rigid.position + targetPos * speed * Time.fixedDeltaTime);
                    }
                    else
                        transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);


                }
                break;
            case bullet.ENEMY_BULLET:
                if (!bulletTrigger)
                {
                    targetDir = (GameManager.Instance.player.transform.position - transform.position).normalized;
                    bulletTrigger = true;
                }
                    rigid.MovePosition(rigid.position + targetDir * speed * Time.fixedDeltaTime);
                break;
            case bullet.ULT_BULLET:
                ultBulletTimer += Time.deltaTime;
                transform.DOMoveX(currentPos.x + 3f, 1f);
                transform.Rotate(new Vector3(0f,0f,-150f * Time.fixedDeltaTime));
                if(ultBulletTimer > 1f)
                {
                    FactoryManager.instance.CreateEffect(bulletEffect, transform.position);
                    Destroy(gameObject);
                }
                break;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (bulletType)
        {
            case bullet.SUBWEAPON_BULLET3:
                speed = saveSpeed;
                anime.Play("Idle");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        pos = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        target = collision.GetComponent<Enemy>();
        switch (bulletType)
        {
            case bullet.NORMAL_BULLET:
            case bullet.SUBWEAPONNORMAL_BULLET:
                FactoryManager.instance.CreateEffect(bulletEffect, pos);
                target.currentHP -= damage;
                AudioManager.instance.PlayerSfx(AudioManager.Sfx.HIT);
                Destroy(gameObject);
                break;

            case bullet.SUBWEAPON_BULLET1:
            case bullet.SUBWEAPON_BULLET2:
                if (isLive)
                {
                    isLive = false;
                    target.currentHP -= damage;
                    AudioManager.instance.PlayerSfx(AudioManager.Sfx.HIT);

                    FactoryManager.instance.CreateEffect(bulletEffect, pos);
                    StartCoroutine(BulletDestroyEffectPlay());
                }
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;


        switch (bulletType)
        {
            case bullet.SUBWEAPON_BULLET3:
            case bullet.SUBWEAPON_BULLET4:
                speed = saveSpeed * 0.5f;
                if (hitTimer >= 0.1f)
                {
                    if (bulletHitCount > 0)
                    {
                        hitTimer = 0;
                        target.currentHP -= damage;
                        anime.Play("Hit");
                        AudioManager.instance.PlayerSfx(AudioManager.Sfx.HIT);

                        FactoryManager.instance.CreateEffect(bulletEffect, pos);
                        bulletHitCount--;
                    }
                    else if (bulletHitCount == 0)
                    {
                        transform.DOScale(0.0f, 0.5f);
                        bulletHitCount--;
                    }
                    else if (bulletHitCount <= 0)
                    {
                        if (transform.localScale.x <= 0.1f)
                            Destroy(gameObject);
                    }
                }
                break;
        }

        hitTimer += Time.deltaTime;
    }
 
    IEnumerator BulletDestroyEffectPlay()
    {
        anime.Play("Die");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
