using UnityEngine;
using System.Collections;

public class CreateRandomPlatforms : MonoBehaviour {

	public GameObject platform1_Prefab ;
	public GameObject platform2_Prefab ; 
	public float nbresPlatformsTotal;
	public float xMin = 5F;
	public float xMax = 1200F;
	public float yMin = 9.5F;
	public float yMax = -9.5F;
	
	
	// Use this for initialization
	void Start () {
		// this function "InvokeRepeating",let you to repeat the call of another function in a number of time 
		// (how repeadtly call a function=9adh mn marra bsh tet3awd c a d)
		// besh t3ayt l fonction "scoreUpdate()"
		// commence : à partir du temp= 1 secondes
		// et elle se répète l'appelle du "createRandomPoupliUpdate()"  : chaque 3.0 secondes
		// => début : lorsque temp = 1 seconde et répétetion chaque 3 secondes
		InvokeRepeating("createRandomPlatformsUpdate",1.0f,3.5f);
	}
	
	void createRandomPlatformsUpdate()
	{
		//GameObject newParent = GameObject.Find("1- Background elements"); // 1
		GameObject newParent_1 = GameObject.FindGameObjectWithTag("Background elements"); // 2
		GameObject newParent_2 = GameObject.FindGameObjectWithTag("Middleground");
		GameObject newParent_3 = GameObject.FindGameObjectWithTag("Foreground");

		for (int i = 0; i < nbresPlatformsTotal ; i++) {
			
			Vector3 newPosition = new Vector3(Random.Range(xMin,xMax),Random.Range(yMin,yMax),0);

			GameObject gameObj_1 = Instantiate (platform1_Prefab,newPosition,Quaternion.identity) as GameObject;
			GameObject gameObj_2 = Instantiate (platform2_Prefab,newPosition,Quaternion.identity) as GameObject;

			gameObj_1.transform.parent  = newParent_1.transform;
			gameObj_1.transform.parent  = newParent_2.transform;
			gameObj_1.transform.parent  = newParent_3.transform;

			gameObj_2.transform.parent  = newParent_1.transform;
			gameObj_2.transform.parent  = newParent_2.transform;
			gameObj_2.transform.parent  = newParent_3.transform;
		}
	}
	
	//	" NullReferenceException: Object reference not set to an instance of an object " 
	//	=> Line 24 :	"gameObj.transform.parent  = newParent.transform;"
	//    the problem IS solved by changing //1 by //2
	//    and on 'Unity' in the 'Hierarchy' -> go to :
	//   1- Background elements -> Tag -> Add Tag -> and name your tag 'Background elements'
	// and like this when the compiler go to the line 19 : 
	// 'GameObject newParent = GameObject.FindGameObjectWithTag("Background elements");' 
	// it will search on 'Hierarchy' and find the game object with the tag 'Background elements'
	// who is '1- Background elements'
}
