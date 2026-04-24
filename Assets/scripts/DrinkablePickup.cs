using UnityEngine;

public class DrinkablePickup : Interactable
{
   
    [SerializeField] float DrinkAmount = 20f;

    public override void Interact(GameObject player)
    {

        player.GetComponent<Playerstatts>().ReduceThirst(DrinkAmount);
        base.Interact(player);

    }

}
