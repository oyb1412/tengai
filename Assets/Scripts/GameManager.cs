using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public GameObject[] ultCountObject;
    public GameObject[] hpCountObject;
    public Text scoreText;
    public bool isLive;
    float startCount;
    // Start is called before the first frame update
    void Start()
    {
        isLive = false;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLive)
        {
            player.transform.Translate(10f * Time.deltaTime, 0f, 0f);
            if(player.transform.position.x >= -6f)
                isLive = true;
        }

        scoreText.text = "1P :    " + player.score;

        if(player.currentHP == 0)
            hpCountObject[0].SetActive(false);       
        else if(player.currentHP == 1)
            hpCountObject[1].SetActive(false);    
        else if(player.currentHP == 2)
            hpCountObject[2].SetActive(false);

        if (player.ultCount == 0)
        {
            ultCountObject[0].SetActive(false);
            ultCountObject[2].SetActive(false);
            ultCountObject[1].SetActive(false);
        }
        else if (player.ultCount == 1)
        {
            ultCountObject[0].SetActive(true);
            ultCountObject[2].SetActive(false);
            ultCountObject[1].SetActive(false);
        }
        else if (player.ultCount == 2)
        {
            ultCountObject[0].SetActive(true);
            ultCountObject[1].SetActive(true);
            ultCountObject[2].SetActive(false);
        }
        else if (player.ultCount == 3)
        {
            ultCountObject[0].SetActive(true);
            ultCountObject[1].SetActive(true);
            ultCountObject[2].SetActive(true);
        }
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
