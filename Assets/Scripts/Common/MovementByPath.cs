using UnityEngine;

public class MovementByPath : MonoBehaviour
{
	[SerializeField] private Transform path;

	[SerializeField] private Node[] pathNode;
	public Node[] PathNode { get => pathNode; set => pathNode = value; }

	[SerializeField] private float moveSpeed = 1f;
	public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

	[SerializeField] private float rotSpeed = 100f;
	public float RotSpeed { get => rotSpeed; set => rotSpeed = value; }

	[SerializeField] private Vector3 currentPositionHolder;

	[SerializeField] private int currentNode;
	public int CurrentNode { get => currentNode; set => currentNode = value; }

	private void Reset()
	{
		ResetValue();
	}

	private void Awake()
	{
		Reset();
		LoadPathNode();
	}

	private void Start()
	{
		CheckNode();
	}

	/// <summary>
	/// Check current node and move to it
	/// Save node position to currentPositionHolder
	/// </summary>
	protected virtual void CheckNode()
	{
		currentPositionHolder = pathNode[currentNode].transform.position;
	}

	private void Update()
	{
		// if position of player not equal position of node, move player to it
		if (transform.parent.position != currentPositionHolder)
		{
			LookNode();
			transform.parent.position = Vector3.MoveTowards(transform.parent.position, currentPositionHolder, moveSpeed * Time.deltaTime);
		}
		else // if equal, player go to next node
		{
			if (currentNode < pathNode.Length - 1)
			{
				currentNode++;
				CheckNode();
			}
			else
			{
				DespawnAndResetNode();
			}
		}
	}

	private void LookNode()
	{
		float dy = currentPositionHolder.y - transform.parent.position.y;
		float dx = currentPositionHolder.x - transform.parent.position.x;
		float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
		float newAngle = transform.parent.rotation.z + angle + 90;
		transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(0, 0, newAngle), rotSpeed * Time.deltaTime);
	}

	protected virtual void DespawnAndResetNode()
	{
		// for override
	}

	protected virtual void ResetValue()
	{
		// for override
	}

	protected virtual void LoadPathNode()
	{
		pathNode = path.GetComponentsInChildren<Node>();
	}
}
