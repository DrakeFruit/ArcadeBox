using System;
using Sandbox;

public sealed class PongBall : Component, Component.ICollisionListener
{
	[Property] int Speed = 650;
	Vector2 Velocity;

	protected override void OnStart()
	{
		Velocity = Vector2.Random.Normal * Speed;
	}
	protected override void OnFixedUpdate()
	{
		//apply velocity
		Transform.Position += new Vector3(Velocity.x, Velocity.y, 0) * Time.Delta;
	}

	void ICollisionListener.OnCollisionStart(Collision other)
	{
		//reverse velocity
		Velocity = -Velocity;
	}

	void PointScored(Connection Player)
	{

	}

	void StartGame()
	{

	}
}
