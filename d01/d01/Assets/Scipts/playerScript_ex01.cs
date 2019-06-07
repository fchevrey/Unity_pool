using UnityEngine;

public class playerScript_ex01 : MonoBehaviour {

	public float speed;
	public float jumpForce = 5;
	public int index = 1;
	private bool _canJump;
	private Rigidbody2D _rigid;

    private void OnDisable()
    {
        _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnEnable()
    {
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void ResetJump()
	{
		_canJump = true;
	}
	private void Start() 
	{
		_rigid = GetComponent<Rigidbody2D>();
		_canJump = true;
	}
	private void Move(Vector2 dir)
	{
		Vector2 velociy_tmp;

		velociy_tmp = _rigid.velocity;
		velociy_tmp.x = dir.x * speed * Time.fixedDeltaTime;
		_rigid.velocity = velociy_tmp;
	}
	private void Jump()
	{
		_rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		_canJump = false;
	}
	private void FixedUpdate() 
	{
		float x;

		x = Input.GetAxis("Horizontal");
		Move(new Vector2(x, 0));
	}
	void Update () 
	{

		if (Input.GetKeyDown(KeyCode.Space) && _canJump)
			Jump();

	}
}
