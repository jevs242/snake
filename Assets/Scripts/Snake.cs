using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]private GameObject _objectTail;
    [SerializeField]private float _frameRates = 0.2f; //Velocity the Frame
    [SerializeField]private float _step = 0.64f; //Sprites scale for moving
    [SerializeField]private List<Transform> _tail = new List<Transform>(); //Add unlimited transformations

    private GameManager _gameManager;
    private int _direction = 1;
    private Vector3 _lastPos;
    public int score = 0;

    enum Direction
    {
        up,   //0
        down, //1
        left, //2
        right //3
    };

    Direction direction;

    void Start()
    {
        Time.timeScale = 1;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("Move", _frameRates, _frameRates);
        direction = Direction.left; //Start moving to the left
    }

    void Move() //Function -> Moving the head in every frame
    {
        _lastPos = transform.position;
        Vector3 nextPos = Vector3.zero;
        if (direction == Direction.up)
        {
            nextPos = Vector3.up;
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (direction == Direction.down)
        {
            nextPos = Vector3.down;
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction == Direction.left)
        {
            nextPos = Vector3.left;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction == Direction.right)
        {
            nextPos = Vector3.right;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        nextPos *= _step;
        transform.position += nextPos;

        MoveTail();

    }

    void MoveTail() //Function -> The Tail Follow the head in every frame
    {
        for(int i = 0; i < _tail.Count; i++)
        {
            Vector3 temp = _tail[i].position;
            _tail[i].position = _lastPos;
            _lastPos = temp;
        }
    }

    void Update()
    {
        InputPlayer(Input.GetKeyDown(KeyCode.W) ,Input.GetKeyDown(KeyCode.S),Input.GetKeyDown(KeyCode.A),Input.GetKeyDown(KeyCode.D));
        ChangeDirection();
    }

    private void InputPlayer(bool Up , bool Down , bool Left , bool Right)
    {
        if(Up || Down)
        {
            if(_direction != 3 && _direction != 4)
            {
                _direction = Down ? 3 : 4;
            }
        }
        else if (Left || Right)
        {
            if (_direction != 1 && _direction != 2)
            {
                _direction = Right ? 2 : 1;
            }
        }
    }

    private void ChangeDirection()
    {
        if (_direction == 1)//Left
        {
            direction = Direction.left;
        }
        else if (_direction == 2)//Right
        {
            direction = Direction.right;

        }
        else if(_direction == 3)//Down
        {
            direction = Direction.down;

        }
        else if (_direction == 4) //Up
        {
            direction = Direction.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Funcion -> Trigger
    {
        if(collision.gameObject.CompareTag("Apple"))
        {
            score++;
            _tail.Add(Instantiate(_objectTail,_tail[_tail.Count - 1].position,Quaternion.identity).transform); //Add in List
            _gameManager.SpawnApple();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Snake") || collision.gameObject.CompareTag("Wall"))
        {
            Time.timeScale = 0;
            _gameManager.GameOver(_tail.Count, score);
        }
    }
}
