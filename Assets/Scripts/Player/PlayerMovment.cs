using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{
    [HideInInspector]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    public float speed;

#pragma warning disable IDE1006 // Styles d'affectation de noms
    public Vector2 facedDirection { get; private set; } = Vector2.zero;
#pragma warning restore IDE1006 // Styles d'affectation de noms


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(GameStateManager.IsGameState(GameStateManager.GameState.InGame) == false)
            return;
        
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;      
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        if(direction != Vector2.zero)
            facedDirection = direction.normalized;


        rb.velocity = (Vector3)direction.normalized * speed;



        if(animator != null)
        {
            // Vérifie si le personnage bouge
            bool isMoving = rb.velocity.magnitude > 0.1f;
            if(facedDirection.x < 0)
                animator.transform.GetComponent<SpriteRenderer>().flipX = true;
            else if (facedDirection.x > 0)
                animator.transform.GetComponent<SpriteRenderer>().flipX = false;
            // Active ou désactive l'animation de marche
            animator.SetBool("isWalking", isMoving);
        }
    }
}
