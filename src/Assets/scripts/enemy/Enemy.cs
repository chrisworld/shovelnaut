using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using static Pathfinding.AIDestinationSetter;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private bool holdsPart = false;
    private bool isWaiting = false;

    private AIDestinationSetter aIDestinationSetter;
    private Animator anim;

    [SerializeField]
    private SpaceshipPart _part;
    private Player _player;
    [SerializeField]
    private float _playerHearDistance = 5.0f;
    [SerializeField]
    private int _idleTimeAfterHide = 90;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.FindObjectOfType<Player>();
        _player.OnPlayerUsedShovel += HandlePlayerUsedShovel;


        aIDestinationSetter = this.GetComponent<AIDestinationSetter>();

        anim = GetComponent<Animator>();
        bool isInitialIdle = aIDestinationSetter.targetType == TargetType.Idle;
        SetWalkingAnimation(!isInitialIdle);

        if (!aIDestinationSetter.target && aIDestinationSetter.targetType != TargetType.Idle)
            GetNewTarget(aIDestinationSetter.targetType);
    }


    void FixedUpdate()
    {
    }

    private void GetNewTarget(TargetType type)
    {
        if(type == TargetType.Idle)
        {
            return;
        }

        if (type == TargetType.EnemyAim)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(type.ToString());
            float dist = float.PositiveInfinity;
            GameObject nearestTarget = null;
            foreach (var t in targets)
            {
                // TODO: somehow restrict to only open aims?
                Vector2 dist2d = new Vector2(this.transform.position.x - t.transform.position.x,
                    this.transform.position.y - t.transform.position.y);
                var d = dist2d.sqrMagnitude;
                if (d < dist && !t.GetComponent<EnemyAim>().hasHiddenPart)
                {
                    dist = d;
                    nearestTarget = t;
                }
            }
            if (nearestTarget)
                aIDestinationSetter.setTarget(nearestTarget.transform, type);
            else
            {
                aIDestinationSetter.setTarget(null, TargetType.Idle);
            }

        }
        else 
        {
            // shouled here only be player or spaceship
            GameObject tar = GameObject.FindGameObjectWithTag(type.ToString());
            if (tar)
            {
                aIDestinationSetter.setTarget(tar.transform, type);
            }
        }
        aIDestinationSetter.targetType = type;
    }

    private void HandlePlayerUsedShovel()
    {
        // TODO: check if 
        float dist2d = Vector2.Distance((Vector2)transform.position, (Vector2)_player.transform.position);
        if (dist2d < _playerHearDistance)
        {
            StartFollowingPlayer();
        }
    }

    private void StartFollowingPlayer()
    {
        SetWalkingAnimation(true);
        aIDestinationSetter.setTarget(_player.transform, TargetType.Player);
    }

    private void SetWalkingAnimation(bool walk)
    {
        anim.SetBool("isWalking", walk);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Return parts to ship
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.Dazzle();
            if(aIDestinationSetter.targetType == TargetType.Player)
            {
                GetNewTarget(TargetType.Spaceship);
            }
            Debug.Log("Collided with player");
            // TODO: set player dazzle or something like that?
        } else  if (collision.CompareTag("Spaceship") && !holdsPart && aIDestinationSetter.targetType == TargetType.Spaceship)
        {
            // Debug.Log("Collided with spaceship");
            Spaceship spaceship = collision.GetComponent<Spaceship>();
            spaceship.StealPart();
            holdsPart = true;
            GetNewTarget(TargetType.EnemyAim);
            // TODO: remove spaceship part
        } else if(collision.CompareTag("EnemyAim") && holdsPart)
        {
            // Debug.Log("Collided with EnemyAim");
            EnemyAim aim = collision.GetComponent<EnemyAim>();
            if(holdsPart)
            {
                aim.AddSpaceshipPart();
                holdsPart = false;
                SpaceshipPart newPart = Instantiate(_part, aim.transform.position, Quaternion.identity);
                newPart.DigIn();
            }
            isWaiting = true;
            StartCoroutine(WaitForNextSteal());
        }
        else if (collision.CompareTag("Enemy"))
        {
            // GameObject otherEnemy = collision.gameObject;
            // TODO: maybe find some sort of avoidance?
            StartCoroutine(CheckIfStuck());
        }
        else
        {
            // Debug.LogWarning("Collided with " + collision);
        }
    }

    IEnumerator CheckIfStuck()
    {
        // TODO: maybe try to unstuck them somehow..
        yield return new WaitForSeconds(2);
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5)
        {
            AstarPath.active.Scan();
        }
    }

    IEnumerator WaitForNextSteal()
    {
        SetWalkingAnimation(false);
        yield return new WaitForSeconds(_idleTimeAfterHide);
        SetWalkingAnimation(true);
        isWaiting = false;
        GetNewTarget(TargetType.Spaceship);
    }
}
