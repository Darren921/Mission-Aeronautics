//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Script/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""15d6b42c-ea72-4562-89be-2bcda4a2f32b"",
            ""actions"": [
                {
                    ""name"": ""Player1Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5a5db870-a1c9-460f-ba3f-769120240a37"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Player1NormalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""0ae4048c-0dd4-46ee-8528-7f3e4f3435b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Player1SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""f2a4d227-5e47-458b-a6d1-04f0cbe67d55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Player1CollectPowerUp"",
                    ""type"": ""Button"",
                    ""id"": ""fe54aa6d-4f95-4012-9f0c-b04e840d6ea8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerShoot"",
                    ""type"": ""Button"",
                    ""id"": ""05e42008-5f20-4dc9-a1a6-6b3bcd3d0f25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerBlock"",
                    ""type"": ""Button"",
                    ""id"": ""01c6b505-61a5-4112-9045-d3957cbfd2fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""NextDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""33698928-c714-41b1-bc62-c0bca32d90e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""22d1273e-5bdc-439c-ad5e-09ca7993ecc3"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f44f18c-f16b-45e2-acf3-0971e05ce8fb"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10e31874-1578-45f6-9c24-a76346c6d076"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""233bb69d-7eff-452a-a978-b1f4ddf6c223"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b8ec45fa-9e99-44b3-a770-ef69ff428215"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4a244544-feb1-4b31-8cfc-eb7cd528c9af"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7c0202b5-e94f-44da-a59c-faeb4de1ca19"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""df2d501c-6848-4268-ae0f-fda9a74e9f1a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""567bc302-e213-4c4c-9c2f-3b5cbbbccb60"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58239aae-1b11-41eb-aa9e-8ff0c2fae1ed"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1CollectPowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc3ba983-0414-4b3b-b14e-68b569625335"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1CollectPowerUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39359921-bffa-4cc1-9c44-c7c0317197f2"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3c22a6b-80d2-4ada-96c0-0e78dd80b090"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""729c4df5-2b9e-49d0-9920-4129d7787e05"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player1SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c261af0f-ef75-48b6-8c6b-8ccdec001d59"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""079c623b-e6ea-4d14-844b-2016566af88b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e482a071-64bb-4d74-a9fc-de2e73e1fc2d"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f947fbd2-e785-41b5-aa5f-8e25810d41b9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Player1Movement = m_InGame.FindAction("Player1Movement", throwIfNotFound: true);
        m_InGame_Player1NormalAttack = m_InGame.FindAction("Player1NormalAttack", throwIfNotFound: true);
        m_InGame_Player1SpecialAttack = m_InGame.FindAction("Player1SpecialAttack", throwIfNotFound: true);
        m_InGame_Player1CollectPowerUp = m_InGame.FindAction("Player1CollectPowerUp", throwIfNotFound: true);
        m_InGame_PlayerShoot = m_InGame.FindAction("PlayerShoot", throwIfNotFound: true);
        m_InGame_PlayerBlock = m_InGame.FindAction("PlayerBlock", throwIfNotFound: true);
        m_InGame_NextDialogue = m_InGame.FindAction("NextDialogue", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private List<IInGameActions> m_InGameActionsCallbackInterfaces = new List<IInGameActions>();
    private readonly InputAction m_InGame_Player1Movement;
    private readonly InputAction m_InGame_Player1NormalAttack;
    private readonly InputAction m_InGame_Player1SpecialAttack;
    private readonly InputAction m_InGame_Player1CollectPowerUp;
    private readonly InputAction m_InGame_PlayerShoot;
    private readonly InputAction m_InGame_PlayerBlock;
    private readonly InputAction m_InGame_NextDialogue;
    public struct InGameActions
    {
        private @Controls m_Wrapper;
        public InGameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Player1Movement => m_Wrapper.m_InGame_Player1Movement;
        public InputAction @Player1NormalAttack => m_Wrapper.m_InGame_Player1NormalAttack;
        public InputAction @Player1SpecialAttack => m_Wrapper.m_InGame_Player1SpecialAttack;
        public InputAction @Player1CollectPowerUp => m_Wrapper.m_InGame_Player1CollectPowerUp;
        public InputAction @PlayerShoot => m_Wrapper.m_InGame_PlayerShoot;
        public InputAction @PlayerBlock => m_Wrapper.m_InGame_PlayerBlock;
        public InputAction @NextDialogue => m_Wrapper.m_InGame_NextDialogue;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void AddCallbacks(IInGameActions instance)
        {
            if (instance == null || m_Wrapper.m_InGameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InGameActionsCallbackInterfaces.Add(instance);
            @Player1Movement.started += instance.OnPlayer1Movement;
            @Player1Movement.performed += instance.OnPlayer1Movement;
            @Player1Movement.canceled += instance.OnPlayer1Movement;
            @Player1NormalAttack.started += instance.OnPlayer1NormalAttack;
            @Player1NormalAttack.performed += instance.OnPlayer1NormalAttack;
            @Player1NormalAttack.canceled += instance.OnPlayer1NormalAttack;
            @Player1SpecialAttack.started += instance.OnPlayer1SpecialAttack;
            @Player1SpecialAttack.performed += instance.OnPlayer1SpecialAttack;
            @Player1SpecialAttack.canceled += instance.OnPlayer1SpecialAttack;
            @Player1CollectPowerUp.started += instance.OnPlayer1CollectPowerUp;
            @Player1CollectPowerUp.performed += instance.OnPlayer1CollectPowerUp;
            @Player1CollectPowerUp.canceled += instance.OnPlayer1CollectPowerUp;
            @PlayerShoot.started += instance.OnPlayerShoot;
            @PlayerShoot.performed += instance.OnPlayerShoot;
            @PlayerShoot.canceled += instance.OnPlayerShoot;
            @PlayerBlock.started += instance.OnPlayerBlock;
            @PlayerBlock.performed += instance.OnPlayerBlock;
            @PlayerBlock.canceled += instance.OnPlayerBlock;
            @NextDialogue.started += instance.OnNextDialogue;
            @NextDialogue.performed += instance.OnNextDialogue;
            @NextDialogue.canceled += instance.OnNextDialogue;
        }

        private void UnregisterCallbacks(IInGameActions instance)
        {
            @Player1Movement.started -= instance.OnPlayer1Movement;
            @Player1Movement.performed -= instance.OnPlayer1Movement;
            @Player1Movement.canceled -= instance.OnPlayer1Movement;
            @Player1NormalAttack.started -= instance.OnPlayer1NormalAttack;
            @Player1NormalAttack.performed -= instance.OnPlayer1NormalAttack;
            @Player1NormalAttack.canceled -= instance.OnPlayer1NormalAttack;
            @Player1SpecialAttack.started -= instance.OnPlayer1SpecialAttack;
            @Player1SpecialAttack.performed -= instance.OnPlayer1SpecialAttack;
            @Player1SpecialAttack.canceled -= instance.OnPlayer1SpecialAttack;
            @Player1CollectPowerUp.started -= instance.OnPlayer1CollectPowerUp;
            @Player1CollectPowerUp.performed -= instance.OnPlayer1CollectPowerUp;
            @Player1CollectPowerUp.canceled -= instance.OnPlayer1CollectPowerUp;
            @PlayerShoot.started -= instance.OnPlayerShoot;
            @PlayerShoot.performed -= instance.OnPlayerShoot;
            @PlayerShoot.canceled -= instance.OnPlayerShoot;
            @PlayerBlock.started -= instance.OnPlayerBlock;
            @PlayerBlock.performed -= instance.OnPlayerBlock;
            @PlayerBlock.canceled -= instance.OnPlayerBlock;
            @NextDialogue.started -= instance.OnNextDialogue;
            @NextDialogue.performed -= instance.OnNextDialogue;
            @NextDialogue.canceled -= instance.OnNextDialogue;
        }

        public void RemoveCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInGameActions instance)
        {
            foreach (var item in m_Wrapper.m_InGameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InGameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    public interface IInGameActions
    {
        void OnPlayer1Movement(InputAction.CallbackContext context);
        void OnPlayer1NormalAttack(InputAction.CallbackContext context);
        void OnPlayer1SpecialAttack(InputAction.CallbackContext context);
        void OnPlayer1CollectPowerUp(InputAction.CallbackContext context);
        void OnPlayerShoot(InputAction.CallbackContext context);
        void OnPlayerBlock(InputAction.CallbackContext context);
        void OnNextDialogue(InputAction.CallbackContext context);
    }
}
