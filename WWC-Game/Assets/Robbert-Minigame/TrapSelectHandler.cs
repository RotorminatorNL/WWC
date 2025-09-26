using UnityEngine;

public class TrapSelectHandler : MonoBehaviour
{
    [SerializeField] private TrapInfoHandler pitTrapInfoHandler;
    [SerializeField] private TrapInfoHandler tunnelTrapInfoHandler;

    private int selectedTrapNr = 0;

    public void SetSelectedTrap(int trapNr)
    {
        if (selectedTrapNr != trapNr) selectedTrapNr = trapNr;
        else selectedTrapNr = 0;
        pitTrapInfoHandler.SetArrowsActive(selectedTrapNr == 1);
        tunnelTrapInfoHandler.SetArrowsActive(selectedTrapNr == 2);
    }

    public Sprite GetSelectedTrapSprite()
    {
        if (selectedTrapNr == 1) return pitTrapInfoHandler.BtnImgSprite;
        if (selectedTrapNr == 2) return tunnelTrapInfoHandler.BtnImgSprite;
        return null;
    }
}