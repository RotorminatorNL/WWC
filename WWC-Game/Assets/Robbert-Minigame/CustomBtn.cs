using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class CustomBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color hoverColor = new(.9f, .9f, .9f);
    [SerializeField] private float hoverFadeTime = .2f;
    [Space(10)]
    [SerializeField] private Color clickedColor = new(.75f, .75f, .75f);
    [SerializeField] private float clickedFadeTime = .1f;
    [Space(10)] 
    [SerializeField] private UnityEvent onClick;

    private Image btnImage;
    private Color initialColor;

    private float hoverColorDifR;
    private float hoverColorDifG;
    private float hoverColorDifB;
    private float clickedColorDifR;
    private float clickedColorDifG;
    private float clickedColorDifB;

    private bool fadingIn = false;
    private bool fadingOut = false;
    private bool clicked = false;
    private bool clickFadeIn = false;
    private float currentHoverFadeTime = 0;
    private float currentClickedFadeTime = 0;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        initialColor = btnImage.color;
        hoverColorDifR = initialColor.r - hoverColor.r;
        hoverColorDifG = initialColor.g - hoverColor.g;
        hoverColorDifB = initialColor.b - hoverColor.b;
        clickedColorDifR = hoverColor.r - clickedColor.r;
        clickedColorDifG = hoverColor.g - clickedColor.g;
        clickedColorDifB = hoverColor.b - clickedColor.b;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fadingIn = true;
        fadingOut = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fadingIn = false;
        fadingOut = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
        clickFadeIn = true;
        onClick.Invoke();
    }

    private void Update()
    {
        if (fadingIn || fadingOut) HoverFadeHandler();
        if (clicked) ClickFadeHandler();
    }

    private void HoverFadeHandler()
    {
        if (fadingIn)
        {
            currentHoverFadeTime += Time.deltaTime;
            if (currentHoverFadeTime >= hoverFadeTime) fadingIn = false;
        }
        if (fadingOut)
        {
            currentHoverFadeTime -= Time.deltaTime;
            if (currentHoverFadeTime <= 0) fadingOut = false;
        }

        float newColorR = initialColor.r - (hoverColorDifR / hoverFadeTime * currentHoverFadeTime);
        float newColorG = initialColor.g - (hoverColorDifG / hoverFadeTime * currentHoverFadeTime);
        float newColorB = initialColor.b - (hoverColorDifB / hoverFadeTime * currentHoverFadeTime);
        btnImage.color = new Color(newColorR, newColorG, newColorB);
    }

    private void ClickFadeHandler()
    {
        if (clickFadeIn)
        {
            currentClickedFadeTime += Time.deltaTime;
            if (currentClickedFadeTime >= clickedFadeTime) clickFadeIn = false;
        }
        else
        {
            currentClickedFadeTime -= Time.deltaTime;
            if (currentClickedFadeTime <= 0) clicked = false;
        }

        float newColorR = (initialColor.r - hoverColorDifR) - (clickedColorDifR / clickedFadeTime * currentClickedFadeTime);
        float newColorG = (initialColor.g - hoverColorDifG) - (clickedColorDifG / clickedFadeTime * currentClickedFadeTime);
        float newColorB = (initialColor.b - hoverColorDifB) - (clickedColorDifB / clickedFadeTime * currentClickedFadeTime);
        btnImage.color = new Color(newColorR, newColorG, newColorB);
    }
}