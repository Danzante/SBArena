using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCtrl : MonoBehaviour {

    Text txt;

	// Use this for initialization
	void Start () {
        cabin = this.transform.parent.parent.parent.Find("Cabin").gameObject.GetComponent<CabinCtrl>();
        txt = this.transform.Find("ButtonCanv").Find("Text").gameObject.GetComponent<Text>();
	}

    CabinCtrl cabin;

    public bool IsFloorButton;
    public bool IsOpenButton;
    public bool IsCloseButton;

    public int floor;

    public void Activate()
    {
        if (IsFloorButton)
        {
            cabin.SelectFloor(floor);
        }
        if (IsCloseButton)
        {
            cabin.CloseFloor();
        }
        if (IsOpenButton)
        {
            cabin.OpenFloor();
        }
    }

	// Update is called once per frame
	void Update () {
        if (IsFloorButton)
        {
            txt.text = floor.ToString();
        }
	}
}
