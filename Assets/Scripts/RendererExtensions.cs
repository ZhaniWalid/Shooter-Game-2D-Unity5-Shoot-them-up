using UnityEngine;
using System.Collections;

public static class RendererExtensions {

/*

NB: 

Extension: the C# language allows you to extend a class with extensions, 
without needing the base source code of the class.
Create a static method starting with a first parameter which looks like this: this Type currentInstance. 
The Type class will now have a new method available everywhere your own class is available.
Inside the extension method, you can refer to the current instance calling the method 
by using the currentInstance parameter instead of this.

NB : a ' static class ' cannot ' derive ' from ' MonoBehaviour ' => false , will create problems on compiling
      => exmple : public static class A : MonoBehaviour {} => false
                  public static class A {} => true 

*/

	public static bool IsVisibleFrom (this Renderer render,Camera camera){

		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (camera);
		return GeometryUtility.TestPlanesAABB (planes, render.bounds);
	}

}
