using Godot;
using System;
using System.Threading.Tasks;

/// <summary>
/// Controls the high-level gameplay loop by transitioning between distinct phases:
/// <see cref="GamePhase.Wave"/>, <see cref="GamePhase.Rest"/>, and <see cref="GamePhase.BossFight"/>.
/// </summary>
public partial class GamePhaseManager : Node2D
{
	[Export]
	public GamePhase Phase;
	public event Action<GamePhase> OnPhaseChanged;

	/// <summary>
	/// Transitions the game to the specified phase and notifies all subscribers.
	/// Has no effect if the requested phase is already active.
	/// </summary>
	/// <param name="newPhase">The phase to transition into.</param>
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

	/// <summary>
	/// Waits for the rest duration to elapse, then determines the next phase based on the current score.
	/// </summary>
	/// <remarks>
	/// After the timer expires, the method checks whether the player's score meets the boss spawn threshold.
	/// If the phase was externally changed during the wait, the transition is aborted.
	/// </remarks>
	private async Task HandleRestPhase()
	{
		if (Phase != GamePhase.Rest)
			return;

		await ToSignal(GetTree().CreateTimer(5.0f), SceneTreeTimer.SignalName.Timeout);

		int bossThreshold = 2500 * (GameSessionManager.Instance.DefeatedBosses + 1);
		ChangePhase(GameSessionManager.Instance.Score >= bossThreshold ? GamePhase.BossFight : GamePhase.Wave);
	}
}
