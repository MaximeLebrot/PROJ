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
            ""name"": ""SymbolAudio"",
            ""id"": ""b1caf9d3-1cf8-4a6d-94b4-35324026fb1b"",
            ""actions"": [
                {
                    ""name"": ""PlayOne"",
                    ""type"": ""Button"",
                    ""id"": ""e404df02-170d-40ce-8594-d950ea7d2930"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwo"",
                    ""type"": ""Button"",
                    ""id"": ""65e88fb7-1a72-43b2-b5f0-1803b5370994"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayThree"",
                    ""type"": ""Button"",
                    ""id"": ""3fdd71f2-7d0d-49f5-82eb-7f943d4f59f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayFour"",
                    ""type"": ""Button"",
                    ""id"": ""0c66dab9-bf1b-4f73-9efd-b322f2fbb268"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayFive"",
                    ""type"": ""Button"",
                    ""id"": ""c8f3379e-af34-4cb4-b93e-6d98ab53f75b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaySix"",
                    ""type"": ""Button"",
                    ""id"": ""35bbb24d-7216-467d-9bd7-3a8f3e953716"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaySeven"",
                    ""type"": ""Button"",
                    ""id"": ""1d16adb0-bb70-4114-a08d-47efebfaa514"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayEight"",
                    ""type"": ""Button"",
                    ""id"": ""29285ec2-a977-4dc4-b747-6171eee6c69a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayNine"",
                    ""type"": ""Button"",
                    ""id"": ""6349192c-f8ab-438a-ac08-93090180a1a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTen"",
                    ""type"": ""Button"",
                    ""id"": ""976d4def-3b66-47e8-bebd-a4eb39f8c46d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayEleven"",
                    ""type"": ""Button"",
                    ""id"": ""92db4846-915a-4fe3-aa42-a7f67903b408"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwelve"",
                    ""type"": ""Button"",
                    ""id"": ""fb19b03d-8b5d-429d-8873-ceb0225f6050"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayThirteen"",
                    ""type"": ""Button"",
                    ""id"": ""83038e3f-5f1d-4732-9a37-a3ec2a93983a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayFourteen"",
                    ""type"": ""Button"",
                    ""id"": ""10779757-48ee-4271-bc15-8e11f3f97745"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayFifteen"",
                    ""type"": ""Button"",
                    ""id"": ""f65465ad-443b-4229-bfee-e032fd02a4c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaySixteen"",
                    ""type"": ""Button"",
                    ""id"": ""926629c0-0791-4e03-8a81-02253fb0abc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaySeventeen"",
                    ""type"": ""Button"",
                    ""id"": ""2a9833b7-7515-4594-a237-204e1406cfe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayEighteen"",
                    ""type"": ""Button"",
                    ""id"": ""c7f4b869-f14f-44aa-ade8-7f6284b96387"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayNineteen"",
                    ""type"": ""Button"",
                    ""id"": ""3f22718a-16e6-438e-8bd9-6ef43abc254c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwenty"",
                    ""type"": ""Button"",
                    ""id"": ""73fc1835-0079-4255-9fb6-03fec5d4467a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwentyOne"",
                    ""type"": ""Button"",
                    ""id"": ""ec73460b-89f4-4504-a825-f0ecfc3e259c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwentyTwo"",
                    ""type"": ""Button"",
                    ""id"": ""5a6b95d6-ee5f-4ea8-9e16-7d84b1db4e78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwentyThree"",
                    ""type"": ""Button"",
                    ""id"": ""68be810e-2de9-4da1-93df-4333e3a92e8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayTwentyFour"",
                    ""type"": ""Button"",
                    ""id"": ""b8fbf49f-ec26-43ed-82b2-e44c85ea559b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""074bd90b-902c-412f-98b1-bb80c43c3c0f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64886b5d-24c4-4343-a7af-5344ca564ae9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b111f8f-7c44-473e-b34b-9224cd7864c7"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e81a0cb-6147-4bfa-997c-4d7e51f53d6a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayFour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""039879a9-1247-437d-9e27-a6e151e4a65b"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayFive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dea58d4-096a-4cae-a4dc-d2accafc0398"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaySix"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""213c64e0-3cfd-42cd-916b-0e5f71d8202f"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaySeven"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23e0c89c-9a76-4d7d-9fe1-6a41f64640d9"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayEight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""014491ef-93c0-4512-9638-5e59ba600ca2"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayNine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""334609e6-aaa3-4c0c-a8fa-3c5f51140898"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayEleven"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cd275d9-bbcb-4984-875e-2f0f0dc540d9"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4096d6fe-e094-4428-af32-85396574172a"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwelve"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbc7e770-0150-454b-90ad-981b65d3cba9"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayThirteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bba1906c-2abc-4bf2-a495-e2715c054d45"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayFourteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a46e88b2-76ca-4ea4-98af-62dab95b220e"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayFifteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6620f812-5352-44f3-a327-cd3b83a69c10"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaySixteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77ec26fb-1b95-4ead-9cdb-dbe20c1dbdc8"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaySeventeen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89cfd423-13d7-4c3f-b2be-5569b5ac44f7"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayEighteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fe17b7a-503a-4f63-9f83-b0888b4fe435"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayNineteen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5018f79a-7e04-4f87-9620-60d2dbcb3b25"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwenty"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""924c4127-67f3-4091-a5bb-f48ba5f0ddc9"",
                    ""path"": ""<Keyboard>/numpadDivide"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwentyOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54721938-975a-4b93-af3b-2e2d0c123611"",
                    ""path"": ""<Keyboard>/numpadMultiply"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwentyTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd12936c-f173-4f87-b4a3-8c36211fbcd1"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwentyThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""105ee07d-a186-4fdb-aa5a-d5130ac40c90"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayTwentyFour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""OneSwitch"",
            ""id"": ""2e34ae07-59c2-4967-b7b1-664d93d94fba"",
            ""actions"": [
                {
                    ""name"": ""OnlyButton"",
                    ""type"": ""Button"",
                    ""id"": ""8f27a7fc-4d31-4034-8500-1f3d6f37d08a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PuzzleTest"",
                    ""type"": ""Value"",
                    ""id"": ""9e4a4e72-ea8b-4809-93ff-0e3fa66e2770"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c274daef-8d09-45ac-9f1d-dba7ee8f3790"",
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
                    ""id"": ""d5aeb90d-d8b0-43f4-9785-d8cc47664d84"",
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
        // SymbolAudio
        m_SymbolAudio = asset.FindActionMap("SymbolAudio", throwIfNotFound: true);
        m_SymbolAudio_PlayOne = m_SymbolAudio.FindAction("PlayOne", throwIfNotFound: true);
        m_SymbolAudio_PlayTwo = m_SymbolAudio.FindAction("PlayTwo", throwIfNotFound: true);
        m_SymbolAudio_PlayThree = m_SymbolAudio.FindAction("PlayThree", throwIfNotFound: true);
        m_SymbolAudio_PlayFour = m_SymbolAudio.FindAction("PlayFour", throwIfNotFound: true);
        m_SymbolAudio_PlayFive = m_SymbolAudio.FindAction("PlayFive", throwIfNotFound: true);
        m_SymbolAudio_PlaySix = m_SymbolAudio.FindAction("PlaySix", throwIfNotFound: true);
        m_SymbolAudio_PlaySeven = m_SymbolAudio.FindAction("PlaySeven", throwIfNotFound: true);
        m_SymbolAudio_PlayEight = m_SymbolAudio.FindAction("PlayEight", throwIfNotFound: true);
        m_SymbolAudio_PlayNine = m_SymbolAudio.FindAction("PlayNine", throwIfNotFound: true);
        m_SymbolAudio_PlayTen = m_SymbolAudio.FindAction("PlayTen", throwIfNotFound: true);
        m_SymbolAudio_PlayEleven = m_SymbolAudio.FindAction("PlayEleven", throwIfNotFound: true);
        m_SymbolAudio_PlayTwelve = m_SymbolAudio.FindAction("PlayTwelve", throwIfNotFound: true);
        m_SymbolAudio_PlayThirteen = m_SymbolAudio.FindAction("PlayThirteen", throwIfNotFound: true);
        m_SymbolAudio_PlayFourteen = m_SymbolAudio.FindAction("PlayFourteen", throwIfNotFound: true);
        m_SymbolAudio_PlayFifteen = m_SymbolAudio.FindAction("PlayFifteen", throwIfNotFound: true);
        m_SymbolAudio_PlaySixteen = m_SymbolAudio.FindAction("PlaySixteen", throwIfNotFound: true);
        m_SymbolAudio_PlaySeventeen = m_SymbolAudio.FindAction("PlaySeventeen", throwIfNotFound: true);
        m_SymbolAudio_PlayEighteen = m_SymbolAudio.FindAction("PlayEighteen", throwIfNotFound: true);
        m_SymbolAudio_PlayNineteen = m_SymbolAudio.FindAction("PlayNineteen", throwIfNotFound: true);
        m_SymbolAudio_PlayTwenty = m_SymbolAudio.FindAction("PlayTwenty", throwIfNotFound: true);
        m_SymbolAudio_PlayTwentyOne = m_SymbolAudio.FindAction("PlayTwentyOne", throwIfNotFound: true);
        m_SymbolAudio_PlayTwentyTwo = m_SymbolAudio.FindAction("PlayTwentyTwo", throwIfNotFound: true);
        m_SymbolAudio_PlayTwentyThree = m_SymbolAudio.FindAction("PlayTwentyThree", throwIfNotFound: true);
        m_SymbolAudio_PlayTwentyFour = m_SymbolAudio.FindAction("PlayTwentyFour", throwIfNotFound: true);
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
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @MoveCamera => m_Wrapper.m_Player_MoveCamera;
        public InputAction @ExitPuzzle => m_Wrapper.m_Player_ExitPuzzle;
        public InputAction @evaluateSolution => m_Wrapper.m_Player_evaluateSolution;
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
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

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

    // SymbolAudio
    private readonly InputActionMap m_SymbolAudio;
    private ISymbolAudioActions m_SymbolAudioActionsCallbackInterface;
    private readonly InputAction m_SymbolAudio_PlayOne;
    private readonly InputAction m_SymbolAudio_PlayTwo;
    private readonly InputAction m_SymbolAudio_PlayThree;
    private readonly InputAction m_SymbolAudio_PlayFour;
    private readonly InputAction m_SymbolAudio_PlayFive;
    private readonly InputAction m_SymbolAudio_PlaySix;
    private readonly InputAction m_SymbolAudio_PlaySeven;
    private readonly InputAction m_SymbolAudio_PlayEight;
    private readonly InputAction m_SymbolAudio_PlayNine;
    private readonly InputAction m_SymbolAudio_PlayTen;
    private readonly InputAction m_SymbolAudio_PlayEleven;
    private readonly InputAction m_SymbolAudio_PlayTwelve;
    private readonly InputAction m_SymbolAudio_PlayThirteen;
    private readonly InputAction m_SymbolAudio_PlayFourteen;
    private readonly InputAction m_SymbolAudio_PlayFifteen;
    private readonly InputAction m_SymbolAudio_PlaySixteen;
    private readonly InputAction m_SymbolAudio_PlaySeventeen;
    private readonly InputAction m_SymbolAudio_PlayEighteen;
    private readonly InputAction m_SymbolAudio_PlayNineteen;
    private readonly InputAction m_SymbolAudio_PlayTwenty;
    private readonly InputAction m_SymbolAudio_PlayTwentyOne;
    private readonly InputAction m_SymbolAudio_PlayTwentyTwo;
    private readonly InputAction m_SymbolAudio_PlayTwentyThree;
    private readonly InputAction m_SymbolAudio_PlayTwentyFour;
    public struct SymbolAudioActions
    {
        private @InputMaster m_Wrapper;
        public SymbolAudioActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayOne => m_Wrapper.m_SymbolAudio_PlayOne;
        public InputAction @PlayTwo => m_Wrapper.m_SymbolAudio_PlayTwo;
        public InputAction @PlayThree => m_Wrapper.m_SymbolAudio_PlayThree;
        public InputAction @PlayFour => m_Wrapper.m_SymbolAudio_PlayFour;
        public InputAction @PlayFive => m_Wrapper.m_SymbolAudio_PlayFive;
        public InputAction @PlaySix => m_Wrapper.m_SymbolAudio_PlaySix;
        public InputAction @PlaySeven => m_Wrapper.m_SymbolAudio_PlaySeven;
        public InputAction @PlayEight => m_Wrapper.m_SymbolAudio_PlayEight;
        public InputAction @PlayNine => m_Wrapper.m_SymbolAudio_PlayNine;
        public InputAction @PlayTen => m_Wrapper.m_SymbolAudio_PlayTen;
        public InputAction @PlayEleven => m_Wrapper.m_SymbolAudio_PlayEleven;
        public InputAction @PlayTwelve => m_Wrapper.m_SymbolAudio_PlayTwelve;
        public InputAction @PlayThirteen => m_Wrapper.m_SymbolAudio_PlayThirteen;
        public InputAction @PlayFourteen => m_Wrapper.m_SymbolAudio_PlayFourteen;
        public InputAction @PlayFifteen => m_Wrapper.m_SymbolAudio_PlayFifteen;
        public InputAction @PlaySixteen => m_Wrapper.m_SymbolAudio_PlaySixteen;
        public InputAction @PlaySeventeen => m_Wrapper.m_SymbolAudio_PlaySeventeen;
        public InputAction @PlayEighteen => m_Wrapper.m_SymbolAudio_PlayEighteen;
        public InputAction @PlayNineteen => m_Wrapper.m_SymbolAudio_PlayNineteen;
        public InputAction @PlayTwenty => m_Wrapper.m_SymbolAudio_PlayTwenty;
        public InputAction @PlayTwentyOne => m_Wrapper.m_SymbolAudio_PlayTwentyOne;
        public InputAction @PlayTwentyTwo => m_Wrapper.m_SymbolAudio_PlayTwentyTwo;
        public InputAction @PlayTwentyThree => m_Wrapper.m_SymbolAudio_PlayTwentyThree;
        public InputAction @PlayTwentyFour => m_Wrapper.m_SymbolAudio_PlayTwentyFour;
        public InputActionMap Get() { return m_Wrapper.m_SymbolAudio; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SymbolAudioActions set) { return set.Get(); }
        public void SetCallbacks(ISymbolAudioActions instance)
        {
            if (m_Wrapper.m_SymbolAudioActionsCallbackInterface != null)
            {
                @PlayOne.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayOne;
                @PlayOne.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayOne;
                @PlayOne.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayOne;
                @PlayTwo.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwo;
                @PlayTwo.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwo;
                @PlayTwo.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwo;
                @PlayThree.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThree;
                @PlayThree.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThree;
                @PlayThree.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThree;
                @PlayFour.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFour;
                @PlayFour.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFour;
                @PlayFour.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFour;
                @PlayFive.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFive;
                @PlayFive.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFive;
                @PlayFive.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFive;
                @PlaySix.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySix;
                @PlaySix.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySix;
                @PlaySix.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySix;
                @PlaySeven.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeven;
                @PlaySeven.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeven;
                @PlaySeven.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeven;
                @PlayEight.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEight;
                @PlayEight.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEight;
                @PlayEight.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEight;
                @PlayNine.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNine;
                @PlayNine.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNine;
                @PlayNine.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNine;
                @PlayTen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTen;
                @PlayTen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTen;
                @PlayTen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTen;
                @PlayEleven.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEleven;
                @PlayEleven.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEleven;
                @PlayEleven.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEleven;
                @PlayTwelve.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwelve;
                @PlayTwelve.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwelve;
                @PlayTwelve.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwelve;
                @PlayThirteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThirteen;
                @PlayThirteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThirteen;
                @PlayThirteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayThirteen;
                @PlayFourteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFourteen;
                @PlayFourteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFourteen;
                @PlayFourteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFourteen;
                @PlayFifteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFifteen;
                @PlayFifteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFifteen;
                @PlayFifteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayFifteen;
                @PlaySixteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySixteen;
                @PlaySixteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySixteen;
                @PlaySixteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySixteen;
                @PlaySeventeen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeventeen;
                @PlaySeventeen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeventeen;
                @PlaySeventeen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlaySeventeen;
                @PlayEighteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEighteen;
                @PlayEighteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEighteen;
                @PlayEighteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayEighteen;
                @PlayNineteen.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNineteen;
                @PlayNineteen.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNineteen;
                @PlayNineteen.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayNineteen;
                @PlayTwenty.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwenty;
                @PlayTwenty.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwenty;
                @PlayTwenty.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwenty;
                @PlayTwentyOne.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyOne;
                @PlayTwentyOne.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyOne;
                @PlayTwentyOne.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyOne;
                @PlayTwentyTwo.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyTwo;
                @PlayTwentyTwo.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyTwo;
                @PlayTwentyTwo.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyTwo;
                @PlayTwentyThree.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyThree;
                @PlayTwentyThree.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyThree;
                @PlayTwentyThree.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyThree;
                @PlayTwentyFour.started -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyFour;
                @PlayTwentyFour.performed -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyFour;
                @PlayTwentyFour.canceled -= m_Wrapper.m_SymbolAudioActionsCallbackInterface.OnPlayTwentyFour;
            }
            m_Wrapper.m_SymbolAudioActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayOne.started += instance.OnPlayOne;
                @PlayOne.performed += instance.OnPlayOne;
                @PlayOne.canceled += instance.OnPlayOne;
                @PlayTwo.started += instance.OnPlayTwo;
                @PlayTwo.performed += instance.OnPlayTwo;
                @PlayTwo.canceled += instance.OnPlayTwo;
                @PlayThree.started += instance.OnPlayThree;
                @PlayThree.performed += instance.OnPlayThree;
                @PlayThree.canceled += instance.OnPlayThree;
                @PlayFour.started += instance.OnPlayFour;
                @PlayFour.performed += instance.OnPlayFour;
                @PlayFour.canceled += instance.OnPlayFour;
                @PlayFive.started += instance.OnPlayFive;
                @PlayFive.performed += instance.OnPlayFive;
                @PlayFive.canceled += instance.OnPlayFive;
                @PlaySix.started += instance.OnPlaySix;
                @PlaySix.performed += instance.OnPlaySix;
                @PlaySix.canceled += instance.OnPlaySix;
                @PlaySeven.started += instance.OnPlaySeven;
                @PlaySeven.performed += instance.OnPlaySeven;
                @PlaySeven.canceled += instance.OnPlaySeven;
                @PlayEight.started += instance.OnPlayEight;
                @PlayEight.performed += instance.OnPlayEight;
                @PlayEight.canceled += instance.OnPlayEight;
                @PlayNine.started += instance.OnPlayNine;
                @PlayNine.performed += instance.OnPlayNine;
                @PlayNine.canceled += instance.OnPlayNine;
                @PlayTen.started += instance.OnPlayTen;
                @PlayTen.performed += instance.OnPlayTen;
                @PlayTen.canceled += instance.OnPlayTen;
                @PlayEleven.started += instance.OnPlayEleven;
                @PlayEleven.performed += instance.OnPlayEleven;
                @PlayEleven.canceled += instance.OnPlayEleven;
                @PlayTwelve.started += instance.OnPlayTwelve;
                @PlayTwelve.performed += instance.OnPlayTwelve;
                @PlayTwelve.canceled += instance.OnPlayTwelve;
                @PlayThirteen.started += instance.OnPlayThirteen;
                @PlayThirteen.performed += instance.OnPlayThirteen;
                @PlayThirteen.canceled += instance.OnPlayThirteen;
                @PlayFourteen.started += instance.OnPlayFourteen;
                @PlayFourteen.performed += instance.OnPlayFourteen;
                @PlayFourteen.canceled += instance.OnPlayFourteen;
                @PlayFifteen.started += instance.OnPlayFifteen;
                @PlayFifteen.performed += instance.OnPlayFifteen;
                @PlayFifteen.canceled += instance.OnPlayFifteen;
                @PlaySixteen.started += instance.OnPlaySixteen;
                @PlaySixteen.performed += instance.OnPlaySixteen;
                @PlaySixteen.canceled += instance.OnPlaySixteen;
                @PlaySeventeen.started += instance.OnPlaySeventeen;
                @PlaySeventeen.performed += instance.OnPlaySeventeen;
                @PlaySeventeen.canceled += instance.OnPlaySeventeen;
                @PlayEighteen.started += instance.OnPlayEighteen;
                @PlayEighteen.performed += instance.OnPlayEighteen;
                @PlayEighteen.canceled += instance.OnPlayEighteen;
                @PlayNineteen.started += instance.OnPlayNineteen;
                @PlayNineteen.performed += instance.OnPlayNineteen;
                @PlayNineteen.canceled += instance.OnPlayNineteen;
                @PlayTwenty.started += instance.OnPlayTwenty;
                @PlayTwenty.performed += instance.OnPlayTwenty;
                @PlayTwenty.canceled += instance.OnPlayTwenty;
                @PlayTwentyOne.started += instance.OnPlayTwentyOne;
                @PlayTwentyOne.performed += instance.OnPlayTwentyOne;
                @PlayTwentyOne.canceled += instance.OnPlayTwentyOne;
                @PlayTwentyTwo.started += instance.OnPlayTwentyTwo;
                @PlayTwentyTwo.performed += instance.OnPlayTwentyTwo;
                @PlayTwentyTwo.canceled += instance.OnPlayTwentyTwo;
                @PlayTwentyThree.started += instance.OnPlayTwentyThree;
                @PlayTwentyThree.performed += instance.OnPlayTwentyThree;
                @PlayTwentyThree.canceled += instance.OnPlayTwentyThree;
                @PlayTwentyFour.started += instance.OnPlayTwentyFour;
                @PlayTwentyFour.performed += instance.OnPlayTwentyFour;
                @PlayTwentyFour.canceled += instance.OnPlayTwentyFour;
            }
        }
    }
    public SymbolAudioActions @SymbolAudio => new SymbolAudioActions(this);

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
    public interface ISymbolAudioActions
    {
        void OnPlayOne(InputAction.CallbackContext context);
        void OnPlayTwo(InputAction.CallbackContext context);
        void OnPlayThree(InputAction.CallbackContext context);
        void OnPlayFour(InputAction.CallbackContext context);
        void OnPlayFive(InputAction.CallbackContext context);
        void OnPlaySix(InputAction.CallbackContext context);
        void OnPlaySeven(InputAction.CallbackContext context);
        void OnPlayEight(InputAction.CallbackContext context);
        void OnPlayNine(InputAction.CallbackContext context);
        void OnPlayTen(InputAction.CallbackContext context);
        void OnPlayEleven(InputAction.CallbackContext context);
        void OnPlayTwelve(InputAction.CallbackContext context);
        void OnPlayThirteen(InputAction.CallbackContext context);
        void OnPlayFourteen(InputAction.CallbackContext context);
        void OnPlayFifteen(InputAction.CallbackContext context);
        void OnPlaySixteen(InputAction.CallbackContext context);
        void OnPlaySeventeen(InputAction.CallbackContext context);
        void OnPlayEighteen(InputAction.CallbackContext context);
        void OnPlayNineteen(InputAction.CallbackContext context);
        void OnPlayTwenty(InputAction.CallbackContext context);
        void OnPlayTwentyOne(InputAction.CallbackContext context);
        void OnPlayTwentyTwo(InputAction.CallbackContext context);
        void OnPlayTwentyThree(InputAction.CallbackContext context);
        void OnPlayTwentyFour(InputAction.CallbackContext context);
    }
    public interface IOneSwitchActions
    {
        void OnOnlyButton(InputAction.CallbackContext context);
        void OnPuzzleTest(InputAction.CallbackContext context);
    }
}
