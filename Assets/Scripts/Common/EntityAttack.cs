using System.Collections;
using UnityEngine;

public abstract class EntityAttack : MonoBehaviour
{
	[SerializeField] private float shootDelay = 1f;
	public float ShootDelay { get => shootDelay; set => shootDelay = value; }

	private void Reset()
	{
		ResetValue();
	}

	protected IEnumerator Shoot()
	{
		while (true)
		{
			yield return new WaitForSeconds(shootDelay);
			Attack();
		}
	}

	/// <summary>
	/// Nếu muốn thay đổi giá trị ban đầu của các tham số
	/// </summary>
	protected abstract void ResetValue();

	/// <summary>
	/// Mỗi đối tượng lại có 1 cách thức tấn công khác nhau
	/// </summary>
	protected abstract void Attack();
}
