using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Item : MonoBehaviour
{
    public enum item { COIN, POWERUP, ULT}
    public item itemType;
    Rigidbody2D rigid;
    float ranx;
    float rany;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ranx = Random.Range(-7f, 7f);
        rany = Random.Range(-4f, 4f);

        rigid.velocity = new Vector3(ranx, rany, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -8.5f)
        {
            ranx = Random.Range(1f, 7f);
            rany = Random.Range(-4f, 4f);

        }
        else if (transform.position.x > 8.5f)
        {
            ranx = Random.Range(-1f, -7f);
            rany = Random.Range(-4f, 4f);

        }
        else if (transform.position.y > 4.5f)
        {
            rany = Random.Range(-7f, -1f);
            ranx = Random.Range(-7f, 7f);

        }
        else if (transform.position.y < -4.5f)
        {
            rany = Random.Range(1f, 7f);
            ranx = Random.Range(-7f, 7f);

        }


        rigid.velocity = Vector3.zero;
        rigid.velocity = new Vector3(ranx, rany, 1f);
    }


}
