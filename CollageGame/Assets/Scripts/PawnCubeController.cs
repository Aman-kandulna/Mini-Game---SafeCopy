using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnCubeController : MonoBehaviour
{
    public Color DefaultColor = Color.green;
    public Color ChangedColor = Color.cyan;
    public bool isAttachedToPlayerCube = false;
    Renderer rend;
    public void Start()
    {
        rend = GetComponent<Renderer>();
    }
    private void ChangeColor()
    {
        rend.material.color = ChangedColor;
    }
    private void ResetColor()
    {
        rend.material.color = DefaultColor;
    }
    public void AttachCube()
    {
        ModifyCollider();
        isAttachedToPlayerCube = true;
        ChangeColor();

    }
    public void DetachCube()
    {
        ResetCollider();
        isAttachedToPlayerCube = false;
        ResetColor();
    }
    private void ModifyCollider()
    {
        this.GetComponent<BoxCollider>().enabled = false;
    }
    private void ResetCollider()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }
   
}
