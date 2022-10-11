using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlyHero/Points System", fileName = "PointsSystem")]
public class PointsSystem : ScriptableObject
{
    private const int POINTS_GOLD = 1;
    private const int POINTS_GREEN = 5;
    private const int POINTS_RWB = 30;

    private bool red, white, blue;

    public int totalPoints = 0;

    public bool GetRed() => red;
    public bool GetWhite() => white;
    public bool GetBlue() => blue;

    public void OnDisable()
    {
        CoinGold.OnCoinCollection -= AddOnePoint;
    }

    public void OnEnable()
    {
        CoinGold.OnCoinCollection += AddOnePoint;

        totalPoints = 0;
    }

    private void AddOnePoint(CoinType type)
    {
        switch(type)
        {
            case CoinType.GOLD:
                AddPoints(POINTS_GOLD);
                break;
            case CoinType.GREEN:
                AddPoints(POINTS_GREEN);
                break;
            case CoinType.RED:
            case CoinType.WHITE:
            case CoinType.BLUE:
                CoinRedWhiteBlue(type);
                break;
        }
    }

    private void CoinRedWhiteBlue(CoinType type)
    {
        SetRedWhiteBlue(type);

        if (IsRedWhiteBlue())
        {
            AddPoints(POINTS_RWB);

            ReSetRedWhiteBlue();
        }
    }

    private void AddPoints(int points)
    {
        totalPoints += points;
    }

    private bool IsRedWhiteBlue()
    {
        return (red && white && blue);
    }

    private void SetRedWhiteBlue(CoinType type)
    {
        switch (type)
        {
            case CoinType.RED:
                red = !red;
                break;
            case CoinType.WHITE:
                white = !white;
                break;
            case CoinType.BLUE:
                blue = !blue;
                break;
        }
    }

    private void ReSetRedWhiteBlue()
    {
        red = false;
        white = false;
        blue = false;
    }
}
