using UnityEngine;

public class HomingBaton : MonoBehaviour
{
    [SerializeField] Transform baton_position;

    void Update()
    {
        transform.position = baton_position.position;
    }
}
