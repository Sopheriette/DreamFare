using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 500f;
    private RectTransform rectTransform;
    private float screenWidth;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenWidth = Screen.width;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rectTransform.anchoredPosition += new Vector2(moveInput * moveSpeed * Time.deltaTime, 0);

        float halfWidth = rectTransform.rect.width * 0.5f;
        float minX = 0 + halfWidth;
        float maxX = screenWidth - halfWidth;
        float clampedX = Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX);

        rectTransform.anchoredPosition = new Vector2(clampedX, rectTransform.anchoredPosition.y);
    }
}