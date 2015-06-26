using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraMirror : MonoBehaviour {

	public bool mirror;

	void OnPreCull()
	{
		if(mirror)
		{
			GetComponent<Camera>().ResetWorldToCameraMatrix ();
			GetComponent<Camera>().ResetProjectionMatrix ();
			GetComponent<Camera>().projectionMatrix = 
					GetComponent<Camera>().projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
		}else{
			GetComponent<Camera>().ResetWorldToCameraMatrix ();
			GetComponent<Camera>().ResetProjectionMatrix ();
		}
	}

	void onPreRender()
	{
		if(mirror) GL.invertCulling = true;
	}

	void onPostRender()
	{
		if(mirror) GL.invertCulling = false;
	}
}
