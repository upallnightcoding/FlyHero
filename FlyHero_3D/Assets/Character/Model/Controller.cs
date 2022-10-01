using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const float LANE_SPACE = 10.0f;

    [SerializeField] private CharacterController charCntrl;
    [SerializeField] private float flySpeed;
    [SerializeField] private float laneSwitchSpeed;
    public float horzSpeed;
    public float vertSpeed;

    public float yawAmount = 90.0f;

    private float yaw;

    private float[] lanePosition = { -LANE_SPACE, 0.0f, LANE_SPACE };
    private int lane = 1;

    private float[] levelPosition = { 4.0f, LANE_SPACE + 4.0f, (2 * LANE_SPACE) + 4.0f };
    private int level = 0;

    private InputAxis inputAxis = null;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();

        inputAxis = new InputAxis();
    }

    private void Update()
    {
        ControllerUpdate();
    }

    private void ControllerUpdate()
    {
        ReadInput(inputAxis);
        UpdatePosition(inputAxis);
        UpdateRotation(inputAxis);
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
        charCntrl.Move(flySpeed * Time.deltaTime * transform.forward);

        int hortAxis = inputAxis.HortInput;
        int vertAxis = inputAxis.VertInput;

        lane = UpdateDirection(hortAxis, lane);
        level = UpdateDirection(vertAxis, level);

        Vector3 position =
            transform.forward   * transform.position.z +
            transform.up        * levelPosition[level] +
            transform.right     * lanePosition[lane];

        transform.position = 
            Vector3.Lerp(transform.position, position, laneSwitchSpeed * Time.deltaTime);
    }

    private void UpdateRotation(InputAxis inputAxis)
    {
        int hortAxis = inputAxis.HortInput;
        int vertAxis = inputAxis.VertInput;

        float hortInput = hortAxis * horzSpeed * Time.deltaTime;
        float vertInput = vertAxis * vertSpeed * Time.deltaTime;

        yaw += hortInput * yawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(vertInput)) * Mathf.Sign(vertInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(hortInput)) * -Mathf.Sign(hortInput);

        if ((hortAxis == 0.0f) && (vertAxis == 0.0f))
        {
            yaw = Mathf.Lerp(yaw, 0.0f, 5.0f * Time.deltaTime);
        }

        transform.rotation =
            Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

        //transform.position = position;
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
    public int VertInput { get; private set; }
    public int HortInput { get; private set; }

    public void Set(int vertInput, int hortInput)
    {
        VertInput = vertInput;
        HortInput = hortInput;
    }
}
