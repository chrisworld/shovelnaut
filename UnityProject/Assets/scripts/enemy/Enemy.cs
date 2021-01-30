using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using static Pathfinding.AIDestinationSetter;

public class Enemy : MonoBehaviour
{
    private GameObject[] aims;
    private GameObject nearestAim;
    private GameObject[] parts;
    private GameObject nearestPart;

    private bool holdsPart = false;
    private bool aggressive = false;

    private AIDestinationSetter aIDestinationSetter;


    UnityEvent m_MyEvent = new UnityEvent();

    [SerializeField]
    private SpaceshipPart _part;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (aims == null)
        {
            aims = GameObject.FindGameObjectsWithTag("EnemyAim");
            
        }
        if (parts == null)
        {
            parts = GameObject.FindGameObjectsWithTag("SpaceshipPart");
        }
        FindNearestObjects(); */

        Animator anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        aIDestinationSetter = this.GetComponent<AIDestinationSetter>();
        if (!aIDestinationSetter.target)
            GetNewTarget(aIDestinationSetter.targetType);
    }


    void FixedUpdate()
    {
    }

    private void GetNewTarget(TargetType type)
    {
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
                aIDestinationSetter.setTarget(nearestTarget.transform);

            // TODO: no target left?


        }
        else 
        {
            // shouled here only be player or spaceship
            aIDestinationSetter.setTarget(GameObject.FindGameObjectWithTag(type.ToString()).transform);
        }
        aIDestinationSetter.targetType = type;
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

            GetNewTarget(TargetType.Spaceship);
            // TODO: Drop/hide spaceship part
        } else
        {
            // Debug.LogWarning("Collided with " + collision);
        }
    }
}
