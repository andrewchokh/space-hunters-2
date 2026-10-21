using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PlayerSpaceshipDatabase : Database
{
    [Export]
    public Array<PlayerSpaceshipData> PlayerSpaceshipDataEntries { get; private set; }

    public PlayerSpaceshipData GetSpaceshipDataByID(string ID)
    {
        var data = PlayerSpaceshipDataEntries.FirstOrDefault(e => e.ID == ID);

        if (data == null)
            GD.PushWarning($"No enemy spaceship found by ID: {ID}");

        return data;
    }
}
