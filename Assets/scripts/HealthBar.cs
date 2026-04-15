
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;

    public void updateHealthBar(float current, float max)
    {
        float healthPercentage = ((current*100)/max)/100f;
        fillImage. fillAmount = healthPercentage;
    }
}
