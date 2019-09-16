// GENERATED AUTOMATICALLY FROM 'Assets/Prefabs/PlayerControlls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControlls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControlls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""481df8a1-fd5a-4ac9-93e1-59e4860a02dd"",
            ""actions"": [
                {
                    ""name"": ""Eat"",
                    ""type"": ""Button"",
                    ""id"": ""93e741b7-f209-44e0-a007-a0da2a30fdf2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""aff1612c-7155-43c4-bc44-cf438b4c1086"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""fe454c37-5c67-4ab4-b201-8f7fc54ca3cd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec083973-58a7-4cc3-ab11-69706305b30d"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Eat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32c82718-e479-423e-892d-930b74a65ea2"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7242c71-a7e5-4018-8ea9-360f23069d3d"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.GetActionMap("GamePlay");
        m_GamePlay_Eat = m_GamePlay.GetAction("Eat");
        m_GamePlay_Move = m_GamePlay.GetAction("Move");
        m_GamePlay_Rotate = m_GamePlay.GetAction("Rotate");
    }

    ~PlayerControlls()
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Eat;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Rotate;
    public struct GamePlayActions
    {
        private PlayerControlls m_Wrapper;
        public GamePlayActions(PlayerControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Eat => m_Wrapper.m_GamePlay_Eat;
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Rotate => m_Wrapper.m_GamePlay_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                Eat.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEat;
                Eat.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEat;
                Eat.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEat;
                Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                Rotate.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
                Rotate.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
                Rotate.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Eat.started += instance.OnEat;
                Eat.performed += instance.OnEat;
                Eat.canceled += instance.OnEat;
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                Rotate.started += instance.OnRotate;
                Rotate.performed += instance.OnRotate;
                Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    public interface IGamePlayActions
    {
        void OnEat(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
