using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public Transform parent;
    public ObjectFactory factory;
    public static FactoryManager instance;
    private void Start()
    {
        instance = this;
    }

    public void CreateBG(GameObject obj, Vector3 pos)
    {
        factory.SpawnBG(obj,parent,pos);
    }

    public Enemy CreateEnemy(GameObject obj, Vector3 pos)
    {
        return factory.SpawnEnemy(obj, parent, pos);
    }

    public Bullet CreateBullet(GameObject obj, Vector3 pos)
    {
        return factory.SpawnBullet(obj, parent, pos);
    }

    public Item CreateItem(GameObject obj, Vector3 pos)
    {
        return factory.SpawnItem(obj, parent, pos);
    }

    public Effect CreateEffect(GameObject obj, Vector3 pos)
    {
        return factory.SpawnEffect(obj, parent, pos);
    }
}
