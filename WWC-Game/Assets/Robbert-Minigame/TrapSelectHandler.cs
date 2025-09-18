using UnityEngine;

public class TrapSelectHandler : MonoBehaviour
{
    [SerializeField] private TrapInfoHandler pitTrapInfoHandler;
    [SerializeField] private TrapInfoHandler tunnelTrapInfoHandler;
    public int SelectedTrap { get; private set; } = 0;
    public bool PitTrapAvailable => pitTrapInfoHandler.CurrentAmount > 0;
    public bool TunnelTrapAvailable => tunnelTrapInfoHandler.CurrentAmount > 0;

    public void SetSelectedTrap(int trapNr)
    {
        if (SelectedTrap == trapNr)
        {
            pitTrapInfoHandler.SetArrowsActive(false);
            tunnelTrapInfoHandler.SetArrowsActive(false);
            SelectedTrap = 0;
            return;
        }
        SelectedTrap = trapNr;
        if (SelectedTrap == 1)
        {
            pitTrapInfoHandler.SetArrowsActive(true);
            tunnelTrapInfoHandler.SetArrowsActive(false);
        }
        else if (SelectedTrap == 2)
        {
            pitTrapInfoHandler.SetArrowsActive(false);
            tunnelTrapInfoHandler.SetArrowsActive(true);
        }
    }

    public void TrapUpdateAmount(int trapNr, int amount)
    {
        if (trapNr == 1) pitTrapInfoHandler.UpdateAmount(amount);
        else if (trapNr == 2) tunnelTrapInfoHandler.UpdateAmount(amount);
    }
}