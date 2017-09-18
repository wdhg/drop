using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public float gravity;
    public float moveSpeed;
    public Vector2 spawnPosition;
    public float moveToSpawnSpeed;
    public float particleSpawnRate;
    public AudioSource landSFX;
    public AudioSource dieSFX;
    [HideInInspector]
    public bool alive;
    [HideInInspector]
    public UnityEvent OnPlayerSpawn, OnPlayerDeath;
    [HideInInspector]
    public bool readyToSpawn;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int bestScore;

    private Rigidbody2D rb;
    private ParticleSystem ps;
    private float nextParticleSpawnTime;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
        bestScore = PlayerPrefs.GetInt("Best Score");
    }

    public void Spawn() {
        score = 0;
        alive = true;
        rb.gravityScale = gravity;
        readyToSpawn = false;
        OnPlayerSpawn.Invoke();
    }

    public void Die() {
        dieSFX.Play();
        alive = false;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        ps.Clear();
        if (score > bestScore) {
            bestScore = score;
            PlayerPrefs.SetInt("Best Score", bestScore);
            PlayerPrefs.Save();
        }
        OnPlayerDeath.Invoke();
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(transform.position, new Vector2(.5f, .1f), 180f, Vector2.down, 1f);
    }

    private void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        // Emit particles when moving on a platform
        if(Time.time > nextParticleSpawnTime && horizontal != 0f && IsGrounded()) {
            ps.Emit(1);
            nextParticleSpawnTime += particleSpawnRate;
        }

        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    private void MoveToSpawn() {
        transform.position = Vector2.Lerp(transform.position, spawnPosition, moveToSpawnSpeed * Time.deltaTime);
    }

    private void LockParticlesToBounds() {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);
        for(int i = 0; i < particles.Length; i++) {
            if(particles[i].position.y > Globals.maxYPos) {
                particles[i].remainingLifetime = 0;
            }
        }
        ps.SetParticles(particles, particles.Length);
    }

    private void Update() {
        LockParticlesToBounds();

        if(alive) {
            Move();
        } else if(Vector2.Distance(transform.position, spawnPosition) > 0.1f) {
            MoveToSpawn();
        } else {
            readyToSpawn = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        PlatformController pc = coll.gameObject.GetComponent<PlatformController>();
        if(!pc.landedOn) {
            pc.landedOn = true;
            if (!coll.collider.GetComponent<PlatformController>().isDamaging) {
                score++;
                landSFX.Play();
            }
        }
    }
}
