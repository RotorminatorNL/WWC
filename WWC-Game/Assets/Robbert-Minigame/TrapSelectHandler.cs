using UnityEngine;

public class TrapSelectHandler : MonoBehaviour
{
    [SerializeField] private TrapAmountHandler pitTrapAmountHandler;
    [SerializeField] private TrapAmountHandler tunnelTrapAmountHandler;
    public int SelectedTrap { get; private set; } = 0;

    public void SetSelectedTrap(int trapNr)
    {
        SelectedTrap = trapNr;
    }

    public void TrapUpdateAmount(int trapNr, int amount)
    {
        if (trapNr == 1) pitTrapAmountHandler.UpdateAmount(amount);
        else if (trapNr == 2) tunnelTrapAmountHandler.UpdateAmount(amount);
    }
}