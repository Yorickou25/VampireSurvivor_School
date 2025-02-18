using TrinketLogique;

public class HollowHeart : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerHealth playerHealth = PlayerMain.playerHealth;
        playerHealth.maxHp *= 1.2f;
    }
}