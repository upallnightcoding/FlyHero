using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlyHero/Game Data")]
public class GameCache : ScriptableObject
{
    [Header("Model Controller")]
    public float laneSwitchSpeed;
    public float flySpeed;
    public float laneSpace;
    public float floatValue;

    public bool laneAnimation;

    public AnimationCurve animationCurve;

    public float[] lanePosition;

    public float[] levelPosition;

    public void Initialize()
    {
        lanePosition = new float[3];
        lanePosition[0] = -laneSpace;
        lanePosition[1] = 0.0f;
        lanePosition[2] = laneSpace;

        levelPosition = new float[3];
        levelPosition[0] = floatValue;
        levelPosition[1] = laneSpace + floatValue;
        levelPosition[2] = (2 * laneSpace) + floatValue;
    }

    public Vector3 GetLaneLevelPos(float zPosition)
    {
        float x = lanePosition[Random.Range(0, 3)];
        float y = levelPosition[Random.Range(0, 3)];
        float z = zPosition;

        return (new Vector3(x, y, z));
    }
}
