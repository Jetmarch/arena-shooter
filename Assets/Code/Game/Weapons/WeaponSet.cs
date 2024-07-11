namespace ArenaShooter.Weapons
{
    public class WeaponSet
    {
        private WeaponType _primaryWeapon;
        private WeaponType _secondaryWeapon;

        public WeaponType PrimaryWeapon { get { return _primaryWeapon; } set { _primaryWeapon = value; } }

        public WeaponType SecondaryWeapon { get => _secondaryWeapon; set => _secondaryWeapon = value; }
    }
}