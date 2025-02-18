using TrinketLogique;

public class EmptyTome : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.delay -= 0.08f;
        playerWeapon.UpdateAllWeaponStats();
    }
}
