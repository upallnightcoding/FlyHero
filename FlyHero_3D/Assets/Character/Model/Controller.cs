using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameCache gameCache;
    [SerializeField] private CharacterController charCntrl;
    [SerializeField] private AnimationCurve curve;

    private float flySpeed;
    private float laneSwitchSpeed;
    private float horizontalSpeed;
    private float verticalSpeed;

    private float yawAmount = 90.0f;

    private float yaw;

    private float[] lanePosition;
    private int lane = 1;

    private float[] levelPosition;
    private int level = 0;

    private InputAxis inputAxis;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();

        inputAxis = new InputAxis();

        laneSwitchSpeed = gameCache.laneSwitchSpeed;
        flySpeed = gameCache.flySpeed;
        horizontalSpeed = gameCache.horizontalSpeed;
        verticalSpeed = gameCache.verticalSpeed;

        lanePosition = new float[3];
        lanePosition[0] = -gameCache.laneSpace;
        lanePosition[1] = 0.0f;
        lanePosition[2] = gameCache.laneSpace;

        levelPosition = new float[3];
        levelPosition[0] = 4.0f;
        levelPosition[1] = gameCache.laneSpace + 4.0f;
        levelPosition[2] = (2 * gameCache.laneSpace) + 4.0f;
    }

    private void Update()
    {
        ControllerUpdate();
    }

    private void ControllerUpdate()
    {
        ReadInput(inputAxis);
        UpdatePosition(inputAxis);
        //UpdateRotation(inputAxis);
    }

    void ReadInput(InputAxis inputAxis)
    {
        int hortAxis = 0, vertAxis = 0;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) hortAxis = 1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) hortAxis = -1;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) vertAxis = -1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) vertAxis = 1;

        inputAxis.Set(vertAxis, hortAxis);
    }

    void UpdatePosition(InputAxis inputAxis)
    {
        charCntrl.Move(flySpeed * Time.deltaTime * Vector3.forward);

        int hortAxis = 0, vertAxis = 0;

        inputAxis.Get(ref vertAxis, ref hortAxis);

        lane = UpdateDirection(hortAxis, lane);
        level = UpdateDirection(vertAxis, level);

        Vector3 position =
            Vector3.forward   * transform.position.z +
            Vector3.up        * levelPosition[level] +
            Vector3.right     * lanePosition[lane];

        transform.position = 
            Vector3.Lerp(transform.position, position, laneSwitchSpeed * Time.deltaTime);

        float distanceRatio = (position.x - transform.position.x) / 10.0f;

        float pitch = 0.0f;
        float roll = 0.0f;
        float yaw = curve.Evaluate(distanceRatio) * 30.0f * 1.0f;

        Debug.Log($"Yaw: {yaw} / Pitch: {pitch} / Roll: {roll}");

        transform.rotation =
            Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
    }

    private void UpdateRotation(InputAxis inputAxis)
    {
        int hortAxis = 0, vertAxis = 0;

        inputAxis.Get(ref vertAxis, ref hortAxis);

        float hortInput = hortAxis * horizontalSpeed * Time.deltaTime;
        float vertInput = vertAxis * verticalSpeed * Time.deltaTime;

        //yaw += hortInput * yawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(vertInput)) * Mathf.Sign(vertInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(hortInput)) * -Mathf.Sign(hortInput);
        float yaw = Mathf.Lerp(0, 30, Mathf.Abs(hortInput)) * -Mathf.Sign(hortInput);

        if ((hortAxis == 0.0f) && (vertAxis == 0.0f))
        {
            //yaw = Mathf.Lerp(yaw, 0.0f, 5.0f * Time.deltaTime);
        }

        Debug.Log($"Yaw: {yaw} / Pitch: {pitch} / Roll: {roll}");

        transform.rotation =
            Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
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

    /*
    void ControllerUpdate1()
    {
        charCntrl.Move(flySpeed * Time.deltaTime * transform.forward);

        float hortAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");

        float hortInput = hortAxis * horzSpeed * Time.deltaTime;
        float vertInput = vertAxis * vertSpeed * Time.deltaTime;

        yaw += hortInput * yawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(vertInput)) * Mathf.Sign(vertInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(hortInput)) * -Mathf.Sign(hortInput);

        if ((hortAxis == 0.0f) && (vertAxis == 0.0f)) {
            yaw = Mathf.Lerp(yaw, 0.0f, 5.0f * Time.deltaTime);
        }

        transform.rotation = 
            Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
    }
    */
}

public class InputAxis
{
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
