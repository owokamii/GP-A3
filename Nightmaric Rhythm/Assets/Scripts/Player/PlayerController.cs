using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem dust;
    public Vector2 raycastAngle;

    private Vector2 moveDirection;
    private bool wasMoving = false;

    void Update()
    {
        if(!(PauseMenu.GameIsPaused))
        {
            ProcessInputs();

            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                raycastAngle = Vector2.left;
            }
            else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                raycastAngle = Vector2.right;
            }
            else if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                raycastAngle = Vector2.up;
            }
            else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                raycastAngle = Vector2.down;
            }

            if (moveDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero && !wasMoving)
        {
            PlayDust();
            wasMoving = true;
        }
        else if (moveDirection == Vector2.zero)
        {
            wasMoving = false;
        }
    }

    void PlayDust()
    {
        dust.Play();
    }
}
