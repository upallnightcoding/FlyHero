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
    public float horizontalSpeed;
    public float verticalSpeed;
}
