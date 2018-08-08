using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseBehaviour : MonoBehaviour {
    
    public SpriteRenderer sr;
    public Sprite[] weaponSprites;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
