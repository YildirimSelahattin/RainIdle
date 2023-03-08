using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance;
    public int currentCircle;
    public float cropPrice = 20;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void CropGrow()
    {
        transform.DOLocalMoveY(9f, (360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed))).SetEase(Ease.Linear);
        transform.DOScale(new Vector3(20,20,20), (360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed))).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cutter1")
        {
            Debug.Log("dasdasdasdasdasdasdd");
            transform.DOKill();
            transform.DOLocalMoveY(-3f, 0.2f).SetEase(Ease.Linear).OnComplete(() => CropGrow());
            transform.DOScale(new Vector3(1, 1, 1) , 0.2f);

            GameDataManager.Instance.TotalMoney += (long)cropPrice;
            UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(GameDataManager.Instance.TotalMoney);
            GameDataManager.Instance.ControlButtons();
        }
    }
}
