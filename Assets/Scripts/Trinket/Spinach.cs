using TrinketLogique;

public class Spinach : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.damage += 0.1f;
        playerWeapon.UpdateAllWeaponStats();
    }
}
