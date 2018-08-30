using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isMoving;
    public Vector3 movementTarget;
    private float turnDelay = 1f;
    private Facing facing;
    private float transitionSpeed;
    private GameController gameController;
    private LevelData levelData;

	// Use this for initialization
	void Start () {
        movementTarget = transform.position;
        isMoving = false;
        if(gameController == null)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            turnDelay = gameController.turnDelay;
            transitionSpeed = gameController.movementSpeed;
        }
        if (levelData == null)
        {
            levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        Vector2 movement = Vector2.zero;
        if (!isMoving)
        {
            bool _attemptMove = false;
            Vector3 _movementTarget = Vector3.zero;
            if (_horizontal > 0)
            {
                facing = Facing.Right;
                _movementTarget = transform.position + new Vector3(-1, 0, 0);
                _attemptMove = true;
            }
            else if (_horizontal < 0)
            {
                facing = Facing.Left;
                _movementTarget = transform.position + new Vector3(1, 0, 0);
                _attemptMove = true;
            }
            else if (_vertical > 0)
            {
                facing = Facing.Up;
                _movementTarget = transform.position + new Vector3(0, 0, -1);
                _attemptMove = true;
            }
            else if (_vertical < 0)
            {
                facing = Facing.Down;
                _movementTarget = transform.position + new Vector3(0, 0, 1);
                _attemptMove = true;
            }
            if (_attemptMove)
            {
                if (levelData.isMoveValid(_movementTarget))
                {
                    StartCoroutine(TakeTurn(_movementTarget));
                }
            }
        }
        
        float step = transitionSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,movementTarget, step);

    }

    IEnumerator TakeTurn(Vector3 _movementTarget)
    {
        switch (facing)
        {
            case Facing.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
            case Facing.Left:
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case Facing.Up:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case Facing.Down:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
        }
        isMoving = true;
        movementTarget = _movementTarget;
        yield return new WaitForSeconds(turnDelay);
        isMoving = false;
        gameController.TakeTurn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision " + collision.tag);
        if(collision.tag.Equals("LevelExit"))
        {
            string nextLevelName = collision.gameObject.GetComponent<LevelExit>().nextLevelName;
            gameController.loadLevel(nextLevelName);
        }
        if(collision.tag.Equals("Enemy"))
        {
            gameController.reloadLevel();
        }
        if (collision.tag.Equals("CollapsingFloor"))
        {
            CollapsingFloor cf = collision.gameObject.GetComponent<CollapsingFloor>();
            if(cf.hasPlayerStepedOn)
            {
                Destroy(collision.gameObject);
            } else
            {
                cf.hasPlayerStepedOn = true;
            }
        }
    }
}
