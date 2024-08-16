using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IAPManager = this;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("1104", ProductType.Consumable);         // 다이아 200개
        builder.AddProduct("1105", ProductType.Consumable);         // 다이아 1,200개
        builder.AddProduct("1106", ProductType.Consumable);         // 다이아 2,000개
        builder.AddProduct("1112", ProductType.Consumable);         // 골든패스
        builder.AddProduct("1113", ProductType.NonConsumable);      // 켠왕 패키지 (모집권 300개, 골드 500개)
        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("IAP Initialization Failed: " + error + ", " + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogError("Purchase Failed: " + reason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        switch (e.purchasedProduct.definition.id)
        {
            case "1104":
                GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, 200);
                break;
            case "1105":
                GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, 1200);
                break;
            case "1106":
                GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, 2000);
                break;
            case "1112":
                GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, 600);
                GameManager.Instance.MoneyChange(Constants.MoneyType.Diamond, 200);
                GameManager.Instance.UnitManager.ChangeUnitPiece(30);
                break;
            case "1113":
                GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, 500);
                GameManager.Instance.UnitManager.ChangeUnitPiece(300);
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP Initialization Failed: " + error);
    }

    public void BuyProduct(string productId)
    {
        if (controller != null)
        {
            controller.InitiatePurchase(productId);
        }
        else
        {
            Debug.LogError("Controller is not initialized.");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }
}
