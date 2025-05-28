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
    public class CharacterGenerator : MonoBehaviour
    {
        [SerializeField] private ItemGenerator itemGenerator;
        [SerializeField] private MessageBoxUI messageUI;

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
                yield return StartCoroutine(FillCharacterData(enemyController, characterCreationData, wrapper.character));
            }
        }

        private IEnumerator FillCharacterData(EnemyController enemyController, CharacterCreationData data, string characterJson)
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

            for (int i = 0; i < data.dropQuantity; i++)
            {
                if(Random.value >= 0.5f)
                {
                    WeaponBase weaponBase = new WeaponBase();
                    yield return StartCoroutine(itemGenerator.GenerateItem(weaponBase, characterJson));
                    enemyController.Inventory.AddItem(weaponBase);
                    enemyController.Equipment.EquipItem(weaponBase);
                }
                else
                {
                    ArmorBase armorBase = new ArmorBase();
                    yield return StartCoroutine(itemGenerator.GenerateItem(armorBase, characterJson));
                    enemyController.Inventory.AddItem(armorBase);
                    enemyController.Equipment.EquipItem(armorBase);
                }
            }
        }
    }
}