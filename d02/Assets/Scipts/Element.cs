using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour 
{
	[SerializeField]private int _life;
	[SerializeField]private int _lifeMax;
	public string elementName;
    private bool _isDead = false;
	/* private void Start() 
	{
		_lifeMax = _life;
	}*/
    
	public bool TakeDamage()
	{
		_life -= 1;
		if (_life <= 0)
		{
			Debug.Log(elementName + " Has died");
            if (!_isDead)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                if (sprite != null)
                    sprite.enabled = false;
                Invoke("destroy_invoke", 1.1f);
                _isDead = true;
                return (true);
            }
		}
		Debug.Log(elementName + "[" + _life + "/" + _lifeMax + "]HP has been attacked");
		return false;
	}
    public void destroy_invoke()
    {
		Destroy(this.gameObject);
    }
}
