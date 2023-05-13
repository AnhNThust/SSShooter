using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float speed = 10f;
	public float Speed { get => speed; }

	[SerializeField]
	private float radius = 1f;

	private Touch touch;

	private void Update()
	{
		if (Input.touchCount <= 0)
		{
			Movement();
		}
		else
		{
			MovementByTouch();
		}
	}

	protected virtual void Movement()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
		newPos.z = transform.position.z;
		transform.parent.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
	}

	protected virtual void MovementByTouch()
	{
		touch = Input.GetTouch(0);

		float distance = Vector3.Distance(transform.position, touch.position);

		if (distance >= radius) return;
		transform.position = touch.position;
	}
}
