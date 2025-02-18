using System;

static public class GameStateManager
{
    public enum GameState
    {
        Menu,
        InGame,
        GameOver,
        Pause
    }

    static private GameState gameState;

    static public Action a_OnMenuState;
    static public Action a_OnInGameState;
    static public Action a_OnGameOverState;
    static public Action a_OnPauseState;


    static public GameState GetGameState()
    {
        return gameState;
    }
    static public bool IsGameState(GameState input)
    {
        return gameState == input;
    }

    static public void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                gameState = GameState.Menu;
                a_OnMenuState?.Invoke();
                return;

            case GameState.InGame:
                gameState = GameState.InGame;
                a_OnInGameState?.Invoke();
                return;

            case GameState.GameOver:
                gameState = GameState.GameOver;
                a_OnGameOverState?.Invoke();
                return;

            case GameState.Pause:
                gameState = GameState.Pause;
                a_OnPauseState?.Invoke();
                return;
        }
    }

}
