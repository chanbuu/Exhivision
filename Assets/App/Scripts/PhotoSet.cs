using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.FB;

public class PhotoSet : MonoBehaviour {

	private void OnCollisionEnter(Collision coll){
		Debug.Log("CollisionEnter");
	}

	private void OnTriggerEnter(Collider coll){
		Debug.Log("Trigger Enter");
	}

	public void OnRayHit(){
		StartCoroutine(getFile());
	}
	
	private IEnumerator getFile(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		yield return new WaitForEndOfFrame();
		string filePath = FileBrowser.OpenSingleFile("Open Files", "", "");
		yield return new WaitForFixedUpdate();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		yield return null;
	}
}
