using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {  get; private set; }

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private DeliveryWorker deliveryWorker;
    public event EventHandler OrderPlaced;

    private int successfulRecipesAmount = 0;

    private void Awake() {
        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Only access plates
                if (!this.HasKitchenObject()) {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    if (successfulRecipesAmount != DeliveryManager.Instance.GetSuccesfulRecipesAmount()) {
                        OrderPlaced?.Invoke(this, EventArgs.Empty);
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        successfulRecipesAmount++;
                    }
                    else {
                        player.GetKitchenObject().DestroySelf();
                    }
                } else {
                    DeliveryWorker.Instance.DeliverOrder();
                }
            }
        }
    }
}
