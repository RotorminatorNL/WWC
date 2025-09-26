using UnityEngine;
using UnityEngine.UI;

public class TileInfoHandler : MonoBehaviour
{
    [SerializeField] private TrapSelectHandler trapSelectHandler;
    [SerializeField] private Image blackBackgroundImage;
    [SerializeField] private Image buttonImage;

    private Sprite initSprite;

    private void Start()
    {
        blackBackgroundImage.sprite = buttonImage.sprite;
        initSprite = buttonImage.sprite;
    }

    public void SwapSprite(bool active)
    {
        if (active)
        {
            Sprite newSprite = trapSelectHandler.GetSelectedTrapSprite();
            if (newSprite == null) return;
            blackBackgroundImage.sprite = newSprite;
            buttonImage.sprite = newSprite;
        }
        else
        {
            blackBackgroundImage.sprite = initSprite;
            buttonImage.sprite = initSprite;
        }
    }
}