using TrinketLogique;
public class Wings : Trinket
{
    protected override void TrinketEffect()
    {
        PlayerMovment playerMovment = PlayerMain.playerMovment;
        playerMovment.speed += 0.3f;
    }
}