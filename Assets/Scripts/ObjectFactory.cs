using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : Factory<GameObject>
{
    public override BGMovement CreateBG(GameObject type)
    {
        BGMovement unit;
        unit = Instantiate(type).GetComponent<BGMovement>();
        return unit;
    }

    public override Bullet CreateBullet(GameObject type)
    {
        Bullet unit;
        unit = Instantiate(type).GetComponent<Bullet>();
        return unit;
    }

    public override Effect CreateEffect(GameObject type)
    {
        Effect unit;
        unit = Instantiate(type).GetComponent<Effect>();
        return unit;
    }
}
