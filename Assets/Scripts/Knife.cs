using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //Static fields
    public static float antigravRandomNoise = 10;
    private static List<Knife> allKnifes = new List<Knife>();
    public static void allAntiGrav()
    {
        foreach (Knife k in allKnifes)
        {
            k.state = States.AntiGrav;
            //TODO make this random
            k.rb.AddForce(new Vector2(Random.Range(0f, antigravRandomNoise), Random.Range(0f, antigravRandomNoise)));
        }
    }

    public Rigidbody2D rb;
    public GameObject lighting;
    public float speed = 50f;

    private enum States
    {
        Thrown,
        Stuck,
        UnStuck,
        AntiGrav
    };
    private float angleOfAttack;
    private GameObject stuck;
    private Vector3 localStuckPosition;
    private States state;


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
        print("knife collision");

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
