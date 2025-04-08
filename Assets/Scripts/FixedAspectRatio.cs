using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 9f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        UpdateViewport();
    }

    void Update()
    {
        //UpdateViewport();
    }

    void UpdateViewport()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float scale = currentAspect / targetAspect;

        //Add black bars on the top and on the bottom
        if (scale < 1f)
        {
            float viewportHeight = scale;
            float yOffset = (1f - viewportHeight) / 2f;
            mainCamera.rect = new Rect(0f, yOffset, 1f, viewportHeight);
        }
        
        //Add black bars on the right and on the left
        else
        {
            float viewportWidth = 1f / scale;
            float xOffset = (1f - viewportWidth) / 2f;
            mainCamera.rect = new Rect(xOffset, 0f, viewportWidth, 1f);
        }
    }
}
