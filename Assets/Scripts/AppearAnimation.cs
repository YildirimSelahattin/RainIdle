using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAnimation : MonoBehaviour
{
    public Vector3[] maxScaleArray;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(maxScaleArray[GameManager.Instance.currentCircle], 1f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
