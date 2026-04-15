using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;


public class Playerstatts : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float CurrentHealth =100f;
     [SerializeField] private float MaxHealth =100f;
     [SerializeField] private HealthBar healthBar; 
     [SerializeField] private DeadUI deadUI;  

     public bool isAlive{ get; private set;} = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth<= 0 && isAlive)
        {
            Die();
        }
        if (healthBar != null)
        {
            healthBar.updateHealthBar(CurrentHealth,MaxHealth);
        }
    }


    void Die()
    {
        isAlive = false;
        GetComponent<ThirdPersonController>().enabled = false;
       DeadUI.Show();
        EnableRagdoll();
    }


    public void TakeDamage(float damage)
    {
        CurrentHealth -=damage;
    }

    public void AddHealth(float health)
    {
        CurrentHealth += health;
    }

    public void DisableRagdoll()
    {
        GetComponent<Animator>().enabled = true;
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rbs in rb)
        {
           if(rbs == GetComponent<Rigidbody>())
            {
                continue;
            } 
            rbs. isKinematic = true;
            rbs. useGravity = false;
        }
    }


    public void EnableRagdoll()
    {
        GetComponent<Animator>().enabled = false;
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rbs in rb)
        {
           if(rbs == GetComponent<Rigidbody>())
            {
                continue;
            } 
            rbs. isKinematic = false;
            rbs. useGravity = true;
        }
    }
}
