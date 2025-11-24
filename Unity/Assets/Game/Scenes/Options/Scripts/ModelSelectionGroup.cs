using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Options.Scripts
{
    public class ModelSelectionGroup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI modelTextMesh;
        [SerializeField] private Button downButton;
        [SerializeField] private Button upButton;
        
        private const string ModelKey = "SELECTED_MODEL";
        private readonly string[] _availableModels = { "gpt-4.1", "gpt-4.1-mini", "gpt-5-mini"};
        private int _currentIndex;
        
        private void Awake()
        {
            downButton.onClick.AddListener(DownButtonOnClick);
            upButton.onClick.AddListener(UpButtonOnClick);
            GetCurrentIndex();
        }

        private void DownButtonOnClick()
        {
            _currentIndex = _currentIndex - 1 >= 0 ? _currentIndex - 1 : _currentIndex;
            modelTextMesh.text = _availableModels[_currentIndex];
            SaveModel();
        }

        private void UpButtonOnClick()
        {
            _currentIndex = _currentIndex + 1 <= _availableModels.Length - 1 ? _currentIndex + 1 : _currentIndex;
            modelTextMesh.text = _availableModels[_currentIndex];
            SaveModel();
        }

        private void GetCurrentIndex()
        {
            string currentModel = PlayerPrefs.GetString(ModelKey, "gpt-4.1");

            for (int i = 0; i < _availableModels.Length; i++)
                if (currentModel == _availableModels[i])
                {
                    modelTextMesh.text = _availableModels[i];
                    _currentIndex = i;
                    break;
                }
        }
        
        private void SaveModel()
        {
            PlayerPrefs.SetString(ModelKey, _availableModels[_currentIndex]);
        }
    }
}