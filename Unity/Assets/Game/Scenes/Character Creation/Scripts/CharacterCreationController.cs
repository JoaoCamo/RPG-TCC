using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game.Static;
using Game.Character.Enum;
using Game.Character;
using Game.Shared.Item.Scripts.Systems;
using Game.Scenes.Character_Creation.Scripts.Character_Lore;
using Game.Scenes.Character_Creation.Scripts.History_Context;
using Game.Scenes.Character_Creation.Scripts.Stats_Selection;
using Game.Static.Enum;

namespace Game.Scenes.Character_Creation.Scripts
{
    public class CharacterCreationController : MonoBehaviour
    {
        [SerializeField] private CharacterLoreController  characterLoreController;
        [SerializeField] private StatSelectionController  statSelectionController;
        [SerializeField] private HistoryContextController  historyContextController;
        [SerializeField] private Button returnButton;
        [SerializeField] private Button continueButton;
        
        private readonly PlayerController _playerController = new PlayerController();
        private CanvasGroup _currentCanvasGroup;
        private int _currentPosition = 0;
        
        private void Awake()
        {
            _currentCanvasGroup = characterLoreController.CanvasGroup;
            returnButton.onClick.AddListener(ReturnButtonOnClick);
            continueButton.onClick.AddListener(ContinueButtonOnClick);
        }

        private void ContinueButtonOnClick()
        {
            if (_currentPosition == 2)
            {
                SetPlayerData();
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
            else
            {
                _currentPosition++;
                StaticVariables.CurrentGameState = GameState.Introduction;
                SetCurrentCanvasGroup(GetCanvasToShow());
            }
        }
        
        private void ReturnButtonOnClick()
        {
            if (_currentPosition == 0)
                SceneManager.UnloadSceneAsync(1);
            else
            {
                _currentPosition--;
                SetCurrentCanvasGroup(GetCanvasToShow());
            }
        }

        private void SetCurrentCanvasGroup(CanvasGroup canvasGroupShow)
        {
            _currentCanvasGroup.alpha = 0;
            _currentCanvasGroup.interactable = false;
            _currentCanvasGroup.blocksRaycasts = false;
            
            canvasGroupShow.alpha = 1;
            canvasGroupShow.interactable = true;
            canvasGroupShow.blocksRaycasts = true;

            _currentCanvasGroup = canvasGroupShow;
        }

        private CanvasGroup GetCanvasToShow()
        {
            return _currentPosition switch
            {
                0 => characterLoreController.CanvasGroup,
                1 => statSelectionController.CanvasGroup,
                2 => historyContextController.CanvasGroup,
                _ => throw new NotSupportedException()
            };
        }

        private void SetPlayerData()
        {
            _playerController.LoadCharacter(characterLoreController.CharacterName, CharacterType.Player);
            _playerController.Class = characterLoreController.SelectedClass;
            statSelectionController.SetCharacterStats(_playerController);
            _playerController.Health.CalculateHealth(10, 1, _playerController.Stats.constitution);
            ClassKit.GiveClassKit(_playerController);
            _playerController.Inventory.CurrentGold += 200;
            
            StaticVariables.HistoryContext = historyContextController.Context;
            StaticVariables.CharacterHistory = characterLoreController.CharacterHistory;
            StaticVariables.PlayerController = _playerController;
        }
    }
}