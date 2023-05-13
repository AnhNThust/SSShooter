using UnityEngine;

public abstract class EntityFly : MonoBehaviour
{
	[SerializeField] private float flySpeed = 10f;
	public float FlySpeed { get => flySpeed; set => flySpeed = value; }

	[SerializeField] private int damage = 1;
	public int Damage { get => damage; set => damage = value; }

	[SerializeField] private bool isFollowTarget = false;
	public bool IsFollowTarget { get => isFollowTarget; set => isFollowTarget = value; }

	public abstract void Fly();
}
