using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2d;
    public TextMeshProUGUI texto;
    public SOInt SOFlame;
    [Header("Light Interval")]
    public float FlickInterval = .5f;
    public float FlickRange = .2f;

 
    private bool rngTrue = true;
    private void Start()
    {
        texto.text = "Player Torch = 0";
        SOFlame.value = 3;
    }
    void Update()
    {
        texto.text = "Player Torch = " + SOFlame.value.ToString();
        StartCoroutine(RandomLight());
    }

    IEnumerator RandomLight()
    {
        if (rngTrue)
        {
            float rng = Random.Range(SOFlame.value - FlickRange, SOFlame.value + FlickRange);
            light2d.pointLightOuterRadius = rng;
            rngTrue = false;
            yield return new WaitForSeconds(FlickInterval);
            rngTrue = true;
        }
    }
}
