using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        if (horizontal != 0)
        {
            transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        }
    }
}
