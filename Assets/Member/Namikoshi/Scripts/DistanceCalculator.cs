using TMPro;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public GameObject baton;

    public TextMeshProUGUI distanceText;
    public float distance = 0;

    void Start()
    {
        distanceText.SetText("Distance: " + distance.ToString() + "m");
    }

    void Update()
    {
        float distanceX = this.transform.position.x - baton.transform.position.x;
        float distanceZ = this.transform.position.z - baton.transform.position.z;

        distance = (float)Mathf.Sqrt(distanceX * distanceX + distanceZ * distanceZ);

        distance = Mathf.Abs(distance);

        distance = Mathf.Floor(distance * 10) / 10;

        Debug.Log(distance);

        distanceText.SetText("Distance: " + distance.ToString() + "m");
    }
}
