using UnityEngine;

public class MovementToGroup : MonoBehaviour
{
	[SerializeField] private Transform target;
	public Transform Target { set => target = value; }

	[SerializeField] private Transform model;
	[SerializeField] private float rotSpeed = 200f;
	[SerializeField] private float moveSpeed = 2f;

	private void Awake()
	{
		model = transform.parent.GetChild(0);
	}

	private void Update()
	{
		if (target == null)
		{
			target = GetTarget();
			//LookAtTarget();
		}

		if (transform.parent.position != target.position)
		{
			transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, moveSpeed * Time.deltaTime);
			//model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.Euler(0f, 0f, )
			LookAtTarget();
		}
		else
		{
			model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.Euler(0f, 0f, 180f), rotSpeed * Time.deltaTime);
		}
	}

	public Transform GetTarget()
	{
		if (EnemySpawnerForGroup.Instance.Slots.Count <= 0) return null;

		return EnemySpawnerForGroup.Instance.Slots.Pop();
	}

	public void LookAtTarget()
	{
		float dx = target.position.x - transform.parent.position.x;
		float dy = target.position.y - transform.parent.position.y;
		float angle = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
		float newAngle = transform.parent.position.x > target.position.x
			? model.eulerAngles.z - angle 
			: model.eulerAngles.z + angle;
		model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.Euler(0, 0, newAngle), rotSpeed * Time.deltaTime);
	}
}
