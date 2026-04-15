using UnityEngine;

public class HealthZone : MonoBehaviour
{
     [SerializeField] float Health = 40f;
    private void OnTriggerEnter(Collider other)
    {
        Playerstatts stats = other. GetComponent<Playerstatts>();
        if(stats != null)
        {
            stats. AddHealth(Health);
        }
    }
}
