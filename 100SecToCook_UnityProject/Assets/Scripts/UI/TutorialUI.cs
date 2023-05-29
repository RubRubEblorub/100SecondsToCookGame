using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI moveUpKeyText;
    [SerializeField] private TextMeshProUGUI moveDownKeyText;
    [SerializeField] private TextMeshProUGUI moveLeftKeyText;
    [SerializeField] private TextMeshProUGUI moveRightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI actionKeyText;
    [SerializeField] private TextMeshProUGUI gamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI gamepadActionButtonText;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        UpdateVisual();

        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsCountdownToStartActive()) {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        moveUpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        actionKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Action);
        gamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadActionButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadAction);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
