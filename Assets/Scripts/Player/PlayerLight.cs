using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2d;
    public TextMeshProUGUI texto;
    public SOPlayer SOFlame;
    [Header("Light Interval")]
    public float FlickInterval = .5f;
    public float FlickRange = .2f;

 
    private bool rngTrue = true;
    private void Start()
    {
        texto.text = "1";
        SOFlame.flameSize = 4;
    }
    void Update()
    {
        texto.text =  (SOFlame.flameSize-3).ToString();
        StartCoroutine(RandomLight());
    }

    IEnumerator RandomLight()
    {
        if (rngTrue)
        {
            float rng = Random.Range(SOFlame.flameSize - FlickRange, SOFlame.flameSize + FlickRange);
            light2d.pointLightOuterRadius = rng;
            rngTrue = false;
            yield return new WaitForSeconds(FlickInterval);
            rngTrue = true;
        }
    }
}
