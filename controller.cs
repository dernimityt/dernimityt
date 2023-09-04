using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public float jumpForce = 5f;
    private bool isGrounded;

    public AudioClip shootSound;
    public GameObject muzzleFlashPrefab;
    private AudioSource audioSource;

    void Start()
    {
        isGrounded = true;
        rb.freezeRotation = true; // Einschränkung der Rotation des Rigidbodies

        // AudioSource-Komponente hinzufügen und konfigurieren
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D Sound
    }

    void Update()
    {
        // Bewegung in alle Richtungen
        if (Input.GetKey("w"))
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }

        // Umschauen nach links und rechts
        if (Input.GetKey("h"))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey("k"))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        // Bewegung nach links und rechts
        if (Input.GetKey("a"))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        // Springen
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;
        }

        // Schießen
        if (Input.GetKeyDown(KeyCode.R))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Soundeffekt abspielen
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Schussanimation abspielen
        if (muzzleFlashPrefab != null)
        {
            Instantiate(muzzleFlashPrefab, transform.position, transform.rotation);
        }

        // Hier kannst du den Code zum Schießen implementieren
        // Zum Beispiel: Erzeugung eines Projektils
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            // Falls der Spieler mit einem anderen Objekt kollidiert, wird er gestoppt
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

public class EnemyAI : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Hier kannst du den Code für den Tod des Gegners implementieren
        // Zum Beispiel: Abspielen einer Todesanimation, Auslösen von Soundeffekten, Punktevergabe, etc.
        Destroy(gameObject);
    }
}
