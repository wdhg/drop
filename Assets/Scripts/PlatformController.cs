using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformController : MonoBehaviour {

    [HideInInspector]
    public bool landedOn;
    public Sprite white;
    [HideInInspector]
    public bool isDamaging;

    private static float turnRedLength = 1.2f;
    private float currentColorValue;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        rb.gravityScale = 0f;
        currentColorValue = 0f;
    }

    private void Move() {
        rb.velocity = new Vector2(0f, Globals.moveUpSpeed);
    }

    private void FixedUpdate() {
        if(isDamaging) {
            sr.color = Color.Lerp(Color.white, Color.red, currentColorValue);
            currentColorValue += (1 / turnRedLength) * Time.fixedDeltaTime;
        }
    }

    private void Update() {
        Move();

        if (landedOn) {
            if(isDamaging) {
                FindObjectOfType<PlayerController>().Die();
            } else if (sr.sprite != white) {
               sr.sprite = white;
            }
        }
    }
}
