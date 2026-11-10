
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 2f, -5f);

    [Header("鼠标控制")]
    public float mouseSensitivity = 3f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    [Header("镜头缩放")]
    public float zoomSpeed = 2f;
    public float minDistance = 2f;
    public float maxDistance = 8f;

    [Header("平滑参数")]
    public float smoothSpeed = 10f;

    private float horizontalAngle;
    private float verticalAngle = 15f;
    private float currentDistance;

    private void Start()
    {
        currentDistance = offset.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // 鼠标控制旋转
        horizontalAngle += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalAngle = Mathf.Clamp(
            verticalAngle,
            minVerticalAngle,
            maxVerticalAngle
        );

        // 滚轮控制距离
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(
            currentDistance,
            minDistance,
            maxDistance
        );

        // 计算摄像机旋转
        Quaternion rotation = Quaternion.Euler(
            verticalAngle,
            horizontalAngle,
            0
        );

        // 计算目标位置
        Vector3 targetPosition = target.position + Vector3.up * offset.y
            - rotation * Vector3.forward * currentDistance;

        // 平滑跟随
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.rotation = rotation;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
