using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance;
    public float growTime = 10f;
    public int currentCircle;
    public float cropPrice = 20;

    void Start()
    {
        CropGrow();
    }

    public void CropGrow()
    {
        transform.DOLocalMove(new Vector3(0, 2.5f, 0), (360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed)));

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Cutter1")
        {
            transform.DOKill();
            transform.DOLocalMoveY(1f, 0.2f).OnComplete(() => CropGrow());

            GameDataManager.Instance.TotalMoney += cropPrice;
            UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
        }
    }
}
