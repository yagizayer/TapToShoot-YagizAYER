using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile
{
    internal string TargetTag;
    public abstract List<GameObject> Execute(GameObject me, GameObject other);
    protected Projectile(string targetTag) => TargetTag = targetTag;
}
