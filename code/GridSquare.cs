using System.Diagnostics;
using Sandbox;

public sealed class GridSquare : Component
{
	[Property] TextRenderer Renderer { get; set; }
	public string content = "";

	protected override void OnFixedUpdate()
	{
		switch (content)
		{
			case "":
				
				break;
			case "X":
				Renderer.Text = "X";
				break;
			case "O":
				Renderer.Text = "O";
				break;
		}
	}
}