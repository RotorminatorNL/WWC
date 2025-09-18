using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

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


    public bool SpaceForTunnelTrap(int tileNr)
    {

        return true;
    }
}
