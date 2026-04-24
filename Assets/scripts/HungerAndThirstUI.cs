using UnityEngine;
using UnityEngine.UI;

public class HungerAndThirstUI : MonoBehaviour
{
    public static HungerAndThirstUI instance;
    public Image hungerFill;
    public Image thirstFill;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance =  this;
    }

    // Update is called once per frame
    public void UpdateHungerAndThirstUI(float hunger, float thirst)
    {
        hungerFill.fillAmount = hunger;
        thirstFill . fillAmount = thirst;
    }
}
