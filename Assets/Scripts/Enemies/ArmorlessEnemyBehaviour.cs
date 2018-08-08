using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorlessEnemyBehaviour : LaserEnemyBehaviour {

    public int shots;

    // Use this for initialization
    void Start()
    {
        container = transform.parent.gameObject;
        points = 50; shots = 0;
        shoot = container.GetComponent<EnemyContainerBehaviour>().rand.Next(450 / (int)(int)((75 * Time.deltaTime) + 1), 850 / (int)((75 * Time.deltaTime) + 1));
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
            if (!container.GetComponent<EnemyContainerBehaviour>().isPaused) frames += 1 / ((75 * Time.deltaTime) + 1);
            if ((System.Math.Floor(frames / ((75 * Time.deltaTime) + 1)).Equals(shoot)
                || System.Math.Floor(frames / ((75 * Time.deltaTime) + 1)).Equals(shoot + 15 / ((75 * Time.deltaTime) + 1))
                || System.Math.Floor(frames / ((75 * Time.deltaTime) + 1)).Equals(shoot + 30 / ((75 * Time.deltaTime) + 1)))
                && container.GetComponent<EnemyContainerBehaviour>().Playar.GetComponent<PlayerBehaviour>().health > 0)
            {
                shots++;
                Instantiate(enemyboolet, transform.position, Quaternion.identity);
            }
            if (shots >= 3)
            {
                frames = shots = 0;
                shoot = container.GetComponent<EnemyContainerBehaviour>().rand.Next(700 / (int)((75 * Time.deltaTime) + 1), 1250 / (int)((75 * Time.deltaTime) + 1));
            }
        }
        else
            killable = false;
    }
}
