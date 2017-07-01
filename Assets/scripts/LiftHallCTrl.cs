using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftHallCTrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        cabin = this.transform.parent.Find("Cabin").gameObject.GetComponent<CabinCtrl>();
        RightD = this.transform.Find("RightDoor").gameObject.GetComponent<DoorCtrl>();
        LeftD = this.transform.Find("LeftDoor").gameObject.GetComponent<DoorCtrl>();
    }

    public int FLOOR;

    CabinCtrl cabin;

    DoorCtrl RightD;
    DoorCtrl LeftD;

    public void Open()
    {
        RightD.Open();
        LeftD.Open();
    }

    public void Close()
    {
        RightD.Close();
        LeftD.Close();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
