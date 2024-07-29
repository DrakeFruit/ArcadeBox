using Sandbox;

public sealed class PaddleController : Component
{
	[Property] public float Speed { get; set; } = 10;
	protected override void OnFixedUpdate()
	{
		Transform.Position += new Vector3( 0, Input.AnalogMove.x * Speed, 0 );
	}
}
