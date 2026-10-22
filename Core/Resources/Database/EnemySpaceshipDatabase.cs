using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class EnemySpaceshipDatabase : Database
{
	[Export]
	public Array<EnemySpaceshipData> EnemySpaceshipDataEntries { get; private set; }

	public EnemySpaceshipData GetSpaceshipDataByID(string ID) {
		var data = EnemySpaceshipDataEntries.FirstOrDefault(e => e.ID == ID);

		if (data == null)
			GD.PushWarning($"No enemy spaceship found by ID: {ID}");

		return data;
	}

	public Array<EnemySpaceshipData> GetSpaceshipDataByTier(int tier) {
		var dataArr = EnemySpaceshipDataEntries.Where(e => e.Tier == tier).ToArray();

		if (dataArr.Length == 0)
			GD.PushWarning($"No enemy spaceship found by tier: {tier}");

		// Converting System.Linq.Array to Godot.Collections.Array
		return new Array<EnemySpaceshipData>(dataArr);
	}
}
