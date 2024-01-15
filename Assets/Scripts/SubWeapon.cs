using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubWeapon : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public GameObject[] subweaponBulletPrefabs;
    public GameObject subweaponNormalBulletPrefabs;

    float chargeTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (player.level <= 0)
            return;

        if(player.subweaponBulletFireTimer > 0.5f && player.subweaponulletFireTrigger && player.level > 0)
        {
            var posUp = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.5f, transform.position.z);
            var posDown = new Vector3(transform.position.x-0.3f, transform.position.y - 0.5f, transform.position.z);
            var posBack = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);

            switch (player.level)
            {

                case 1:
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, transform.position);
                    break;
                case 2:
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, transform.position);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posDown);
                    break;
                case 3:
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posUp);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, transform.position);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posDown);
                    break;
                case 4:
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posUp);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, transform.position);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posBack);
                    FactoryManager.instance.CreateBullet(subweaponNormalBulletPrefabs, posDown);
                    break;
            }
            player.subweaponBulletFireTimer = 0;
        }

        if(Input.GetKey(KeyCode.E) && chargeTimer < 1f)
        {
            OnAnimation("Charge");
            chargeTimer += Time.deltaTime;
            player.normalBulletFireTrigger = false;
            player.subweaponulletFireTrigger = false;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            OnAnimation("Idle");
            chargeTimer = 0;
            player.normalBulletFireTrigger = true;
            player.subweaponulletFireTrigger = true;
        }
        if (chargeTimer >= 1f)
        {
            FactoryManager.instance.CreateBullet(subweaponBulletPrefabs[player.level - 1], transform.position);
            player.normalBulletFireTrigger = true;
            player.subweaponulletFireTrigger = true;
            chargeTimer = 0;
            OnAnimation("Idle");
        }
    }


    void OnAnimation(string trigger)
    {
        animator.Play(trigger);
    }
}
