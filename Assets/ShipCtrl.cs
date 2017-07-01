using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipCtrl : MonoBehaviour {//NetworkBehaviour {

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}

    Rigidbody rig;

    const float maxspd = 36;
    float speed = 1.0f;
    float rotSpeed = 450;
    float spd = 0;

    Vector3 mvd = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;

    void Play()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime, 0);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime, 0, 0));

        transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime));
        float delta = Input.GetAxis("Vertical");
        if(delta < 0)
        {
            delta /= 4;
        }
        spd += delta;
        if (spd > maxspd)
        {
            spd = maxspd;
        }
        if(spd < 0)
        {
            spd = 0;
        }
        mvd = new Vector3(0, 0, spd);
        moveDirection = transform.TransformDirection(mvd);
        moveDirection *= speed;

        // Move the controller
        rig.velocity = moveDirection;
    }

    void Update()
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}

        gameController.Update2();
        if (!gameController.paused)
            Play();
    }
}
