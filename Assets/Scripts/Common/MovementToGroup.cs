using UnityEngine;

public class MovementToGroup : MonoBehaviour
{
	[SerializeField] private Transform target;
	public Transform Target { set => target = value; }

	[SerializeField] private Transform model;
	[SerializeField] private float rotSpeed = 10f;
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
		}

		if (transform.parent.position != target.position)
		{
			transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, moveSpeed * Time.deltaTime);
			LookAtTarget();
		}
		else
		{
			model.up = Vector3.MoveTowards(model.up, Vector3.down, rotSpeed * Time.deltaTime);
		}
	}

	public Transform GetTarget()
	{
		if (EnemySpawnerForGroup.Instance.Slots.Count <= 0) return null;

		return EnemySpawnerForGroup.Instance.Slots.Pop();
	}

	public void LookAtTarget()
	{
		Vector3 direction = target.position - model.position;
		if (direction != Vector3.zero)
		{
			model.up = direction;
		}
	}
}
