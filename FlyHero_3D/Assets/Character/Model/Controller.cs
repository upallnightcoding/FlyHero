using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterController charCntrl;
    public float flySpeed = 20.0f;
    public float horzSpeed;
    public float vertSpeed;

    public float yawAmount = 90.0f;

    private float yaw;

    private float horizontalInput, verticalInput;
    private float oldHorizontalInput, oldVerticalInput;

    private float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = null;
        charCntrl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //TranslateUpdate();
        ControllerUpdate();
    }

    void ControllerUpdate1()
    {
        float hortAxis = Input.GetAxisRaw("Horizontal");
        float vertAxis = Input.GetAxisRaw("Vertical");

        charCntrl.Move(transform.forward * flySpeed * Time.deltaTime);

        float horizontalInput = hortAxis * horzSpeed * Time.deltaTime;
        float verticalInput = vertAxis * vertSpeed * Time.deltaTime;

        horizontalInput = Mathf.MoveTowards(oldHorizontalInput, horizontalInput, 1 * Time.deltaTime);
        verticalInput = Mathf.MoveTowards(oldVerticalInput, verticalInput, 1 * Time.deltaTime);

        t += 1.0f * Time.deltaTime;

        yaw += horizontalInput * yawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        transform.rotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

        oldHorizontalInput = horizontalInput;
        oldVerticalInput = verticalInput;

        if (t > 1.0f) t = 0.0f;
    }

    void ControllerUpdate()
    {
        float hortAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");

        charCntrl.Move(transform.forward * flySpeed * Time.deltaTime);

        float horizontalInput = hortAxis * horzSpeed * Time.deltaTime;
        float verticalInput = vertAxis * vertSpeed * Time.deltaTime;

        //horizontalInput = Mathf.Lerp(oldHorizontalInput, horizontalInput, Time.deltaTime);
        //verticalInput = Mathf.Lerp(oldVerticalInput, verticalInput, Time.deltaTime);

        yaw += horizontalInput * yawAmount * Time.deltaTime;

        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 30, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        if ((hortAxis == 0.0f) && (vertAxis == 0.0f)) {
            yaw = Mathf.Lerp(yaw, 0.0f, 5.0f * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);


        oldHorizontalInput = horizontalInput;
        oldVerticalInput = verticalInput;
    }

    void TranslateUpdate()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;

        float horizontalInput = Input.GetAxisRaw("Horizontal") * horzSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxisRaw("Vertical") * vertSpeed * Time.deltaTime;

        horizontalInput = Mathf.Lerp(oldHorizontalInput, horizontalInput, Time.deltaTime);
        verticalInput = Mathf.Lerp(oldVerticalInput, verticalInput, Time.deltaTime);

        yaw += horizontalInput * yawAmount * Time.deltaTime;
        //yaw = Mathf.Clamp(yaw, -90.0f, 90.0f);

        float pitch = Mathf.Lerp(0, 45, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 45, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //Debug.Log($"Roll: {roll} / {horizontalInput}");

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        //transform.rotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

        oldHorizontalInput = horizontalInput;
        oldVerticalInput = verticalInput;
    }
}
