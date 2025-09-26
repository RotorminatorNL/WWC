using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrapInfoHandler : MonoBehaviour
{
    [SerializeField] private Image blackBlackgroundImage;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private GameObject arrows;
    [SerializeField] private int maxAmount;

    public Sprite BtnImgSprite { get { return buttonImage.sprite; } }
    public int CurrentAmount { get; private set; }
    
    private void Start()
    {
        CurrentAmount = maxAmount;
        blackBlackgroundImage.sprite = buttonImage.sprite;
    }

    public void SetArrowsActive(bool active) => arrows.SetActive(active);

    //public void UpdateAmount(int amount)
    //{
    //    CurrentAmount += amount;
    //    textAmount.SetText($"[{CurrentAmount}/{maxAmount}]");
    //}
}