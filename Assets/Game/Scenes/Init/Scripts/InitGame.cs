using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scenes.Init.Scripts
{
    public class InitGame : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene(1);
        }
    }
}