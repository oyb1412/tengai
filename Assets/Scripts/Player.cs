using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public float speed;
    Vector2 moveDir;
    Rigidbody2D rb;
    Animator animator;
    public GameObject normalBulletPrefabs;
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        normalBulletFireTimer += Time.deltaTime;
        subweaponBulletFireTimer += Time.deltaTime;

        if (normalBulletFireTimer > 0.2f && normalBulletFireTrigger)
        {
            var ran = Random.Range(0, normalBulletImage.Length);
            var bullet = FactoryManager.instance.CreateBullet(normalBulletPrefabs,transform.position);
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

        if (Input.GetKeyDown(KeyCode.Q) && level < 4)
        {
            level++;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    void OnAnimation(string trigger)
    {
        animator.Play(trigger);
    }
}
