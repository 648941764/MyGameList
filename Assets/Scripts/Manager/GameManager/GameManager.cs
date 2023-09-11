using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoSingleton<GameManager>
{
    float dt;

    private Game _currentGame;
    private Scene _currentScene;

    protected override void Start()
    {
        base.Start();
        ModelManager.Instance.InstantiateModel();
        GameView.Instance.ConstructView();
        StartCoroutine(StartGame(ModelManager.Instance.GetModel<PickupModel>()));
    }

    private IEnumerator StartGame(GameModel gameModel)
    {
        if (_currentGame != null)
        {
            _currentGame.OnEnd();
        }
        AsyncOperation operation;
        if (_currentScene.isLoaded)
        {
            operation = SceneManager.UnloadSceneAsync(_currentScene);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
        operation = SceneManager.LoadSceneAsync(gameModel.GetSceneName(), LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            yield return null;
        }
        _currentScene = SceneManager.GetSceneByName(gameModel.GetSceneName());
        GameObject[] gameObjects = _currentScene.GetRootGameObjects();
        int i = -1;
        while (++i < gameObjects.Length)
        {
            _currentGame = gameObjects[i].transform.GetComponent<Game>();
            if (_currentGame != null)
            {
                _currentGame.OnBegin();
                break;
            }
        }
    }

    protected override void Update()
    {
        GameUpdate();
    }

    private void GameUpdate()
    {
        dt = Time.deltaTime;
        _UpdateInput();
        _UpdateTime();
        CharacterManager.Instance.CharacterUpdate(dt);
    }

    private void _Broadcast(EventParam param)
    {
        EventManager.Instance.Broadcast(param, false);
    }
}