using UnityEngine;
using UnityEngine.UI;

public class TileButtonHandler : MonoBehaviour
{
    [SerializeField] private TrapSelectHandler trapSelectHandler;
    [SerializeField] private Sprite pitTrapImage;
    [SerializeField] private Sprite tunnelTrapImage;
    private Sprite standardBtnImage;
    private Image btnImage;
    private int trapNr = 0;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        standardBtnImage = btnImage.sprite;
    }

    public void SwapButtonImage()
    {
        if (trapNr == 0)
        {
            trapNr = trapSelectHandler.SelectedTrap;
            if (trapNr == 0) return;

            trapSelectHandler.TrapUpdateAmount(trapNr, -1);
            if (trapNr == 1) btnImage.sprite = pitTrapImage;
            if (trapNr == 2) btnImage.sprite = tunnelTrapImage;
        }
        else
        {
            trapSelectHandler.TrapUpdateAmount(trapNr, 1);
            trapNr = 0;
            btnImage.sprite = standardBtnImage;
        }
    }
}