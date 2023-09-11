using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGame : Game
{
    [SerializeField] Text _currentText;
    [SerializeField] Text _highestText;

    [SerializeField] Character[] _characters;

    protected override void Awake()
    {
        base.Awake();
        EnrollEvents(_ =>
        {
            switch (_.eventName)
            {
                case EventName.PickupScoreChange:
                    _currentText.text = _.Get<string>();
                    break;
                case EventName.PickupHighestScoreChange:
                    _highestText.text = _.Get<string>();
                    break;
            }
        });
    }

    public override void Begin()
    {
        base.Begin();
        ModelManager.Instance.GetModel<PickupModel>().ResetScore();
        ModelManager.Instance.GetModel<PickupModel>().ResetPlayer();
        int i = -1;
        while (++i < _characters.Length)
        {
            _characters[i].Begin();
        }
    }
}
