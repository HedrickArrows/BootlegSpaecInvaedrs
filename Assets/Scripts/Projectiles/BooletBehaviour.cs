using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooletBehaviour : MonoBehaviour {

    public bool colliding = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameObject.FindObjectOfType<EnemyContainerBehaviour>() || !GameObject.FindObjectOfType<EnemyContainerBehaviour>().isPaused)
        {
            Vector3 position = this.transform.position;
            position.y += (float)0.235 * 75 * Time.deltaTime;
            this.transform.position = position;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
