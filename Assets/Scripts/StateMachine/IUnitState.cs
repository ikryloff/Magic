using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitState 
{
    void Enter();
    void Tick();
    void Exit();

}
