// GENERATED AUTOMATICALLY FROM 'Assets/Utilities/Input/InputMaster.inputactions'

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
                },
                {
                    ""name"": ""evaluateSolution"",
                    ""type"": ""Button"",
                    ""id"": ""7e46c209-a5f4-4ece-8eb5-edd6dcd61968"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ContrastMode"",
                    ""type"": ""Button"",
                    ""id"": ""19fab7fd-e07d-45de-9389-2916cd53a3b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anykey"",
                    ""type"": ""Button"",
                    ""id"": ""44ebee72-387e-4aef-84b5-5732261434e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""c480e120-4e87-4ad7-8512-1f825dd22bae"",
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
                    ""id"": ""afcc40d2-e23f-4323-bd0a-d6918a3ee0b6"",
                    ""path"": ""<Keyboard>/i"",
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
                    ""processors"": """",
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
                    ""processors"": ""ScaleVector2(x=7,y=7)"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""f9774996-7d0f-40a0-a8e5-722c86a490d3"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""evaluateSolution"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6141eb4b-b7ae-4c77-bd9e-e6a046f7387d"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""ContrastMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82394bc3-48f9-448b-af9c-7c5875ff1aaa"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90d4db88-97ae-4326-a18b-429a8c16159f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerNoInputMode"",
            ""id"": ""896677fe-1962-4d22-92c4-dc398dbe0f8b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ca09b453-5e4e-4e8d-b321-23b60fbd397e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""19aa56c6-3a98-4106-88e3-cc4da32fa503"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""d403b2ed-8d84-45b6-977d-81cd0dd36d6d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ExitPuzzle"",
                    ""type"": ""Button"",
                    ""id"": ""7b2c0095-81e0-4fa3-9e84-92e235d52296"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""evaluateSolution"",
                    ""type"": ""Button"",
                    ""id"": ""7dddd600-a442-4c0e-900e-009a72c45915"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ContrastMode"",
                    ""type"": ""Button"",
                    ""id"": ""2baa9f62-4db5-4043-930b-8a2474d79672"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anykey"",
                    ""type"": ""Button"",
                    ""id"": ""678c287f-f784-40a1-b867-a124523d1f89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""63c577cf-4eb7-424c-8266-7a3965c3bae3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d4e4d338-93b1-4829-a1c5-d07da91fba9a"",
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
                    ""id"": ""d2fb29f7-2bb8-494e-820d-2853ffba2375"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2de35f23-5e62-475b-8a70-298cfa136a0a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7b2258a-008c-47e9-aa38-58ce0ea9cf13"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=7,y=7)"",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21a1501b-ab5b-4dd8-b6dd-441941a6db82"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""ExitPuzzle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfd4d572-d451-4503-b99c-e96e55ed66ae"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""evaluateSolution"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f9c7034-d17d-49bb-8c0b-952993089f9b"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""ContrastMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99ef1e52-613d-4190-8862-3af77435b139"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12c7449f-6145-4f8c-b297-ab9ce6f0d5dc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""Menu"",
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
                    ""name"": ""ShowSolution"",
                    ""type"": ""Button"",
                    ""id"": ""476d96cc-4f16-4a60-ad17-b3c6a7baaee2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressAnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""4cee70df-5b01-4643-b4a7-07b5940c8d1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BlindMode"",
                    ""type"": ""Button"",
                    ""id"": ""0138de2d-e3ea-4925-93cf-c16dd65d3f07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AutoPilotPuzzle"",
                    ""type"": ""Button"",
                    ""id"": ""648b08b0-9ce0-4edf-af91-51b979ec98d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleFullAutopilot"",
                    ""type"": ""Button"",
                    ""id"": ""b43aed17-b847-4357-877b-702de380e37c"",
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
                    ""id"": ""3d27998a-7002-42fa-b7ef-974c6a33875e"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowSolution"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""155990f9-3485-4830-8b4c-04a80baf5ed8"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressAnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6322b77d-980c-47e7-b9e8-609edab09bd1"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BlindMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""182fe015-1281-4589-8258-eaae32a5c0dd"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""AutoPilotPuzzle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30075741-e858-4240-945d-1ab19523366a"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Stnd KBM"",
                    ""action"": ""ToggleFullAutopilot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""96df4aaa-4b8b-4edb-843e-d362b8f1da9f"",
            ""actions"": [
                {
                    ""name"": ""BackToMain"",
                    ""type"": ""Button"",
                    ""id"": ""5f8c38ae-35dd-43b1-89c0-ca5b8464fe39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RestartScene"",
                    ""type"": ""Button"",
                    ""id"": ""ebe1deb2-5145-4161-8cf2-597a196a11c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1cc9bc13-ef3d-4191-8a52-669d97249e5d"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BackToMain"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c988f0bb-2ac0-4809-8cf9-b515e48b9b97"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OneSwitch"",
            ""id"": ""1c650195-fd7e-4a81-a847-7cc027d7e2a8"",
            ""actions"": [
                {
                    ""name"": ""OnlyButton"",
                    ""type"": ""Button"",
                    ""id"": ""a7c3378b-2aaa-4b9b-8be4-9c3824308de7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PuzzleTest"",
                    ""type"": ""Button"",
                    ""id"": ""244df09a-6c4b-4af8-b9af-307360c847e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5c38d5e4-8db4-4e94-9b86-77535c5e76ed"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnlyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8cabd77-3209-4838-b88c-e3db55095b28"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PuzzleTest"",
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
        m_Player_evaluateSolution = m_Player.FindAction("evaluateSolution", throwIfNotFound: true);
        m_Player_ContrastMode = m_Player.FindAction("ContrastMode", throwIfNotFound: true);
        m_Player_Anykey = m_Player.FindAction("Anykey", throwIfNotFound: true);
        m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
        // PlayerNoInputMode
        m_PlayerNoInputMode = asset.FindActionMap("PlayerNoInputMode", throwIfNotFound: true);
        m_PlayerNoInputMode_Movement = m_PlayerNoInputMode.FindAction("Movement", throwIfNotFound: true);
        m_PlayerNoInputMode_Interact = m_PlayerNoInputMode.FindAction("Interact", throwIfNotFound: true);
        m_PlayerNoInputMode_MoveCamera = m_PlayerNoInputMode.FindAction("MoveCamera", throwIfNotFound: true);
        m_PlayerNoInputMode_ExitPuzzle = m_PlayerNoInputMode.FindAction("ExitPuzzle", throwIfNotFound: true);
        m_PlayerNoInputMode_evaluateSolution = m_PlayerNoInputMode.FindAction("evaluateSolution", throwIfNotFound: true);
        m_PlayerNoInputMode_ContrastMode = m_PlayerNoInputMode.FindAction("ContrastMode", throwIfNotFound: true);
        m_PlayerNoInputMode_Anykey = m_PlayerNoInputMode.FindAction("Anykey", throwIfNotFound: true);
        m_PlayerNoInputMode_Menu = m_PlayerNoInputMode.FindAction("Menu", throwIfNotFound: true);
        // PuzzleDEBUGGER
        m_PuzzleDEBUGGER = asset.FindActionMap("PuzzleDEBUGGER", throwIfNotFound: true);
        m_PuzzleDEBUGGER_calculatesolution = m_PuzzleDEBUGGER.FindAction("calculate solution", throwIfNotFound: true);
        m_PuzzleDEBUGGER_ShowSolution = m_PuzzleDEBUGGER.FindAction("ShowSolution", throwIfNotFound: true);
        m_PuzzleDEBUGGER_PressAnyButton = m_PuzzleDEBUGGER.FindAction("PressAnyButton", throwIfNotFound: true);
        m_PuzzleDEBUGGER_BlindMode = m_PuzzleDEBUGGER.FindAction("BlindMode", throwIfNotFound: true);
        m_PuzzleDEBUGGER_AutoPilotPuzzle = m_PuzzleDEBUGGER.FindAction("AutoPilotPuzzle", throwIfNotFound: true);
        m_PuzzleDEBUGGER_ToggleFullAutopilot = m_PuzzleDEBUGGER.FindAction("ToggleFullAutopilot", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_BackToMain = m_UI.FindAction("BackToMain", throwIfNotFound: true);
        m_UI_RestartScene = m_UI.FindAction("RestartScene", throwIfNotFound: true);
        // OneSwitch
        m_OneSwitch = asset.FindActionMap("OneSwitch", throwIfNotFound: true);
        m_OneSwitch_OnlyButton = m_OneSwitch.FindAction("OnlyButton", throwIfNotFound: true);
        m_OneSwitch_PuzzleTest = m_OneSwitch.FindAction("PuzzleTest", throwIfNotFound: true);
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
    private readonly InputAction m_Player_evaluateSolution;
    private readonly InputAction m_Player_ContrastMode;
    private readonly InputAction m_Player_Anykey;
    private readonly InputAction m_Player_Menu;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @MoveCamera => m_Wrapper.m_Player_MoveCamera;
        public InputAction @ExitPuzzle => m_Wrapper.m_Player_ExitPuzzle;
        public InputAction @evaluateSolution => m_Wrapper.m_Player_evaluateSolution;
        public InputAction @ContrastMode => m_Wrapper.m_Player_ContrastMode;
        public InputAction @Anykey => m_Wrapper.m_Player_Anykey;
        public InputAction @Menu => m_Wrapper.m_Player_Menu;
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
                @evaluateSolution.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEvaluateSolution;
                @evaluateSolution.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEvaluateSolution;
                @evaluateSolution.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEvaluateSolution;
                @ContrastMode.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContrastMode;
                @ContrastMode.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContrastMode;
                @ContrastMode.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnContrastMode;
                @Anykey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnykey;
                @Anykey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnykey;
                @Anykey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAnykey;
                @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
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
                @evaluateSolution.started += instance.OnEvaluateSolution;
                @evaluateSolution.performed += instance.OnEvaluateSolution;
                @evaluateSolution.canceled += instance.OnEvaluateSolution;
                @ContrastMode.started += instance.OnContrastMode;
                @ContrastMode.performed += instance.OnContrastMode;
                @ContrastMode.canceled += instance.OnContrastMode;
                @Anykey.started += instance.OnAnykey;
                @Anykey.performed += instance.OnAnykey;
                @Anykey.canceled += instance.OnAnykey;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // PlayerNoInputMode
    private readonly InputActionMap m_PlayerNoInputMode;
    private IPlayerNoInputModeActions m_PlayerNoInputModeActionsCallbackInterface;
    private readonly InputAction m_PlayerNoInputMode_Movement;
    private readonly InputAction m_PlayerNoInputMode_Interact;
    private readonly InputAction m_PlayerNoInputMode_MoveCamera;
    private readonly InputAction m_PlayerNoInputMode_ExitPuzzle;
    private readonly InputAction m_PlayerNoInputMode_evaluateSolution;
    private readonly InputAction m_PlayerNoInputMode_ContrastMode;
    private readonly InputAction m_PlayerNoInputMode_Anykey;
    private readonly InputAction m_PlayerNoInputMode_Menu;
    public struct PlayerNoInputModeActions
    {
        private @InputMaster m_Wrapper;
        public PlayerNoInputModeActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerNoInputMode_Movement;
        public InputAction @Interact => m_Wrapper.m_PlayerNoInputMode_Interact;
        public InputAction @MoveCamera => m_Wrapper.m_PlayerNoInputMode_MoveCamera;
        public InputAction @ExitPuzzle => m_Wrapper.m_PlayerNoInputMode_ExitPuzzle;
        public InputAction @evaluateSolution => m_Wrapper.m_PlayerNoInputMode_evaluateSolution;
        public InputAction @ContrastMode => m_Wrapper.m_PlayerNoInputMode_ContrastMode;
        public InputAction @Anykey => m_Wrapper.m_PlayerNoInputMode_Anykey;
        public InputAction @Menu => m_Wrapper.m_PlayerNoInputMode_Menu;
        public InputActionMap Get() { return m_Wrapper.m_PlayerNoInputMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerNoInputModeActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerNoInputModeActions instance)
        {
            if (m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnInteract;
                @MoveCamera.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMoveCamera;
                @ExitPuzzle.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnExitPuzzle;
                @ExitPuzzle.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnExitPuzzle;
                @ExitPuzzle.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnExitPuzzle;
                @evaluateSolution.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnEvaluateSolution;
                @evaluateSolution.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnEvaluateSolution;
                @evaluateSolution.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnEvaluateSolution;
                @ContrastMode.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnContrastMode;
                @ContrastMode.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnContrastMode;
                @ContrastMode.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnContrastMode;
                @Anykey.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnAnykey;
                @Anykey.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnAnykey;
                @Anykey.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnAnykey;
                @Menu.started -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_PlayerNoInputModeActionsCallbackInterface = instance;
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
                @evaluateSolution.started += instance.OnEvaluateSolution;
                @evaluateSolution.performed += instance.OnEvaluateSolution;
                @evaluateSolution.canceled += instance.OnEvaluateSolution;
                @ContrastMode.started += instance.OnContrastMode;
                @ContrastMode.performed += instance.OnContrastMode;
                @ContrastMode.canceled += instance.OnContrastMode;
                @Anykey.started += instance.OnAnykey;
                @Anykey.performed += instance.OnAnykey;
                @Anykey.canceled += instance.OnAnykey;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public PlayerNoInputModeActions @PlayerNoInputMode => new PlayerNoInputModeActions(this);

    // PuzzleDEBUGGER
    private readonly InputActionMap m_PuzzleDEBUGGER;
    private IPuzzleDEBUGGERActions m_PuzzleDEBUGGERActionsCallbackInterface;
    private readonly InputAction m_PuzzleDEBUGGER_calculatesolution;
    private readonly InputAction m_PuzzleDEBUGGER_ShowSolution;
    private readonly InputAction m_PuzzleDEBUGGER_PressAnyButton;
    private readonly InputAction m_PuzzleDEBUGGER_BlindMode;
    private readonly InputAction m_PuzzleDEBUGGER_AutoPilotPuzzle;
    private readonly InputAction m_PuzzleDEBUGGER_ToggleFullAutopilot;
    public struct PuzzleDEBUGGERActions
    {
        private @InputMaster m_Wrapper;
        public PuzzleDEBUGGERActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @calculatesolution => m_Wrapper.m_PuzzleDEBUGGER_calculatesolution;
        public InputAction @ShowSolution => m_Wrapper.m_PuzzleDEBUGGER_ShowSolution;
        public InputAction @PressAnyButton => m_Wrapper.m_PuzzleDEBUGGER_PressAnyButton;
        public InputAction @BlindMode => m_Wrapper.m_PuzzleDEBUGGER_BlindMode;
        public InputAction @AutoPilotPuzzle => m_Wrapper.m_PuzzleDEBUGGER_AutoPilotPuzzle;
        public InputAction @ToggleFullAutopilot => m_Wrapper.m_PuzzleDEBUGGER_ToggleFullAutopilot;
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
                @ShowSolution.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnShowSolution;
                @ShowSolution.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnShowSolution;
                @ShowSolution.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnShowSolution;
                @PressAnyButton.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
                @PressAnyButton.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
                @PressAnyButton.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnPressAnyButton;
                @BlindMode.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnBlindMode;
                @BlindMode.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnBlindMode;
                @BlindMode.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnBlindMode;
                @AutoPilotPuzzle.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnAutoPilotPuzzle;
                @AutoPilotPuzzle.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnAutoPilotPuzzle;
                @AutoPilotPuzzle.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnAutoPilotPuzzle;
                @ToggleFullAutopilot.started -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnToggleFullAutopilot;
                @ToggleFullAutopilot.performed -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnToggleFullAutopilot;
                @ToggleFullAutopilot.canceled -= m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface.OnToggleFullAutopilot;
            }
            m_Wrapper.m_PuzzleDEBUGGERActionsCallbackInterface = instance;
            if (instance != null)
            {
                @calculatesolution.started += instance.OnCalculatesolution;
                @calculatesolution.performed += instance.OnCalculatesolution;
                @calculatesolution.canceled += instance.OnCalculatesolution;
                @ShowSolution.started += instance.OnShowSolution;
                @ShowSolution.performed += instance.OnShowSolution;
                @ShowSolution.canceled += instance.OnShowSolution;
                @PressAnyButton.started += instance.OnPressAnyButton;
                @PressAnyButton.performed += instance.OnPressAnyButton;
                @PressAnyButton.canceled += instance.OnPressAnyButton;
                @BlindMode.started += instance.OnBlindMode;
                @BlindMode.performed += instance.OnBlindMode;
                @BlindMode.canceled += instance.OnBlindMode;
                @AutoPilotPuzzle.started += instance.OnAutoPilotPuzzle;
                @AutoPilotPuzzle.performed += instance.OnAutoPilotPuzzle;
                @AutoPilotPuzzle.canceled += instance.OnAutoPilotPuzzle;
                @ToggleFullAutopilot.started += instance.OnToggleFullAutopilot;
                @ToggleFullAutopilot.performed += instance.OnToggleFullAutopilot;
                @ToggleFullAutopilot.canceled += instance.OnToggleFullAutopilot;
            }
        }
    }
    public PuzzleDEBUGGERActions @PuzzleDEBUGGER => new PuzzleDEBUGGERActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_BackToMain;
    private readonly InputAction m_UI_RestartScene;
    public struct UIActions
    {
        private @InputMaster m_Wrapper;
        public UIActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @BackToMain => m_Wrapper.m_UI_BackToMain;
        public InputAction @RestartScene => m_Wrapper.m_UI_RestartScene;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @BackToMain.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBackToMain;
                @BackToMain.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBackToMain;
                @BackToMain.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBackToMain;
                @RestartScene.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRestartScene;
                @RestartScene.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRestartScene;
                @RestartScene.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRestartScene;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BackToMain.started += instance.OnBackToMain;
                @BackToMain.performed += instance.OnBackToMain;
                @BackToMain.canceled += instance.OnBackToMain;
                @RestartScene.started += instance.OnRestartScene;
                @RestartScene.performed += instance.OnRestartScene;
                @RestartScene.canceled += instance.OnRestartScene;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // OneSwitch
    private readonly InputActionMap m_OneSwitch;
    private IOneSwitchActions m_OneSwitchActionsCallbackInterface;
    private readonly InputAction m_OneSwitch_OnlyButton;
    private readonly InputAction m_OneSwitch_PuzzleTest;
    public struct OneSwitchActions
    {
        private @InputMaster m_Wrapper;
        public OneSwitchActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnlyButton => m_Wrapper.m_OneSwitch_OnlyButton;
        public InputAction @PuzzleTest => m_Wrapper.m_OneSwitch_PuzzleTest;
        public InputActionMap Get() { return m_Wrapper.m_OneSwitch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OneSwitchActions set) { return set.Get(); }
        public void SetCallbacks(IOneSwitchActions instance)
        {
            if (m_Wrapper.m_OneSwitchActionsCallbackInterface != null)
            {
                @OnlyButton.started -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnOnlyButton;
                @OnlyButton.performed -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnOnlyButton;
                @OnlyButton.canceled -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnOnlyButton;
                @PuzzleTest.started -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnPuzzleTest;
                @PuzzleTest.performed -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnPuzzleTest;
                @PuzzleTest.canceled -= m_Wrapper.m_OneSwitchActionsCallbackInterface.OnPuzzleTest;
            }
            m_Wrapper.m_OneSwitchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OnlyButton.started += instance.OnOnlyButton;
                @OnlyButton.performed += instance.OnOnlyButton;
                @OnlyButton.canceled += instance.OnOnlyButton;
                @PuzzleTest.started += instance.OnPuzzleTest;
                @PuzzleTest.performed += instance.OnPuzzleTest;
                @PuzzleTest.canceled += instance.OnPuzzleTest;
            }
        }
    }
    public OneSwitchActions @OneSwitch => new OneSwitchActions(this);
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
        void OnEvaluateSolution(InputAction.CallbackContext context);
        void OnContrastMode(InputAction.CallbackContext context);
        void OnAnykey(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
    public interface IPlayerNoInputModeActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnExitPuzzle(InputAction.CallbackContext context);
        void OnEvaluateSolution(InputAction.CallbackContext context);
        void OnContrastMode(InputAction.CallbackContext context);
        void OnAnykey(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
    public interface IPuzzleDEBUGGERActions
    {
        void OnCalculatesolution(InputAction.CallbackContext context);
        void OnShowSolution(InputAction.CallbackContext context);
        void OnPressAnyButton(InputAction.CallbackContext context);
        void OnBlindMode(InputAction.CallbackContext context);
        void OnAutoPilotPuzzle(InputAction.CallbackContext context);
        void OnToggleFullAutopilot(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnBackToMain(InputAction.CallbackContext context);
        void OnRestartScene(InputAction.CallbackContext context);
    }
    public interface IOneSwitchActions
    {
        void OnOnlyButton(InputAction.CallbackContext context);
        void OnPuzzleTest(InputAction.CallbackContext context);
    }
}
