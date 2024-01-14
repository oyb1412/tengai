using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject[] currentBackGroundObjects;

    public GameObject[] backGroundObjects;

    int count;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (backGroundObjects[0].transform.position.x < -26f)
        {
            backGroundObjects[0].transform.position = backGroundObjects[2].transform.position;
            backGroundObjects[0] = backGroundObjects[1];
            backGroundObjects[1] = backGroundObjects[2];
        }
    }
}
