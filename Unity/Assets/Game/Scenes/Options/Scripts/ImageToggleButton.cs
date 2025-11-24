using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Options.Scripts
{
    public class ImageToggleButton : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private TextMeshProUGUI statusTextMesh;
        
        private const string ImageKey = "TOGGLE_IMAGE";
        private bool _isEnabled;

        private void Awake()
        {
            toggleButton.onClick.AddListener(ToggleButtonOnClick);
            GetConfiguration();
        }

        private void ToggleButtonOnClick()
        {
            _isEnabled = !_isEnabled;
            statusTextMesh.text = _isEnabled ? "Enabled" : "Disabled";
            SaveImageSetting();
        }

        private void GetConfiguration()
        {
            _isEnabled = PlayerPrefs.GetInt(ImageKey, 1) == 1;
            statusTextMesh.text = _isEnabled ? "Enabled" : "Disabled";
        }

        private void SaveImageSetting()
        {
            PlayerPrefs.SetInt(ImageKey, _isEnabled ? 1 : 0);
        }
    }
}