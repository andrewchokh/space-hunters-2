using Godot;
using Godot.Collections;

[GlobalClass]
public partial class PlayerDatabase : Resource
{
    [Export]
    public Array<PlayerSpaceshipData> PlayerDataList { get; private set; }
}
