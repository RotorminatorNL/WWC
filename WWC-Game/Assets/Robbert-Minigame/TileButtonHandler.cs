using UnityEngine;
using UnityEngine.UI;

public class TileButtonHandler : MonoBehaviour
{
    [SerializeField] private TrapSelectHandler trapSelectHandler;
    [SerializeField] private Sprite pitTrapImage;
    [SerializeField] private Sprite tunnelTrapImage;
    private Sprite standardBtnImage;
    private Image btnImage;
    public int TrapNr { get; private set; } = 0;
    public GridHandler GridHandler { get; set; }
    public int TileNr { get; set; } = 0;
    public int AmountTrapsBlockingTile { get; set; } = 0;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        standardBtnImage = btnImage.sprite;
    }

    public void SwapButtonImage(int tunnelTrap = 0)
    {
        if (tunnelTrap >= 1)
        {
            if (tunnelTrap == 1)
            {
                TrapNr = 2;
                btnImage.sprite = tunnelTrapImage;
                return;
            }
            else
            {
                if (TrapNr != 2) return;
                TrapNr = 0;
                btnImage.sprite = standardBtnImage;
                return;
            }
        }

        if (TrapNr == 0)
        {
            if (trapSelectHandler.SelectedTrap == 0) return;

            if (trapSelectHandler.SelectedTrap == 1 && trapSelectHandler.PitTrapAvailable && AmountTrapsBlockingTile == 0)
            {
                TrapNr = 1;
                trapSelectHandler.TrapUpdateAmount(TrapNr, -1);
                GridHandler.DisableSurroundingBtns(TileNr, true);
                btnImage.sprite = pitTrapImage;
            }
            if (trapSelectHandler.SelectedTrap == 2 && trapSelectHandler.TunnelTrapAvailable)
            {
                bool placed = GridHandler.PlaceTunnelTrap(TileNr);
                if (!placed) return;
                trapSelectHandler.TrapUpdateAmount(TrapNr, -1);
            }
        }
        else
        {
            trapSelectHandler.TrapUpdateAmount(TrapNr, 1);
            if (TrapNr == 1)
            {
                GridHandler.DisableSurroundingBtns(TileNr, false);
                TrapNr = 0;
                btnImage.sprite = standardBtnImage;
            }
            if (TrapNr == 2) GridHandler.RemoveTunnelTrap(TileNr);
        }
    }
}