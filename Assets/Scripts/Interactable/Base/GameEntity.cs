using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueTitleText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    protected string myName;

    public bool IsPlayerNearby { get; protected set; }

    public bool IsInteracting { get; protected set; }

    protected List<string> dialogueTexts;
    private int currentIndex;

    // PlayerInteraction 스크립트에서 대화를 시작할 때 호출
    public void Interact()
    {
        if (IsPlayerNearby && !IsInteracting)
        {
            ShowDialogue();
            return;
        }

        if (IsInteracting)
        {
            NextDialogue();
        }
    }

    protected void ShowDialogue()
    {
        if (!dialogueUI || !dialogueText || dialogueTexts.Count == 0)
            return;

        IsInteracting = true;
        dialogueUI.SetActive(true);
        currentIndex = 0;
        dialogueTitleText.text = myName;
        dialogueText.text = dialogueTexts[currentIndex];
    }

    protected void NextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogueTexts.Count)
            dialogueText.text = dialogueTexts[currentIndex];
        else
            HideDialogue();
    }

    protected virtual void HideDialogue()
    {
        dialogueUI.SetActive(false);
        IsInteracting = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            IsPlayerNearby = true;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerNearby = false;
            HideDialogue();
        }
    }
}
