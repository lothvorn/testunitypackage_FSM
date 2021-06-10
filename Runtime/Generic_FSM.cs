
using System;

using System.Collections.Generic;

using UnityEngine;


    /// <summary>
    /// Generic definition for a Finite State Machine. At the core is a list of states and a system to jump from one state
    /// to anohter by calling GoToState method and providing the type of the target state.
    ///
    /// About transitions: note that there is no explicit implementation for transitions in the FSM. Transitions will be
    /// needed only if when entering into a specific state could require differents behaviours. In case this is needed
    /// any number for basic transitions can be implemented as GenericStates overriding only GenericState.Enter and, at
    /// the end of that method, including a jump to the target state. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Generic_FSM<T>
    {

        public void UpdateCurrentState()
        {
            m_current.Update();
        }
        /// <summary>
        /// To set up the FSM by adding new details. Note that the first state provided when built will be the initial state.
        /// </summary>

        public void AddState(GenericState<T> newState)
        {
            m_allStates.Add(newState.GetType(), newState);
            if (m_allStates.Count == 1)
            {
                GoToState(newState.GetType());
            }
        }

        /// <summary>
        /// To jump into a new state. Is deliberately built to launch exceptions if try to jump into a non-existing state:
        /// If that ever happenes it means that either the implementation of the FSM is not working properly or a design
        /// logic in the specific FSM. In any case we want to be aware of the situation.
        /// </summary>
        public void GoToState(System.Type targetState)
        {
            if (m_current != null && targetState == m_current.GetType())
            {
                Debug.Log ("<color=yellow>STATE CHANGE WARNING: </color> Requested state change to" + targetState +" when FMS is already at that state. This is probably due a redundant and harmless state change request. Please check that is the case and try to prevent the situation as unnecessary state change requests, though harmless, hamper flow traceability.\n Note that redundant state changes like this are prevented in Generic_FSM.GoToState");
                return;
            }
            if (m_current != null)
                m_current.Exit();
            GenericState<T> locatedTargetState = m_allStates[targetState];
            m_current = locatedTargetState;
            m_current.Enter();

            m_currentStateWatch = m_current.m_stateName;
        }

        public Generic_FSM()
        {
            m_allStates = new Dictionary<Type, GenericState<T>>();
        }

        private Dictionary<System.Type, GenericState<T>> m_allStates;
        public GenericState<T> m_current;

        //this field is used to serialize the current state in the FSM. For debug purposes only
        [SerializeField] string m_currentStateWatch;
    }
