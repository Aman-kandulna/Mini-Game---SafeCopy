using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPointScript : MonoBehaviour
{

    public static EndPointScript instance;
    public GameObject winscreen;
    
    public void Awake()
    {
        instance = this;
        if(winscreen == null)
        {
            Debug.Log("No winscreen detected");
        }
    }

   
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.Instance.PlayerHasReachedTarget == true)
        {
  
            if (Vector3.Distance(transform.position, other.transform.position) < 0.5f)
            {
                TriggerPlayerReachedEndpointEvent();
                if(winscreen!=null)
                winscreen.SetActive(true);
            }
            
        }
    }
    public void TriggerPlayerReachedEndpointEvent()
    {
        GameManager.Instance.OnReachEndPoint();
    }
    
   


}
