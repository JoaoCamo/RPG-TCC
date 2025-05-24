using UnityEngine.Events;

namespace Game.UI.Data
{
    public struct MessageBoxButtonData
    {
        public UnityAction onClick;
        public string text;

        public MessageBoxButtonData(UnityAction onClick, string text)
        {
            this.onClick = onClick;
            this.text = text;
        }
    }
}