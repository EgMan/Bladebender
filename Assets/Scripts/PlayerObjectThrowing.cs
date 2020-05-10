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
            Vector3 shootFromPos = throwFrom.transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootingVector = new Vector2(
                mousePos.x - throwFrom.transform.position.x,
                mousePos.y - throwFrom.transform.position.y);
            shootingVector /= shootingVector.magnitude;
            // Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(shootingVector.x,shootingVector.y), Vector3.forward);

            mousePos.z = shootFromPos.z; // ensure there is no 3D rotation by aligning Z position

            // vector from this object towards the target location
            Vector3 vectorToTarget = mousePos - shootFromPos;

            // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
            // (resulting in the X axis facing the target)
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

            GameObject obj = Instantiate(throwable, throwFrom.position, targetRotation);
            obj.GetComponent<Knife>().throw_(shootingVector);
        }
    }
}
