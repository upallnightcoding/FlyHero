using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameCache gameCache;
    [SerializeField] private GameObject leftGun;
    [SerializeField] private GameObject rightGun;
    [SerializeField] private GameObject bullet;

    private float flySpeed;
    private float laneSwitchSpeed;
    private bool laneAnimation;
    private AnimationCurve animationCurve;
    private CharacterController charCntrl;

    //private float yawAmount = 90.0f;

    //private float yaw;

    //private float[] lanePosition;
    private int lane = 1;

    //private float[] levelPosition;
    private int level = 0;

    private InputAxis inputAxis;

    // Start is called before the first frame update
    void Start()
    {
        gameCache.Initialize();

        charCntrl = GetComponent<CharacterController>();

        inputAxis = new InputAxis();

        laneSwitchSpeed = gameCache.laneSwitchSpeed;
        flySpeed = gameCache.flySpeed;
        laneAnimation = gameCache.laneAnimation;
        animationCurve = gameCache.animationCurve;
    }

    private void Update()
    {
        ReadInput(inputAxis);
        Vector3 newPosition = UpdatePosition(inputAxis);
        UpdateLaneAnimation(newPosition);
        FirerGuns(inputAxis);
    }

    private void FirerGuns(InputAxis inputAxis)
    {
        if (inputAxis.Firer)
        {
            GameObject leftBullet = Instantiate(bullet, leftGun.transform.position, leftGun.transform.rotation);
            GameObject rightBullet = Instantiate(bullet, rightGun.transform.position, rightGun.transform.rotation);
         }
    }

    private Vector3 UpdatePosition(InputAxis inputAxis)
    {
        charCntrl.Move(flySpeed * Time.deltaTime * Vector3.forward);

        int hortAxis = 0, vertAxis = 0;

        inputAxis.Get(ref vertAxis, ref hortAxis);

        lane = UpdateDirection(hortAxis, lane);
        level = UpdateDirection(vertAxis, level);

        Vector3 position =
            Vector3.forward   * transform.position.z +
            Vector3.up        * gameCache.levelPosition[level] +
            Vector3.right     * gameCache.lanePosition[lane];            

        transform.position = 
            Vector3.Lerp(transform.position, position, laneSwitchSpeed * Time.deltaTime);

        return (position);
    }

    private void UpdateLaneAnimation(Vector3 position)
    {
        if (laneAnimation)
        {
            float delta = (position.x - transform.position.x);

            float distanceRatio = delta / 10.0f;
            float heightRatio = (position.y - transform.position.y) / 10.0f;

            float pitch = animationCurve.Evaluate(Mathf.Abs(heightRatio)) * 50.0f * -Mathf.Sign(heightRatio);
            float roll = animationCurve.Evaluate(Mathf.Abs(distanceRatio)) * 50.0f * -Mathf.Sign(distanceRatio);
            float yaw = animationCurve.Evaluate(Mathf.Abs(distanceRatio)) * 50.0f * Mathf.Sign(distanceRatio);

            pitch = 0.0f;

            transform.rotation =
                Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        }
    }

    private void ReadInput(InputAxis inputAxis)
    {
        int hortAxis = 0, vertAxis = 0;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) hortAxis = 1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) hortAxis = -1;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) vertAxis = -1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) vertAxis = 1;

        inputAxis.Set(vertAxis, hortAxis);

        inputAxis.Firer = Input.GetKeyDown(KeyCode.Space);
    }

    private int UpdateDirection(int direction, int slot)
    {
        switch (direction)
        {
            case -1:
                slot = (slot == 0) ? 0 : --slot;
                break;
            case 1:
                slot = (slot == 2) ? 2 : ++slot;
                break;
        }

        return (slot);
    }
}

public class InputAxis
{
    public bool Firer { get; set; }

    public int vertInput;
    public int hortInput;

    public void Set(int vertInput, int hortInput)
    {
        this.vertInput = vertInput;
        this.hortInput = hortInput;
    }

    public void Get(ref int vertInput, ref int hortInput)
    {
        vertInput = this.vertInput;
        hortInput = this.hortInput;
    }
}
