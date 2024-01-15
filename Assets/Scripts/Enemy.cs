using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public enum enemy { GREEN_ENEMY, STING_ENEMY, KA_ENEMY, RED_ENEMY,YOROI_ENEMY,DEMON_ENEMY,SISYA_ENEMY}
    public enemy enemyType;

    public float currentHP;
    public float maxHP;
    Animator animator;
    public GameObject dieEffect;
    public GameObject dropItem;
    public GameObject bulletPrefabs;
    float fireTimer;
    public float speed;
    bool moverTrigger;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (currentHP <= 0f)
        {
            FactoryManager.instance.CreateEffect(dieEffect, transform.position);
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.DESTORY);
            if (dropItem != null)
            {
                FactoryManager.instance.CreateItem(dropItem, transform.position);

            }

            Destroy(gameObject);
            if (enemyType == enemy.YOROI_ENEMY)
                GameManager.Instance.isLive = false;
        }
        if(transform.position.x < -10f)
            Destroy(gameObject);

        fireTimer += Time.deltaTime;
        switch (enemyType)
        {
            case enemy.GREEN_ENEMY:
            case enemy.RED_ENEMY:
                transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                if (fireTimer > 4f)
                {
                    FactoryManager.instance.CreateBullet(bulletPrefabs, transform.position);
                    fireTimer = 0;
                }
                break;
            case enemy.STING_ENEMY:
                transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                break;
            case enemy.KA_ENEMY:
                if (transform.position.x >= 4.5f && !moverTrigger)
                {
                    transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                    animator.Play("Front");
                    if (transform.position.x <= 4.5f)
                    {
                        moverTrigger = true;
                        FactoryManager.instance.CreateBullet(bulletPrefabs, transform.position);

                    }
                }
                if (moverTrigger)
                {
                    transform.Translate(speed /2 * Time.deltaTime, 0f, 0f);
                    animator.Play("Back");
                }
                break;

            case enemy.SISYA_ENEMY:
                transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                if (fireTimer >3f)
                {
                    FactoryManager.instance.CreateBullet(bulletPrefabs, transform.position);
                    fireTimer = 0;
                }
                break;


            case enemy.DEMON_ENEMY:
                transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                if (fireTimer > 2f)
                {
                    FactoryManager.instance.CreateBullet(bulletPrefabs, transform.position);
                    fireTimer = 0;
                }
                break;

            case enemy.YOROI_ENEMY:
                if (transform.position.x >= 4.5f && !moverTrigger)
                {
                    transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);
                    animator.Play("Idle");
                    if (transform.position.x <= 4.5f)
                    {
                        moverTrigger = true;
                    }
                }
                if(moverTrigger)
                {
                    if (transform.position.y >= 4f)
                        transform.DOMoveY(-3.3f, 5);
                    if(transform.position.y <=-3.2f)
                        transform.DOMoveY(3.9f, 5);

                    if (fireTimer > 0.5f)
                    {
                        animator.Play("Attack");
                        FactoryManager.instance.CreateBullet(bulletPrefabs, transform.position);
                        fireTimer = 0;
                    }
                }
                break;
        }
    }

}
