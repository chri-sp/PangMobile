using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
   
   
   void OnEnable()
   {
      GameManager.OnGameOver += LoadMenuScene;
   }

   void OnDisable()
   {
      GameManager.OnGameOver -= LoadMenuScene;
   }
   
   public void LoadGameScene()
   {
      SceneManager.LoadSceneAsync(1);
   }
   
   void LoadMenuScene()
   {
      //aggiungere delay prima di tornare al menu principale
      //SceneManager.LoadSceneAsync(0);
   }

   public void Quit()
   {
      Application.Quit();
   }
}
