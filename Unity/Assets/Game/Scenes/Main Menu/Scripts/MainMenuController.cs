using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Scenes.Main_Menu.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGame);
            optionsButton.onClick.AddListener(OpenOptions);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        private void OpenOptions()
        {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}