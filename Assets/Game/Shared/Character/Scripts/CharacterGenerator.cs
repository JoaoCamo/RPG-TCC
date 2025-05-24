using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Game.Character.Enemy;
using Game.Character.Data;
using Game.Character.Enum;
using Game.Backend.Data;
using Game.UI;
using Game.UI.Data;
using Game.Static;
using Game.Item;

namespace Game.Character
{
    public class CharacterGenerator
    {
        private readonly MessageBoxUI messageUI;

        public CharacterGenerator(MessageBoxUI messageBoxUI)
        {
            messageUI = messageBoxUI;
        }

        public IEnumerator GenerateEnemy(EnemyController enemyController)
        {
            string url = "http://127.0.0.1:5000/generate/character/";

            LevelSendData data = new LevelSendData() { level = StaticVariables.PlayerController.Stats.level };
            string dataJson = JsonUtility.ToJson(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(dataJson);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                messageUI.RequestMessageBox(request.error, new MessageBoxButtonData(messageUI.CloseMessageBox, "Close"), new MessageBoxButtonData());
            else
            {
                string response = request.downloadHandler.text;
                CharacterCreationDataWrapper wrapper = JsonUtility.FromJson<CharacterCreationDataWrapper>(response);
                CharacterCreationData characterCreationData = JsonUtility.FromJson<CharacterCreationData>(wrapper.character);
                FillCharacterData(enemyController, characterCreationData);
            }
        }

        private void FillCharacterData(EnemyController enemyController, CharacterCreationData data)
        {
            enemyController.LoadCharacter(data.name, CharacterType.Enemy);
            enemyController.Size = data.size;
            enemyController.Stats.level = data.level;
            enemyController.Stats.strength = data.stats.strength;
            enemyController.Stats.dexterity = data.stats.dexterity;
            enemyController.Stats.constitution = data.stats.constitution;
            enemyController.Stats.intelligence = data.stats.intelligence;
            enemyController.Stats.wisdom = data.stats.wisdom;
            enemyController.Stats.charisma = data.stats.charisma;
            enemyController.Health.CalculateHealth((int)enemyController.Size, enemyController.Stats.level, enemyController.Stats.constitution);
            enemyController.Inventory.UpdateCapacity(enemyController.Stats.strength);
        }

        private ItemBase RequestItem()
        {
            return null;
        }
    }
}