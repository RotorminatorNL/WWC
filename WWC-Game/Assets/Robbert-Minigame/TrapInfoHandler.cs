using TMPro;
using UnityEngine;

public class TrapInfoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private GameObject arrows;
    [SerializeField] private int maxAmount;
    private int currentAmount;
    
    private void Start()
    {
        currentAmount = maxAmount;
    }

    public void SetArrowsActive(bool active)
    {
        arrows.SetActive(active);
    }

    public void UpdateAmount(int amount)
    {
        currentAmount += amount;
        textAmount.SetText($"[{currentAmount}/{maxAmount}]");
    }
}