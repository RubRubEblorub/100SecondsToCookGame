using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button replayButton;

    private void Awake() {
        Instance = this;

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
            Hide();
        });

        replayButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
            GameManager.Instance.TogglePause();
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
