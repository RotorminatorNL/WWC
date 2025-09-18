using TMPro;
using UnityEngine;

public class TrapAmountHandler : MonoBehaviour
{
    [SerializeField] private int maxAmount;
    private int currentAmount;
    private TextMeshProUGUI textAmount;

    private void Start()
    {
        textAmount = GetComponent<TextMeshProUGUI>();
        currentAmount = maxAmount;
    }

    public void UpdateAmount(int amount)
    {
        currentAmount += amount;
        textAmount.SetText($"[{currentAmount}/{maxAmount}]");
    }
}