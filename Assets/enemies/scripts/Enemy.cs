using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

    [SerializeField]
    private float _speed;

    private Transform _player;
    private Rigidbody2D _rb2D;

    void Start()
    {
        _player = GameObject.Find("maguin").transform;
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb2D.velocity = (_player.position - transform.position).normalized * _speed;
    }

    public void TakeDamage() 
    {
        Debug.Log("Ouch");
        DestroyImmediate(this.gameObject);
    }
}