using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x2ScoreUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnComboDelivered += x2ScoreUI_OnComboDelivered;

        gameObject.SetActive(false);
    }

    private void Update() {
        if (ScoreUI.Instance.comboTimer < 0f) {
            gameObject.SetActive(false);
        }
    }

    private void x2ScoreUI_OnComboDelivered(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
    }
}
