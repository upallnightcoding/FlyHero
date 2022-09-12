using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public CharacterController cc;
    public float flySpeed = 20.0f;
    public float horzSpeed;
    public float vertSpeed;

    public float yawAmount = 100.0f;

    private float yaw;

    float horizontalInput, verticalInput;
    float oldHorizontalInput, oldVerticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = null;
    }

    private void Update()
    {
        //TranslateUpdate();
        ControllerUpdate();
    }

    // Update is called once per frame
    void ControllerUpdate()
    {
        //transform.position += transform.forward * flySpeed * Time.deltaTime;

        cc.Move(transform.forward * flySpeed * Time.deltaTime);

        float horizontalInput = Input.GetAxisRaw("Horizontal") * horzSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxisRaw("Vertical") * vertSpeed * Time.deltaTime;

        horizontalInput = Mathf.Lerp(oldHorizontalInput, horizontalInput, Time.deltaTime);
        verticalInput = Mathf.Lerp(oldVerticalInput, verticalInput, Time.deltaTime);

        yaw += horizontalInput * yawAmount * Time.deltaTime;
        //yaw = Mathf.Clamp(yaw, -90.0f, 90.0f);

        float pitch = Mathf.Lerp(0, 45, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 45, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        //rb.velocity = new Vector3(0.0f, 0.0f, 75.0f);

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        //transform.rotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);

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
