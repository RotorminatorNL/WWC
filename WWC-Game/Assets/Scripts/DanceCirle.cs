using UnityEngine;

public class DanceCircle : MonoBehaviour
{
    public float growDuration = 1.5f;
    public float minScale = 0.3f;
    public float maxScale = 1.2f;

    private float timer = 0f;
    private bool clicked = false;
    private DanceController2 controller;

    void Start()
    {
        transform.localScale = Vector3.one * minScale;
        controller = FindObjectOfType<DanceController2>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / growDuration;
        transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, t);

        if (timer >= growDuration && !clicked)
        {
            controller.Miss(this.gameObject);
        }
    }

    public void OnClick()
    {
        if (!clicked)
        {
            clicked = true;
            controller.Hit(this.gameObject);
        }
    }
}