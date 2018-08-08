using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {



    public int points = 15;
    
    public GameObject container;

    public GameObject[] drops;

    public GameObject explode;

    public float scatterX, scatterY;

    public bool killable = true;

	// Use this for initialization
	void Start () {
        scatterX = scatterY = 999;
        container = transform.parent.gameObject;
        //container = null;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject == null) Destroy(gameObject);
        if (!container.GetComponent<EnemyContainerBehaviour>().commencingScramble)
        {
            float d = container.GetComponent<EnemyContainerBehaviour>().dir;
            Vector3 position = this.transform.position;
            position.x += (float)0.03 * 75 * Time.deltaTime * d;
            this.transform.position = position;
            killable = true;
        }
        else
            killable = false;
        
    }
    

    protected void CollideWithBoolet(Collider2D other) {
        if (killable)
        {
            Instantiate(explode, transform.position, Quaternion.identity);
            other.GetComponent<BooletBehaviour>().colliding = true;
            if (container.GetComponent<EnemyContainerBehaviour>().rand.Next(0, 100) < 7)
                Instantiate(drops[container.GetComponent<EnemyContainerBehaviour>().rand.Next(0, drops.Length)], transform.position, Quaternion.identity);
            container.GetComponent<EnemyContainerBehaviour>().dir *= (float)(1.04165);
            container.GetComponent<EnemyContainerBehaviour>().Playar.GetComponent<PlayerBehaviour>().score += points;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //moveL = moveR = true;
        if (other.gameObject.name.Contains("Boolet") && !other.GetComponent<BooletBehaviour>().colliding)
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

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Boolet") && !other.GetComponent<BooletBehaviour>().colliding)
        {
            CollideWithBoolet(other);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
        {
        if (other.gameObject.name.Contains("Boolet") && !other.GetComponent<BooletBehaviour>().colliding)
        {
            CollideWithBoolet(other);
        }
        if (other.gameObject.name.Contains("Wall"))
            container.GetComponent<EnemyContainerBehaviour>().d = true;
        }
    
    protected void OnBecameInvisible()
    {   
            Destroy(gameObject);
    }
    /*
    protected void OnDestroy()
    {
        //if(gameObject!=null)
        //if(!gameObject.Equals(null))
        try
        {
            if (container.GetComponent<EnemyContainerBehaviour>().ene.Contains(gameObject))
                container.GetComponent<EnemyContainerBehaviour>().ene.Remove(gameObject);
        }
        catch (MissingReferenceException) {
            Debug.Log("This goddamn MissingReferenceEx again");
        }

    }*/
}
