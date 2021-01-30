using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Transform diggingHole;
    public float moveSpeed;

    //how long shovel hitbox appears when hitting
    public float hitTime;

    private Vector2 targetPosition;

    private SpaceshipPart diggablePart;

    public int carriedShipPartsCount;

    public int shovelMovesCount;

    public bool canDig;

    private bool dazzled = false;

    public float dazzleFactor = 0.5f;
    public int dazzleDuration = 4;

    Rigidbody2D rigidbody;

    public delegate void PlayerUsedShovelEvent();
    public event PlayerUsedShovelEvent OnPlayerUsedShovel;


    enum Direction {Up, Down, Left, Right};

    private Direction direction;

    // Start is called before the first frame update
    void Start()
    {

        canDig = true;

        targetPosition = new Vector2(0.0f, 0.0f);

        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Controls
        /*
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, 0.0f));
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        */


        float horizontal = Input.GetAxisRaw("Horizontal");
        float horizontalGamepad = Gamepad.current.leftStick.ReadValue().x;
        float vertical = Input.GetAxisRaw("Vertical");
        float verticalGamepad = Gamepad.current.leftStick.ReadValue().y;

        rigidbody.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        if(horizontal < 0 || horizontalGamepad < 0)
        {
            direction = Direction.Left;
        }
        else if (horizontal > 0 || horizontalGamepad > 0)
        {
            direction = Direction.Right;
        }
        else if (vertical < 0 || verticalGamepad < 0)
        {
            direction = Direction.Down;
        }
        else if (vertical > 0 || verticalGamepad < 0)
        {
            direction = Direction.Up;
        }

        //Digging with shovel
        if ((Input.GetKeyDown(KeyCode.X) || Gamepad.current.xButton.wasPressedThisFrame)  && shovelMovesCount > 0 && canDig){
            Tilemap ground = GameObject.Find("Ground").GetComponent<Tilemap>();

           Vector3Int tilePosition = ground.WorldToCell(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
            //ground.SetTile(tilePosition);

           Vector3 newPosition = ground.CellToWorld(tilePosition);

            Transform d = Instantiate(diggingHole) as Transform;

            d.position = new Vector3(newPosition.x + 0.1875f + 0.3125f, newPosition.y - 0.1875f - 0.3125f, 0);

            //If collided with diggable ship part -> Dig it out!
            if(diggablePart != null)
            {
                diggablePart.DigOut();
            }
            OnPlayerUsedShovel();
            shovelMovesCount--;
        }

        //Hit with shovel
        if ((Input.GetKeyDown(KeyCode.C) || Gamepad.current.aButton.wasPressedThisFrame) && shovelMovesCount > 0)
        {

            Transform shovelhitbox = null;

            if(direction == Direction.Left)
            {
              shovelhitbox = transform.Find("ShovelHitboxLeft");


            }
            else if (direction == Direction.Right)
            {
                shovelhitbox = transform.Find("ShovelHitboxRight");

            }
            else if (direction == Direction.Up)
            {
                shovelhitbox = transform.Find("ShovelHitboxUp");

            }
            else if (direction == Direction.Down)
            {
                shovelhitbox = transform.Find("ShovelHitboxDown");

            }

            shovelhitbox.gameObject.SetActive(true);
            OnPlayerUsedShovel();

            StartCoroutine(DeactivateShovelHitbox(shovelhitbox.gameObject));
        }


       /* if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0);

            //TODO: Animation left

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0);

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody.velocity = new Vector2(0, moveSpeed);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rigidbody.velocity = new Vector2(0, -moveSpeed);

        }*/
        



    }

    IEnumerator DeactivateShovelHitbox(GameObject hitbox)
    {
        yield return new WaitForSeconds(hitTime);
        AstarPath.active.Scan();
        hitbox.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceshipPart"))
        {
            Debug.Log("Collided with SpaceshipPart");
            diggablePart = collision.GetComponent<SpaceshipPart>();

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceshipPart"))
        {
            Debug.Log("UnCollided with SpaceshipPart");
            diggablePart = null;
        }
    }

    //Collect a ship part   
    public void GetShipPart()
    {
        carriedShipPartsCount++;
    }

    public void LoseShipPart()
    {
        carriedShipPartsCount--;
    }

    public void Dazzle()
    {
        if(!dazzled)
        {
            dazzled = true;
            moveSpeed *= dazzleFactor;
            // TODO: remove dazzle after X seconds
            StartCoroutine(RegainMovementSpeed());
        }
        if (shovelMovesCount > 0) shovelMovesCount--;
    }
    IEnumerator RegainMovementSpeed()
    {
        yield return new WaitForSeconds(dazzleDuration);

        dazzled = false;
        moveSpeed *= (1/dazzleFactor);

    }
}
