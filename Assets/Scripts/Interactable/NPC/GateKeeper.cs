using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateKeeper : GameEntity
{
    [SerializeField] private GameObject choiceUI;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(Enter);
        exitButton.onClick.AddListener(Exit);
    }

    private void Start()
    {
        myName = "문지기";
        dialogueTexts = new List<string>
        {
            "나는 이 던전의 문지기다!",
            "던전에 입장할 자격이 있는지 시험해보겠다.",
        };
    }
   
    protected override void HideDialogue()
    {
        base.HideDialogue();
        if (IsPlayerNearby)
            choiceUI.SetActive(true);
    }

    public void Enter()
    {
        GameManager.Instance.LoadMiniGameScene();
    }

    public void Exit()
    {
        choiceUI.SetActive(false);
    }
}
