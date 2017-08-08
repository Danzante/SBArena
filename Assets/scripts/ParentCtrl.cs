using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCtrl : MonoBehaviour {

    GameObject player; 

	// Use this for initialization
	void Start () {
        player = this.transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
