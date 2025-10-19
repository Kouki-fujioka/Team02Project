using UnityEngine;

public class Player : MonoBehaviour
{

    public bool m_isControllerCharcter = true;
    [SerializeField] private float m_rotationtTime = 60;
    [SerializeField] private float m_speed = 1.0f;
    public float _moveSpeed;
    public float _maxDistance;

    public void Update()
    {
        if (!m_isControllerCharcter) return;

        Vector2 vector = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) vector.x--;
        if (Input.GetKey(KeyCode.D)) vector.x++;
        if (Input.GetKey(KeyCode.W)) vector.y++;
        if (Input.GetKey(KeyCode.S)) vector.y--;

        Vector3 velocity = Camera.main.transform.right * vector.x + Camera.main.transform.forward * vector.y;
        velocity.y = 0.0f;
        transform.Translate(velocity.normalized * m_speed  * Time.deltaTime,Space.World);

        if (velocity.sqrMagnitude > 0.0f)
        {
            //Quaternion rotation = Quaternion.LookRotation(velocity);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity), m_rotationtTime * Time.deltaTime);
            transform.rotation = rotation;
        }

    }
}
