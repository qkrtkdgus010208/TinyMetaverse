using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable nearestInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearestInteractable != null)
        {
            nearestInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GameEntity"))
        {
            nearestInteractable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GameEntity"))
        {
            nearestInteractable = null;
        }
    }
}
