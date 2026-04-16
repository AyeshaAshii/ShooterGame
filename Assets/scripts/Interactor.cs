using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private GameObject InteractText;
    [SerializeField] private KeyCode interactkey = KeyCode.E;
    private Playerstatts stats;
    private Interactable interactable; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats= GetComponent<Playerstatts>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractText.SetActive(interactable != null);
        if(Input.GetKeyDown(interactkey)&& stats.isAlive)
        {
            Interact();
        }

    void Interact()
        {
            if (interactable != null)
            {
                interactable. Interact(gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable newinteractable = other. GetComponent<Interactable>();
        if (interactable == null)
        {
            interactable = newinteractable;
        }
    }

     private void OnTriggerExit(Collider other)
    {
        Interactable newinteractable = other. GetComponent<Interactable>();
        if (interactable == newinteractable)
        {
            interactable = newinteractable;
        }
    }
}
