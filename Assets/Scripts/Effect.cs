using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    float deleteTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer += Time.deltaTime;
        if(deleteTimer > 0.4f)
            Destroy(gameObject);
    }
}
