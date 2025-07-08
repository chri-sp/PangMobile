using UnityEngine;

public class ShowMobileInput : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(Application.platform == RuntimePlatform.Android);
    }
}
