//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Input/UserInput.inputactions
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

public partial class @UserInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UserInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UserInput"",
    ""maps"": [
        {
            ""name"": ""Control"",
            ""id"": ""99d505a7-4194-4fbc-98e3-784c8a299387"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""919b8e0f-955a-4651-b80b-ab0a756645c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""00028756-4ea4-4527-bab5-259344446634"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""03710a40-e989-4107-9524-0f477dcd38ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0d8a1c69-5784-42eb-ad82-9d6c8f1b4d1f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b1c05da1-437e-4893-a2ac-a8a98c358643"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b25d69e5-f788-4155-b189-02267589e7c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""92cc3a22-4608-46e2-bd44-b037be955cc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e39bf0d8-d838-479a-9b24-448028828113"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""35f8196a-a021-4e74-98ca-036009e76859"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1034ed57-d8a4-469b-bc22-e92162daea6e"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""8913b002-a10a-4d25-bce7-e028920128fe"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34d1ed33-0db6-4669-bafa-63fe4885e020"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e715983d-c219-471b-ae01-5a356d8360f2"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""372c0719-e86e-4bfe-809c-c4aae8d513e3"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cd32e042-6e43-448c-9b6e-8bee48a9fde3"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e8f5c93-75df-4027-afc8-a4d218dbfdbb"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b3ca0d83-0f8b-4d5e-be3f-e78de244c250"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d8350a38-7594-4a85-adde-2c9b16cef78d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6815c77d-dfa7-4dce-abf2-6a5798c4ef21"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""657f0f46-b179-42d5-8740-827080c02dfd"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""a2238af1-d31e-412b-805a-e41d1d9f0fff"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e9226061-0ec9-4c57-be02-af44abb61d73"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5e4f82bb-4042-4521-88de-a73589fe1acc"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f303b098-ae60-4a5c-bd6e-d91f69a0559e"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0bf2219e-6560-4f60-b6fb-6e4a58353133"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""530b192e-660a-44e6-bb49-8b47ae6def74"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1756b788-c3d7-4cb6-8f9b-820570982538"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""53daf477-2ec7-49d6-9535-2406eba1aa53"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""97cbcbb7-4ef6-4012-84de-0380477d6178"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cb862aa9-660d-4bf5-9e74-2fde3cb6a2f3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""537bdab8-26f5-4d34-99df-fb6375f75c3e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""06f39364-d6b6-4ca9-b1ec-a741db3d6461"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f2b9d6ff-c566-416c-9482-ce67eec9e266"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5cef40ae-e4d3-40f0-bf35-322d2f484ed4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""221fe031-cbac-465d-b9be-437168719fae"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62408c24-52c4-41d6-b48c-14e2bc73fdc1"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea4b7789-4640-4b9c-bcab-a98b7cf03f7a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4b150db-f038-4299-8778-c22259782175"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2bbaaa8-d3ab-4614-a93d-a256fcad1933"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1452c997-cff1-4b41-91a3-563d625d7050"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""834e6d38-0499-4212-a553-1ef1b631df53"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fa72b65-c796-4aa5-9a7b-b0cde6ac776e"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eca90879-dd15-4af0-a838-39c597ad7648"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71c949bd-7d00-4f40-85f4-58c20e70b2fe"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e150089-dc7b-47cb-b682-c14824a65de2"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbb38768-86ba-4159-aefe-100005050f8f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d92b539-53b2-4a58-8473-7dc271bcaea3"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b79af4ae-29bb-47a0-af1c-1968d23d71cd"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Control
        m_Control = asset.FindActionMap("Control", throwIfNotFound: true);
        m_Control_Navigate = m_Control.FindAction("Navigate", throwIfNotFound: true);
        m_Control_Submit = m_Control.FindAction("Submit", throwIfNotFound: true);
        m_Control_Cancel = m_Control.FindAction("Cancel", throwIfNotFound: true);
        m_Control_Point = m_Control.FindAction("Point", throwIfNotFound: true);
        m_Control_Click = m_Control.FindAction("Click", throwIfNotFound: true);
        m_Control_ScrollWheel = m_Control.FindAction("ScrollWheel", throwIfNotFound: true);
        m_Control_MiddleClick = m_Control.FindAction("MiddleClick", throwIfNotFound: true);
        m_Control_RightClick = m_Control.FindAction("RightClick", throwIfNotFound: true);
        m_Control_TrackedDevicePosition = m_Control.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_Control_TrackedDeviceOrientation = m_Control.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
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

    // Control
    private readonly InputActionMap m_Control;
    private IControlActions m_ControlActionsCallbackInterface;
    private readonly InputAction m_Control_Navigate;
    private readonly InputAction m_Control_Submit;
    private readonly InputAction m_Control_Cancel;
    private readonly InputAction m_Control_Point;
    private readonly InputAction m_Control_Click;
    private readonly InputAction m_Control_ScrollWheel;
    private readonly InputAction m_Control_MiddleClick;
    private readonly InputAction m_Control_RightClick;
    private readonly InputAction m_Control_TrackedDevicePosition;
    private readonly InputAction m_Control_TrackedDeviceOrientation;
    public struct ControlActions
    {
        private @UserInput m_Wrapper;
        public ControlActions(@UserInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Control_Navigate;
        public InputAction @Submit => m_Wrapper.m_Control_Submit;
        public InputAction @Cancel => m_Wrapper.m_Control_Cancel;
        public InputAction @Point => m_Wrapper.m_Control_Point;
        public InputAction @Click => m_Wrapper.m_Control_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_Control_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_Control_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_Control_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_Control_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_Control_TrackedDeviceOrientation;
        public InputActionMap Get() { return m_Wrapper.m_Control; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions set) { return set.Get(); }
        public void SetCallbacks(IControlActions instance)
        {
            if (m_Wrapper.m_ControlActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnTrackedDeviceOrientation;
            }
            m_Wrapper.m_ControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
            }
        }
    }
    public ControlActions @Control => new ControlActions(this);
    public interface IControlActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
    }
}
