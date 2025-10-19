using UnityEngine;

public class LookAtBaton : MonoBehaviour
{
    [SerializeField] private Transform baton;

    void FixedUpdate()
    {

        if (baton)
        {
            transform.LookAt(baton);
        }
    }
}
