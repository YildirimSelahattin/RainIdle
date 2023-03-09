using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance;
    public int currentCircle;
    public float cropPrice = 20;
    public Vector3 originalPos;
    public Vector3 wantedPos;
    public Vector3 originalScale;
    public int cropIndex;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
    }

    public void CropGrow()
    {
        
        transform.DOLocalMoveY(originalPos.y, (360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed))).SetEase(Ease.Linear);
        transform.DOScale(originalScale, (360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed))).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cutter1")
        {
            Debug.Log("dasdasdasdasdasdasdd");
            transform.DOKill();
            transform.DOLocalMoveY(wantedPos.y, 0.2f).SetEase(Ease.Linear).OnComplete(() => CropGrow());
            transform.DOScale(originalScale/20 , 0.2f);

            GameDataManager.Instance.TotalMoney += (long)(GameDataManager.Instance.cropPrices[cropIndex] * GameDataManager.Instance.incomeMultiplier);
            UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(GameDataManager.Instance.TotalMoney);
            GameDataManager.Instance.ControlButtons();
        }
    }
}
