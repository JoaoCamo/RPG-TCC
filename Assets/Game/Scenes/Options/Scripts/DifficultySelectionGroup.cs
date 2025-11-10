using Game.Static;
using Game.Static.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Options.Scripts
{
    public class DifficultySelectionGroup : MonoBehaviour
    {
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private TextMeshProUGUI difficultyTextMesh;

        private const string DifficultyKey = "GAME_DIFFICULTY";
        private int _currentDifficulty;

        private void Awake()
        {
            upButton.onClick.AddListener(UpButtonOnClick);
            downButton.onClick.AddListener(DownButtonOnClick);
            
            _currentDifficulty = PlayerPrefs.GetInt(DifficultyKey, 1);
            StaticVariables.GameDifficulty = (GameDifficulty)_currentDifficulty;
            UpdateText();
        }

        private void UpButtonOnClick()
        {
            if (_currentDifficulty == 2)
                return;

            _currentDifficulty++;
            StaticVariables.GameDifficulty = (GameDifficulty)_currentDifficulty;
            UpdateText();
        }

        private void DownButtonOnClick()
        {
            if (_currentDifficulty == 0)
                return;
            
            _currentDifficulty--;
            StaticVariables.GameDifficulty = (GameDifficulty)_currentDifficulty;
            UpdateText();
        }
        
        private void UpdateText()
        {
            difficultyTextMesh.text = ((GameDifficulty)_currentDifficulty).ToString();
        }
    }
}