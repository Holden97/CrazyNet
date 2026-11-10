using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("传送方向（局部坐标）")]
    public Vector3 direction = Vector3.forward;

    [Header("反向")] public bool reverse = false;
    [Header("传送速度")]
    public float speed = 2f;

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Food"))
            return;

        Rigidbody rb = collision.rigidbody;

        if (rb == null)
            return;

        Vector3 moveDirection =
            transform.TransformDirection(direction.normalized);

        // 直接平移，不施加物理力
        rb.MovePosition(
            rb.position + moveDirection *(reverse?(-1):1)* speed * Time.fixedDeltaTime
        );
    }
}