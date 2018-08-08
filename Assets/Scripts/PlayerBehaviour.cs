using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject boolet, shotgun, BFG, swirly;
    //public Text result;
    public bool moveL, moveR, shoot, lazor;
    public int score;
    public int health = 7;

    public float time, limit, interval = 0, acceleratione = 0;
    public int weapontype = 0;

    Vector3 position;
    // Use this for initialization
    void Start()
    {
        //result = Instantiate(result) as Text;
        score = 0;
        lazor = false;
        moveL = moveR = shoot = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveL || acceleratione < 0) acceleratione = 0;
        if (!moveR || acceleratione > 0) acceleratione = 0;
        

        //result.text = score.ToString();
        if (Input.GetKey(KeyCode.LeftArrow) && moveL)
        {

            acceleratione = (float)(acceleratione <= 1.1 ? 1.1 : (acceleratione > 0 ? acceleratione / 1.75 :(acceleratione == 0 ?  0.3 : acceleratione +0.001 )));
            /*
            Vector3 position = this.transform.position;
            position.x-=(float)0.1 * 75 * Time.deltaTime;
            this.transform.position = position;
            */
        }
        if (Input.GetKey(KeyCode.RightArrow) && moveR)
        {
            acceleratione = (float)(acceleratione >= -1.1 ? -1.1 : (acceleratione < 0 ? acceleratione / 1.75 : (acceleratione == 0 ? -0.3: acceleratione +0.001 )));
            /*
            Vector3 position = this.transform.position;
            position.x+= (float)0.1 * 75 * Time.deltaTime;
            this.transform.position = position;
            */
        }


        position = this.transform.position;
        position.x -= (float)0.1 * 75 * acceleratione * Time.deltaTime;
        this.transform.position = position;



        if (Input.GetKey(KeyCode.Space) && shoot){
            if (weapontype == 0) //regular lasr
                if (Time.time >= interval + .25f)
                {
                    Instantiate(boolet, transform.position, Quaternion.identity);
                    interval = Time.time;
                }
            if (weapontype == 1){ //shotgun balls
                if (Time.time >= interval + .475f)
                {
                    GameObject g;
                    for (int eueue = 0; eueue < 3; eueue++)
                    {
                        g = (Instantiate(shotgun, transform.position, Quaternion.identity) as GameObject);
                        g.GetComponent<ShotgunBooletBehaviour>().dir = eueue - 1;
                    }
                    interval = Time.time;
                }
            }
            if (weapontype == 2 && !lazor) //BFLazor
            {
                lazor = true;
                GameObject g;
                g =(Instantiate(swirly, transform) as GameObject);
                //g.transform.parent = transform;
                g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + 1.875f);
                //g.GetComponent<Animator>().Play(3);

            }
            
            if (weapontype == 3)  //machinegune
            {
                if (Time.time >= interval + .0725f)
                {
                    Instantiate(boolet, transform.position, Quaternion.identity);
                    interval = Time.time;
                }
            }
        }
        

        if (weapontype != 0) {
            if (Time.time >= time + limit)
                weapontype = 0;
        }

        if (lazor == true && transform.childCount == 0) { //BFBooler now Fires
            GameObject g;
            g = (Instantiate(BFG, transform) as GameObject);
            g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + 5f);
            g.transform.localScale = new Vector3(2 / transform.lossyScale.x, 5.25f / transform.lossyScale.y);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) || weapontype != 2)
        {
            lazor = false;
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        //moveL = moveR = true;
        if (other.gameObject.name.Equals("RightWall"))
        {
            moveR = false;
            acceleratione = 0;
        }
        if (other.gameObject.name.Equals("LeftWall"))
        {
            moveL = false;
            acceleratione = 0;
        }

        if (other.gameObject.name.Contains("danger"))
        {
            health--;// += points;
            if (other.gameObject.name.Contains("Orb")) health--;
            Destroy(other.gameObject);
            //Destroy(gameObject);
        }
        if (other.gameObject.name.Contains("nemy")) {
            health -= 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name.Contains("Health"))
        {
            if(health < 12)
                health++;// += points;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name.Contains("Parachuter"))
        {
            score += 15;
            if (other.gameObject.name.Contains("Chef"))
                score += 85;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name.Contains("Weapon")) {
            time = Time.time;
            int pickupw = 0;
            if (other.gameObject.name.Contains("Shotgun")) {
                pickupw = 1;
                limit = 3.2f;
            }
            if (other.gameObject.name.Contains("BF"))
            {
                pickupw = 2;
                limit = 2.2f;
            }
            if (other.gameObject.name.Contains("Barrage"))
            {
                pickupw = 3;
                limit = 3f;
            }
            if (pickupw != weapontype)
                interval = 0;
            weapontype = pickupw;
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("RightWall")) {
            moveR = false;
        }
        if (other.gameObject.name.Equals("LeftWall")) {
            moveL = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("RightWall") || other.gameObject.name.Equals("LeftWall"))
        
            moveR = moveL = true;
    }


}
