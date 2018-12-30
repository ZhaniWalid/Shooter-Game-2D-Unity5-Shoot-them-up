using UnityEngine;
using System.Collections;

/// Simply moves the current game object

public class MoveScript : MonoBehaviour {


	// Use this for initialization
	/*void Start () {
	
	}*/

	// 1 - Designer variables
	/// Object speed
	public Vector2 speed = new Vector2(10 , 10);

	/// Moving direction
	public Vector2 direction = new Vector2(-1,0);

	private Vector3 movement;
	private Rigidbody2D rigidbodyComponent;
	
	// Update is called once per frame
	void Update () {
	
		// 2 - Movement
		movement = new Vector3 ( speed.x * direction.x, speed.y * direction.y , 0);
		movement *= Time.deltaTime;

		transform.Translate (movement);

	}

	/*void FixedUpdate () {
	
		if (rigidbodyComponent == null) {		
			GetComponent<Rigidbody2D>();
		}

		// Apply movement to the rigidbody
		rigidbodyComponent.velocity = movement;

	}*/
}
