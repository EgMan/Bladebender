using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //Static fields
    private static List<Knife> allKnifes = new List<Knife>();

    public static List<Knife> getKnives()
    {
        return allKnifes;
    }
    public static List<Knife> getKnives(Vector2 location, float distance)
    {
        return allKnifes.FindAll(x => Vector2.Distance(location, x.transform.position) < distance);
    }

    public enum States
    {
        Thrown,
        Stuck,
        UnStuck,
        AntiGrav
    };
    public States state;
    public Rigidbody2D rb;
    public GameObject lighting;
    public float speed = 50f;

    private float angleOfAttack;
    private GameObject stuck;
    private Vector3 localStuckPosition;


    void Start()
    {
        // rb = this.GetComponent<Rigidbody2D>();
        allKnifes.Add(this);
    }
    void FixedUpdate()
    {
        switch (state)
        {
            case States.Thrown:
                lighting.SetActive(true);
                rb.gravityScale = 0f;
                break;
            case States.Stuck:
                lighting.SetActive(false);
                rb.gravityScale = 0f;
                rb.rotation = angleOfAttack;
                transform.position = stuck.transform.position + localStuckPosition;
                break;
            case States.UnStuck:
                lighting.SetActive(false);
                rb.gravityScale = 1f;
                break;
            case States.AntiGrav:
                lighting.SetActive(true);
                rb.gravityScale = 0f;
                break;
            default:
                throw new System.Exception("Unsupported state, ya dingus");
        }
    }
    public void throw_(Vector2 normVect, float throwerSpeed)
    {
        rb.AddForce((throwerSpeed * normVect) + (speed * normVect));
        angleOfAttack = rb.rotation;
        state = States.Thrown;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "Player":
                stuck = null;
                state = States.UnStuck;
                break;
            case "Throwable":
                stuck = null;
                if (state == States.Stuck || state == States.Thrown)
                {
                    state = States.UnStuck;
                }
                break;
            default:
                if (stuck != null && other.collider.gameObject != stuck)
                {
                    stuck = null;
                    state = States.UnStuck;
                }
                else
                {
                    //Otherwise we're stuck
                    state = States.Stuck;
                    stuck = other.collider.gameObject;
                    rb.velocity = Vector2.zero;
                    localStuckPosition = transform.position - stuck.transform.position;
                }
                break;

        }
        // if (other.collider.tag == "Player" || other.collider.tag == "Throwable")
        // {
        //     stuck = null;
        //     state = States.UnStuck;
        // }
        // if (other.collider.gameObject != stuck)
        // {
        //     stuck = null;
        //     state = States.UnStuck;
        // }
        // //Otherwise we're stuck
        // state = States.Stuck;
        // stuck = other.collider.gameObject;
        // rb.velocity = Vector2.zero;
        // localStuckPosition = transform.position - stuck.transform.position;
    }
}
