using TMPro;
using UnityEngine;

public class TrapInfoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private GameObject arrows;
    [SerializeField] private int maxAmount;
    public int CurrentAmount { get; private set; }
    
    private void Start()
    {
        CurrentAmount = maxAmount;
    }

    public void SetArrowsActive(bool active)
    {
        arrows.SetActive(active);
    }

    public void UpdateAmount(int amount)
    {
        CurrentAmount += amount;
        textAmount.SetText($"[{CurrentAmount}/{maxAmount}]");
    }
}