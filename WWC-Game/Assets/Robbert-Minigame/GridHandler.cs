using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GridHandler : MonoBehaviour
{
    private List<TileButtonHandler> tileBtns;

    private void Start()
    {
        tileBtns = new List<TileButtonHandler>();
        for (int i = 0; i < transform.childCount; i++)
        {
            TileButtonHandler tileBtn = transform.GetChild(i).GetChild(0).GetComponent<TileButtonHandler>();
            tileBtn.GridHandler = this;
            tileBtn.TileNr = i;
            tileBtns.Add(tileBtn);
        }
    }

    public void DisableSurroundingBtns(int tileNr, bool disable)
    {
        int increase = disable ? 1 : -1;

        int tileRow = Mathf.FloorToInt(tileNr / 8f);

        int tileLeftNr = tileNr - 1;
        bool sameRow = Mathf.FloorToInt(tileLeftNr / 8f) == tileRow;
        if (sameRow && tileLeftNr >= 0 && tileLeftNr <= tileBtns.Count - 1) tileBtns[tileLeftNr].AmountTrapsBlockingTile += increase;

        int tileRightNr = tileNr + 1;
        sameRow = Mathf.FloorToInt(tileRightNr / 8f) == tileRow;
        if (sameRow && tileRightNr >= 0 && tileRightNr <= tileBtns.Count - 1) tileBtns[tileRightNr].AmountTrapsBlockingTile += increase;

        int tileUpNr = tileNr - 8;
        if (tileUpNr >= 0 && tileUpNr <= tileBtns.Count - 1) tileBtns[tileUpNr].AmountTrapsBlockingTile += increase;

        int tileDownNr = tileNr + 8;
        if (tileDownNr >= 0 && tileDownNr <= tileBtns.Count - 1) tileBtns[tileDownNr].AmountTrapsBlockingTile += increase;
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
        int tileRow = Mathf.FloorToInt(tileNr / 8f);
        bool sameRow = true;
        bool blocked = false;
        for (int i = 0; i < 6; i++)
        {
            sameRow = Mathf.FloorToInt((tileNr + i) / 8f) == tileRow;
            if (sameRow && !blocked) blocked = tileBtns[tileNr + i].TrapNr != 0;
        }

        return sameRow && !blocked;
    }

    public void RemoveTunnelTrap(int tileNr)
    {
        int tileRow = Mathf.FloorToInt(tileNr / 8f);
        bool sameRow = true; 
        for (int i = -6; i < 0; i++)
        {
            sameRow = Mathf.FloorToInt((tileNr + i) / 8f) == tileRow;
            if (sameRow) tileBtns[tileNr + i].SwapButtonImage(2);
        }
        for (int i = 0; i < 6; i++)
        {
            sameRow = Mathf.FloorToInt((tileNr + i) / 8f) == tileRow;
            if (sameRow) tileBtns[tileNr + i].SwapButtonImage(2);
        }   
    }
}
