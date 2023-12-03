using TMPro;
using UnityEngine;

public enum Speeds { Note_1 = 0, Note_2 = 1, Fast = 2};

public class Notes : MonoBehaviour
{
    public Speeds currentSpeed;
    public Transform player;
    public Transform note;
    public Rigidbody2D rb;
    public TMP_Text miss;

    float[] speedValues = { 8f, 10.5f, 3f};

    public PolygonCollider2D noteCollider;
    bool triggered = false;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if(gameObject.CompareTag("Note_1"))
        {
            //transform.position += Vector3.right * speedValues[(int)currentSpeed] * Time.deltaTime;
            //transform.position += new Vector2(1f, 0f) * speedValues[(int)currentSpeed] * Time.deltaTime;
            Vector3 dir = (player.position - note.position).normalized;
            rb.AddForce(dir * speedValues[(int)currentSpeed] * Time.deltaTime, (ForceMode2D)ForceMode.VelocityChange);
        }

        else if (gameObject.CompareTag("Note_2"))
        {
            Vector3 dir = (player.position - note.position);
            rb.AddForce(dir * speedValues[(int)currentSpeed] * Time.deltaTime, (ForceMode2D)ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if(!triggered)
            {
                noteCollider.enabled = false;
                Invoke("Reappear", 0.9f);
            }
        }
    }

    void Reappear()
    {
        triggered = true;
        noteCollider.enabled = true;
    }
}
