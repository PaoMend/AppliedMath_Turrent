using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        transform.position += movement * speed * Time.deltaTime;
    }
}
