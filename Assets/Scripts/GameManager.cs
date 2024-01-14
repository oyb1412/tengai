using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetLowHPTarget(GameObject[] targets)
    {
        Enemy[] enemys = new Enemy[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            for (int q = 0; q < targets.Length - 1; q++)
            {
                enemys[q] = targets[q].transform.GetComponent<Enemy>();
                enemys[q + 1] = targets[q + 1].transform.GetComponent<Enemy>();

                if (enemys[q].currentHP > enemys[q + 1].currentHP)
                {
                    var save = enemys[q];
                    enemys[q] = enemys[q + 1];
                    enemys[q + 1] = save;
                }
            }
        }

        return enemys[0].transform;
    }
}
