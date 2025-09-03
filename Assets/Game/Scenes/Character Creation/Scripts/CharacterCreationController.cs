using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scenes.Character_Creation.Scripts
{
    public class CharacterCreationController : MonoBehaviour
    {
        [SerializeField] private Button returnButton;
        [SerializeField] private Button continueButton;

        private void Awake()
        {
            returnButton.onClick.AddListener(ReturnButtonOnClick);
        }

        private void ReturnButtonOnClick()
        {
            SceneManager.UnloadSceneAsync(2);
        }
    }
}