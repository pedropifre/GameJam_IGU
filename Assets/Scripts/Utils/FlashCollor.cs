using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class FlashCollor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float duration = .3f;

    private Tween _curretnTween;

    public void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Flash();
        }
    }

    public void Flash()
    {
        if(_curretnTween!=null)
        {
            _curretnTween.Kill();
            spriteRenderers.ForEach(i => i.color = Color.white);
        }
        foreach(var s in spriteRenderers)
        {
            _curretnTween = s.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
