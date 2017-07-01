using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour
{

    static GameObject PM;
    static GameObject[] players;
    static bool active;
    public static bool inited = false;
    public static bool paused { get; private set; }
    static int id = 0;

    public static int GetNewID(GameObject p)
    {
        players[id] = p;
        id++;
        return id - 1;
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
    }

    public static void UpdateMoves(CarierCtrl carier)
    {
        for(int i = 0; i < id; i++)
        {
            if(players[i] == null)
            {
                continue;
            }
            if (carier.IsInThis(players[i].transform))
            {
                players[i].GetComponent<PlayerCtrl>().UpdateSpeed(carier);
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
