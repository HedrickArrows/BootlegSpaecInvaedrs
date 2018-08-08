using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

    public string animatia = "Explos";
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().Play(animatia, 0);
        //Debug.Log("Boom");
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.0f);
    }
	
	// Update is called once per frame
	void Update () {


    }
}
