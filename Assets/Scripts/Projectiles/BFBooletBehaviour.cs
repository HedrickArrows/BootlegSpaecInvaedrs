using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFBooletBehaviour : BooletBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       colliding = false;
	}
    
    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("nemy"))
        {
            Destroy(other.gameObject);
        }
    }*/
}
