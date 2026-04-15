using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] float damage = 40f;
    private void OnTriggerEnter(Collider other)
    {
        Playerstatts stats = other. GetComponent<Playerstatts>();
        if(stats != null)
        {
            stats. TakeDamage(damage);
        }
    }
}
