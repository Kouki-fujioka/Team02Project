using UnityEngine;

public class ShowStatPos : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    private float offset;

    private void Awake()
    {
        offset = Vector3.Magnitude(target.transform.position - player.transform.position);
    }

    void Update()
    {
        Vector3 sub = (target.transform.position - player.transform.position).normalized;
        transform.position = player.position + offset * sub;
        transform.LookAt(target);
    }
}
