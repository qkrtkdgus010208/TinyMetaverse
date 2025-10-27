
public interface IInteractable
{
    // Player와 상호작용을 시작할 때 호출하는 메서드
    void Interact();

    // 플레이어가 근처에 있는지 여부
    bool IsPlayerNearby { get; }

    // 현재 상호작용이 진행 중인지 (대화창이 열린 상태인지)
    bool IsInteracting { get; }
}
