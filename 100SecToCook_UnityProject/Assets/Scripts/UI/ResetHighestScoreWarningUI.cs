using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetHighestScoreWarningUI : MonoBehaviour
{
    public static ResetHighestScoreWarningUI Instance { get; private set; }

    [SerializeField] private Button yesButton;
    [SerializeField] private Button nevermindButton;

    private void Awake() {
        Instance = this;

        yesButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt(GameOverUI.HIGHEST_SCORE, 0);
            PlayerPrefs.Save();
            GameOverUI.Instance.SetHighestScore(0);
            Hide();
            OptionsUI.Instance.Show();
        });

        nevermindButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show();
        });
    }

    private void Start() {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
