using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2d;
    public TextMeshProUGUI texto;
    [Header("Light Interval")]
    public float FlickInterval = .5f;
    public float FlickRange = .2f;

    private float lightCount = 0;
    private bool rngTrue = true;
    private void Start()
    {
        texto.text = "Player Torch = " + lightCount.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            lightCount++;
            light2d.pointLightOuterRadius = lightCount;
            texto.text = "Player Torch = " + lightCount.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            lightCount--;
            light2d.pointLightOuterRadius = lightCount;
            texto.text = "Player Torch = " + lightCount.ToString();
        }

        StartCoroutine(RandomLight());
    }

    IEnumerator RandomLight()
    {
        if (rngTrue)
        {
            float rng = Random.Range(lightCount - FlickRange, lightCount + FlickRange);
            light2d.pointLightOuterRadius = rng;
            rngTrue = false;
            yield return new WaitForSeconds(FlickInterval);
            rngTrue = true;
        }
    }
}
