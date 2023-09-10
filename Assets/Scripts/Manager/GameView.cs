using UnityEngine;
using Excalibur.Physical;

public class GameView : Singleton<GameView>
{
    private Box _viewBox;

    public void ConstructView()
    {
        _viewBox = new Box();
        Transform trans = Camera.main.transform;
        _viewBox.UpdateCenter(trans.position);
        Vector2 view;
        view.y = Camera.main.orthographicSize;
        view.x = Screen.width / Screen.height * view.y;
        _viewBox.UpdateExtents(view);
}

    public bool IsInSight(Vector2 position)
    {
        return _viewBox.ContainsPoint2D(position);
    }

    public Vector3 GetViewCenter() => _viewBox.center;

    /// <returns>元组结构</returns>
    public (float, float) GetRangeHorizontal()
    {
        float half = _viewBox.extents.x / 2f;
        return (-half, half);
    }

    /// <returns>元组结构</returns>
    public (float, float) GetRangeVertical()
    {
        float half = _viewBox.extents.y / 2f;
        return (-half, half);
    }
}