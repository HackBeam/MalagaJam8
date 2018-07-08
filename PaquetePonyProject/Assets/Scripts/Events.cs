using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsChangedEvent
{
    public int playerId = -1;
    public int newHealthPowerUp = 0;
    public int newDamagePowerUp = 0;
    public int newSpeedPowerUp = 0;
    public StatsChangedEvent() { }
}

public class HealthChangedEvent
{
    public int playerId = -1;
    public float newCurrentHealthPercent = 0;
    public HealthChangedEvent() { }
}

public class DeathEvent
{
    public int deadPlayerId = -1;
    public DeathEvent() { }
}

public class GameOver
{
    public GameOver() { }
}

public class CenterScenaryEvent
{
    public Transform transformReference = null;
    public CenterScenaryEvent() { }
}

public class ScreenShakeEvent
{
    public ScreenShakeEvent () {}
}
