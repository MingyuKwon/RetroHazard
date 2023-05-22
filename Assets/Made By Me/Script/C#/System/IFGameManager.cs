using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFGameManager
{    
    void SetPlayerMove(bool flag);
    void SetPausePlayer(bool flag);

    void ResetPlayerAnimationState();
}
