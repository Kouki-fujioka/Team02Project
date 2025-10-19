using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField, Tooltip("êßå¿éûä‘")] private float limitTime;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private string result;
    private float leftTimeFloat;
    private int leftTimeInt;
    public bool finished;

    void Awake()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
        leftTimeFloat = limitTime;
        leftTimeInt = Mathf.FloorToInt(leftTimeFloat);
        timer.SetText("Time: " + leftTimeInt.ToString());
    }

    void Update()
    {
        leftTimeFloat -= Time.deltaTime;
        leftTimeInt = Mathf.FloorToInt(leftTimeFloat);

        timer.SetText("Time: " + leftTimeInt.ToString());

        if (leftTimeFloat <= 0)
        {
            finished = true;
#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.None;
#endif
            SceneManager.LoadScene(result);
        }
    }
}
