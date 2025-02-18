using TrinketLogique;

public class Spellbinder : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.duration += 0.1f;
        playerWeapon.UpdateAllWeaponStats();
    }
}
