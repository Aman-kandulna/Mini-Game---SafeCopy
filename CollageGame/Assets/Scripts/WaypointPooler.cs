using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPooler : MonoBehaviour
{
    public Queue<Transform> waypoints; 
    public static WaypointPooler instance;
   
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        waypoints = new Queue<Transform>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform temp = this.transform.GetChild(i);
            waypoints.Enqueue(temp);
            temp.gameObject.SetActive(false);
        }

    }
    public Transform getWaypointFromPool()
    {
        Transform temp = waypoints.Dequeue();
        temp.gameObject.SetActive(true);
        return temp;
    }
    public void returnWaypointToPool(Transform temp)
    {
        waypoints.Enqueue(temp);
        temp.gameObject.SetActive(false);
    }
}
