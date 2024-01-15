using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> : MonoBehaviour
{
    public Enemy SpawnEnemy(T type, Transform parent, Vector3 pos)
    {
        Enemy unit = CreateEnemy(type);
        unit.transform.SetParent(parent);
        unit.transform.position = pos;
        return unit;
    }
    public BGMovement SpawnBG(T type, Transform parent, Vector3 pos)
    {
        BGMovement unit = CreateBG(type);
        unit.transform.SetParent(parent);
        unit.transform.position = pos;
        return unit;
    }

    public Bullet SpawnBullet(T type, Transform parent, Vector3 pos)
    {
        Bullet unit = CreateBullet(type);
        unit.transform.SetParent(parent);
        unit.transform.position = pos;
        return unit;
    }

    public Effect SpawnEffect(T type, Transform parent, Vector3 pos)
    {
        Effect unit = CreateEffect(type);
        unit.transform.SetParent(parent);
        unit.transform.position = pos;
        return unit;
    }

    public Item SpawnItem(T type, Transform parent, Vector3 pos)
    {
        Item unit = CreateItem(type);
        unit.transform.SetParent(parent);
        unit.transform.position = pos;
        return unit;
    }

    public abstract BGMovement CreateBG(T type);
    public abstract Item CreateItem(T type);
    public abstract Enemy CreateEnemy(T type);


    public abstract Bullet CreateBullet(T type);

    public abstract Effect CreateEffect(T type);

}
