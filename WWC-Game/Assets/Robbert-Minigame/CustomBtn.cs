using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField, Range(0, 1)] private float hoverAlphaPercent = 0.8f;
    [SerializeField, Range(0, 1)] private float hoverFadeTime = 0.2f;
    [Space(10)]
    [SerializeField, Range(0, 1)] private float clickedAlphaPercent = 0.6f;
    [SerializeField, Range(0, 1)] private float clickedFadeTime = 0.075f;
    [Space(10)]
    [SerializeField] private UnityEvent onMouseEnter;
    [SerializeField] private UnityEvent onMouseExit;
    [SerializeField] private UnityEvent onClick;

    private Image btnImage;
    private Color initColor;

    private float hoverAlphaDif;
    private float clickedAplhaDif;

    private bool fadingIn = false;
    private bool fadingOut = false;
    private bool clicked = false;
    private bool clickFadeIn = false;
    private float currentHoverFadeTime = 0;
    private float currentClickedFadeTime = 0;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        initColor = btnImage.color;
        hoverAlphaDif = initColor.a - hoverAlphaPercent;
        clickedAplhaDif = hoverAlphaPercent - clickedAlphaPercent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fadingIn = true;
        fadingOut = false;
        onMouseEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fadingIn = false;
        fadingOut = true;
        onMouseExit.Invoke();
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

        float newAplha = initColor.a - (hoverAlphaDif / hoverFadeTime * currentHoverFadeTime);
        btnImage.color = new Color(initColor.r, initColor.g, initColor.b, newAplha);
    }

    private void ClickFadeHandler()
    {
        currentClickedFadeTime += clickFadeIn ? Time.deltaTime : -Time.deltaTime;
        if (currentClickedFadeTime >= clickedFadeTime) clickFadeIn = false;
        if (!clickFadeIn && currentClickedFadeTime <= 0) clicked = false;

        float newAplha = (initColor.a - hoverAlphaDif) - (clickedAplhaDif / clickedFadeTime * currentClickedFadeTime);
        btnImage.color = new Color(initColor.r, initColor.g, initColor.b, newAplha);
    }
}