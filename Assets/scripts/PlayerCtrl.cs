using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCtrl : NetworkBehaviour
{
    GameObject Head;
    GameObject Body;

    CarierCtrl carier;

    // Use this for initialization
    void Start()
    {
        gameController.Start2();
        CheckPlayerName();
        Body = GameObject.Find(this.gameObject.name + "/Body");
        Head = GameObject.Find(this.gameObject.name + "/Head");
        carier = GameObject.Find("Carier").GetComponent<CarierCtrl>();
    }

    void CheckPlayerName()
    {
        if (this.gameObject.name == "Player(Clone)")
            this.gameObject.name = "Player" + gameController.GetNewID(this.gameObject);
    }

    float OpenSpaceSpeed = 0.1f;
    float speed = 6.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    float rotSpeed = 900;

    Vector3 moveDirection = Vector3.zero;
    public Vector3 lastmove = Vector3.zero;

    public GameObject inventory = null;

    public void Save(GameObject ob)
    {
        if (inventory == null)
        {
            inventory = ob;
            //inventory.GetComponent<MeshCollider>().enabled = false;
            inventory.GetComponent<BoxCollider>().enabled = false;
            inventory.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Load(Vector3 point)
    {
        if (inventory != null)
        {
            inventory.transform.position = point;
            //inventory.GetComponent<MeshCollider>().enabled = false;
            inventory.GetComponent<BoxCollider>().enabled = true;
            inventory.GetComponent<MeshRenderer>().enabled = true;
            inventory = null;
        }
    }

    void Play()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime, 0);
        float rotationY = Input.GetAxis("Mouse Y") * 10F;
        if (((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) - rotationY) < 50) && (Vector3.Angle(Head.transform.forward, Body.transform.up) > 90)) || ((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) + rotationY) < 70) && (Vector3.Angle(Head.transform.forward, Body.transform.up) <= 90)))
            Head.transform.Rotate(new Vector3(-rotationY, 0, 0));

        CharacterController controller = GetComponent<CharacterController>();
        if (carier.IsInThis(this.transform))
        {
            if (controller.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,
                                        Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
                moveDirection += lastmove;
            }

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            //доработать между этой версией и moveDirection = mv;
            lastmove = Vector3.zero;
            Vector3 mv = new Vector3(Input.GetAxis("Horizontal"), 0,
                                        Input.GetAxis("Vertical"));
            mv = transform.TransformDirection(mv);
            mv *= OpenSpaceSpeed;

            if (Input.GetButton("Jump"))
            {
                mv.y = OpenSpaceSpeed;
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                mv.y -= OpenSpaceSpeed;
            }
            moveDirection += mv;
        }

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void UpdateSpeed(CarierCtrl carier)
    {
        moveDirection -= lastmove;
        lastmove = carier.moveDirection;
        lastmove.y = 0;
        moveDirection += lastmove;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        gameController.Update2();
        if (!gameController.paused)
                Play();
    }
}