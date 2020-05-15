using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleThrow : Ability
{
    public float translateSpeed = 1.75f;
    public float rotateSpeed = 1f;
    public int mouseDataPoints = 100;

    private Dictionary<Knife, Vector3> offsets = new Dictionary<Knife, Vector3>();
    private Vector3 lastMousePos = Vector3.zero;
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
        k.rb.position = Vector2.Lerp(k.rb.position, mousePos + offset, Time.deltaTime * translateSpeed);

        // interpolate rotation
        if ((lastMousePos - mousePos).magnitude < .1f)
        {
            Vector2 vectorToTarget = mousePos - k.transform.position;
            if (vectorToTarget.magnitude > .1f)
            {
                Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
                k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed);
            }
        }
        else
        {
            print("fast enough");
            if (deltaMouseVector.magnitude > 0.01f)
            {
                Quaternion rotateTo = Quaternion.LookRotation(forward: Vector3.forward, upwards: deltaMouseVector);
                k.transform.rotation = Quaternion.Lerp(rotateTo, transform.rotation, Time.deltaTime * rotateSpeed / 5000);
            }
        }
    }

    public override void release(Knife k)
    {
        k.rb.velocity = Vector2.zero;
        k.rb.angularVelocity = 0f;
        Vector3 throwVector = k.transform.rotation * Vector3.up;
        k.launch(throwVector, 0f);

        offsets.Clear();
    }

    void Update()
    {
        // TODO abstract this logic into a mouseDirectionSmoother class
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastMousePos = mousePos;
        mousePoints.Enqueue(mousePos);
        deltaMouseVector = mousePos - mousePoints.Peek();
        if (mousePoints.Count > mouseDataPoints)
        {
            mousePoints.Dequeue();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            rotateAboutCenter(-10);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            rotateAboutCenter(10);
        }
    }

    private void rotateAboutCenter(float angle)
    {
        Quaternion rotate = Quaternion.Euler(0, 0, angle);

        Vector3 center = Vector2.zero;
        foreach ( KeyValuePair<Knife, Vector3> entry in offsets )
        {
            center += entry.Value;
        }
        center = center / offsets.Count;

        List<Knife> keys = new List<Knife>(offsets.Keys);
        foreach ( Knife key in keys)
        {
            offsets[key] = (rotate * (offsets[key] - center)) + center;
        }
    }

    private Vector3 findCenter()
    {
        Vector3 sum = Vector2.zero;
        foreach ( KeyValuePair<Knife, Vector3> entry in offsets )
        {
            sum += entry.Value;
        }
        return sum / offsets.Count;
    }
}
