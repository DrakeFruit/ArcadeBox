using System;
using Sandbox;

public sealed class PongBall : Component
{
	[Property] int Speed = 650;
	Vector3 Velocity;
	Connection Owner;
	SceneTraceResult trCollide;
	PongNetworker networker;
	protected override void OnStart()
	{
		networker = Scene.Components.GetInChildrenOrSelf<PongNetworker>();
		Velocity = new Vector3(Vector2.Random.Normal * Speed, 0);
	}
	protected override void OnFixedUpdate()
	{
		trCollide = Scene.Trace.Sphere( 5f, Transform.Position, Transform.Position + Velocity.Normal )
			.IgnoreGameObjectHierarchy( GameObject )
			.Run();
		
		//collision
		if ( trCollide.Hit ) 
		{
			if ( trCollide.GameObject.Tags.Has("paddle") ) Owner = trCollide.GameObject.Network.OwnerConnection;
			Velocity = Vector3.Reflect( Velocity, trCollide.Normal );
		}

		//score points
		if ( trCollide.GameObject.Tags.Has("goal") ) PointScored( Owner );

		//apply velocity
		Transform.Position += new Vector3(Velocity.x, Velocity.y, 0) * Time.Delta;
	}

	void PointScored(Connection Player)
	{
		//TODO fix this shit
		if( networker.Players[0].connection == Player ) ; //set player score
	}

	void StartGame()
	{

	}
}
