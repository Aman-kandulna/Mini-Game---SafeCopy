using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LimitList<T>
{
    int maxLimit;
    Queue<T> collection;
    public LimitList(int Limit)
    {
        maxLimit = Limit;
        collection = new Queue<T>();
    }
    public void Enqueue(T item)
    {
        if(collection.Count<maxLimit)
        collection.Enqueue(item);
        else
        {
            Dequeue();
            collection.Enqueue(item);
        }
    }
    public T Dequeue()
    {
        return collection.Dequeue();
    }
    public T Pop()
    {
        Stack<T> tempcollection = new Stack<T>(collection);
        T temp = tempcollection.Pop();
        Stack<T> anothertempCollection = new Stack<T>(tempcollection); // this is a just wrong man correct this.
        collection = new Queue<T>(anothertempCollection);
        return temp;
    }
    public int Count
    {
        get
        {
            return collection.Count;
        }
    }
}
public class CommandInvoker : MonoBehaviour
{
    public LimitList<Command> commandList;
    public InputManager inputmanager;
    public Player player;
    public int UndoLimit;
  
    private void Awake()
    {
        inputmanager = new InputManager();
        commandList = new LimitList<Command>(UndoLimit);

    }
    private void OnEnable()
    {
        inputmanager.Enable();
    }
    private void OnDisable()
    {
        inputmanager.Disable();
    }
    void Start()
    {
       
        inputmanager.PlayerControls.Movement.performed += MoveCube;
        inputmanager.PlayerControls.Detach.performed += DetachCube;
        inputmanager.PlayerControls.Undo.performed += Undo;
    }
    public void MoveCube(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.PlayerHasReachedTarget && !GameManager.hasReachedEndPoint)
        {
            GameManager.Instance.PlayerHasReachedTarget = false;
            
                MoveCommand move = new MoveCommand(player, context.ReadValue<Vector2>());
                move.Execute();
                commandList.Enqueue(move);
                
        }
        
    }
    public void DetachCube(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.PlayerHasReachedTarget && !GameManager.hasReachedEndPoint && GameManager.Instance.isPawnCubeAttached)
        {
            DetachCommand detach = new DetachCommand(player);
            detach.Execute();
            commandList.Enqueue(detach);
        }
    }
    public void Undo(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.PlayerHasReachedTarget && !GameManager.hasReachedEndPoint)
        {
            if (commandList.Count > 0)
                commandList.Pop().Unexecute();
            else
                Debug.Log("Cannot undo any more than this");
        }
    }
    
}
