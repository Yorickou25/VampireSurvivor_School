using TrinketLogique;

public class Duplicator : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerWeapon playerWeapon = PlayerMain.playerWeapon;
        playerWeapon.playerBaseWeaponStats.amount += 1;
        playerWeapon.UpdateAllWeaponStats();
    }
}
