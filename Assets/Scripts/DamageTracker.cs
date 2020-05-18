using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTracker : MonoBehaviour
{
    public string takeDamageFromTag;
    public float health = 1f;
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == takeDamageFromTag)
        {
            health -= 1f;
            if (health <=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
