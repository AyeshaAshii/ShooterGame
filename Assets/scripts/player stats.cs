using StarterAssets;
using UnityEngine;


public class Playerstatts : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float CurrentHealth =100f;
     [SerializeField] private float MaxHealth =100f;
     [SerializeField] private HealthBar healthBar; 
     [SerializeField] private DeadUI deadUI;  


     [Header("FallDamage Setting")]
     [SerializeField] private float minimumFallHeight = 3f;
     [SerializeField] private float FallDamage = 7f; 

     [Header("Hunger")]
     [SerializeField] public float hunger =0f;
     [SerializeField] private float maxHunger= 100F;
     [SerializeField] private float  hungereDamage = 0.5f;
     [SerializeField] private float hungerRate = 0.5f;


      [Header("Thirst")]
     [SerializeField] public float thirst =0f;
     [SerializeField] private float maxThirst= 100F;
     [SerializeField] private float  thirstDamage = 0.5f;
     [SerializeField] private float thirstRate = 0.8f;    

     public bool isAlive{ get; private set;} = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
        DisableRagdoll();
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
        UpdateHunger();
        UpdateThirst();

        HungerAndThirstUI.instance.UpdateHungerAndThirstUI(hunger/ maxHunger, thirst/maxThirst);
    }
    


    void Die()
    {
        isAlive = false;
        GetComponent<ThirdPersonController>().enabled = false;
       deadUI.Show();
        EnableRagdoll();
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible=true; 
    }


    public void TakeDamage(float damage)
    {
        CurrentHealth -=damage;
    }

    public void AddHealth(float health)
    {
        CurrentHealth += health;
    }

    public void Land(float LastHeight)
    {
        float height = Vector3.Distance(Vector3.up * LastHeight, Vector3.up* transform.position.y);
        if (height< minimumFallHeight || LastHeight < transform.position.y)
        {
            return; 
        }
        TakeDamage(FallDamage*height);
    }

    public void UpdateHunger()
    {
        hunger += hungerRate * Time.deltaTime;
        if (hunger > maxHunger) 
        {
            hunger = maxHunger;
            CurrentHealth -= hungereDamage* Time.deltaTime;
        }

    }

    public void UpdateThirst()
    {
        thirst += thirstRate * Time . deltaTime;
        if(thirst> maxThirst) 
        {
            thirst = maxThirst;
            CurrentHealth -= thirstDamage * Time.deltaTime;  
        }
    }

    public void ReduceHunger(float food)
    {
        hunger -= food;
    }

    public void ReduceThirst(float water)
    {
        thirst -= water;
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
