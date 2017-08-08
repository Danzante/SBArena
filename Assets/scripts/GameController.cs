using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour
{

    static GameObject PM;
    static GameObject[] players;
    static CarierCtrl[] cariers;
    static CabinCtrl[] lifts;
    static bool active;
    public static bool inited = false;
    public static bool paused { get; private set; }
    static int id = 0;
    static int lid = 0;
    static int cid = 0;

    public static int GetNewID(GameObject p)
    {
        players[id] = p;
        id++;
        return id - 1;
    }

    public static int GetNewLiftID(CabinCtrl l)
    {
        lifts[lid] = l;
        lid++;
        return lid - 1;
    }

    public static int GetNewCarierID(CarierCtrl c)
    {
        cariers[cid] = c;
        cid++;
        return cid - 1;
    }

    public static void Pause()
    {
        active = !active;
        PM.SetActive(active);
        paused = !paused;
    }

    // Update is called once per frame
    public static void Update2()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
        if (!paused)
        {
            UpdateMoves();
        }
    }

    public static void UpdateMoves()
    {
        for(int i = 0; i < id; i++)
        {
            if(players[i] == null)
            {
                continue;
            }
            players[i].transform.parent = null;
            for (int j = 0; j < cid; j++)
            {
                if (cariers[j].IsInThis(players[i].transform))
                {
                    players[i].transform.parent = cariers[j].transform;
                }
            }
            for (int j = 0; j < lid; j++)
            {
                if (lifts[j].IsInThis(players[i].transform))
                {
                    players[i].transform.parent = lifts[j].transform;
                }
            }
        }
    }

    void Start()
    {
        PM = GameObject.Find("/PauseMenu");
        PM.SetActive(false);
        active = false;
    }

    // Use this for initialization
    public static void Start2()
    {
        players = new GameObject[20];
        cariers = new CarierCtrl[20];
        lifts = new CabinCtrl[20];
        if (PM == null)
            PM = GameObject.Find("/PauseMenu");
        PM.SetActive(false);
        active = false;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
