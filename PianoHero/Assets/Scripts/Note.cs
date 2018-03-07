using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	
	Rigidbody2D rb;
    GameManager gm;
    float speed;

	void Awake(){
        rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        speed = gm.noteSpeed;
        print(speed);
		rb.velocity = new Vector2 (0, -speed);
	}
}
