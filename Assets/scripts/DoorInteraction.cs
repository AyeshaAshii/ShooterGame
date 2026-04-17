
using Unity.VisualScripting;

using UnityEngine;

public class DoorInteraction : Interactable
{
    [Header("Door Setting")]
    [SerializeField] private Transform Door;
    [SerializeField] private float DoorSpeed = 5f;
    
    [Header("Open Vector")]
    [SerializeField] Vector3 OpenPosition = new Vector3(0,0,0);
    [SerializeField] Quaternion OpenRotation = Quaternion.identity;

    [Header("Close Vector")]
    [ SerializeField] Vector3 ClosePosition = new Vector3(0,0,0);
    [SerializeField] Quaternion CloseRotation =  Quaternion.identity;

    public bool Opened {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Door.localPosition = ClosePosition;
        Door.localRotation = CloseRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Opened)
        {
            Door. localPosition = Vector3.Lerp(Door.localPosition,OpenPosition,DoorSpeed*Time.deltaTime);
            Door.localRotation = Quaternion.Lerp(Door.localRotation,OpenRotation,DoorSpeed*Time.deltaTime);
        }
        else
        {
             Door. localPosition = Vector3.Lerp(Door.localPosition,ClosePosition,DoorSpeed*Time.deltaTime);
            Door.localRotation = Quaternion.Lerp(Door.localRotation,CloseRotation,DoorSpeed*Time.deltaTime);
        }

    }
    public override void Interact(GameObject player)
    {
        Opened=!Opened;
        base.Interact(player);
    }
}
