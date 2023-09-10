using UnityEngine;
using Excalibur.Physical;

public class GameView : Singleton<GameView>
{
    private Box _viewBox;
    private Vector2 _view;

    public void ConstructView()
    {
        _viewBox = new Box();
        Transform trans = Camera.main.transform;
        _viewBox.UpdateCenter(trans.position);
        //_view.y = mainCam.orthographicSize;
        //_view.x = Screen.width / Screen.height * _view.y;
    }

    public bool IsInSight(Vector2 position)
    {
        return IsInSightX(position.x) && IsInSightY(position.y);
    }

    public bool IsInSightX(float posX)
    {
        return Mathf.Abs(posX) <= _view.x;
    }

    public bool IsInSightY(float posY)
    {
        return Mathf.Abs(posY) <= _view.y;
    }
}