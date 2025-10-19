using UnityEngine;

public class InfoSave : MonoBehaviour
{
    private DistanceCalculator distanceCalc;
    private Timer timer;
    private Baton baton;
    public float distance;
    public float combo;
    public float total;
    public bool finished;

    private static InfoSave instance;

    public static InfoSave Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<InfoSave>();

            if (instance == null)
                Debug.LogWarning($"{typeof(InfoSave).Name} is not found in the scene.");

            return instance;
        }
    }

    void Start()
    {
        distanceCalc = FindAnyObjectByType<DistanceCalculator>();
        timer = FindAnyObjectByType<Timer>();
        baton = FindAnyObjectByType<Baton>();

        if (instance == null)
        {
            instance = this as InfoSave;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        finished = timer.finished;
        distance = distanceCalc.distance;
        combo = baton.BattonPassComboCounter;
        total = baton.BattonPassTotalCounter;
    }
}
