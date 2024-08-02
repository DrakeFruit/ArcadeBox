using Sandbox.Network;
using System.Threading.Tasks;
using System;
using Sandbox;
using System.Dynamic;


[Title( "Pong Networker" )]
[Category( "Networking" )]
[Icon( "electrical_services" )]
public sealed class PongNetworker : Component, Component.INetworkListener
{
	/// <summary>
	/// Create a server (if we're not joining one)
	/// </summary>
	[Property] public bool StartServer { get; set; } = true;

	/// <summary>
	/// The prefab to spawn for the player to control.
	/// </summary>
	[Property] public GameObject PlayerPrefab { get; set; }

	/// <summary>
	/// A list of points to choose from randomly to spawn the player in. If not set, we'll spawn at the
	/// location of the NetworkHelper object.
	/// </summary>
	[Property] public List<GameObject> SpawnPoints { get; set; }

	public List<PlayerData> Players { get; set; } = new();

	public struct PlayerData
	{
		public Connection connection;
		public int Score;
	}

	protected override async Task OnLoad()
	{
		if ( Scene.IsEditor )
			return;

		if ( StartServer && !GameNetworkSystem.IsActive )
		{
			LoadingScreen.Title = "Creating Lobby";
			await Task.DelayRealtimeSeconds( 0.1f );
			GameNetworkSystem.CreateLobby();
		}
	}

	/// <summary>
	/// A client is fully connected to the server. This is called on the host.
	/// </summary>
	public void OnActive( Connection channel )
	{
		Log.Info( $"Player '{channel.DisplayName}' has joined the game" );
		Players.Add( new PlayerData { connection = channel, Score = 0 } );

		if ( PlayerPrefab is null )
			return;

		//
		// Find a spawn location for this player
		//
		GameObject start;
		if (channel.IsHost) start = SpawnPoints.First(); else start = SpawnPoints.Last();

		// Spawn this object and make the client the owner
		var player = PlayerPrefab.Clone( start.Transform.World, name: $"Player - {channel.DisplayName}" );
		player.NetworkSpawn( channel );
	}
}
