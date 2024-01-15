using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public int ultCount;
    public float speed;
    public int currentHP;
    public int maxHP;
    public int score;
    Vector2 moveDir;
    Rigidbody2D rb;
    Animator animator;
    public GameObject normalBulletPrefab;
    public GameObject ultBulletPrefab;
    public GameObject powerUpEffectPrefab;
    public GameObject scoreUpEffectPrefab;

    public Sprite[] normalBulletImage;
    float normalBulletFireTimer;
    public float subweaponBulletFireTimer;

    bool subweaponCreateTrigger;
    public bool normalBulletFireTrigger;
    public bool subweaponulletFireTrigger;
    // Start is called before the first frame update
    void Start()
    {
        normalBulletFireTrigger = true;
        subweaponulletFireTrigger = true;
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        normalBulletFireTimer += Time.deltaTime;
        subweaponBulletFireTimer += Time.deltaTime;

        if (normalBulletFireTimer > 0.2f && normalBulletFireTrigger)
        {
            var ran = Random.Range(0, normalBulletImage.Length);
            var bullet = FactoryManager.instance.CreateBullet(normalBulletPrefab,transform.position);
            bullet.GetComponent<SpriteRenderer>().sprite = normalBulletImage[ran];
            normalBulletFireTimer = 0;
        }
        if(transform.position.y <= -4.3f)
        {
            OnAnimation("Run");
        }
        else if(moveDir.x < 0)
        {
            OnAnimation("BackIdle");
        }
        else
        {
            OnAnimation("FrontIdle");
        }

        if(level == 1 && !subweaponCreateTrigger)
        {
            subweaponCreateTrigger = true;
            var sub = GetComponentInChildren<SubWeapon>();
            sub.transform.localScale = Vector3.one;
            sub.animator.Play("Create");
        }

        if(Input.GetKeyUp(KeyCode.Q) && ultCount > 0)
        {
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.ULT);

            FactoryManager.instance.CreateBullet(ultBulletPrefab, transform.position);
            ultCount--;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    void OnAnimation(string trigger)
    {
        animator.Play(trigger);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            SetHP(-1);
        }
        else if(collision.CompareTag("Item"))
        {
            var item = collision.GetComponent<Item>();
            switch(item.itemType)
            {
                case Item.item.COIN:
                    FactoryManager.instance.CreateEffect(scoreUpEffectPrefab, transform.position);
                    AudioManager.instance.PlayerSfx(AudioManager.Sfx.COIN);

                    score += 500;
                    break;
                case Item.item.POWERUP:
                    if (level < 4)
                    {
                        FactoryManager.instance.CreateEffect(powerUpEffectPrefab, transform.position);
                        AudioManager.instance.PlayerSfx(AudioManager.Sfx.POWER_UP);

                        level++;
                    }
                    else
                    {
                        FactoryManager.instance.CreateEffect(scoreUpEffectPrefab, transform.position);

                        score += 500;
                    }
                    break;
                case Item.item.ULT:
                    if(ultCount < 3)
                    {
                        ultCount++;
                    }
                    else
                    {
                        FactoryManager.instance.CreateEffect(scoreUpEffectPrefab, transform.position);
                        score += 500;
                    }
                    break;
            }
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            animator.Play("Hit");
            SetHP(-1);
        }
    }



    public void SetHP(int hp)
    {
        currentHP += hp;
    }
}
