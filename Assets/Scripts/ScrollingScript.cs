using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// Parallax scrolling script that should be assigned to a layer
public class ScrollingScript : MonoBehaviour {

	/// Scrolling speed
	public Vector2 speed = new Vector2(2,2);

	/// Moving direction
	public Vector2 direction = new Vector2(-1,0);

	/// Movement should be applied to camera
	public bool isLinkedToCamera = false;

	/// 1 - Background is infinite
	public bool isLooping = false;

	/// 2 - List of children with a renderer.
	private List<SpriteRenderer> backgroundPart;



	// 3 - Get all the children
	void Start() {
	    
		// For infinite background only
		if (isLooping) {
		
			// Get all the children of the layer with a renderer
			backgroundPart = new List<SpriteRenderer>();
			  
			for (int i=0 ; i<transform.childCount ; i++){

				Transform child = transform.GetChild(i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer>();

				// Add only the visible children
				if (r!= null){
					backgroundPart.Add(r);
				}
			}

			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();
		
		}
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Movement
		Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		movement *= Time.deltaTime;

		transform.Translate (movement);

		// Move the camera
		if (isLinkedToCamera) {
			Camera.main.transform.Translate(movement);
		}

		// 4 - Loop
		if (isLooping) {
			// Get the first object.
			// The list is ordered from left (x position) to right.
			SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

			if(firstChild != null){

				     // Check if the child is already (partly) before the camera.
					// We test the position first because the IsVisibleFrom
					// method is a bit heavier to execute.
				if(firstChild.transform.position.x < Camera.main.transform.position.x){

					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					if(firstChild.IsVisibleFrom(Camera.main)==false){

						// Get the last child position.
						SpriteRenderer lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

						// Set the position of the recyled one to be AFTER the last child.
						// Note: Only work for horizontal scrolling currently.
                        firstChild.transform.position =  new Vector3(lastPosition.x + lastSize.x 
						                                             ,firstChild.transform.position.y
						                                             ,firstChild.transform.position.z);

						// Set the recycled child to the last position of the backgroundPart list.
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);

					}
				}
			}	
		}
	}

   /*
Explanations:

We need a public variable to turn on the “looping” mode in the “Inspector” view.
We also have to use a private variable to store the layer children.
In the Start() method, we set the backgroundPart list with the children that have a renderer. 
Thanks to a bit of LINQ, we order them by their X position and put the leftmost at the first position of the array.
In the Update() method, if the isLooping flag is set to true, we retrieve the first child stored in the backgroundPart list. 
We test if it’s completely outside the camera field. When it’s the case, we change its position to be after the last (rightmost) child. Finally, we put it at the last position of backgroundPart list.
Indeed, the backgroundPart is the exact representation of what is happening in the scene.


Remember :  to enable the “Is Looping” property of the “ScrollingScript” for the 0 - Background in the “Inspector” pane. Otherwise, it will (predictably enough) not work.



Note: why don’t we use the OnBecameVisible() and OnBecameInvisible() methods? Because they are broken.

The basic idea of these methods is to execute a fragment of code when the object is rendered (or vice-versa). 
They work like the Start() or Stop() methods (if you need one, simply add the method in the MonoBehaviour and Unity will use it).
The problem is that these methods are also called when rendered by the “Scene” view of the Unity editor. 
This means that we will not get the same behavior in the Unity editor and in a build (whatever the platform is). 
This is dangerous and absurd. We highly recommend to avoid these methods.


Remember: we’ve added a “ScrollingScript” to this layer in order to move the camera along with the player.
But there is a simple solution: move the “ScrollingScript” from the Foreground layer to the player!
Why not after all? The only thing that is moving in this layer is him, 
and the script is not specific to a kind of object.
	 */

}
