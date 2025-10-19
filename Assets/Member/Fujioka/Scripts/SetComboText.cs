using TMPro;
using UnityEngine;

public class SetComboText : MonoBehaviour
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
            distancer.SetText("BattonPass: " + infoSave.combo.ToString());
        }
    }
}
