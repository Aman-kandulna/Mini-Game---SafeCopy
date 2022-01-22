using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command 
{
    /*protected Player player;*/
    public  void Execute();
    public void Unexecute();
    
}
public class MoveCommand : Command
{
    Player player;
    Vector2 dir;
    public float distanceMoved;
    public bool movementAttachesAPawnCube = false;
    public MoveCommand(Player _player,Vector2 direction)
    {
        player = _player;
        dir = direction;
    }
    public  void Execute( )
    {
        player.Move(new Vector3(dir.x,0,dir.y),this); // converting input on x,y plane to x,z plane
        
    }
    public  void Unexecute()
    {
        if (movementAttachesAPawnCube)
        { 
            player.Detach();// if cube movement attached the pawn then detach the pawn at time of unexecution first before reversing the movement
        }
        player.UndoMove(new Vector3(dir.x, 0, dir.y)*-1,distanceMoved); // converting input on x,y plane to x,z plane
       // Debug.Log(distanceMoved);
        
    }
}
public class DetachCommand:Command
{
    Player player;
    public Vector3 pawnCubeDirectionWithRespectToPlayerCube;
    public DetachCommand(Player _player)
    {
        player = _player;
    }
    public  void Execute()
    {
        player.Detach(this);
    }
    public  void Unexecute()
    {
        player.Attach(this);
    }
}
