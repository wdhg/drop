using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    //public Text title, pressAnyKey;
    public GameObject titleScreen;
    public PlayerController playerController;
    public Text scoreText;
    public Text endScoreText;
    public Text bestScoreText;
    public string endScoreMessage;
    public string bestScoreMessage;

    private void Start() {
        Screen.SetResolution(450, 800, false);
        Camera.main.ResetAspect();
        playerController.OnPlayerSpawn.AddListener(HideTitle);
        playerController.OnPlayerDeath.AddListener(ShowTitle);
        ShowTitle();
    }

    private void ShowTitle() {
        scoreText.enabled = false;
        titleScreen.SetActive(true);
        endScoreText.text = endScoreMessage + playerController.score.ToString();
        bestScoreText.text = bestScoreMessage + playerController.bestScore.ToString();
    }

    private void HideTitle() {
        scoreText.enabled = true;
        titleScreen.SetActive(false);
    }

    private void Update() {
        // Spawning
        if(!playerController.alive && playerController.readyToSpawn && Input.anyKeyDown) {
            playerController.Spawn();
        }
        // Quiting
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        // Score
        if(playerController.alive) {
            scoreText.text = playerController.score.ToString();
        }
    }
}
