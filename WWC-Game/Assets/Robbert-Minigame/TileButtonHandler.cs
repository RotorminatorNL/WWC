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
    public GridHandler GridHandler { get; set; }
    public int TileNr { get; set; } = 0;
    public int AmountTrapsBlockingTile { get; set; } = 0;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        standardBtnImage = btnImage.sprite;
    }

    public void SwapButtonImage()
    {
        if (trapNr == 0)
        {
            if (trapSelectHandler.SelectedTrap == 0) return;

            if (trapSelectHandler.SelectedTrap == 1 && trapSelectHandler.PitTrapAvailable && AmountTrapsBlockingTile == 0)
            {
                trapNr = 1;
                trapSelectHandler.TrapUpdateAmount(trapNr, -1);
                GridHandler.DisableSurroundingBtns(TileNr, true);
                btnImage.sprite = pitTrapImage;
            }
            if (trapSelectHandler.SelectedTrap == 2 && trapSelectHandler.TunnelTrapAvailable)
            {
                trapNr = 2;
                trapSelectHandler.TrapUpdateAmount(trapNr, -1);
                btnImage.sprite = tunnelTrapImage;
            }
        }
        else
        {
            trapSelectHandler.TrapUpdateAmount(trapNr, 1);
            if (trapNr == 1) GridHandler.DisableSurroundingBtns(TileNr, false);
            trapNr = 0;
            btnImage.sprite = standardBtnImage;
        }
    }
}