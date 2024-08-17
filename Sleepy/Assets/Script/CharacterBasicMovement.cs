using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBasicMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float horizontal; //horizontal movement
    private float speed = 3f;
    private float jumpingPower = 5f;
    private bool isFacingRight = true;

    private bool doubleJump;

    [SerializeField] private Rigidbody2D rb;
    public bool grounded; //is on ground
    public bool running = false; // not running

    [SerializeField] private Image staminaBar;

    private float stamina = 100.0f;
    private float maxStamina = 100.0f;
    private float runCost = 45f;
    private float chargeRate = 20f;

    private Coroutine recharge;

    private void Update()
    {

        //Walking
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        if (Input.GetButton("Horizontal"))
        {
            FindObjectOfType<AudioManager>().Play("Walk");
        }
        //Checking for double jump
        CheckDJ();

        //Double Jump
        DoubleJump();

        //Jumping
        Jump();

        //Flipping the character
        Flip();

        //Running
        Run();

        //Checking if running
        if (running)
        {
            //changing stamina UI
            stamina -= runCost * Time.deltaTime;
            //Stoping running when no stamina
            if (stamina < 0) { stamina = 0; speed = 8f; }

            //Filling stamina UI
            staminaBar.fillAmount = stamina / maxStamina;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
    }

    //walking
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //Checking if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    //Checking if the character is grounded
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    //Checking for Double Jump
    private void CheckDJ()
    {
        if (grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
    }

    //Dounle Jump
    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;

                FindObjectOfType<AudioManager>().Play("Jump");
            }
        }
    }

    //Jumping
    private void Jump()
    {
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    //Flipping the character
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    //Recharging stamina
    private IEnumerator RechargeStamina()
    {
        //waiting
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)
        {
            //charging stamina
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

    //Running
    private void Run()
    {
        //Checking if the key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            speed = 6f;
        }
        //Checking if the key is pressed
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            speed = 3f;
        }
    }
}