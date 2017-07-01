using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorTxt : MonoBehaviour {

    CabinCtrl cabin;

	// Use this for initialization
	void Start () {
        cabin = this.transform.parent.parent.parent.gameObject.GetComponent<CabinCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "Floor: " + cabin.GetFloor().ToString();
	}
}
