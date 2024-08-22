using System.Transactions;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    [SerializeField] private GameObject failMessage;
    private IStoreController controller;
    private IExtensionProvider extensions;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IAPManager = this;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("1104", ProductType.Consumable);         // ¥Ÿ¿Ãæ∆ 200∞≥
        builder.AddProduct("1105", ProductType.Consumable);         // ¥Ÿ¿Ãæ∆ 1,200∞≥
        builder.AddProduct("1106", ProductType.Consumable);         // ¥Ÿ¿Ãæ∆ 2,000∞≥
        builder.AddProduct("1112", ProductType.NonConsumable);      // ∞ÒµÁ∆–Ω∫
        builder.AddProduct("1113", ProductType.NonConsumable);         // ƒ“ø’ ∆–≈∞¡ˆ (∏¡˝±« 300∞≥, ∞ÒµÂ 500∞≥)
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
            case "1112"://∞ÒµÁ∆–Ω∫
                GameManager.Instance.BuyGoldenPass();
                break;
            case "1113"://ƒ“ø’
                GameManager.Instance.MoneyChange(Constants.MoneyType.Gold, 500);
                GameManager.Instance.UnitManager.ChangeUnitPiece(300);
                break;
        }

        SaveSystem.Save();

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
            if (productId == "1112" && PlayerEvent.GoldenPass)//∞ÒµÁ∆–Ω∫
            {
                failMessage.SetActive(true);
                return;
            }

            if (productId == "1113" && PlayerEvent.Package1)
            {
                failMessage.SetActive(true);
                return;
            }

            controller.InitiatePurchase(productId);
        }
        else
        {
            Debug.LogError("Controller is not initialized.");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log("∞·¡¶æ»µ ");


        //throw new System.NotImplementedException();
    }
}
