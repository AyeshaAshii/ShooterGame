using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Update()
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
   
}