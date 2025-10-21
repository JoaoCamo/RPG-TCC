using UnityEngine.Events;

namespace Game.UI.Data
{
    public struct MessageBoxButtonData
    {
        public readonly UnityAction onClick;
        public readonly string text;

        public MessageBoxButtonData(UnityAction onClick, string text)
        {
            this.onClick = onClick;
            this.text = text;
        }
    }
}