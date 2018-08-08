using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemyBehaviour : LaserEnemyBehaviour {

    public GameObject shed;

    public int shots;

	// Use this for initialization
	void Start () {
        container = transform.parent.gameObject;
        points = 0;shots = 0;
        shoot = container.GetComponent<EnemyContainerBehaviour>().rand.Next(450 / (int)(int)((75 * Time.deltaTime) + 1), 850 / (int)((75 * Time.deltaTime) + 1));
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject == null) Destroy(gameObject);
        if (!container.GetComponent<EnemyContainerBehaviour>().commencingScramble)
        {
            float d = container.GetComponent<EnemyContainerBehaviour>().dir;
            Vector3 position = this.transform.position;
            position.x += (float)0.03 * 75 * Time.deltaTime * d;
            this.transform.position = position;
            if (!container.GetComponent<EnemyContainerBehaviour>().isPaused) frames += 1 / (int)((75 * Time.deltaTime) + 1);
            if ((System.Math.Floor(frames / (int)((75 * Time.deltaTime) + 1)).Equals(shoot)
                || System.Math.Floor(frames / (int)((75 * Time.deltaTime) + 1)).Equals(shoot + 15 / (int)(int)((75 * Time.deltaTime) + 1))
                || System.Math.Floor(frames / (int)((75 * Time.deltaTime) + 1)).Equals(shoot + 30 / (int)(int)((75 * Time.deltaTime) + 1)))
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

    protected new void CollideWithBoolet(Collider2D other)
    {
        other.GetComponent<BooletBehaviour>().colliding = true;
        GameObject n = Instantiate(shed, transform.position, Quaternion.identity) as GameObject;
        //n.GetComponent<ArmorlessEnemyBehaviour>().container = container;
        n.transform.parent = transform.parent;
        //container.GetComponent<EnemyContainerBehaviour>().ene.Add(n);

        Instantiate(explode, transform.position, Quaternion.identity);
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    public new void OnTriggerEnter2D(Collider2D other)
    {
        //moveL = moveR = true;
        if (other.gameObject.name.Contains("Boolet") && !other.GetComponent<BooletBehaviour>().colliding && killable)
        {
            CollideWithBoolet(other);
        }
        if (other.gameObject.name.Contains("Wall") && transform.parent.gameObject.GetComponent<EnemyContainerBehaviour>().d)
        {
            container.GetComponent<EnemyContainerBehaviour>().d = false;
            container.GetComponent<EnemyContainerBehaviour>().dir *= -1;
            foreach (Transform child in transform.parent.transform)
            {
                //if(g!=null)
                if (!child.gameObject.name.Contains("Event"))
                {
                    Vector3 position = child.position;
                    position.y -= (float)0.175;
                    child.position = position;
                }
            }
            transform.parent.gameObject.GetComponent<EnemyContainerBehaviour>().height -= (float)0.15;
        }
    }

}
