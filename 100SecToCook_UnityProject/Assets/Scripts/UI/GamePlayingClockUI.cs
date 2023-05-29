using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private Animator animator;

    private void Awake() {
        timerText.enabled = false;
    }

    private void Start() {
        animator = GetComponent<Animator>();

        GameManager.Instance.OnStateChanged += GamePlayingClockUI_OnStateChanged;
        DeliveryManager.Instance.OnRecipeSuccess += GamePlayingClockUI_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += GamePlayingClockUI_OnRecipeFailed;
    }

    private void GamePlayingClockUI_OnRecipeFailed(object sender, System.EventArgs e) {
        animator.SetTrigger(POPUP);
        GameManager.Instance.gamePlayingTimer -= 5f;
    }

    private void GamePlayingClockUI_OnRecipeSuccess(object sender, System.EventArgs e) {
        switch (DeliveryManager.Instance.ingredientsCount) {
            case 2:
                if (ScoreUI.Instance.IsCombo == true) {
                    GameManager.Instance.gamePlayingTimer += 5f * 2;
                } else {
                    GameManager.Instance.gamePlayingTimer += 5f;
                }
                break;
            case 3:
                if (ScoreUI.Instance.IsCombo == true) {
                    GameManager.Instance.gamePlayingTimer += 7f * 2;
                }
                else {
                    GameManager.Instance.gamePlayingTimer += 7f;
                }
                break;
            case 4:
                if (ScoreUI.Instance.IsCombo == true) {
                    GameManager.Instance.gamePlayingTimer += 9f * 2;
                }
                else {
                    GameManager.Instance.gamePlayingTimer += 9f;
                }
                break;
            case 5:
                if (ScoreUI.Instance.IsCombo == true) {
                    GameManager.Instance.gamePlayingTimer += 12f * 2;
                }
                else {
                    GameManager.Instance.gamePlayingTimer += 12f;
                }
                break;
            case 6:
                if (ScoreUI.Instance.IsCombo == true) {
                    GameManager.Instance.gamePlayingTimer += 15f * 2;
                }
                else {
                    GameManager.Instance.gamePlayingTimer += 15f;
                }
                break;
            case 7:
                GameManager.Instance.gamePlayingTimer += 30f;
                break;
        }
    }

    private void GamePlayingClockUI_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            timerText.enabled = true;
        }
    }

    private void Update() {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
        timerText.text = (GameManager.Instance.GetGamePlayingTimer()).ToString("F0");
    }
}
