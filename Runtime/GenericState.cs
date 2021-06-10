using System.Threading;
using UnityEngine;


    /// <summary>
    /// Base class for generic states in FSMs. Provide a set of methods to be overriden (Enter, Exit, Update, FixedUpdate)
    /// plus a reference T to the owner object of the state. In this way any state will be able to manipulate its owner
    /// in any required way.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class GenericState<T> 
    {
        public virtual void Enter()
        {
            string logText = "[" + Time.frameCount + "]" + "<color=cyan>STATE CHANGE: </color>" + m_target.ToString() + " ENTERS to " + m_stateName; 
            Debug.Log ( logText);
            
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Exit()
        {
            string logText = "[" + Time.frameCount + "]" + "<color=cyan>STATE CHANGE: </color>" + m_target.ToString() + " EXITS from  " + m_stateName;
            Debug.Log(logText);
            
        }


        public GenericState<T> Init(T target)
        {
            string [] tokenizedString =this.ToString().Split('.');
            m_stateName = tokenizedString[tokenizedString.Length - 1];
            m_target = target;
            return this;
        }

        protected T m_target;
        public string m_stateName;

    }
