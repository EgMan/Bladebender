using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY, smoothTimeX;

    public GameObject toFollow;
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, toFollow.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, toFollow.transform.position.y, ref velocity.y, smoothTimeX);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
