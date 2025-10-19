using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    private const string EFFECT_PATH = "Effects";

    private Dictionary<string, GameObject> _Effects = new Dictionary<string, GameObject>();

    private static EffectManager instance;

    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<EffectManager>();

            if (instance == null)
                Debug.LogWarning($"{typeof(EffectManager).Name} is not found in the scene.");

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as EffectManager;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
        LoadEffectAssets();
    }

    private void LoadEffectAssets()
    {
        foreach (GameObject efxRes in Resources.LoadAll<GameObject>(EFFECT_PATH))
        {
            _Effects[efxRes.name] = efxRes;
        }
    }

    public void PlayEffect(Vector3 pos, string efxname, float delay = 0.0f)
    {
        foreach(var effect in _Effects)
        {
            Debug.Log(effect.Key);
            Debug.Log(effect.Value);
        }

        if (!_Effects.TryGetValue(efxname, out var efxRes))
        {
            Debug.LogWarning($"指定されたSE '{efxname}' は存在しません。");
            return;
        }

        Instantiate(efxRes, pos, Quaternion.identity);
    }
}
