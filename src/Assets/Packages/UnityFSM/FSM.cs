using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityFSM
{
    public delegate bool Reason();

    public abstract class FSM : MonoBehaviour
    {
        #region Fields

        private Dictionary<Type, State> stateLibrary = new Dictionary<Type, State>();
        private Type defaultState;

        #endregion

        #region Properties

        private State currentState = null;
        public State CurrentState
        {
            get
            {
                if (currentState == null)
                {
                    currentState = stateLibrary[defaultState];
                    currentState.Enter();
                }
                return currentState;
            }
        }

        private State previousState = null;
        public State PreviousState
        {
            get
            {
                return previousState;
            }
        }

        private float stateStartTime = 0.0f;
        public float ActiveStateTime
        {
            get
            {
                return Time.time - stateStartTime;
            }
        }

        #endregion

        #region Protected Methods

        protected void AddState<T>(bool isDefault = false)
        {
            Type stateType = typeof(T);
            if (!stateType.IsSubclassOf(typeof(State)))
            {
                Debug.LogError("Error adding State. State " + stateType + " does not derive from State class.");
                return;
            }

            if (stateLibrary.ContainsKey(stateType))
            {
                Debug.LogError("Error adding State. State " + stateType + " already registered.");
                return;
            }

            stateLibrary.Add(stateType, (State)System.Activator.CreateInstance(stateType, new object[] { this }));

            if (isDefault)
            {
                defaultState = stateType;
                SetState(stateType);
            }
        }

        protected State GetState<T>()
        {
            return stateLibrary[typeof(T)];
        }

        protected void SetState(Type stateType)
        {
            currentState.Exit();
            previousState = currentState;
            currentState = stateLibrary[stateType];
            currentState.Enter();
            stateStartTime = Time.time;
        }

        #endregion

        #region Abstract Methods

        protected abstract void FSMAwake();

        #endregion

        #region Private Methods

        private void ValidateStateMachine()
        {
            bool errors = false;
            foreach (var state in stateLibrary.Values)
            {
                if (state.Transitions.Count == 0)
                {
                    Debug.LogError("State Machine Layout Error.  There is no transition from " + state);
                    errors = true;
                }
            }

            if (errors)
            {
                throw new Exception("Invalid State Machine layout.");
            }
        }

        private void UpdateTransitions()
        {
            foreach (var reason in CurrentState.Transitions.Keys)
            {
                if (reason())
                {
                    SetState(CurrentState.Transitions[reason]);
                    break;
                }
            }
        }

        #endregion

        #region MonoBehaviour Messages

        void Awake()
        {
            currentState = new NullState(this);
            currentState.Enter();

            FSMAwake();
            ValidateStateMachine();
        }

        void Start()
        {
            CurrentState.Start();
        }

        void Update()
        {
            CurrentState.Update();
        }

        void FixedUpdate()
        {
            UpdateTransitions();
            CurrentState.FixedUpdate();
        }

        void LateUpdate()
        {
            CurrentState.LateUpdate();
        }

        void OnAnimatorIK(int layerIndex)
        {
            CurrentState.OnAnimatorIK(layerIndex);
        }

        void OnAnimatorMove()
        {
            CurrentState.OnAnimatorMove();
        }

        void OnApplicationFocus(bool focusState)
        {
            CurrentState.OnApplicationFocus(focusState);
        }

        void OnApplicationPause(bool pauseState)
        {
            CurrentState.OnApplicationPause(pauseState);
        }

        void OnAudioFilterRead(float[] data, int channels)
        {
            CurrentState.OnAudioFilterRead(data, channels);
        }

        void OnBecameInvisible()
        {
            CurrentState.OnBecameInvisible();
        }

        void OnBecameVisible()
        {
            CurrentState.OnBecameVisible();
        }

        void OnCollisionEnter(Collision collision)
        {
            CurrentState.OnCollisionEnter(collision);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            CurrentState.OnCollisionEnter2D(collision);
        }

        void OnCollisionExit(Collision collision)
        {
            CurrentState.OnCollisionExit(collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            CurrentState.OnCollisionExit2D(collision);
        }

        void OnCollisionStay(Collision collision)
        {
            CurrentState.OnCollisionStay(collision);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            CurrentState.OnCollisionStay2D(collision);
        }

        void OnConnectedToServer()
        {
            CurrentState.OnConnectedToServer();
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            CurrentState.OnControllerColliderHit(hit);
        }

        void OnDestroy()
        {
            CurrentState.OnDestroy();
        }

        void OnDisable()
        {
            CurrentState.OnDisable();
        }

        void OnDisconnectedFromServer(NetworkDisconnection info)
        {
            CurrentState.OnDisconnectedFromServer(info);
        }

        void OnDrawGizmos()
        {
            if (currentState != null)
            {
                CurrentState.OnDrawGizmos();
            }
        }

        void OnDrawGizmosSealed()
        {
            if (currentState != null)
            {
                CurrentState.OnDrawGizmosSelected();
            }
        }

        void OnEnable()
        {
            CurrentState.OnEnable();
        }

        void OnFailedToConnect(NetworkConnectionError error)
        {
            CurrentState.OnFailedToConnect(error);
        }

        void OnFailedToConnectToMasterServer(NetworkConnectionError error)
        {
            CurrentState.OnFailedToConnectToMasterServer(error);
        }

        void OnGUI()
        {
            CurrentState.OnGUI();
        }

        void OnJointBreak(float breakForce)
        {
            CurrentState.OnJointBreak(breakForce);
        }

        void OnLevelWasLoaded(int level)
        {
            CurrentState.OnLevelWasLoaded(level);
        }

        void OnMasterServerEvent(MasterServerEvent msEvent)
        {
            CurrentState.OnMasterServerEvent(msEvent);
        }

        void OnMouseDown()
        {
            CurrentState.OnMouseDown();
        }

        void OnMouseDrag()
        {
            CurrentState.OnMouseDrag();
        }

        void OnMouseEnter()
        {
            CurrentState.OnMouseEnter();
        }

        void OnMouseExit()
        {
            CurrentState.OnMouseExit();
        }

        void OnMouseOver()
        {
            CurrentState.OnMouseOver();
        }

        void OnMouseUp()
        {
            CurrentState.OnMouseUp();
        }

        void OnMouseUpAsButton()
        {
            CurrentState.OnMouseUpAsButton();
        }

        void OnNetworkInstantiate(NetworkMessageInfo info)
        {
            CurrentState.OnNetworkInstantiate(info);
        }

        void OnParticleCollision(GameObject obj)
        {
            CurrentState.OnParticleCollision(obj);
        }

        void OnPlayerConnected(NetworkPlayer player)
        {
            CurrentState.OnPlayerConnected(player);
        }

        void OnPlayerDisconnected(NetworkPlayer player)
        {
            CurrentState.OnPlayerDisconnected(player);
        }

        void OnPostRender()
        {
            CurrentState.OnPostRender();
        }

        void OnPreCull()
        {
            CurrentState.OnPreCull();
        }

        void OnPreRender()
        {
            CurrentState.OnPreRender();
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            CurrentState.OnRenderImage(source, destination);
        }

        void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
        {
            CurrentState.OnSerializeNetworkView(stream, info);
        }

        void OnServerInitialized()
        {
            CurrentState.OnServerInitialized();
        }

        void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            CurrentState.OnTriggerEnter2D(other);
        }

        void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExit(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            CurrentState.OnTriggerExit2D(other);
        }

        void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStay(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            CurrentState.OnTriggerStay2D(other);
        }

        void OnValidate()
        {
            if (currentState != null)
            {
                CurrentState.OnValidate();
            }
        }

        void OnWillRenderObject()
        {
            CurrentState.OnWillRenderObject();
        }

        void Reset()
        {
            CurrentState.Reset();
        }

        #endregion
    }
}
