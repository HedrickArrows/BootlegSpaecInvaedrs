using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBooletBehaviour : BooletBehaviour {

    public int dir = 0;

    void Update()
    {

        if (!GameObject.FindObjectOfType<EnemyContainerBehaviour>().isPaused)
        {
            Vector3 position = this.transform.position;
            position.x += (float)0.07 *(75 * Time.deltaTime) * dir;
            position.y += (float)0.17 * 75 * Time.deltaTime;
            this.transform.position = position;
        }
    }

}
