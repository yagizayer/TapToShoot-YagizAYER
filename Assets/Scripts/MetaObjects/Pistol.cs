using System.Collections.Generic;
using UnityEngine;

public class Pistol : Projectile
{
    internal Vector3 StandingPos;
    public Pistol(string targetTag, Vector3 standingPos) : base(targetTag)
    {
        StandingPos = standingPos;
    }

    public override List<GameObject> Execute(GameObject me, GameObject other)
    {
        List<GameObject> result = new List<GameObject>();

        if (other.GetComponent<Rigidbody>() == null)
            result.Add(other);
        Rigidbody tempRB = other.GetComponent<Rigidbody>();
        if (tempRB == null) tempRB = other.AddComponent<Rigidbody>();
        tempRB.AddForce((other.transform.position - StandingPos).normalized, ForceMode.VelocityChange);
        return result;
    }
}