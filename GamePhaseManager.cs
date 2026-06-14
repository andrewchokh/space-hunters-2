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
				break;
			case GamePhase.BossFight:
				break;
		}

		OnPhaseChanged?.Invoke(Phase);
	}
}
