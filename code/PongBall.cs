using Sandbox;

public sealed class PongBall : Component
{
	protected override void OnFixedUpdate()
	{
		Transform.Position += new Vector3(-1, 0, 0);
	}
}
