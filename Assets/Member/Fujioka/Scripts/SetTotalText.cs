using TMPro;
using UnityEngine;

public class SetTotalText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distancer;
    InfoSave infoSave;
    void Start()
    {
        infoSave = FindAnyObjectByType<InfoSave>();
    }

    void Update()
    {
        bool finished = infoSave.finished;
        if (finished)
        {
            distancer.SetText("TotalPass: " + infoSave.total.ToString());
        }
    }
}
