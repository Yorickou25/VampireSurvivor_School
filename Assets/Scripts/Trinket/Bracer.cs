using TrinketLogique;

public class Bracer : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.speed += 0.1f;
        playerWeapon.UpdateAllWeaponStats();
    }
}
