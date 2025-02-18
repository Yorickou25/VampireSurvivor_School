using TrinketLogique;

public class Candelabrador : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.area += 0.1f;
        playerWeapon.UpdateAllWeaponStats();
    }
}
