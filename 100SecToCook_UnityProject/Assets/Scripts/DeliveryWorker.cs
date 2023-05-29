using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryWorker : BaseCounter
{
    public static DeliveryWorker Instance { get; private set; }

    private const string ORDER_READY = "OrderReady";

    private Animator animator;

    [SerializeField] private DeliveryCounter deliveryCounter;

    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryCounter.Instance.OrderPlaced += DeliveryWorker_OrderPlaced;
    }

    private void DeliveryWorker_OrderPlaced(object sender, System.EventArgs e) {
        animator.SetTrigger(ORDER_READY);
    }

    private void TakeOrder() {
        deliveryCounter.GetKitchenObject().SetKitchenObjectParent(this);
    }

    private void DestroyOrder() {
        if (this.HasKitchenObject()) {
            this.GetKitchenObject().DestroySelf();
        }
    }

    public void DeliverOrder() {
        animator.SetTrigger(ORDER_READY);
    }
}
