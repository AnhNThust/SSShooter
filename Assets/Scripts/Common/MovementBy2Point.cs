using UnityEngine;

public class MovementBy2Point : MonoBehaviour
{
	[SerializeField] private Transform beginPoint;
	[SerializeField] private Transform endPoint;
	[SerializeField] private float duration;

	protected virtual void Movement()
	{
		Vector3 path = Vector3.Lerp(beginPoint.position, endPoint.position, duration);
		path.Normalize();
	}
}
