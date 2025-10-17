using Game.UI.Data;
using System;

namespace Game.Static
{
    public static class StaticEvents
    {
        public static Action OnItemUse;
        public static Action OnLevelUp;

        public static Action<string> RequestMessageBoxUI;
        public static Action<string, MessageBoxButtonData, MessageBoxButtonData> RequestMessageBoxUIWithOptions;
        public static Action CloseMessageBoxUI;
    }
}