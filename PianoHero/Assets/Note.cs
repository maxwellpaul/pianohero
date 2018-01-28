using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	//The Rigidbody2D class essentially provides the same functionality in 2D that the Rigidbody class provides in 3D. 
	//Adding a Rigidbody2D component to a sprite puts in under the control of the physics engine. 
	//By itself, this means that the sprite will be affected by gravity and can be controlled from scripts using forces. 
	//By adding the appropriate collider component, the sprite will also respond to collisions with other sprites.
	Rigidbody2D rb;
	public float speed;

	//Awake is used to initialize any variables or game state before the game starts. 
	//Awake is called only once during the lifetime of the script instance. 
	//Awake is called after all objects are initialized
	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		rb.velocity = new Vector2 (0, -speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
