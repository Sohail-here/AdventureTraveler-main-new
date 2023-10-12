using AdventureTraveler.Scroll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using static UnDestroyData;

namespace AdventureTraveler
{
    [System.Serializable] public class EventForJourneys : UnityEvent<Journeys> { }

    public class GlobalPanelManager : MonoBehaviour
    {
        [SerializeField] GlobalPanelScrollView globalPanelScrollView;
        

        void Start()
        {

        }

        public void GetJourneysData(Journeys journeys)
        {
            globalPanelScrollView.UpdateData(journeys.msg);
        }
    }
}