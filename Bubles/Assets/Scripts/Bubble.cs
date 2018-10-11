using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [Range(1, 10)] public float MaxSizeModifer = 1;
    public float spawnDownOffset = 1;
    public float speedModifer = 1;

    private int _score = 1;
    private float _sizeModifer = 1;
    private float _force = 200;
    private Vector3 _gameAreaSize;

    private Vector3 _pos;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;

    #region Properties

    public Vector3 Position
    {
        get { return _pos; }
        set { _pos = value; }
    }

    public SpriteRenderer Render
    {
        get { return _spriteRenderer; }
    }

    public Collider2D Collider
    {
        get { return _collider; }
    }

    public Rigidbody2D Rigid
    {
        get { return _rigidbody2D; }
    }

    public int GetScore
    {
        get { return _score; }
    }

    #endregion

    public void Move(float timeModifer)
    {
        Rigid.AddForce(new Vector2(0, _force * timeModifer * speedModifer * (1 / _sizeModifer)));
    }

    public void Burst()
    {
        //TODO: Animation of destroy
        Destroy(gameObject);
    }

    void Awake()
    {
        _pos = transform.position;
        _collider = gameObject.GetComponent<Collider2D>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // Setup bubble's size & score points
        _sizeModifer = Random.Range(1f, MaxSizeModifer);
        this.transform.localScale += transform.localScale * _sizeModifer;
        _score = (int)(MaxSizeModifer - _sizeModifer);
        // Setup bubble's color
        Render.material.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        // Setup bubble's start position
        _gameAreaSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 startPos = new Vector3(Random.Range(-_gameAreaSize.x, _gameAreaSize.x), -spawnDownOffset - _gameAreaSize.y / 2);
        if (Mathf.Abs(startPos.x) + Collider.bounds.size.x / 2 > _gameAreaSize.x)
        {
            if (startPos.x > 0)
            {
                startPos.x -= Collider.bounds.size.x / 2;
            }
            else
            {
                startPos.x += Collider.bounds.size.x / 2;
            }
        }
        transform.position = startPos;
    }
}
