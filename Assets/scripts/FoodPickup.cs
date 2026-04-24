using UnityEngine;

public class FoodPickup : Interactable
{
    [SerializeField] float foodAmount = 20f;

    public override void Interact(GameObject player)
    {

        player.GetComponent<Playerstatts>().ReduceHunger(foodAmount);
        base.Interact(player);

    }

}
