using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Light moon;

    public float timeOfTheDay= 10;
    public float dayDuration = 120f;

    private float sunInitialIntensity;
    private float moonInitialIntensity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       sunInitialIntensity = sun.intensity;
       moonInitialIntensity =moon. intensity;
    
    }

    // Update is called once per frame
    void Update()
    {
        timeOfTheDay+= (24/ dayDuration) * Time.deltaTime;
        if (timeOfTheDay >=24) timeOfTheDay = 0;

        float rotation = (timeOfTheDay/ 24) * 360 -90f;
        sun.transform.rotation = Quaternion.Euler(rotation, 0, 0);
        moon.transform.rotation = Quaternion.Euler(rotation - 180f, 0, 0);

        float dot = Vector3.Dot (sun.transform.forward, Vector3.down);

        sun.intensity = Mathf.Clamp01(dot)* sunInitialIntensity;
        moon.intensity = Mathf .Clamp01(-dot) * moonInitialIntensity;
          
    }
}
