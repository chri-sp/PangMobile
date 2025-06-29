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
        GameManager.OnVictory += showVictory;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= showGameOver;
        GameManager.OnVictory -= showVictory;
    }

    void showGameOver()
    {
        _GameOverText.SetActive(true);
    }
    
    void showVictory()
    {
        _VictoryText.SetActive(true);
    }
    
}
