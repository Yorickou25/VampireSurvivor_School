using UnityEngine;
using UnityEngine.SceneManagement;

namespace UILogique
{
    public class UI_MenuPause : MonoBehaviour
    {
        [SerializeField]
        private GameObject groupMenu;


        private void Awake()
        {
            UIMain.menuPause = this;
        }

        void Update()
        {
            if (GameStateManager.IsGameState(GameStateManager.GameState.GameOver) == true)
                return;

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (groupMenu.activeSelf == true)
                {
                    CloseMenuPause();
                }
                else
                {
                    OppenMenuPause();
                }
            }

        }

        #region MenuUI

        public void OppenMenuPause()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.Pause);

            Time.timeScale = 0f;

            groupMenu.SetActive(true);
            UIMain.menuPlayerInfo.OppenPlayerInfoUI();

        }
        public void CloseMenuPause()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.InGame);

            Time.timeScale = 1.0f;

            groupMenu.SetActive(false);
            UIMain.menuPlayerInfo.ClosePlayerInfoUI();

        }
        #endregion

        public void ReturnToMainMenu()
        {
            CloseMenuPause();

            GameStateManager.ChangeState(GameStateManager.GameState.Menu);

            SceneManager.LoadScene("MainMenu");
        }
    }
}