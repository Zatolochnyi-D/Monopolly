//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputActions/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""ded3bba5-aca4-42ea-9419-ce00247ef204"",
            ""actions"": [
                {
                    ""name"": ""ThrowDice"",
                    ""type"": ""Button"",
                    ""id"": ""32f29ff1-884c-407b-ad7e-cb4552a305d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseStats"",
                    ""type"": ""Button"",
                    ""id"": ""e0def61b-6c3f-49df-8c80-3eb0559bc06b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""16090f47-1bb1-4574-a063-25a576953593"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TestButton"",
                    ""type"": ""Button"",
                    ""id"": ""bb23a4e3-14b6-4c2d-a03a-269f515258ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f471ea9b-b95e-407e-b5e5-d1b5a42d0055"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowDice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3f5d097-c52a-4193-b15f-b50947520250"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseStats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0867a119-3f2e-4f30-9968-5c74631a7ef9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b38058d2-f87e-4c9c-b08d-5a22e3d1e87c"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pause"",
            ""id"": ""c3aa518f-2680-417b-bf1e-67946a527142"",
            ""actions"": [
                {
                    ""name"": ""Unpause"",
                    ""type"": ""Button"",
                    ""id"": ""08862fa9-08ef-451c-9251-bd9c60e69492"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9c06a47c-0436-4494-9d05-08f98bd7efcd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_ThrowDice = m_Game.FindAction("ThrowDice", throwIfNotFound: true);
        m_Game_OpenCloseStats = m_Game.FindAction("OpenCloseStats", throwIfNotFound: true);
        m_Game_Pause = m_Game.FindAction("Pause", throwIfNotFound: true);
        m_Game_TestButton = m_Game.FindAction("TestButton", throwIfNotFound: true);
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_Unpause = m_Pause.FindAction("Unpause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Game
    private readonly InputActionMap m_Game;
    private List<IGameActions> m_GameActionsCallbackInterfaces = new List<IGameActions>();
    private readonly InputAction m_Game_ThrowDice;
    private readonly InputAction m_Game_OpenCloseStats;
    private readonly InputAction m_Game_Pause;
    private readonly InputAction m_Game_TestButton;
    public struct GameActions
    {
        private @InputActions m_Wrapper;
        public GameActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ThrowDice => m_Wrapper.m_Game_ThrowDice;
        public InputAction @OpenCloseStats => m_Wrapper.m_Game_OpenCloseStats;
        public InputAction @Pause => m_Wrapper.m_Game_Pause;
        public InputAction @TestButton => m_Wrapper.m_Game_TestButton;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void AddCallbacks(IGameActions instance)
        {
            if (instance == null || m_Wrapper.m_GameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameActionsCallbackInterfaces.Add(instance);
            @ThrowDice.started += instance.OnThrowDice;
            @ThrowDice.performed += instance.OnThrowDice;
            @ThrowDice.canceled += instance.OnThrowDice;
            @OpenCloseStats.started += instance.OnOpenCloseStats;
            @OpenCloseStats.performed += instance.OnOpenCloseStats;
            @OpenCloseStats.canceled += instance.OnOpenCloseStats;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @TestButton.started += instance.OnTestButton;
            @TestButton.performed += instance.OnTestButton;
            @TestButton.canceled += instance.OnTestButton;
        }

        private void UnregisterCallbacks(IGameActions instance)
        {
            @ThrowDice.started -= instance.OnThrowDice;
            @ThrowDice.performed -= instance.OnThrowDice;
            @ThrowDice.canceled -= instance.OnThrowDice;
            @OpenCloseStats.started -= instance.OnOpenCloseStats;
            @OpenCloseStats.performed -= instance.OnOpenCloseStats;
            @OpenCloseStats.canceled -= instance.OnOpenCloseStats;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @TestButton.started -= instance.OnTestButton;
            @TestButton.performed -= instance.OnTestButton;
            @TestButton.canceled -= instance.OnTestButton;
        }

        public void RemoveCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameActions instance)
        {
            foreach (var item in m_Wrapper.m_GameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameActions @Game => new GameActions(this);

    // Pause
    private readonly InputActionMap m_Pause;
    private List<IPauseActions> m_PauseActionsCallbackInterfaces = new List<IPauseActions>();
    private readonly InputAction m_Pause_Unpause;
    public struct PauseActions
    {
        private @InputActions m_Wrapper;
        public PauseActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Unpause => m_Wrapper.m_Pause_Unpause;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void AddCallbacks(IPauseActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseActionsCallbackInterfaces.Add(instance);
            @Unpause.started += instance.OnUnpause;
            @Unpause.performed += instance.OnUnpause;
            @Unpause.canceled += instance.OnUnpause;
        }

        private void UnregisterCallbacks(IPauseActions instance)
        {
            @Unpause.started -= instance.OnUnpause;
            @Unpause.performed -= instance.OnUnpause;
            @Unpause.canceled -= instance.OnUnpause;
        }

        public void RemoveCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseActions @Pause => new PauseActions(this);
    public interface IGameActions
    {
        void OnThrowDice(InputAction.CallbackContext context);
        void OnOpenCloseStats(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnTestButton(InputAction.CallbackContext context);
    }
    public interface IPauseActions
    {
        void OnUnpause(InputAction.CallbackContext context);
    }
}
