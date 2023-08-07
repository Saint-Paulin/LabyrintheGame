using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap collisionTilemap;
    Vector2 initialPos;
    Vector2 newPos;
    PlayerInputAction controls;
    Rigidbody2D rb;
    Animator animator;
    CircleCollider2D playerCollider;
    [SerializeField] GameObject playerSpriteInstance;
    GameSession gameSession;

    bool isAlive = true;
    string directionPos;

    void Awake()
    {
        controls = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        gameSession = FindAnyObjectByType<GameSession>();
    }

    void OnEnable()
    {
	    controls.Enable();
    }

    void OnDisable()
    {
	    controls.Disable();
    }

    private void Start()
    {
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        //controls.Player.Move.WASD;
    }

    void Move(Vector2 direction)
    {
        if (!isAlive) { return; }
        initialPos = transform.position;
        if (CanMove(direction))
            //Replicat();
            transform.position += (Vector3)direction;
            newPos = transform.position;
            // Debug.Log(transform.position);
            //Debug.Log(CalPosition());
            AnimSprite(CalPosition());
            //Debug.Log(transform.position);
    }

    string CalPosition()
    {
        //Debug.Log(initialPos.x + " " + newPos.x);
        if (initialPos.x < newPos.x && initialPos.y == newPos.y)
        {
            directionPos = "Right";
        }
        else if (initialPos.x == newPos.x && initialPos.y < newPos.y)
        {
            directionPos = "Up";
        }
        else if (initialPos.x > newPos.x && initialPos.y == newPos.y)
        {
            directionPos = "Left";
        }
        else if (initialPos.x == newPos.x && initialPos.y > newPos.y)
        {
            directionPos = "Down";
        }
        return directionPos;
    }

    void AnimSprite(string switchCase)
    {
        switch (switchCase)
            {
                case "Right":
                    animator.SetTrigger("isRight");
                    break;
                case "Left":
                    animator.SetTrigger("isLeft");
                    break;
                case "Up":
                    animator.SetTrigger("isBehind");
                    break;
                case "Down":
                    animator.SetTrigger("isFront");
                    break;
            }
    }

    bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition)) { return false; }
        else { return true; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Door"))) { Doors(); }
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("FakeDoors")))
        {
            Destroy(other.gameObject);
            FakeDoors();
        }
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Instance"))) { Debug.Log("Instance"); }
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            // gameSession.LoadLevel("GameOver");
            if (!isAlive) { return; }
            FindObjectOfType<AudioPlayer>().PlayHitClip();
            FindObjectOfType<ScoreKeeper>().DecreaseScore(50);
            StartCoroutine(gameSession.RelaodGame(0.5f));
            isAlive = false;
        }
        // var othertag = new Regex(other.tag);
        //string pattern = @"* [0-9]";
        // Match match = Regex.Match(other.tag, pattern);
        // if (match.Success) { Debug.Log("ok");}
        //Debug.Log(othertag.Match("FakeDoors [0-9]").ToString());
        // if (othertag.Match("FakeDoors [0-9]")) {FakeDoors(); }
    }

    void FakeDoors()
    {
        // audio
    }

    [ContextMenu("Replicat")]
    void Replicat()
    {
        Instantiate(playerSpriteInstance, transform.position, transform.rotation);
        // Game Over
    }

    void Doors()
    {
        gameSession.ResetGameSession();
        gameSession.NextLevel();
    }
}
