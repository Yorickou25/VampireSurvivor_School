using TrinketLogique;
public class Attractorb : Trinket
{
    protected override void TrinketEffect()
    {
        ItemMagnet itemMagnet = PlayerMain.instance.transform.GetComponentInChildren<ItemMagnet>();
        itemMagnet.magnetRange += 1;
        itemMagnet.UpdateMagnetRange();
    }
}
