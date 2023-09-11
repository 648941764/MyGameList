using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGame : Game
{
    [SerializeField] Text _currentText;
    [SerializeField] Text _highestText;

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

    protected override void OnBegin()
    {
        base.OnBegin();
        ModelManager.Instance.GetModel<PickupModel>().ResetScore();
    }
}
