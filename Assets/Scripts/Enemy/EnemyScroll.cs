using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScroll : MonoBehaviour
{
    public GameObject Enemy; 
    public List<Transform> positions;
    public float duration = 1f;
    public bool alive = true;


    private int _index = 0;


    public void Start()
    {
        transform.position = positions[0].transform.position;
        NextIndex();
        StartCoroutine(StartMoviment());
    }

    public void NextIndex()
    {
        _index++;
        if (_index >= positions.Count)
        {
            _index = 0;
        }
    }


    IEnumerator StartMoviment()
    {
        float time = 0;

        while (true && alive)
        {
            var currentPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector2.Lerp(currentPosition, positions[_index].transform.position, (time / duration));
                time += Time.deltaTime;
                yield return null;
            }
            _index++;

            if (_index >= positions.Count)
            {
                _index = 0;
            }
            time = 0;

            yield return null;
        }
    }
}
