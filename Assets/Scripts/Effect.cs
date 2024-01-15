using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Effect : MonoBehaviour
{
    public enum effect { NORMAL_EFFECT, ULT_EFFECT,POWERUP_EFFECT}
    public effect effectType;
    float deleteTimer;
    public Sprite[] mojiImage;
    public SpriteRenderer mojiObject;
    public GameObject hitEffectPrefabs;
    float mojiTimer;
    public float damage;
    float hitTimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer += Time.deltaTime;
        if(deleteTimer > 0.4f && effectType == effect.NORMAL_EFFECT)
            Destroy(gameObject);

        if (effectType == effect.ULT_EFFECT)
        {
            var ran = Random.Range(0, mojiImage.Length);
            mojiTimer += Time.deltaTime;
            if (mojiTimer > 0.2f)
            {
                mojiObject.sprite = mojiImage[ran];
                mojiTimer = 0;
            }
            if(mojiObject.transform.localScale.x <= 2.1f)
            {
                mojiObject.transform.DOScale(3.6f, 0.2f);
            }
            else if(mojiObject.transform.localScale.x >= 3.5f)
            {
                mojiObject.transform.DOScale(2f, 0.2f);
            }
            if (deleteTimer > 3f)
            {
                Destroy(gameObject);
            }
            
        }

        if(effectType == effect.POWERUP_EFFECT)
        {
            transform.Translate(0f, 2f * Time.deltaTime, 0f);
            if (deleteTimer > 1f)
                Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (effectType == effect.ULT_EFFECT)
        {
            //Àû ºÒ·¿ Á¦°Å
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (effectType == effect.ULT_EFFECT)
        {
            if (!collision.CompareTag("Enemy"))
                return;

            var target = collision.GetComponent<Enemy>();
            hitTimer += Time.deltaTime;
            if (hitTimer > 0.1f)
            {
                target.currentHP -= damage;
                FactoryManager.instance.CreateEffect(hitEffectPrefabs, target.transform.position);
                hitTimer = 0;
            }
        }
    }
}
