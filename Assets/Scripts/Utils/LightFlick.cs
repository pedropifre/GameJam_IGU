using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlick : MonoBehaviour
{

    public UnityEngine.Rendering.Universal.Light2D light2d;
    [Header("Light Interval")]
    public float FlickInterval = .5f;
    public float FlickRange = .2f;
    private bool rngTrue = true;
    public float intensity;


    void Update()
    {
        
        StartCoroutine(RandomLight());
    }
    IEnumerator RandomLight()
    {
        if (rngTrue)
        {
            rngTrue = false;
            float rng = Random.Range(intensity - FlickRange, intensity + FlickRange);
            light2d.intensity = rng;
            yield return new WaitForSeconds(FlickInterval);
            rngTrue = true;
        }
    }
}
