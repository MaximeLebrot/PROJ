// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Utilities/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""74a2d63c-d56e-40c1-8fb3-b5d863329bd9"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ceb9857a-5f9b-4280-9829-6449daf0daca"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b4de5da9-c326-427e-8001-67ca6e0e5b2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""2b897f4b-fc28-43c5-a522-c69442704706"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ExitPuzzle"",
                    ""type"": ""Button"",
                    ""id"": ""2a54affd-5f79-4bb0-b3bf-58e6880ec4b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Axis"",
                    ""id"": ""0bf53775-7fbe-4d75-929b-e14a8457f91b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""849924d9-3b2e-473d-a60d-043ddfe14419"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8466c131-468d-478a-99ab-b6dd2966b57e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8c9540d2-1729-4ac3-a9ca-ec27cae85a1f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""256fff70-0bd2-40e4-b5f7-96510458a96c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""39ed72fd-1f47-4b07-9178-73b1dd2d1e78"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""xboxcontroller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5300fa11-f126-4697-8ccb-5020722b0397"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""125b7564-f804-44d0-aeb7-1768ea5c58d0"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1362b017-5dac-4a05-b539-af7917f1d25b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45eb080b-dbfe-4265-9a68-72645599fb2e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""ExitPuzzle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PuzzleDEBUGGER"",
            ""id"": ""2941194f-298e-48b1-aaf1-fb18798b700f"",
            ""actions"": [
                {
                    ""name"": ""calculate solution"",
                    ""type"": ""Button"",
                    ""id"": ""33f15d10-f9d5-48f4-a23d-a8db1dd4a753"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressAnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""5a8ea15a-7a94-4647-b788-7aadfa9ce0a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f86de9b-62f1-4b3a-bff5-42d8dd09a060"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""calculate solution"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e9ff052-eb3e-4533-a13b-dd6533ab3ec3"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""PressAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Stnd KBM"",
            ""bindingGroup"": ""Stnd KBM"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<VirtualMouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""xboxcontroller"",
            ""bindingGroup"": ""xboxcontroller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XboxOneGampadiOS>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_MoveCamera = m_Player.FindAction("MoveCamera", throwIfNotFound: true);
        m_Player_ExitPuzzle = m_Player.FindAction("ExitPuzzle", throwIfNotFound: true);
        // PuzzleDEBUGGER
        m_PuzzleDEBUGGER = asset.FindActionMap("PuzzleDEBUGGER", throwIfNotFound: true);
        m_PuzzleDEBUGGER_calculatesolution = m_PuzzleDEBUGGER.FindAction("calculate solution", throwIfNotFound: true);
        m_PuzzleDEBUGGER_PressAnyButton = m_PuzzleDEBUGGER.FindAction("PressAnyButton", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_MoveCamera;
    private readonly InputAction m_Player_ExitPuzzle;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @MoveCamera => m_Wrapper.m_Player_MoveCamera;
        public InputAction @ExitPuzzle => m_Wrapper.m_Player_ExitPuzzle;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @MoveCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveCamera;
                @ExitPuzzle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitPuzzle;
                @ExitPuzzle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitPuzzle;
                @ExitPuzzle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitPuzzle;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @ExitPuzzle.started += instance.OnExitPuzzle;
                @ExitPuzzle.performed += instance.OnExitPuzzle;
                @ExitPuzzle.canceled += instance.OnExitPuzzle;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // PuzzleDEBUGGER
    private readonly InputActionMap m_PuzzleDEBUGGER;
    private IPuzzleDEBUGGERActions m_PuzzleDEBUGGERActionsCallbackInterface;
    private readonly InputAction m_PuzzleDEBUGGER_calculatesolution;
    private readonly InputAction m_PuzzleDEBUGGER_PressAnyButton;
    public struct PuzzleDEBUGGERActions
    {
        private @InputMaster m_Wrapper;
        public PuzzleDEBUGGERActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @calculatesolution => m_Wrapper.m_PuzzleDEBUGGER_calculatesolution;
        public InputAction @PressAnyButton => m_Wrapper.m_PuzzleDEBUGGER_PressAnyButton;
        public InputActionMap Get() { return m_Wrapper.m_PuzzleDEBUGGER; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PuzzleDEBUGGERActions set) { return set.Get(); }
        public void SetCallbacks(IPuzzleDEBUGGERActions instance)
        {
            if (m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface != null)
            {
                @calculatesolution.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnCalculatesolution;
                @calculatesolution.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnCalculatesolution;
                @calculatesolution.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnCalculatesolution;
                @PressAnyButton.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
                @PressAnyButton.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
                @PressAnyButton.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
            }
            m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface = instance;
            if (instance != null)
            {
                @calculatesolution.started += instance.OnCalculatesolution;
                @calculatesolution.performed += instance.OnCalculatesolution;
                @calculatesolution.canceled += instance.OnCalculatesolution;
                @PressAnyButton.started += instance.OnPressAnyButton;
                @PressAnyButton.performed += instance.OnPressAnyButton;
                @PressAnyButton.canceled += instance.OnPressAnyButton;
            }
        }
    }
    public PuzzleDEBUGGERActions @PuzzleDEBUGGER => new PuzzleDEBUGGERActions(this);
    private int m_StndKBMSchemeIndex = -1;
    public InputControlScheme StndKBMScheme
    {
        get
        {
            if (m_StndKBMSchemeIndex == -1) m_StndKBMSchemeIndex = asset.FindControlSchemeIndex("Stnd KBM");
            return asset.controlSchemes[m_StndKBMSchemeIndex];
        }
    }
    private int m_xboxcontrollerSchemeIndex = -1;
    public InputControlScheme xboxcontrollerScheme
    {
        get
        {
            if (m_xboxcontrollerSchemeIndex == -1) m_xboxcontrollerSchemeIndex = asset.FindControlSchemeIndex("xboxcontroller");
            return asset.controlSchemes[m_xboxcontrollerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnExitPuzzle(InputAction.CallbackContext context);
    }
    public interface IPuzzleDEBUGGERActions
    {
        void OnCalculatesolution(InputAction.CallbackContext context);
        void OnPressAnyButton(InputAction.CallbackContext context);
    }
}
