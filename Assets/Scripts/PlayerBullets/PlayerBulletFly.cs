using UnityEngine;

public class PlayerBulletFly : EntityFly
{
  private void FixedUpdate()
  {
    Fly();
  }

  public override void Fly()
  {
    transform.parent.Translate(FlySpeed * Time.deltaTime * Vector3.up);
  }
}
