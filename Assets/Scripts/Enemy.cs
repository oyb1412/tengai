using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum enemy { GREEN_ENEMY,RED_ENEMY,KA_ENEMY,STING_ENEMY,YOROI_ENEMY,DEMON_ENEMY,SISYA_ENEMY}
    public enemy enemyType;

    public float currentHP;
    public float maxHP;
    Animator animator;
    public GameObject dieEffect;
    public GameObject dropItem;
    public float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if(currentHP <= 0f)
        {
            FactoryManager.instance.CreateEffect(dieEffect, transform.position);
            FactoryManager.instance.CreateItem(dropItem, transform.position);
            Destroy(gameObject);
        }
    }
}
