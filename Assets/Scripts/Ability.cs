using UnityEngine;
using System.Collections.Generic;
public abstract class Ability : MonoBehaviour
{
    /// <summary>
    /// Return the in-game name for this ability
    /// </summary>
    public abstract string getName();

    /// <summary>
    /// Called once when ability is first activated
    /// </summary>
    public abstract void activate(Knife projectile);

    /// <summary>
    /// Called once ability is first activated (shift evokes alternate functionality)
    /// TODO implement this. Currently doesn't function as stated above.
    /// </summary>
    public virtual void activateShift(Knife projectile) { }

    /// <summary>
    /// Called on every frame for knives which are still affected by the ability, but the ability is no longer being held down.
    /// </summary>
    public virtual void update(Knife projectile) { }

    /// <summary>
    /// Called on every frame for knives which are still affected by the ability, and only while the ability is still being held down.
    /// </summary>
    public virtual void updateHold(Knife projectile) { }

    /// <summary>
    /// Called once ability is released.
    /// </summary>
    public virtual void release(Knife projectile) { }
}