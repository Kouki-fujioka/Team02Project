using UnityEngine;

public class PlayerBind : MonoBehaviour
{
    private Player m_player;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.m_isControllerCharcter = true;
            m_player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == m_player.transform)
        {
            m_player.m_isControllerCharcter = false;
            m_player = null;
        }
    }
}
