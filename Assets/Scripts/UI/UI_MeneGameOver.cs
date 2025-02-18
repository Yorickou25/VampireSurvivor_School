using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UILogique
{
    public class UI_MeneGameOver : MonoBehaviour
    {
        [SerializeField]
        private GameObject groupMenu;

        [SerializeField]
        private TextMeshProUGUI titre;


        private void Awake()
        {
            UIMain.menuMeneGameOver = this;
        }


        #region MenuUI
        [ContextMenu("ManualOppen")]
        public void OppenMenuGameOver()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.GameOver);
            Time.timeScale = 0f;

            groupMenu.SetActive(true);
        }
        public void CloseMenuGameOver()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.InGame);
            Time.timeScale = 1.0f;

            groupMenu.SetActive(false);

        }

        public void OppenMenuVictoire()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.GameOver);
            Time.timeScale = 0f;

            titre.text = "Victoire";

            groupMenu.SetActive(true);
        }
        #endregion

        public void Restart()
        {
            CloseMenuGameOver();

            GameStateManager.ChangeState(GameStateManager.GameState.InGame);
              
            SceneManager.LoadScene("MainGame");
        }

        public void ReturnToMainMenu()
        {
            CloseMenuGameOver();

            GameStateManager.ChangeState(GameStateManager.GameState.Menu);

            SceneManager.LoadScene("MainMenu");
        }
    }
}