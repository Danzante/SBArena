using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarierCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameController.Start2();
        rig = this.gameObject.GetComponent<Rigidbody>();
        speed = 0;
        upspeed = 0;
        gameController.GetNewCarierID(this);
    }

    Rigidbody rig;

    const float xscale = 120;
    const float yscale = 40;
    const float zscale = 60;

    const float maxspeed = 2;
    const float maxupspeed = 1;
    const float maxangspeed = 0.1f;

    float speed;
    float upspeed;
    float angspeed;
    float upthrust = 0.001f;
    float mainthrust = 0.01f;
    float sidethrust = 1;

    void RecountSpeed()
    {
        if (Mathf.Abs(speed + mainthrust) > maxspeed)
        {
            speed = Mathf.Sign(speed + mainthrust) * maxspeed;
        }
        else
        {
            speed = speed + mainthrust;
        }

        if (Mathf.Abs(upspeed + upthrust) > maxupspeed)
        {
            upspeed = Mathf.Sign(upspeed + upthrust) * maxupspeed;
        }
        else
        {
            upspeed = upspeed + upthrust;
        }

        if (speed == 0)
        {
            angspeed = 0;
        }
        else {
            angspeed = sidethrust / speed;
            angspeed *= 2 * Mathf.PI;
            angspeed /= 360;
            if(Mathf.Abs(angspeed) > maxangspeed)
            {
                angspeed = Mathf.Sign(angspeed) * maxangspeed;
            }
        }
    }

    public Vector3 moveDirection;

    void ApplySpeed()
    {
        transform.Rotate(0, angspeed * Time.fixedDeltaTime, 0);

        moveDirection = new Vector3(speed, upspeed, 0);
        moveDirection = transform.TransformDirection(moveDirection);

        //gameController.UpdateMoves(this);
        moveDirection *= Time.fixedDeltaTime;
        transform.position += moveDirection;
    }

    void Play()
    {
        lastUpdate = Time.time;
        RecountSpeed();
        ApplySpeed();
    }

    public float GetLastUpdate()
    {
        return lastUpdate;
    }

    public bool IsInThis(Transform t)
    {
        Vector3 dist = t.position - this.transform.position;
        if (Mathf.Abs(dist.x) <= xscale && Mathf.Abs(dist.y) <= yscale && Mathf.Abs(dist.z) <= zscale)
            return true;
        return false;
    }

    public Vector3 GetMove()
    {
        return moveDirection;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetMaxSpeed()
    {
        return maxspeed;
    }

    public float GetUpSpeed()
    {
        return upspeed;
    }

    public float GetAngSpeed()
    {
        return angspeed;
    }

    public float GetMaxAngSpeed()
    {
        return maxangspeed;
    }

    public float GetThrust()
    {
        return mainthrust;
    }

    public float GetUpThrust()
    {
        return upthrust;
    }

    public float GetSideThrust()
    {
        return sidethrust;
    }

    float lastUpdate;

    public void ForceUpdate()
    {
        if (Time.time - lastUpdate >= Time.fixedDeltaTime)
        {
            Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Time.time - lastUpdate >= Time.fixedDeltaTime) {
            gameController.Update2();
            if (!gameController.paused)
                Play();
        }
    }
}
