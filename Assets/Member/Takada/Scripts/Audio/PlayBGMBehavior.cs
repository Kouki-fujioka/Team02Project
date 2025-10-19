using UnityEngine;

public class PlayBGMBehavior : MonoBehaviour
{
    [SerializeField]
    private string _BGMName = default;
    private bool _IsRequested = false;

    void Start()
    {
        if (_IsRequested) return;

        if (AudioManager.Instance != null && AudioManager.Instance.IsLoaded) 
        {
            AudioManager.Instance.PlayBGM(_BGMName);
            _IsRequested = true;
        }
    }

    private void Update()
    {
        if (_IsRequested) return;

        if (AudioManager.Instance != null && AudioManager.Instance.IsLoaded)
        {
            AudioManager.Instance.PlayBGM(_BGMName);
            _IsRequested = true;
        }
    }
}
