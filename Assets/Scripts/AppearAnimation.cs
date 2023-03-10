using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1f).OnComplete(() =>
        {
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
