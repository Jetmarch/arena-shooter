using UnityEngine;


namespace ArenaShooter.Mechanics
{
    public interface IInteractable
    {
        bool CanInteract();
        void Interact();
    }
}