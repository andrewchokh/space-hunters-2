using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class EnemyDatabase : Resource
{
	[Export]
	public Array<EnemySpaceshipData> EnemyDataList { get; private set; }

	public EnemySpaceshipData GetEnemyDataByID(string ID) {
		var data = EnemyDataList.FirstOrDefault(e => e.ID == ID);

		if (data == null) 
			GD.PushWarning($"No enemy spaceship found by ID: {ID}");

		return data;
	} 

	public Array<EnemySpaceshipData> GetEnemiesDataByTier(int tier) {
		var dataArr = EnemyDataList.Where(e => e.Tier == tier).ToArray();

		if (dataArr.Length == 0)
			GD.PushWarning($"No enemy spaceship found by tier: {tier}");

		// Converting System.Linq.Array to Godot.Collections.Array
		return new Array<EnemySpaceshipData>(dataArr);
	}
}
