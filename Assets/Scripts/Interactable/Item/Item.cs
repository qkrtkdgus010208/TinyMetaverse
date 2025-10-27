using System.Collections.Generic;
using UnityEngine;

public enum ItemName { Sign, Box }

public class Item : GameEntity
{
    [SerializeField] private ItemName itemName;

    private void Start()
    {
        switch (itemName)
        {
            case ItemName.Sign:
                myName = "안내판";
                dialogueTexts = new List<string>
                {
                    "고대 문자라 읽을 수 없다..",
                };
                break;

            case ItemName.Box:
                myName = "보물상자";
                dialogueTexts = new List<string>
                {
                    "보물이 들어있을 것 같은 상자를 발견했다!!",
                    "열어볼까?",
                    "아무것도 없네..",
                };
                break;
        }
    }
}
