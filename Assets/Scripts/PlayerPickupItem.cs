using UnityEngine;

public class PlayerPickupItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickupable")
        {
            Knife knife = other.GetComponentInParent<Knife>();
            if ( knife != null && knife.state != Knife.States.Thrown)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }
}
