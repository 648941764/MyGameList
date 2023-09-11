using Excalibur.Physical;
using System;
using System.Collections.Generic;
using System.Numerics;

public class PhysicalManager : Singleton<PhysicalManager>
{
    private readonly List<IPhysicalObject> _physicalComponents = new List<IPhysicalObject>();
    private readonly List<IPhysicalObject> _addList = new List<IPhysicalObject>();
    private readonly List<IPhysicalObject> _delList = new List<IPhysicalObject>();
    private int _current, _target;

    public void PhysicalUpdate()
    {
        if (_addList.Count > 0)
        {
            _physicalComponents.AddRange(_addList);
            _addList.Clear();
        }

        if (_delList.Count > 0)
        {
            _current = -1;
            while (++_current < _delList.Count)
            {
                int index = _physicalComponents.IndexOf(_delList[_current]);
                _physicalComponents[index] = _physicalComponents[_physicalComponents.Count - 1];
                _physicalComponents.RemoveAt(_physicalComponents.Count - 1);
            }
            _delList.Clear();
        }

        if (_physicalComponents.Count == 0) { return; }

        _current = -1;
        _target = -1;
        while (++_current < _physicalComponents.Count)
        {
            IPhysicalObject current = _physicalComponents[_current];
            while (++_target < _physicalComponents.Count)
            {
                if (_target == _current) { continue; }
                IPhysicalObject target = _physicalComponents[_target];
                if (current.PhysicalComponent.HitPoint2D(target.PhysicalComponent.position))
                {
                    if (current.CollisionTags.Contains(target.Tag))
                    {
                        current.OnCollisionWith(target);
                    }
                }
            }
        }
    }

    public void Add(IPhysicalObject target)
    {
        _addList.Add(target);
    }

    public void Del(IPhysicalObject target)
    {
        _delList.Add(target);
    }
}