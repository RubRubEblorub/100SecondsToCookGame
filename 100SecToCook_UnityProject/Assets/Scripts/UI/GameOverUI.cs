using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public const string HIGHEST_SCORE = "HighestScore";
    public static GameOverUI Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private Button replayButton;

    private int highestScore;
    private int currentScore;

    private void Awake() {
        highestScore = PlayerPrefs.GetInt(HIGHEST_SCORE, highestScore);
    }

    private void Start() {
        Instance = this;

        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        Hide();

        replayButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();

            currentScore = ScoreUI.Instance.GetScore();
            if (currentScore > highestScore) {
                currentScoreText.text = "NEW highest score: " + currentScore.ToString();
                PlayerPrefs.SetInt(HIGHEST_SCORE, currentScore);
                PlayerPrefs.Save();
            } else {
                currentScoreText.text = "Your highest score: " + highestScore.ToString();
            }
            ScoreUI.Instance.Hide();

            if (currentScore > highestScore) {
                
            }
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public void SetHighestScore(int highestScore) {
        this.highestScore = highestScore;
    }
}
