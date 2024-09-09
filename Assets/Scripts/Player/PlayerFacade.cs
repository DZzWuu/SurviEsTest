using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, IKill
{
    [Inject]
    private readonly SignalBus m_signalBus;

    public void Kill()
    {
        m_signalBus.Fire(new EndGameSignal());
        gameObject.SetActive(false);
    }
}
