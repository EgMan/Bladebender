using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectThrowing : MonoBehaviour
{
    public GameObject throwable;
    public Transform throwFrom;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 shootingVector = new Vector2(
                mousePos.x - throwFrom.transform.position.x, 
                mousePos.y - throwFrom.transform.position.y);
            Instantiate(throwable, throwFrom.position, Quaternion.AngleAxis(Mathf.Atan2(shootingVector.x,shootingVector.y), Vector3.up));
        }
    }
}
