using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public bool isLeftZone;
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isLeftZone)
            {
                scoreManager.RightPlayerScores();
            }
            else
            {
                scoreManager.LeftPlayerScores();
            }
        }
    }
}
