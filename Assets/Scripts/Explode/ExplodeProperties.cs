using UnityEngine;

namespace Assets.Scripts.Explode
{
	public class ExplodeProperties : EntityProperties
	{
		[SerializeField] private ExplodeType explodeType;
		public ExplodeType ExplodeType { get => explodeType; set => ExplodeType = value; }

		protected override void ResetValue()
		{
			Hp = 0;
			Damage = 0;
		}
	}
}
