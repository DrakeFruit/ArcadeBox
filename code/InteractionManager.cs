using System;
using Sandbox;

public sealed class InteractionManager : Component
{
	[Property] List<GridSquare> Squares = null;
	[Property] WinScreen Winner { get; set; }
	bool GameOver = false;
	bool IsXsTurn = true;
	string Turn = "X";
	protected override void OnStart()
	{
		Mouse.Visible = true;
	}
	protected override void OnFixedUpdate()
    {
        //trace a ray at the mouse position
        var tr = Scene.Trace.Ray((Scene.Camera.ScreenPixelToRay( Mouse.Position )), 1000f).Run();
		if ( !GameOver && tr.GameObject != null && Input.Pressed("Attack1") )
		{
			tr.GameObject.Components.Get<GridSquare>().content = Turn;
			IsXsTurn = !IsXsTurn;
		}

		//this is awful and I feel bad
		if ( IsXsTurn ) {
			Turn = "X";
		} else {
			Turn = "O";
		}
		
		//this is even worse
		if( Squares[0].content == Squares[1].content && Squares[1].content == Squares[2].content && Squares[2].content != ""||
			Squares[3].content == Squares[4].content && Squares[4].content == Squares[5].content && Squares[5].content != ""|| 
			Squares[6].content == Squares[7].content && Squares[7].content == Squares[8].content && Squares[8].content != ""||
			Squares[0].content == Squares[3].content && Squares[3].content == Squares[6].content && Squares[6].content != ""||
			Squares[1].content == Squares[4].content && Squares[4].content == Squares[7].content && Squares[7].content != ""||
			Squares[2].content == Squares[5].content && Squares[5].content == Squares[8].content && Squares[8].content != ""||
			Squares[0].content == Squares[4].content && Squares[4].content == Squares[8].content && Squares[8].content != ""||
			Squares[2].content == Squares[4].content && Squares[4].content == Squares[6].content && Squares[6].content != ""){
				if( !GameOver ) Winner.Text = IsXsTurn ? "O Wins!" : "X Wins!";
				GameOver = true;
			} 

	}
}