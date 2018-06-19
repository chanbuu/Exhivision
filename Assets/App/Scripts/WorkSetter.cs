using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSetter : MonoBehaviour {
	private float rayLength = 500f;
	public LayerMask rayExclusionLayers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TouchInfo info = AppUtil.GetTouch();
		if(info == TouchInfo.Began){
			Ray ray = Camera.main.ScreenPointToRay(AppUtil.GetTouchPosition());
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, rayLength, ~rayExclusionLayers)){
				Debug.Log("hit object name"+hit.transform.gameObject.name);

				if(hit.transform.GetComponent<PhotoSet>() != null){
					hit.transform.GetComponent<PhotoSet>().OnRayHit();
				}
			}
		}
	}

	
}
