using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MenuController : MonoBehaviour

    {
        [SerializeField] private string sceneName;
        
        public void StartMenu()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}