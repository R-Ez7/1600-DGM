using UnityEngine;
using System.Collections;
// class set to abstract so inheriting scripts can complete them.
public abstract class Movers : MonoBehaviour {
	// start function set to protected virtual so the inheriting playerController and enemy scripts can override it
	protected virtual void Start () 
	{
		boxColl = GetComponent<BoxCollider2D> ();
		rigBod = GetComponent<Rigidbody2D> ();
		modMoveTime = 1f / moveTime;

	}
	// OnCantMove function set to abstract so it can be completed by inheriting classes. 
	// Sets up Scenario for inability to move
	protected abstract void OnCantMove <T> (T component)
		where T : Component;

	// AttemptMove function set to virtual so it can be overidden
	protected virtual void AttemptMove <T> (int leftRight, int upDown)
		where T : Component
	{
		RaycastHit2D hit;
		bool canMove = Move (leftRight, upDown, out hit);
		
		if (hit.transform == null)
			return;
		
		T hitComponent = hit.transform.GetComponent<T> ();
		
		if (!canMove && hitComponent != null)
			OnCantMove (hitComponent);
	}
	//move function allows movement when true, no movement when false
	protected bool Move (int leftRight, int upDown, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (leftRight, upDown);

		boxColl.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxColl.enabled = true;

		if (hit.transform == null)
		{
			StartCoroutine(SmoothMovement (end));
			return true;
		}

		return false;
	}
	//SmoothMovement function allows movement from one floortile to the next
	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards (rigBod.position, end, modMoveTime * Time.deltaTime);
			rigBod.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}
	// public variables
	public float moveTime = 0.1f;
	public LayerMask blockingLayer;
	// private variables
	private BoxCollider2D boxColl;
	private Rigidbody2D rigBod;
	private float modMoveTime;
}
