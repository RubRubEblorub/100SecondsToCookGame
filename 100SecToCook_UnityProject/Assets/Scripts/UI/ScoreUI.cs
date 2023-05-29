using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private const string SCORE_GAINED = "ScoreGained";

    public static ScoreUI Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreGainedText;

    private Animator animator;
    
    private int score = 0;
    private int currentScore = 0;
    public float comboTimer = 0f;
    public bool IsCombo = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Instance = this;

        DeliveryManager.Instance.OnRecipeSuccess += ScoreUI_OnRecipeSuccess;
        DeliveryManager.Instance.OnComboDelivered += ScoreUI_OnComboDelivered;
    }

    private void ScoreUI_OnComboDelivered(object sender, System.EventArgs e) {
        IsCombo = true;
        comboTimer = 10f;
    }

    private void Update() {
        comboTimer -= Time.deltaTime;
        if (comboTimer < 0f) {
            IsCombo = false;
        }
    }

    private void ScoreUI_OnRecipeSuccess(object sender, System.EventArgs e) {
        switch (DeliveryManager.Instance.ingredientsCount) {
            case 2:
                if (IsCombo == true) {
                    currentScore = 50 * 2;
                    score += 50 * 2;
                } else {
                    currentScore = 50;
                    score += 50;
                }
                UpdateVisual();
                break;
            case 3:
                if (IsCombo == true) {
                    currentScore = 100 * 2;
                    score += 100 * 2;
                }
                else {
                    currentScore = 100;
                    score += 100;
                }
                UpdateVisual();
                break;
            case 4:
                if (IsCombo == true) {
                    currentScore = 150 * 2;
                    score += 150 * 2;
                }
                else {
                    currentScore = 150;
                    score += 150;
                }
                UpdateVisual();
                break;
            case 5:
                if (IsCombo == true) {
                    currentScore = 250 * 2;
                    score += 250 * 2;
                }
                else {
                    currentScore = 250;
                    score += 250;
                }
                UpdateVisual();
                break;
            case 6:
                if (IsCombo == true) {
                    currentScore = 400 * 2;
                    score += 400 * 2;
                }
                else {
                    currentScore = 400;
                    score += 400;
                }
                UpdateVisual();
                break;
            case 7:
                currentScore = 1000;
                score += 1000;
                UpdateVisual();
                break;
        }
        scoreGainedText.text = "+" + currentScore;
        animator.SetTrigger(SCORE_GAINED);
    }

    private void UpdateVisual() {
        scoreText.text = "Score: " + score;
    }

    public int GetScore() {
        return score;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
