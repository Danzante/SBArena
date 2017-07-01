using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActionCtrl : NetworkBehaviour {
    Ray myray;
    RaycastHit help;
    const float dist = 5;

    public GameObject parrow;
    public GameObject pdisk;
    public GameObject psmoke;

    GameObject Head;
    GameObject Body;

    float v = 0;

    // Use this for initialization
    void Start()
    {
        CheckPlayerName();
        Body = GameObject.Find(this.gameObject.name + "/Body");
        Head = GameObject.Find(this.gameObject.name + "/Head");
    }

    void CheckPlayerName()
    {
        if (this.gameObject.name == "Player(Clone)")
            this.gameObject.name = "Player" + gameController.GetNewID(this.gameObject);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!gameController.paused)
        {
            Update2();
        }
    }

    [Command]
    void CmdStartArrow()
    {
        GameObject arrow = GameObject.Instantiate(parrow);
        arrow.transform.position = Head.transform.position + Head.transform.forward * 0.6f;
        arrow.transform.rotation = Head.transform.rotation;
        arrow.transform.Rotate(new Vector3(90, 0, 0));
        //arrow.GetComponent<ArrowCtrl>().Init();

        NetworkServer.Spawn(arrow);
    }

    // Update is called once per frame
    void Update2()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myray = new Ray(Head.transform.position, Head.transform.forward);
            Physics.Raycast(myray, out help, dist);
            if (help.collider != null)
            {
                GameObject coll = help.collider.gameObject;
                if (coll != null)
                {
                    ActivateCtrl act = coll.GetComponent<ActivateCtrl>();
                    if (act != null)
                    {
                        act.Activate();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //CmdStartArrow();
        }
    }
}
