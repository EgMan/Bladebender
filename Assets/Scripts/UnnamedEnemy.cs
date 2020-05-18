using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnnamedEnemy : MonoBehaviour
{
    public float timeBetweenShots = 1f;

    // Start is called before the first frame update
    public Transform shootFrom, shootAt;
    public GameObject throwable;

    private void Start() {
        lookAndFire();
    }

    private void lookAndFire()
    {
        Vector3 shootTowards = shootAt.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootTowards, 900, ~(1 << 12));
        if (hit && hit.collider.gameObject.tag == "Player")
        {
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: shootTowards);
            GameObject obj = Instantiate(throwable, shootFrom.position, targetRotation);
            obj.GetComponent<KnifeMain>().launch(shootTowards, 0f);
        }
        Invoke("lookAndFire", timeBetweenShots);
    }
}
