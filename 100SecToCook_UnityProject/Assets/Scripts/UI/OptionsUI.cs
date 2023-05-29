using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    private const string IS_IMAGE_VISIBLE = "IsImageVisible";

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadActionButton;
    [SerializeField] private Button hideTutorialButton;
    [SerializeField] private Button resetHighestScoreButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadActionText;
    [SerializeField] private Image hideTutorialImage;

    public bool isImageVisible = false;

    private void Awake() {
        Instance = this;

        isImageVisible = PlayerPrefs.GetInt(IS_IMAGE_VISIBLE, 0) == 1;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            Hide();
            GamePauseUI.Instance.Show();
        });

        moveUpButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(moveUpButton, moveUpText);
            RebindingBinding(GameInput.Binding.MoveUp);
        });

        moveDownButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(moveDownButton, moveDownText);
            RebindingBinding(GameInput.Binding.MoveDown);
        });

        moveLeftButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(moveLeftButton, moveLeftText);
            RebindingBinding(GameInput.Binding.MoveLeft);
        });

        moveRightButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(moveRightButton, moveRightText);
            RebindingBinding(GameInput.Binding.MoveRight);
        });

        interactButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(interactButton, interactText);
            RebindingBinding(GameInput.Binding.Interact);
        });

        actionButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(actionButton, actionText);
            RebindingBinding(GameInput.Binding.Action);
        });

        gamepadInteractButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(gamepadInteractButton, gamepadInteractText);
            RebindingBinding(GameInput.Binding.GamepadInteract);
        });

        actionButton.onClick.AddListener(() => {
            PressAnyKeyButtonTransform(gamepadActionButton, gamepadActionText);
            RebindingBinding(GameInput.Binding.GamepadAction);
        });

        hideTutorialButton.onClick.AddListener(() => {
            if (isImageVisible == false) {
                hideTutorialImage.enabled = !isImageVisible;
                isImageVisible = true;
            } else {
                hideTutorialImage.enabled = !isImageVisible;
                isImageVisible = false;
            }

            PlayerPrefs.SetInt(IS_IMAGE_VISIBLE, isImageVisible ? 1 : 0);
            PlayerPrefs.Save();
        });

        resetHighestScoreButton.onClick.AddListener(() => {
            ResetHighestScoreWarningUI.Instance.Show();
            Hide();
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();

        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        if (isImageVisible == true) {
            TutorialUI.Instance.Hide();
        }

        soundEffectsText.text = "Sound Effects - " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music - " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        ResetButtonSize(moveUpButton);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        ResetButtonSize(moveDownButton);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        ResetButtonSize(moveLeftButton);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        ResetButtonSize(moveRightButton);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        ResetButtonSize(interactButton);
        actionText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Action);
        ResetButtonSize(actionButton);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        ResetButtonSize(gamepadInteractButton);
        gamepadActionText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadAction);
        ResetButtonSize(gamepadActionButton);

        hideTutorialImage.enabled = isImageVisible;
    }

    public void Show() {
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void RebindingBinding(GameInput.Binding binding) {       
        GameInput.Instance.RebindBindings(binding, () => {
            UpdateVisual();
        });
    }

    private void PressAnyKeyButtonTransform(Button button,TextMeshProUGUI buttonText) {
        buttonText.text = "Press Any Key";
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        float pressAnyKeyWidth = 300f;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pressAnyKeyWidth);
    }

    private void ResetButtonSize(Button button) {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        float originalWidth = 50f;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalWidth);
    }
}
