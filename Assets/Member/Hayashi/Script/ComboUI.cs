using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private Baton m_baton;
    [SerializeField] private TextMeshProUGUI m_combo_conunt_UI_;
    [SerializeField] private TextMeshProUGUI m_max_combo_conunt_UI_;

    void Update()
    {
        m_combo_conunt_UI_.text = "BatonPass: " + m_baton.BattonPassComboCounter.ToString();
        m_max_combo_conunt_UI_.text = "TotalPass: " + m_baton.BattonPassTotalCounter.ToString();
    }
}
