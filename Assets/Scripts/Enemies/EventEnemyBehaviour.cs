using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemyBehaviour : EnemyBehaviour {

	// Use this for initialization
	void Start () {
        container = transform.parent.gameObject;
        points = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject == null) Destroy(gameObject);
        if (!container.GetComponent<EnemyContainerBehaviour>().isPaused)
        {
            scatterX = transform.position.x;
            scatterY = transform.position.y;
            Vector3 position = this.transform.position;
            position.x -= (float)0.1 * 75 * Time.deltaTime;
            this.transform.position = position;
        }
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Chef")){ }
        //moveL = moveR = true;
        if (other.gameObject.name.Contains("Boolet"))
        {
            Vector3 vek = transform.position;
            vek.y -= 0.5f;
            Instantiate(explode, vek, Quaternion.identity);
            if(container.GetComponent<EnemyContainerBehaviour>().rand.Next(0,5)<=1)
                Instantiate(drops[0], vek, Quaternion.identity);
            container.GetComponent<EnemyContainerBehaviour>().Playar.GetComponent<PlayerBehaviour>().score += points;
            //container.GetComponent<EnemyContainerBehaviour>().ene.Remove(this.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        //if (other.gameObject.name.Equals("LeftWall"))
        //    Destroy(gameObject);
    }
    private new void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("LeftWall"))
            Destroy(gameObject);
        
    }


}
