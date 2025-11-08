namespace BattleScripts.Enums
{
    public enum BattleStateEnum
    {
        PRE_BATTLE,
        WAITING,
        ACTIVE_TURN,
        // note: having a hard time wrapping my head around a "paused" state
        // where things are frozen etc. when the player pauses the game.
        RESOLVED,
    }
}