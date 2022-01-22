using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject StartPoint;
    [HideInInspector]
    public GameObject player;
    public static bool hasReachedEndPoint = false;

    public UnityEvent playerReachedEndpoint;

    public bool PlayerHasReachedTarget = true;
    public bool isPawnCubeAttached = false;

    private float moveCounter;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public void Start()
    {
        if (playerReachedEndpoint == null)
        {
            playerReachedEndpoint = new UnityEvent();
        }

        ResetPlayerPosition();
        moveCounter = 0;
    }
    public void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    private void Awake()
    {
        StartPoint = GameObject.FindGameObjectWithTag("StartPoint");
        if(StartPoint == null)
        {
            Debug.LogWarning("No StartPoint exists in the scene. Place a StartPoint in the scene");
        }
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null )
        {
            Debug.LogWarning("No player gameobject found in the scene. Place a player gameobject in the scene");
        }
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
     public void OnReachEndPoint()
    {
        hasReachedEndPoint = true;
        playerReachedEndpoint.Invoke();
    }

    private void ResetPlayerPosition()
    {
        if(StartPoint != null && player !=null)
        player.transform.position = StartPoint.transform.position;
    }
   
    public void Quit()
    {
        Application.Quit();
    }
    public void IncreaseMoveCounter()
    {
        ++moveCounter;
       // Debug.Log(moveCounter);
    }
   

}
