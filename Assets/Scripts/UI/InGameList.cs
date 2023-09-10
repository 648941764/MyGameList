using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameListUI : SelectionUI
{
    private TextMeshProUGUI _titleText;
    protected override void AttachComponents()
    {
        selectionButton = selectionObject.GetComponent<Button>();
        _titleText = selectionObject.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    protected override string PrefabPath()
    {
        return "UI/SelectionButton";
    }
    protected override void InitUIData()
    {
        _titleText.color = selectionData.titleColor;
        _titleText.text = selectionData.title;
    }
}

public class InGameList : MonoBehaviour
{
    private SelectionList _selectionList;
    [SerializeField] private Transform _GameListParent;
    [SerializeField] private GameObject selection;
    private void Awake()
    {
        InitGameList();
    }

    private void InitGameList()
    {
        _selectionList = new SelectionList(_GameListParent);
        GameListUI goldMiner = new GameListUI();
        goldMiner.Instantiate(new SelectionData()
        {
            title = "黄金矿工",
            titleColor = new Color(1f, 1f, 0f),
            onSelectedAction =() => 
            {
                Debug.Log("打开黄金矿工");
                SceneManager.LoadScene(2);
            }
        });
        _selectionList.AddSelection(goldMiner);

        GameListUI flappyBrid = new GameListUI();
        flappyBrid.Instantiate(new SelectionData()
        {
            title = "FlappyBrid",
            titleColor = new Color(0f, 0.8f, 1f),
            onSelectedAction = () =>
            {
                Debug.Log("打开FlappyBrid");
                SceneManager.LoadScene(1);
            }
        });
        _selectionList.AddSelection(flappyBrid);

        GameListUI Quit = new GameListUI();
        Quit.Instantiate(new SelectionData()
        {
            title = "返回主页面",
            titleColor = Color.white,
            onSelectedAction = () =>
            {
                Debug.Log("返回主页面");
                _GameListParent.gameObject.SetActive(false);
                selection.gameObject.SetActive(true);
            }
        });
        _selectionList.AddSelection(Quit);

    }
}

