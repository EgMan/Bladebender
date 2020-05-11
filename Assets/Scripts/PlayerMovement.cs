using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;

    public float initialJumpBoost = 50f;
    public float jumpForce = 200f;
    public float runForce = 500f;
    private Vector2 jumpVector;
    private Vector2 runVector;
    private float jumpScalar;
    private float runScalar;

    private int collisionCount = 0;
    private bool initialJump = false;
    private Vector2 up = new Vector2(0f, 1f);
    private bool jumpQueued = false;
    void Start()
    {
        jumpVector = new Vector2(0, jumpForce);
        runVector = new Vector2(runForce, 0);
        jumpScalar = runScalar = 0f;
    }
    void FixedUpdate()
    {
        if (jumpScalar != 0f)
        {
            if (initialJump)
            {
                body.AddForce(initialJumpBoost * Time.deltaTime * jumpScalar * jumpVector);
                collisionCount = 0;
                initialJump = false;
                jumpQueued = false;
            }
            else
            {
                body.AddForce(Time.deltaTime * jumpScalar * jumpForce * up);
            }
        }
        if (runScalar != 0f)
        {
            body.AddForce(Time.deltaTime * runScalar * runVector);
        }
    }
    //TODO add force add to fixedupdate
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            runScalar = 1f;
            setDirectionFacing(false);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            runScalar = -1f;
            setDirectionFacing(true);
        }
        else
        {
            runScalar = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (collisionCount != 0)
            {
                jumpScalar = 1f;
                initialJump = true;
            }
            else
            {
                jumpQueued = true;
            }
        }
        else if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (collisionCount != 0)
            {
                jumpScalar = 1f;
                if (jumpQueued)
                {
                    initialJump = true;
                }
            }
        }
        else
        {
            jumpScalar = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpQueued = false;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Knife.allAntiGrav();
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        Vector2 furthestUp = other.contacts[0].normal;
        float dotProduct = Vector2.Dot(up, furthestUp);
        for (int i = 1; i < other.contacts.Length; i++)
        {
            Vector2 upCandidate = other.contacts[i].normal;
            float upCandidateDotProduct = Vector2.Dot(furthestUp, up);
            if (upCandidateDotProduct > dotProduct)
            {
                furthestUp = upCandidate;
                dotProduct = upCandidateDotProduct;
            }
        }
        float rad = Mathf.Atan2(furthestUp.x, furthestUp.y);
        if (Mathf.Abs(rad * Mathf.Rad2Deg) < 91f)
        {
            furthestUp = angleUp(furthestUp, rad);
        }
        jumpVector = jumpForce * furthestUp;
        collisionCount = other.contactCount;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        collisionCount = other.contactCount;
    }

    private Vector2 angleUp(Vector2 vector, float rad)
    {
        float rotate = -0.55f * rad;
        return new Vector2(Mathf.Sin(rad + rotate), Mathf.Cos(rad + rotate));
    }

    private void setDirectionFacing(bool right)
    {
        if (right)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
