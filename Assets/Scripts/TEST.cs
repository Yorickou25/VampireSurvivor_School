using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField]
    private GameObject animationGameobjectList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            PlayExplosion(new Vector3(1, 2, 0));
    }

    public void PlayExplosion(Vector3 position)
    {
        print("ererererereererererererereererr");
        animationGameobjectList.GetComponent<Animator>().Play("Animation_WhipSlash", 0,0);

    }
}
