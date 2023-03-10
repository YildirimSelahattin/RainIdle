using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( ShakeLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShakeLoop()
    {
        yield return new WaitForSeconds(0.2f);
        transform.parent.DOShakePosition(0.3f,25,2,0).OnComplete(()=>StartCoroutine(ShakeLoop()));
    }
}
