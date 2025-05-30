using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject Player1Winner;
    public GameObject Player2Winner;

    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;

    public Text leftScoreText;
    public Text rightScoreText;

    public BallPhysics ball;

    private void UpdateScoreUI()
    {
        leftScoreText.text = leftPlayerScore.ToString();
        rightScoreText.text = rightPlayerScore.ToString();
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void RightPlayerScores()
    {
        rightPlayerScore++;
        UpdateScoreUI();
        GameOver();
        StartCoroutine(ball.LaunchBallAfterDelay(1.5f));
    }

    public void LeftPlayerScores()
    {
        leftPlayerScore++;
        UpdateScoreUI();
        GameOver();
        StartCoroutine(ball.LaunchBallAfterDelay(1.5f));
    }

    private void GameOver()
    {
        if(leftPlayerScore >= 11 || rightPlayerScore >= 11)
        {
            
            if(leftPlayerScore >= (rightPlayerScore + 2))
            {
                GameOverScreen.SetActive(true);
                Player2Winner.SetActive(false);
                Time.timeScale = 0f;
            }
            else if(rightPlayerScore >= (leftPlayerScore + 2))
            {
                GameOverScreen.SetActive(true);
                Player1Winner.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }
}
