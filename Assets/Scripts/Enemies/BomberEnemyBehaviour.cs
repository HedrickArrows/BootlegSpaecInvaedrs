using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemyBehaviour : LaserEnemyBehaviour {

	// Use this for initialization
	void Start () {
        container = transform.parent.gameObject;
        points = 30;
        shoot = container.GetComponent<EnemyContainerBehaviour>().rand.Next(550 / (int)((75 * Time.deltaTime) + 1), 900 / (int)((75 * Time.deltaTime) + 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null) Destroy(gameObject);
        if (!container.GetComponent<EnemyContainerBehaviour>().commencingScramble)
        {
            float d = container.GetComponent<EnemyContainerBehaviour>().dir;
            Vector3 position = this.transform.position;
            position.x += (float)0.03 * 75 * Time.deltaTime * d;
            this.transform.position = position;
            if (!container.GetComponent<EnemyContainerBehaviour>().isPaused) frames += 1 / (int)((75 * Time.deltaTime) + 1);
            if (frames / (int)((75 * Time.deltaTime) + 1) >= shoot && container.GetComponent<EnemyContainerBehaviour>().Playar.GetComponent<PlayerBehaviour>().health > 0
                && !container.GetComponent<EnemyContainerBehaviour>().isPaused)
            {
                frames = 0;
                shoot = container.GetComponent<EnemyContainerBehaviour>().rand.Next(650 / (int)((75 * Time.deltaTime) + 1), 1250 / (int)((75 * Time.deltaTime) + 1));
                Instantiate(enemyboolet, transform.position, Quaternion.identity);
            }
        }
        else
            killable = false;
    }
}
