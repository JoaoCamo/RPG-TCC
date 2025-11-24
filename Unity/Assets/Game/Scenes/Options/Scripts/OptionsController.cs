using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Scenes.Options.Scripts
{
    public class OptionsController : MonoBehaviour
    {
        [SerializeField] private Button returnButton;

        private void Awake()
        {
            returnButton.onClick.AddListener(ReturnButtonOnClick);
        }

        private void ReturnButtonOnClick()
        {
            SceneManager.UnloadSceneAsync(3);
        }
    }
}