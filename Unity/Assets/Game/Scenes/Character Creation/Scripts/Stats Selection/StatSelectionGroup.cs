using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Character.Enum;

namespace Game.Scenes.Character_Creation.Scripts.Stats_Selection
{
    public class StatSelectionGroup : MonoBehaviour
    {
        [SerializeField] private StatsType statsType;
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private TextMeshProUGUI valueTextMesh;

        private StatSelectionController _controller;
        
        public StatsType StatsType => statsType;
        public int  TotalPoints { get; private set; } = 8;

        private void Awake()
        {
            upButton.onClick.AddListener(UpButtonClick);
            downButton.onClick.AddListener(DownButtonClick);
        }

        public void Initialize(StatSelectionController statSelectionController)
        {
            _controller = statSelectionController;
        }

        private void UpButtonClick()
        {
            if (TotalPoints == 15 || (_controller.PointsRemaining - (TotalPoints >= 13 ? 2 : 1) < 0))
                return;
            
            TotalPoints++;
            _controller.PointsRemaining -= TotalPoints > 13 ? 2 : 1;
            valueTextMesh.text = TotalPoints.ToString();
            _controller.UpdatePointsRemainingText();
            
        }

        private void DownButtonClick()
        {
            if (TotalPoints == 8)
                return;
            
            TotalPoints--;
            _controller.PointsRemaining += TotalPoints >= 13 ? 2 : 1;
            valueTextMesh.text = TotalPoints.ToString();
            _controller.UpdatePointsRemainingText();
        }
    }
}