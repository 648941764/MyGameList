public enum EventName
{
    Nothing = 0,

    KeyDownInput,
    KeyInput,
    KeyUpInput,

    /* PickupGame Begin */
    PickupAppleEscape,
    PickupGameStageChange,
    PickupScoreChange,
    PickupHighestScoreChange,
    /* PickupGame End */

    GameOver,

    All,
}