using UnityEngine;

public class Throwable2D : MonoBehaviour
{
    public Rigidbody2D body;
    public void throw_(Vector2 vect)
    {
        body.velocity = vect;
    }
}