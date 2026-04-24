using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool destroyObject = false;
    public bool hasDynamicText = false;
    public string dynamicText = string.Empty;

    public virtual void Interact(GameObject player)
    {
        if (destroyObject)
        {
            Destroy(gameObject);
        }
    }
}
