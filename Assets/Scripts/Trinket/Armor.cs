using TrinketLogique;

public class Armor : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerHealth playerHealth = PlayerMain.playerHealth;
        playerHealth.armor += 1;
    }
}