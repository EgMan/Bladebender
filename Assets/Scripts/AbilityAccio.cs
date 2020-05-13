using UnityEngine;

public class AbilityAccio : Ability
{
    // public float antigravRandomNoise = 10f;
    public Transform toFollow;
    public float maxSpeed = 1000f;
    public float translateSpeed = 1.75f;
    public float rotateSpeed = 1f;
    public override void activate(Knife k)
    {
        k.state = Knife.States.AntiGrav;
    }
    public override void update(Knife k)
    {
        if (k.state != Knife.States.AntiGrav)
        {
            k.removeAbility();
        }

        //interpolate translation
        Vector2 currnt_velocity = k.rb.velocity;
        k.rb.position = Vector2.Lerp(k.rb.position, toFollow.position, Time.deltaTime * translateSpeed);

        //interpolate rotation
        Vector2 vectorFromTarget = k.transform.position - toFollow.position;
        Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorFromTarget);
        k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed);
    }
    public override string getName()
    {
        return "Accio";
    }
}