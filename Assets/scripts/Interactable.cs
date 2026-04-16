using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool destroyObject = false;

    public virtual void Interact(GameObject player)
    {
        if (destroyObject)
        {
            Destroy(gameObject);
        }
    }
}
