using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 50f;
    private float angleOfAttack;
    private GameObject stuck;
    private Vector3 localStuckPosition;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (stuck != null)
        {
            transform.position = stuck.transform.position + localStuckPosition;
            rb.rotation = angleOfAttack;
        }
    }
    public void throw_(Vector2 normVect, float throwerSpeed)
    {
        rb.AddForce((throwerSpeed * normVect) + (speed *normVect));
        angleOfAttack = rb.rotation;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        print("knife collision");
        if (other.collider.tag == "Player" || other.collider.tag == "Throwable")
        {
            stuck = null;
            return;
        }
        if (stuck != null && other.collider.gameObject != stuck)
        {
            stuck = null;
            return;
        }
        // rb.isKinematic = false;


        // transform.parent = other.transform;
        rb.velocity = Vector2.zero;
        stuck = other.collider.gameObject;
        // rb.isKinematic = true;
        localStuckPosition = transform.position - stuck.transform.position;

        // rb.rotation = angleOfAttack;
        // transform.parent = other.transform;
    }
}
