using TrinketLogique;

public class Pummarola : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerHealth playerHealth = PlayerMain.playerHealth;
        playerHealth.recovery += 0.2f;
    }
}