using ArenaShooter.Projectiles;
using ArenaShooter.Units.Player;
using UnityEngine;

namespace ArenaShooter.Artefacts
{
    public class BulletsPassThroughMechanic
    {
        private PlayerFacade _playerFacade;

        public BulletsPassThroughMechanic(IPlayerProvider playerProvider)
        {
            _playerFacade = playerProvider.Player;
        }

        public void SetProjectileHitCount(ProjectileFacade projectileFacade, GameObject owner)
        {
            if (owner != _playerFacade.gameObject) return;

            projectileFacade.DestroyOnHitMechanic.SetCountOfHitBeforeDestroy(int.MaxValue);
        }
    }
}