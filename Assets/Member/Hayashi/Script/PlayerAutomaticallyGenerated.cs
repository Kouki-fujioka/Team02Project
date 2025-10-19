using UnityEngine;

public class PlayerAutomaticallyGenerated : MonoBehaviour
{

    [SerializeField] int m_minAreaX;
    [SerializeField] int m_maxAreaX;
    [SerializeField] int m_minAreaY;
    [SerializeField] int m_maxAreaY;

    [SerializeField] int m_playerIndex = 10;
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_playerRed;
    [SerializeField] GameObject m_playerBlue;
    [SerializeField] GameObject m_playerGreen;
    [SerializeField] GameObject m_playerYellow;

    void Start()
    {
        for (int i = 0; i < m_playerIndex; i++)
        {
            int x = Random.Range(m_minAreaX, m_maxAreaX);
            int y = Random.Range(m_minAreaY, m_maxAreaY);
            int r = Random.Range(0, 360);
                  
            if(i % 10 == 1) Instantiate(m_playerRed, new Vector3(x, 0.69f, y), Quaternion.Euler(0,r,0), transform);
            else if (i % 10 == 2) Instantiate(m_playerBlue, new Vector3(x, 0.69f, y), Quaternion.Euler(0,r,0), transform);
            else if (i % 10 == 3) Instantiate(m_playerGreen, new Vector3(x, 0.69f, y), Quaternion.Euler(0, r, 0), transform);
            else if (i % 10 == 4) Instantiate(m_playerYellow, new Vector3(x, 0.69f, y), Quaternion.Euler(0, r, 0), transform);
            else Instantiate(m_player, new Vector3(x, 0.69f, y), Quaternion.Euler(0, r, 0), transform);
        }
    }
}
