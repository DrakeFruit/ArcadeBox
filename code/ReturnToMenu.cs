public sealed class ReturnToMenu : Component
{
	protected override void OnUpdate()
	{
		if ( Input.EscapePressed )
		{
			Input.EscapePressed = false;
			Game.ActiveScene.LoadFromFile( "scenes/menu.scene" );
		}
	}
}
