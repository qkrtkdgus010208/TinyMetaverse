using System.Collections.Generic;
using UnityEngine;

public enum PatrolNPCName { Zombie, Demon, Pumpkin, GateKeeper, }

public class PatrolNPC : MoveEntity
{
    [SerializeField] private PatrolNPCName nPCName;

    private void Start()
    {
        switch (nPCName)
        {
            case PatrolNPCName.Zombie:
                myName = "좀비";
                dialogueTexts = new List<string>
                {
                    "나는 좀비다!",
                    "물리기 싫으면 얼른 꺼져!",
                };
                break;

            case PatrolNPCName.Demon:
                myName = "악마";
                dialogueTexts = new List<string>
                {
                    "나는 악마다!",
                    "세 가지 소원을 들어주지..",
                    "소원을 다 빌면 너의 영혼을 가져가겠다..",
                };
                break;

            case PatrolNPCName.Pumpkin:
                myName = "호박괴물";
                dialogueTexts = new List<string>
                {
                    "나는 호박괴물이다!",
                    "사탕 하나 줄까?",
                };
                break;
        }
    }
}
