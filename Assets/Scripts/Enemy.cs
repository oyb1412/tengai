using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum enemy { GREEN_ENEMY,RED_ENEMY}
    public enemy enemyType;

    public float currentHP;
    public float maxHP;

    public float speed;

}
