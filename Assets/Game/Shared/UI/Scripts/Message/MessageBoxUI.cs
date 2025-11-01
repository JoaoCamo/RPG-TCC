using UnityEngine;
using TMPro;
using DG.Tweening;
using Game.UI.Data;
using Game.Static;

namespace Game.UI
{
    public class MessageBoxUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private MessageBoxButton buttonOption1;
        [SerializeField] private MessageBoxButton buttonOption2;

        private bool _isOpen = false;

        private const Ease CANVAS_FADE_EASE = Ease.Linear;
        private const float CANVAS_FADE_DELAY = 0.25f;

        private void OnEnable()
        {
            StaticEvents.RequestMessageBoxUI += RequestMessageBox;
            StaticEvents.RequestMessageBoxUIWithOptions += RequestMessageBox;
            StaticEvents.CloseMessageBoxUI += CloseMessageBox;
        }

        private void OnDisable()
        {
            StaticEvents.RequestMessageBoxUI -= RequestMessageBox;
            StaticEvents.RequestMessageBoxUIWithOptions -= RequestMessageBox;
            StaticEvents.CloseMessageBoxUI -= CloseMessageBox;
        }

        private void ToggleCanvas(bool mode)
        {
            canvasGroup.interactable = mode;
            canvasGroup.blocksRaycasts = mode;
            canvasGroup.DOFade(mode ? 1 : 0, CANVAS_FADE_DELAY).SetEase(CANVAS_FADE_EASE);
        }

        public void RequestMessageBox(string message)
        {
            if (!_isOpen)
                ToggleCanvas(true);

            textMesh.text = message;
            buttonOption1.gameObject.SetActive(false);
            buttonOption2.gameObject.SetActive(false);

            _isOpen = true;
        }

        public void RequestMessageBox(string message, MessageBoxButtonData boxButtonData1, MessageBoxButtonData boxButtonData2)
        {
            if (!_isOpen)
                ToggleCanvas(true);

            textMesh.text = message;
            buttonOption1.gameObject.SetActive(true);
            buttonOption1.SetButton(boxButtonData1);

            if(!string.IsNullOrEmpty(boxButtonData2.text))
            {
                buttonOption2.gameObject.SetActive(true);
                buttonOption2.SetButton(boxButtonData2);
            }
            else
                buttonOption2.gameObject.SetActive(false);

            _isOpen = true;
        }

        public void CloseMessageBox()
        {
            ToggleCanvas(false);
            _isOpen = false;
        }
    }
}