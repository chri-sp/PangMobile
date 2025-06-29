using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _GameOverText;
    
    [SerializeField]
    private GameObject _VictoryText;
    
    void OnEnable()
    {
        GameManager.OnGameOver += showGameOver;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= showGameOver;
    }

    void showGameOver()
    {
        _GameOverText.SetActive(true);
    }
    
    void showVictory()
    {
        
    }
    
}
