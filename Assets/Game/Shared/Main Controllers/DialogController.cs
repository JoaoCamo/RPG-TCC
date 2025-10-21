using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Game.UI;
using Game.Static;
using Game.UI.Data;
using Game.Static.Enum;
using Game.Backend.Data;
using Game.Character.Enum;
using Game.Shared.Backend.Data;
using Game.Shared.UI.Data;

namespace Game.Controllers
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private MessageBoxUI messageUI;
        [SerializeField] private DialogUI dialogUI;
        [SerializeField] private PlayerStatsUI playerStatsUI;

        private const int MaxHistoryArcs = 3;
        private int _currentArc = 1;

        private void Awake()
        {
            StartCoroutine(StartCampaign(StaticVariables.PlayerController.Name, StaticVariables.PlayerController.Class));
        }

        private IEnumerator StartCampaign(string playerName, ClassType classType)
        {
            messageUI.RequestMessageBox("Waiting for Response");

            string url = "http://127.0.0.1:5000/generate/main_story/";

            StartCampaignData startCampaignData = new StartCampaignData(classType.ToString(), playerName, StaticVariables.CharacterHistory, StaticVariables.HistoryContext);
            string dataJson = JsonUtility.ToJson(startCampaignData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                CampaignStartInfo campaignStartInfo = JsonUtility.FromJson<CampaignStartInfo>(response);
                StaticVariables.CampaignStartInfo = campaignStartInfo;
                dialogUI.SetCampaignStartText(campaignStartInfo, this);
                messageUI.CloseMessageBox();
            }
        }

        public IEnumerator SendDialogToAI(string dialog)
        {
            messageUI.RequestMessageBox("Waiting for Response");

            string url = "http://127.0.0.1:5000/generate/story/";

            DialogSendData data = new DialogSendData(dialog);
            string dataJson = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                DialogData dialogData = JsonUtility.FromJson<DialogData>(response);
                dialogUI.SetNewDialog(dialogData, this);
                messageUI.CloseMessageBox();
            }
        }

        public IEnumerator AdvanceArc(string lastPlayerAction)
        {
            messageUI.RequestMessageBox("Waiting for Response");
            
            string url = "http://127.0.0.1:5000/generate/arc/";

            HistoryArcRequestData data = new HistoryArcRequestData(lastPlayerAction, ++_currentArc, _currentArc == MaxHistoryArcs);
            string dataJson = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);
            
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single), "Return to main menu"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                ArcData arcData = JsonUtility.FromJson<ArcData>(response);
                dialogUI.SetNewDialog(arcData, this, _currentArc == MaxHistoryArcs);
                messageUI.CloseMessageBox();
            }
        }

        public void ShowDungeonMessage()
        {
            string message = "Selecting this dialogue will lead you into the dungeon. Do you wish to proceed?";
            MessageBoxButtonData boxButtonDataExit = new MessageBoxButtonData(messageUI.CloseMessageBox, "Close");
            MessageBoxButtonData boxButtonDataContinue = new MessageBoxButtonData(() => 
            {   
                StaticVariables.CurrentGameState = GameState.Dungeon;
                messageUI.CloseMessageBox();
                mapController.LoadNewMap(); 
            }, "Enter Dungeon");

            messageUI.RequestMessageBox(message, boxButtonDataExit, boxButtonDataContinue);
        }

        public void ShowEndingMessage()
        {
            string message = "Thus ends your adventure… The main menu awaits your next tale.";
            MessageBoxButtonData boxButtonDataContinue = new MessageBoxButtonData(() => 
            {   
                StaticVariables.CurrentGameState = GameState.Dungeon;
                messageUI.CloseMessageBox();
                mapController.LoadNewMap(); 
            }, "Enter Dungeon");
            
            messageUI.RequestMessageBox(message, boxButtonDataContinue, new MessageBoxButtonData(null, string.Empty));
        }
    }
}