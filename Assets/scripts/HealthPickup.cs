using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] private float Health= 20f;
    public override void Interact(GameObject player)
    {
        player.gameObject.GetComponent <Playerstatts>().AddHealth(Health);
        base.Interact(player);
    }
    
}
