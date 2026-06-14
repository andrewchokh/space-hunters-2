using Godot;
using System;
using System.Threading.Tasks;

public partial class GamePhaseManager : Node2D
{
	[Export]
	public GamePhase Phase;
	public event Action<GamePhase> OnPhaseChanged;

	public void ChangePhase(GamePhase newPhase)
	{
		if (Phase == newPhase)
			return;

		Phase = newPhase;

		switch (newPhase)
		{
			case GamePhase.Wave:
				break;
			case GamePhase.Rest:
				_ = HandleRestPhase();
				break;
			case GamePhase.BossFight:
				break;
		}

		OnPhaseChanged?.Invoke(Phase);
	}

	private async Task HandleRestPhase()
	{
		if (Phase != GamePhase.Rest)
			return;

		await ToSignal(GetTree().CreateTimer(5.0f), SceneTreeTimer.SignalName.Timeout);

		int bossThreshold = 2500 * (GameSessionManager.Instance.DefeatedBosses + 1);
		ChangePhase(GameSessionManager.Instance.Score >= bossThreshold ? GamePhase.BossFight : GamePhase.Wave);
	}
}
