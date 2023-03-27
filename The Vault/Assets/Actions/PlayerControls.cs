//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Actions/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Kaitlyn"",
            ""id"": ""fae89db9-db6f-4714-830d-f78fc72059ed"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1272e656-ce01-441b-b070-56426fd3555f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""cb08292a-8b30-4800-8a47-501891c256f5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""c9fb4c28-a112-4ab8-b0b6-ec0f8f76490d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6f401a6c-a042-46b4-bef4-a939a636c389"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""4a0480c4-1d41-4f19-8e8f-8e7ffbcb56fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""119291f8-539a-4661-9ec1-b9a44cbabe17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""d0ee0e4a-bccb-47f1-8cd3-bf002a53e6f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""f89e789b-7862-4017-92d8-13221242764d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2cc9c7a0-c8cb-4a91-b905-82932c1c2196"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""276638cf-5667-4e4b-99a5-01224d223156"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e105abc4-bc33-421d-9c85-77e277deda07"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2d423cd0-9892-4ead-b744-df2e77a55a05"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""49b89535-2fbb-40b1-aec1-afe32f0f4a10"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""34f06c09-d631-488a-9cde-02d312689f95"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6683eeaf-426f-44e8-818f-82af9d51f0ae"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""312d38da-0e14-4afe-a59c-407d7a28a5ae"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c030193-7b4e-4ef3-9547-860e3aa63d3a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c777f31d-47f9-44f7-9288-060b21072c15"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca1662e4-b8fe-4833-a00a-5414824094fb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""eecdb728-ab78-4460-b2c2-b86fe8b661c5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ba49c55a-f90c-4bfa-9529-cb2731a1d87d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a332326c-bab7-4a2d-a4be-24d0cd3f50ca"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb7ba14c-f4f4-4947-936c-94695b8bc016"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27dfb58e-5b6a-4699-8204-4d7197a6d74d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94f542ae-6557-4f4d-b281-117a6b599605"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9314f49-d870-4208-80ca-6422954012df"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""e1c7aeb4-6ac1-4278-ba5c-316b769bc3e3"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""761c07c6-f28a-47ee-88cf-7ebd00a82d08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""7249a076-e769-41bf-a5d5-6f5a31ea0532"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ea310e4-7e7c-454e-aff9-1062e9438c70"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f436ec90-bb7b-4713-8de3-c1e318ff7073"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Kaitlyn
        m_Kaitlyn = asset.FindActionMap("Kaitlyn", throwIfNotFound: true);
        m_Kaitlyn_Move = m_Kaitlyn.FindAction("Move", throwIfNotFound: true);
        m_Kaitlyn_Zoom = m_Kaitlyn.FindAction("Zoom", throwIfNotFound: true);
        m_Kaitlyn_Rotate = m_Kaitlyn.FindAction("Rotate", throwIfNotFound: true);
        m_Kaitlyn_Jump = m_Kaitlyn.FindAction("Jump", throwIfNotFound: true);
        m_Kaitlyn_Crouch = m_Kaitlyn.FindAction("Crouch", throwIfNotFound: true);
        m_Kaitlyn_Sprint = m_Kaitlyn.FindAction("Sprint", throwIfNotFound: true);
        m_Kaitlyn_Attack = m_Kaitlyn.FindAction("Attack", throwIfNotFound: true);
        m_Kaitlyn_Grab = m_Kaitlyn.FindAction("Grab", throwIfNotFound: true);
        m_Kaitlyn_Pause = m_Kaitlyn.FindAction("Pause", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Pause = m_Menu.FindAction("Pause", throwIfNotFound: true);
        m_Menu_Click = m_Menu.FindAction("Click", throwIfNotFound: true);
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

    // Kaitlyn
    private readonly InputActionMap m_Kaitlyn;
    private IKaitlynActions m_KaitlynActionsCallbackInterface;
    private readonly InputAction m_Kaitlyn_Move;
    private readonly InputAction m_Kaitlyn_Zoom;
    private readonly InputAction m_Kaitlyn_Rotate;
    private readonly InputAction m_Kaitlyn_Jump;
    private readonly InputAction m_Kaitlyn_Crouch;
    private readonly InputAction m_Kaitlyn_Sprint;
    private readonly InputAction m_Kaitlyn_Attack;
    private readonly InputAction m_Kaitlyn_Grab;
    private readonly InputAction m_Kaitlyn_Pause;
    public struct KaitlynActions
    {
        private @PlayerControls m_Wrapper;
        public KaitlynActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Kaitlyn_Move;
        public InputAction @Zoom => m_Wrapper.m_Kaitlyn_Zoom;
        public InputAction @Rotate => m_Wrapper.m_Kaitlyn_Rotate;
        public InputAction @Jump => m_Wrapper.m_Kaitlyn_Jump;
        public InputAction @Crouch => m_Wrapper.m_Kaitlyn_Crouch;
        public InputAction @Sprint => m_Wrapper.m_Kaitlyn_Sprint;
        public InputAction @Attack => m_Wrapper.m_Kaitlyn_Attack;
        public InputAction @Grab => m_Wrapper.m_Kaitlyn_Grab;
        public InputAction @Pause => m_Wrapper.m_Kaitlyn_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Kaitlyn; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KaitlynActions set) { return set.Get(); }
        public void SetCallbacks(IKaitlynActions instance)
        {
            if (m_Wrapper.m_KaitlynActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnMove;
                @Zoom.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnZoom;
                @Rotate.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnRotate;
                @Jump.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnCrouch;
                @Sprint.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnSprint;
                @Attack.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnAttack;
                @Grab.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnGrab;
                @Pause.started -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KaitlynActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_KaitlynActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public KaitlynActions @Kaitlyn => new KaitlynActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Pause;
    private readonly InputAction m_Menu_Click;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Menu_Pause;
        public InputAction @Click => m_Wrapper.m_Menu_Click;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Click.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IKaitlynActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
}
