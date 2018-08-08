using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBooletBehaviour : BooletBehaviour
{
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.FindObjectOfType<EnemyContainerBehaviour>().isPaused)
        {
            Vector3 position = this.transform.position;
            position.y -= (float)0.105 * 75 * Time.deltaTime;
            this.transform.position = position;
        }
    }
}
