using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenCtrl : MonoBehaviour {

    GameObject ship;
    CarierCtrl ctrl;
    GameObject txt;

	// Use this for initialization
	void Start () {
        ship = this.transform.parent.parent.parent.gameObject;
        ctrl = ship.GetComponent<CarierCtrl>();
        txt = this.transform.Find("Text").gameObject;
	}

	// Update is called once per frame
	void Update () {
        string s = "Speed: ";
        s += ctrl.GetSpeed();
        s += "\nThrust: ";
        s += ctrl.GetThrust();
        s += "\n\nVertical Speed: ";
        s += ctrl.GetUpSpeed();
        s += "\nVertical Thrust: ";
        s += ctrl.GetUpThrust();
        s += "\n\nAngular Speed: ";
        s += ctrl.GetAngSpeed();
        s += "\nSide Thrust: ";
        s += ctrl.GetSideThrust();
        txt.GetComponent<Text>().text = s;
    }
}
