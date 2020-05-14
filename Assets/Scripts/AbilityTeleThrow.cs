using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleThrow : Ability
{
    public float translateSpeed = 1.75f;
    public float rotateSpeed = 1f;
    public override string getName()
    {
        return "Tele-Throw";
    }
    public override void activate(Knife projectile)
    {
        projectile.state = Knife.States.AntiGrav;
    }
    public override void updateHold(Knife k)
    {
        if (k.state != Knife.States.AntiGrav)
        {
            k.removeAbility();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //interpolate translation
        Vector2 currnt_velocity = k.rb.velocity;
        k.rb.position = Vector2.Lerp(k.rb.position, mousePos, Time.deltaTime * translateSpeed);

        //interpolate rotation
        Vector2 vectorToTarget = mousePos - k.transform.position;
        Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed);
    }

    public override void release(Knife k)
    {
        k.rb.velocity = Vector2.zero;
        k.rb.angularVelocity = 0f;
        Vector3 throwVector = k.transform.rotation * Vector3.up;
        k.launch(throwVector, 0f);
    }
}
