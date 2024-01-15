using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    static int bgCount;
    float fadeCount;
    public GameObject bg;
    bool trigger;
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (gameObject.name == "12(Clone)" && transform.position.x <= -7f)
        {
            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
        }
        else if(gameObject.name == "13(Clone)" )
        {
            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
        }
        else
        {
            transform.Translate(-4f * Time.deltaTime, 0f, 0f);
        }

        if (transform.position.x < -20f && !trigger)
        {
            FactoryManager.instance.CreateBG(bg, new Vector3(17.3f, -5f, 1f));
            trigger = true;
            bgCount++;
        }
        if (transform.position.x < -26f && gameObject.name != "12(Clone)")
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -30f && gameObject.name == "12(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
