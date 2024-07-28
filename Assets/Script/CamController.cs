using UnityEngine;

public class CamController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float fastMovementSpeed = 50f;
    public float freeLookSensitivity = 3f;
    public float zoomSensitivity = 10f;
    public float fastZoomSensitivity = 50f;

    public float minY = 1f;
    public float maxY = 25f;

    private bool looking = false;

    void Update()
    {
        float speed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = fastMovementSpeed;
        }

        Vector3 newPosition = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            newPosition += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newPosition -= transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            newPosition += transform.up * speed * Time.deltaTime;
        }

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;

        if (Input.GetMouseButtonDown(1))
        {
            looking = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            looking = false;
        }

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float scrollSpeed = zoomSensitivity;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                scrollSpeed = fastZoomSensitivity;
            }
            newPosition += transform.forward * scroll * scrollSpeed;
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            transform.position = newPosition;
        }
    }
}
