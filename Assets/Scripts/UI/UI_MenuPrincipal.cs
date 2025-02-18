using UnityEngine;
using UnityEngine.SceneManagement;

namespace UILogique
{
    public class UI_MenuPrincipal : MonoBehaviour
    {
        private void Awake()
        {
            UIMain.menuPrincipal = this;
        }

        public void ButtonStart()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.InGame);

            SceneManager.LoadScene("MainGame");

        }

        public void ButtonOption()
        {
            Debug.Log("fonction ButtonOption non implémenter !");
        }

        public void ButtonQuit()
        {
            //ScoreManager.SaveScores();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }




    public static class UIMain
    {
        public static UI_MenuPrincipal menuPrincipal;
        public static UI_MenuPause menuPause;
        public static UI_MeneGameOver menuMeneGameOver;
        public static UI_MenuLevelUp menuLevelUp;
        public static UI_PlayerInfo menuPlayerInfo;
    }
}