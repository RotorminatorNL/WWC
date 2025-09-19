using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GridHandler : MonoBehaviour
{

    private readonly int columnLength = 8;

    private List<TileButtonHandler> tileBtns;

    private void Start()
    {
        tileBtns = new List<TileButtonHandler>();
        for (int i = 0; i < transform.childCount; i++)
        {
            TileButtonHandler tileBtn = transform.GetChild(i).GetChild(1).GetComponent<TileButtonHandler>();
            tileBtn.GridHandler = this;
            tileBtn.TileNr = i;
            tileBtns.Add(tileBtn);
        }
    }

    public void DisableSurroundingBtns(int tileNr, bool disable)
    {
        int increase = disable ? 1 : -1;
        int tileRow = Mathf.FloorToInt(tileNr / columnLength);

        int tileLeftNr = tileNr - 1;
        bool sameRow = Mathf.FloorToInt(tileLeftNr / columnLength) == tileRow;
        if (sameRow && tileLeftNr >= 0 && tileLeftNr < tileBtns.Count) tileBtns[tileLeftNr].AmountTrapsBlockingTile += increase;

        int tileRightNr = tileNr + 1;
        sameRow = Mathf.FloorToInt(tileRightNr / columnLength) == tileRow;
        if (sameRow && tileRightNr >= 0 && tileRightNr < tileBtns.Count) tileBtns[tileRightNr].AmountTrapsBlockingTile += increase;

        int tileUpNr = tileNr - columnLength;
        if (tileUpNr >= 0 && tileUpNr < tileBtns.Count) tileBtns[tileUpNr].AmountTrapsBlockingTile += increase;

        int tileDownNr = tileNr + columnLength;
        if (tileDownNr >= 0 && tileDownNr < tileBtns.Count) tileBtns[tileDownNr].AmountTrapsBlockingTile += increase;
    }

    public bool PlaceTunnelTrap(int tileNr)
    {
        bool able = AbleToTunnelTrap(tileNr);
        if (able)
        {
            for (int i = 0; i < 6; i++) tileBtns[tileNr + i].SwapButtonImage(1);
        }
        return able;
    }

    private bool AbleToTunnelTrap(int tileNr)
    {
        int tileRow = Mathf.FloorToInt(tileNr / columnLength);
        bool sameRow = true;
        bool blocked = false;
        for (int i = 0; i < 6; i++)
        {
            sameRow = Mathf.FloorToInt((tileNr + i) / columnLength) == tileRow;
            if (sameRow && !blocked)
            {
                if (tileNr + 1 >= tileBtns.Count) return false;
                blocked = tileBtns[tileNr + i].TrapNr != 0;
            }
        }

        return sameRow && !blocked;
    }

    public void RemoveTunnelTrap(int tileNr)
    {
        int tileRow = Mathf.FloorToInt(tileNr / columnLength);
        for (int i = -6; i < 0; i++)
        {
            bool sameRow = Mathf.FloorToInt((tileNr + i) / columnLength) == tileRow;
            if (sameRow) tileBtns[tileNr + i].SwapButtonImage(2);
        }
        for (int i = 0; i < 6; i++)
        {
            bool sameRow = Mathf.FloorToInt((tileNr + i) / columnLength) == tileRow;
            if (sameRow) tileBtns[tileNr + i].SwapButtonImage(2);
        }   
    }
}