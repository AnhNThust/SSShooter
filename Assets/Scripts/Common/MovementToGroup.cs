using UnityEngine;

public class MovementToGroup : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Transform Target { set => target = value; }

    [SerializeField] private Transform model;
    [SerializeField] private float rotSpeed = 100f;

	private void Awake()
	{
		model = transform.parent.GetChild(0);
	}

	private void Update()
	{
        if (transform.parent.position != target.position)
        {
            LookAtTarget();
        }
        else
        {
            model.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
	}

	public void LookAtTarget()
    {
        float dx = target.position.x - transform.parent.position.x;
        float dy = target.position.y - transform.parent.position.y;
        float angle = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
        float newAngle = model.rotation.z + angle + 90;
        model.rotation = Quaternion.RotateTowards(model.rotation, Quaternion.Euler(0, 0, newAngle), rotSpeed * Time.deltaTime);
    }
}
