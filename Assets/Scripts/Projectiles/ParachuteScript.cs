using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteScript : PickupScript
{
    public GameObject explode;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //moveL = moveR = true;
        if (other.gameObject.name.Contains("Boolet") && !other.GetComponent<BooletBehaviour>().colliding)
        {
            other.GetComponent<BooletBehaviour>().colliding = true;
            Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
