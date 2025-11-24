using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Game.Character.Enum;

namespace Game.LevelUp
{
    public class LevelUpStatGroup : MonoBehaviour
    {
        [SerializeField] private StatsType statType;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private TextMeshProUGUI pointsTextMesh;

        private int _originalStatPoints;
        private int _currentStatPoints = 0;

        public StatsType StatType => statType;
        public int OriginalStatPoints => _originalStatPoints;
        public int CurrentStatPoints { get => _currentStatPoints; set => _currentStatPoints = value; }

        public void Initialize(int statPoints, UnityAction upButtonOnClick, UnityAction downButtonOnClick)
        {
            upButton.onClick.AddListener(upButtonOnClick);
            downButton.onClick.AddListener(downButtonOnClick);
            pointsTextMesh.text = statPoints.ToString();
            _originalStatPoints = statPoints;
        }

        public void UpdateText()
        {
            pointsTextMesh.text = (_originalStatPoints + _currentStatPoints).ToString();
        }
    }
}