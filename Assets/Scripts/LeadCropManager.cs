using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LeadCropManager : MonoBehaviour
{
    public static LeadCropManager Instance;
    public int currentCircle;
    public float cropPrice = 20;
    public Vector3 originalPos;
    public Vector3 wantedPos;
    public Vector3 originalScale;
    public int cropIndex;
    public GameObject floatingParent;
    private float x = 0.007f;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
    }

    public void CropGrow()
    {

        transform.DOLocalMoveY(originalPos.y, (0.8f * 360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed * RotateCircle.rainMultiplier * RotateCircle.tapSpeedMultiplier))).SetEase(Ease.Linear);
        transform.DOScale(originalScale, (0.8f * 360 / Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed * RotateCircle.rainMultiplier * RotateCircle.tapSpeedMultiplier))).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cutter1")
        {
            Debug.Log("dasdasdasdasdasdasdd");
            transform.DOKill();
            transform.DOLocalMoveY(wantedPos.y, 0.2f).SetEase(Ease.Linear).OnComplete(() => CropGrow());
            transform.DOScale(originalScale / 20, 0.2f);

            if (GameDataManager.Instance.playSound == 1 )
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.cropSounds[cropIndex]);
                Destroy(sound, GameDataManager.Instance.cropSounds[cropIndex].length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            GameDataManager.Instance.TotalMoney += (long)(GameDataManager.Instance.cropPrices[cropIndex] * GameDataManager.Instance.cropAmount[cropIndex]*GameDataManager.Instance.incomeMultiplier);
            GameObject prices = Instantiate(floatingParent, transform.position, Quaternion.Euler(60f, 0, 0)) as GameObject;
            prices.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + FormatNumbers.AbbreviateNumber((long)(GameDataManager.Instance.cropPrices[cropIndex] * GameDataManager.Instance.cropAmount[cropIndex] * GameDataManager.Instance.incomeMultiplier)) + "$";
            // Instantiate Floating Number
            UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(GameDataManager.Instance.TotalMoney);
            GameDataManager.Instance.ControlButtons();
        }
    }
}
