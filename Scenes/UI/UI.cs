using Godot;
using System;
using R3;

[SceneTree]
public partial class UI : Control
{
	public ReactiveProperty<int> Score { get; } = new(0);

	[ExportGroup("Dependencies")] 
	[Export] private Label ScoreLabel { get; set; } = null!;

	public override void _Ready()
	{
		Score.Subscribe(this, (score, self) => self._.ScoreCounter.Text = score.ToString());
	}
}
