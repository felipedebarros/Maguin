﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerActions : MonoBehaviour {

	[SerializeField]
	private float _speed;
	[SerializeField]
	private LayerMask _enemyLayer;
	[SerializeField]
	private GameObject _fire;

	private float _horz, _vert;
	private bool _zBtn;
	
	private Rigidbody2D _rb2D;
	private Animator _anim;

	void Start () 
	{
		_rb2D = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
	}
	
	void Update () 
	{
		HandleInput();
	}

	void HandleInput()
	{
		_horz = Input.GetAxisRaw("Horizontal");
		_vert = Input.GetAxisRaw("Vertical");

		Move(_horz, _vert);

		_zBtn = Input.GetKeyDown(KeyCode.Z);
		if(_zBtn) Attack(_horz, _vert);		
	}

	void Move(float h, float v)
	{
		_anim.SetInteger("horz", (int) h);
		_anim.SetInteger("vert", (int) v);		
		Vector2 dir = new Vector2(h, v);
		_rb2D.velocity = dir.normalized * _speed;
	}

	void Attack(float h, float v)
	{
		Vector3 dir = new Vector3(h, v, 0);
		if(h == 0f && v == 0f) 
			dir.x = 1f;
		var hit = Physics2D.BoxCastAll(dir.normalized + transform.position, Vector2.one, 0f, Vector2.up, 1f, _enemyLayer);
		_fire.SetActive(true);
		_fire.transform.position = dir.normalized + transform.position;
		foreach(RaycastHit2D e in hit)
		{
			Enemy enemy = e.transform.GetComponent<Enemy>();
			if(enemy != null)
			{
				enemy.TakeDamage();
			}
		}
	}
}