using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    internal float Range;
    internal float Force;
    public Grenade(string targetTag, float range, float force) : base(targetTag)
    {
        Range = range;
        Force = force;
    }

    public override List<GameObject> Execute(GameObject me, GameObject other)
    {
        List<GameObject> result = new List<GameObject>();
        Vector3 explosionCenter = Vector3.Lerp(me.transform.position, other.transform.position, .5f);
        Collider[] targets = Physics.OverlapSphere(explosionCenter, Range);
        foreach (Collider cube in targets)
        {
            if (!cube.gameObject.CompareTag(TargetTag)) continue;
            Rigidbody cubeRB = cube.GetComponent<Rigidbody>();
            if (cubeRB == null)
            {
                result.Add(cube.gameObject);
                cubeRB = cube.gameObject.AddComponent<Rigidbody>();
            }
            cubeRB.AddExplosionForce(Force * 100, explosionCenter, Range);
        }
        return result;
    }
}