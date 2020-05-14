using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleThrow : Ability
{
    public float translateSpeed = 1.75f;
    public float rotateSpeed = 1f;
    public int mouseDataPoints = 100;

    private Dictionary<Knife, Vector3> offsets = new Dictionary<Knife, Vector3>();
    // private Vector3 lastMousePos = Vector3.zero;
    private Vector3 deltaMouseVector = Vector3.zero;
    private Queue<Vector3> mousePoints = new Queue<Vector3>();
    public override string getName()
    {
        return "Tele-Throw";
    }
    public override void activate(Knife projectile)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offsets[projectile] = projectile.transform.position - mousePos;

        projectile.state = Knife.States.AntiGrav;

        // lastMousePos = mousePos;
    //    mousePoints.Enqueue(mousePos);
    }
    public override void updateHold(Knife k)
    {
        if (k.state != Knife.States.AntiGrav)
        {
            k.removeAbility();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //interpolate translation
        Vector3 offset;
        if (!offsets.TryGetValue(k, out offset))
        {
            offset = Vector2.zero;
        }
        Vector2 currnt_velocity = k.rb.velocity;
        k.rb.position = Vector2.Lerp(k.rb.position, mousePos+offset, Time.deltaTime * translateSpeed);

        // interpolate rotation
        // Vector2 vectorToTarget = mousePos - k.transform.position;
        // if (vectorToTarget.magnitude > .1f)
        // {
        //     Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        //     k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed);
        // }

        if (deltaMouseVector.magnitude > 0.01f)
        {
            Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: deltaMouseVector);
            k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed/5000);
        }

    }

    public override void release(Knife k)
    {
        k.rb.velocity = Vector2.zero;
        k.rb.angularVelocity = 0f;
        Vector3 throwVector = k.transform.rotation * Vector3.up;
        k.launch(throwVector, 0f);
    }

    void Update()
    {
        // TODO abstract this logic into a mouseDirectionSmoother class
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoints.Enqueue(mousePos);
        deltaMouseVector = mousePos - mousePoints.Peek();
        if (mousePoints.Count > mouseDataPoints)
        {
            mousePoints.Dequeue();
        }
    }
}
