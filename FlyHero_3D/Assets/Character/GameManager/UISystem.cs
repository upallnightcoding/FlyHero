using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private TMP_Text score; 
    [SerializeField] private PointsSystem pointsSystem;
    [SerializeField] private RawImage redStarImage;
    [SerializeField] private RawImage whiteStarImage;
    [SerializeField] private RawImage blueStarImage;

    public void OnEnable() => CoinGold.OnCoinCollection += UpdateScore;

    public void OnDisable() => CoinGold.OnCoinCollection -= UpdateScore;

    public void UpdateScore(CoinType type)
    {
        score.text = pointsSystem.totalPoints.ToString();

        redStarImage.gameObject.SetActive(pointsSystem.GetRed());
        whiteStarImage.gameObject.SetActive(pointsSystem.GetWhite());
        blueStarImage.gameObject.SetActive(pointsSystem.GetBlue());
    }
}
