using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public bool right;
    public bool inner;

    float angs;

    float rot = 0;

    public void Open()
    {
        if((right && inner)||(!right && !inner))
        {
            angs = -45;
        }
        else
        {
            angs = 45;
        }
    }

    public void Close()
    {
        if ((right && inner) || (!right && !inner))
        {
            angs = 45;
        }
        else
        {
            angs = -45;
        }
    }

    void Play()
    {
        this.transform.Rotate(0, angs * Time.fixedDeltaTime, 0);
        rot += angs * Time.deltaTime;
        if ((right && inner) || (!right && !inner))
        {
            if(rot < -90)
            {
                this.transform.Rotate(0, -90 - rot, 0);
                rot = -90;
                angs = 0;
            }
            if(rot > 0)
            {
                this.transform.Rotate(0, -rot, 0);
                rot = 0;
                angs = 0;
            }
        }
        else
        {
            if (rot > 90)
            {
                this.transform.Rotate(0, 90 - rot, 0);
                rot = 90;
                angs = 0;
            }
            if (rot < 0)
            {
                this.transform.Rotate(0, -rot, 0);
                rot = 0;
                angs = 0;
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        gameController.Update2();
        if (!gameController.paused)
            Play();
    }
}
