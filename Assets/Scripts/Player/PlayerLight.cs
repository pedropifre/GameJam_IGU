using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class PlayerLight : MonoBehaviour
{
    public Light2D light2d;
    public TextMeshProUGUI texto;

    private float lightCount = 0;

    private void Start()
    {
        texto.text = "Player Torch = " + lightCount.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            lightCount++;
            light2d.pointLightOuterRadius = lightCount;
            texto.text = "Player Torch = " + lightCount.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lightCount--;
            light2d.pointLightOuterRadius = lightCount;
            texto.text = "Player Torch = " + lightCount.ToString();
        }
    }
}
