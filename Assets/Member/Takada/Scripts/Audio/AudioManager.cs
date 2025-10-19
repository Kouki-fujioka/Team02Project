using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager:MonoBehaviour
{
    private const float BGM_VOLUME_DEFAULT = 0.5f;
    private const float SE_VOLUME_DEFAULT = 1.0f;

    private const string BGM_PATH = "Sounds/BGM";
    private const string SE_PATH = "Sounds/SE";
    public bool IsLoaded => _IsLoaded;
    private bool _IsLoaded = false;

    public const float BGM_FADE_SPEED_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_LOW = 0.3f;

    private float _bgmFadeSpeed = BGM_FADE_SPEED_HIGH;
    private string _nextBGMName;
    private string _nextSEName;

    private bool _isFadingOut = false;

    private AudioSource _bgmSource;
    private List<AudioSource> _seSources = new List<AudioSource>();
    private const int SE_SOURCE_COUNT = 10;

    private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _seClips = new Dictionary<string, AudioClip>();

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<AudioManager>();

            if (instance == null)
                Debug.LogWarning($"{typeof(AudioManager).Name} is not found in the scene.");

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as AudioManager;
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
        InitializeAudioSources();
        LoadAudioClips();
    }

    private void InitializeAudioSources()
    {
        // BGM AudioSource
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.volume = BGM_VOLUME_DEFAULT;

        // SE AudioSources
        for (int i = 0; i < SE_SOURCE_COUNT; i++)
        {
            AudioSource se = gameObject.AddComponent<AudioSource>();
            se.playOnAwake = false;
            se.volume = SE_VOLUME_DEFAULT;
            _seSources.Add(se);
        }

        // AudioListenerがなければ追加
        if (FindAnyObjectByType<AudioListener>() == null)
        {
            gameObject.AddComponent<AudioListener>();
        }
    }

    private void LoadAudioClips()
    {
        foreach (AudioClip bgm in Resources.LoadAll<AudioClip>(BGM_PATH))
        {
            _bgmClips[bgm.name] = bgm;
        }

        foreach (AudioClip se in Resources.LoadAll<AudioClip>(SE_PATH))
        {
            _seClips[se.name] = se;
        }

        _IsLoaded = true;
    }

    private void Update()
    {
        HandleFadeOut();
    }

    private void HandleFadeOut()
    {
        if (!_isFadingOut) return;

        _bgmSource.volume -= Time.deltaTime * _bgmFadeSpeed;

        if (_bgmSource.volume <= 0f)
        {
            _bgmSource.Stop();
            _bgmSource.volume =BGM_VOLUME_DEFAULT;
            _isFadingOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }

    //==============================================================================
    // BGM
    //==============================================================================

    public void PlayBGM(string bgmName, float fadeSpeed = BGM_FADE_SPEED_HIGH)
    {
        if (_isFadingOut)
        {
            Debug.Log("BGMフェードアウト中。再生保留: " + bgmName);
            _nextBGMName = bgmName;
            return;
        }

        if (!_bgmClips.ContainsKey(bgmName))
        {
            Debug.LogWarning($"指定されたBGM '{bgmName}' は存在しません。");
            return;
        }

        if (_bgmSource.isPlaying && _bgmSource.clip.name == bgmName)
        {
            return; // 同じBGMが再生中ならスルー
        }

        if (_bgmSource.isPlaying)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeed);
        }
        else
        {
            _bgmSource.clip = _bgmClips[bgmName];
            _bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void FadeOutBGM(float fadeSpeed = BGM_FADE_SPEED_LOW)
    {
        _bgmFadeSpeed = fadeSpeed;
        _isFadingOut = true;
    }

    //==============================================================================
    // SE
    //==============================================================================

    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!_seClips.ContainsKey(seName))
        {
            Debug.LogWarning($"指定されたSE '{seName}' は存在しません。");
            return;
        }

        _nextSEName = seName;
        Invoke(nameof(DelayedPlaySE), delay);
    }

    private void DelayedPlaySE()
    {
        foreach (var seSource in _seSources)
        {
            if (!seSource.isPlaying)
            {
                seSource.PlayOneShot(_seClips[_nextSEName]);
                return;
            }
        }

        Debug.LogWarning("SEの再生に失敗：使用可能なAudioSourceがありません。");
    }
}
