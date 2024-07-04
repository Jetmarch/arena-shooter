using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class GunShellParticlesController : IInitializable, ILateDisposable
    {
        private ParticleSystem _gunShellParticles;
        private BaseWeaponShootMechanic _shootMechanic;

        public GunShellParticlesController(ParticleSystem gunShellParticles, BaseWeaponShootMechanic shootMechanic)
        {
            _gunShellParticles = gunShellParticles;
            _shootMechanic = shootMechanic;
        }

        public void Initialize()
        {
            _shootMechanic.ShootComplete += _gunShellParticles.Play;
        }

        public void LateDispose()
        {
            _shootMechanic.ShootComplete -= _gunShellParticles.Play;
        }
    }
}