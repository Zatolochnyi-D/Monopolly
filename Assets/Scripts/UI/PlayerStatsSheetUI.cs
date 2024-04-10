using TMPro;
using UnityEngine;

public class PlayerStatsSheetUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private TextMeshProUGUI numberDisplay;
    [SerializeField] private TextMeshProUGUI balanceDisplay;
    [SerializeField] private TextMeshProUGUI imageDisplay;
    [SerializeField] private TextMeshProUGUI productionDispay;
    [SerializeField] private TextMeshProUGUI passiveProductDispay;
    [SerializeField] private TextMeshProUGUI airlinesText;
    [SerializeField] private TextMeshProUGUI carManufacturerText;
    [SerializeField] private TextMeshProUGUI tourismAgencyText;
    [SerializeField] private TextMeshProUGUI tvCompanyText;
    [SerializeField] private TextMeshProUGUI buildingAgencyText;
    [SerializeField] private TextMeshProUGUI bookPublisherText;

    public string Name
    {
        set => nameDisplay.text = value;
    }
    public string Number
    {
        set => numberDisplay.text = value;
    }
    public string Balance
    {
        set => balanceDisplay.text = value + "00$"; // 1 money in code = 100 money displayed.
    }
    public string Image
    {
        set => imageDisplay.text = value;
    }
    public string Production
    {
        set => productionDispay.text = value;
    }
    public string PassiveProduct
    {
        set => passiveProductDispay.text = value;
    }
    public PlayerLogic.PlayerShares Shares
    {
        set 
        {
            airlinesText.text = $"Airlines: {value.Airlines}";
            carManufacturerText.text = $"Car Manufacturer: {value.CarManufacturer}";
            tourismAgencyText.text = $"Tourism Agecny: {value.TourismAgency}";
            tvCompanyText.text = $"TV Company: {value.TVCompany}";
            buildingAgencyText.text = $"Building Agency: {value.BuildingAgency}";
            bookPublisherText.text = $"Book Publisher: {value.BookPublisher}";
        }
    }
}
