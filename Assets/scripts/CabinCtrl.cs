using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinCtrl : MonoBehaviour {

    CarierCtrl carier;

	// Use this for initialization
	void Start ()
    {
        carier = this.transform.parent.parent.gameObject.GetComponent<CarierCtrl>();
        RightD = this.transform.Find("RightDoor").gameObject.GetComponent<DoorCtrl>();
        LeftD = this.transform.Find("LeftDoor").gameObject.GetComponent<DoorCtrl>();
        OpenFloor();
        gameController.GetNewLiftID(this);
    }

    DoorCtrl RightD;
    DoorCtrl LeftD;

    public int maxFloor;

    int floor = 0;

    int nowFloor = 0;

    float lastFloorSelect = -5;
    float lfChangeTime = 0;
    float lastDoor = -5;
    const float DoorChangeTime = 2;

    public void SelectFloor(int num)
    {
        if (Time.time - lastFloorSelect < lfChangeTime)
            return;
        if (Time.time - lastDoor < DoorChangeTime)
            return;
        floor = num;
        if (floor < 0)
        {
            floorH = floor * 40;
        }
        else if (floor > maxFloor)
        {
            floorH = maxH + (floor - maxFloor) * 40;
        }
        else
        {
            floorH = floor * 25;
        }
        CloseFloor();
    }

    public int GetFloor()
    {
        return nowFloor;
    }

    public void OpenFloor()
    {
        if (Time.time - lastFloorSelect < lfChangeTime)
            return;
        if (Time.time - lastDoor < DoorChangeTime)
            return;
        lastDoor = Time.time;
        RightD.Open();
        LeftD.Open();
        this.transform.parent.Find("LiftHall" + nowFloor.ToString()).gameObject.GetComponent<LiftHallCTrl>().Open();
    }

    public void CloseFloor()
    {
        if (Time.time - lastFloorSelect < lfChangeTime)
            return;
        if (Time.time - lastDoor < DoorChangeTime)
            return;
        lastDoor = Time.time;
        RightD.Close();
        LeftD.Close();
        this.transform.parent.Find("LiftHall" + nowFloor.ToString()).gameObject.GetComponent<LiftHallCTrl>().Close();
    }

    float currspeed;
    public float currH = 0;
    public float floorH;
    float maxH;

    bool inway = false;

    void Play()
    {
        maxH = maxFloor * 25;
        if ((Time.time - lastFloorSelect < lfChangeTime) || (currspeed != 0))
        {
            currH += currspeed * Time.fixedDeltaTime;
            this.transform.localPosition += new Vector3(0, currspeed * Time.fixedDeltaTime, 0);
            if (currH < 0) {
                nowFloor = Mathf.RoundToInt(currH) / 40;
            }
            else if(currH > maxH)
            {
                nowFloor = maxFloor + Mathf.RoundToInt(currH - maxH) / 40;
            }
            else
            {
                nowFloor = Mathf.RoundToInt(currH) / 25;
            }
            if((currspeed != 0)&&(Mathf.Sign(currspeed) * (floorH - currH) < 0.01f))
            {
                currspeed = 0;
            }
        }
        else if (Time.time - lastDoor > DoorChangeTime)
        {
            if (floor != nowFloor)
            {
                lastFloorSelect = Time.time;
                int flr = 0;
                int angars = 0;
                if (floor > maxFloor)
                {
                    flr = maxFloor;
                    angars += floor - maxFloor;
                }
                else if (floor < 0) {
                    angars += floor;
                }
                else
                {
                    flr = floor;
                }
                if (nowFloor > maxFloor)
                {
                    flr -= maxFloor;
                    angars -= nowFloor - maxFloor;
                }
                else if (nowFloor < 0)
                {
                    angars -= nowFloor;
                }
                else
                {
                    flr -= nowFloor;
                }
                angars = Mathf.Abs(angars);
                flr = Mathf.Abs(flr);
                int dist = angars * 40 + flr * 20;
                lfChangeTime = dist / 5;
                if (floor > nowFloor)
                {
                    currspeed = 5;
                }
                else
                {
                    currspeed = -5;
                }
                inway = true;
            }
            else
            {
                if (inway)
                {
                    OpenFloor();
                    inway = false;
                }
            }
        }
    }


    const float xscale = 5;
    const float yscale = 5;
    const float zscale = 5;

    public bool IsInThis(Transform t)
    {
        Vector3 dist = t.position - this.transform.position;
        if (Mathf.Abs(dist.x) <= xscale && Mathf.Abs(dist.y) <= yscale && Mathf.Abs(dist.z) <= zscale)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        gameController.Update2();
        if (!gameController.paused)
            Play();
    }
}
