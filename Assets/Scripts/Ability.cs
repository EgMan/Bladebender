using UnityEngine;
using System.Collections.Generic;
public abstract class Ability : MonoBehaviour
{
    public abstract string getName();
    public abstract void activate(Knife projectile);
    public virtual void update(Knife projectile) {}
    public virtual void updateHold(Knife projectile) {}
}