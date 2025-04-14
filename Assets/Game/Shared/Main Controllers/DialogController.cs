using UnityEngine;
using Game.UI;
using Game.UI.Data;

namespace Game.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private DialogUI dialogUI;

        public void LoadDialog(DialogData dialogData)
        {
            dialogUI.SetNewDialog(dialogData, this);
        }

        public void SendDialogToAI(string dialog)
        {
            
        }
    }
}