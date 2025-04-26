using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CabInputTesting : MonoBehaviour
{
    public Sprite idleButton;
    public Sprite activeButton;

    public Image stickUp;
    public Image stickDown;
    public Image stickLeft;
    public Image stickRight;

    public Color stickActiveColor;
    
    public Image bottomRow1;
    public Image bottomRow2;
    public Image topRow1;
    public Image topRow2;
    public Image topRow3;
    public Image bottomRow3;
    public Image playerButton;
    public Image coinSlot;

    public bool isPlayerOne;

    public TMPro.TMP_Text playerDeviceIDText;
    public TMPro.TMP_Text playerDeviceManufacturerText;
    public TMPro.TMP_Text deviceList;
    
    private BGSCabinetInput _bgsCabinetInput;
    private InputAction _stick;

    private ReadOnlyArray<InputDevice> devices;
    private List<Joystick> joysticks = new List<Joystick>();
    private List<Gamepad> gamepads = new List<Gamepad>();
    private int _lastDeviceID = -1;
    
    
    void Awake()
    {
        _bgsCabinetInput = new BGSCabinetInput();

        string[] joystickNames = Input.GetJoystickNames();
        
        devices = InputSystem.devices;
        
        joysticks = new List<Joystick>(InputSystem.devices.OfType<Joystick>());
        gamepads = new List<Gamepad>(InputSystem.devices.OfType<Gamepad>());

        if (joysticks.Count() > 0)
        {
            for (int i = 0; i < joysticks.Count; i++)
            {
                Debug.Log(joysticks[i].deviceId);
                if (i == 0) DeviceConstants.playerOneDeviceID = joysticks[i].deviceId;
                if (i == 1) DeviceConstants.playerTwoDeviceID = joysticks[i].deviceId;
            }
    
            if (joysticks.Count() > 1)
            {
                playerDeviceIDText.text = (isPlayerOne) ? "Player 1 DeviceID: " + joysticks[0].deviceId.ToString() : "Player 2 DeviceID: " + joysticks[1].deviceId.ToString();
                playerDeviceManufacturerText.text = (isPlayerOne) ? "Player 1 Manufacturer: " + joysticks[0].description.manufacturer : "Player 2 Manufacturer: " + joysticks[1].description.manufacturer;
            }
            else
            {
                playerDeviceIDText.text = "Player 1 DeviceID: " + joysticks[0].deviceId.ToString();
                playerDeviceManufacturerText.text = "Player 1 Manufacturer: " + joysticks[0].description.manufacturer;
            }
        }
        
        if (gamepads.Count() > 0)
        {
            for (int i = 0; i < gamepads.Count; i++)
            {
                if (i == 0) DeviceConstants.playerOneDeviceID = gamepads[i].deviceId;
                if (i == 1) DeviceConstants.playerTwoDeviceID = gamepads[i].deviceId;
            }
    
            if (gamepads.Count() > 1)
            {
                playerDeviceIDText.text = (isPlayerOne) ? "Player 1 DeviceID: " + gamepads[0].deviceId.ToString() : "Player 2 DeviceID: " + gamepads[1].deviceId.ToString();
                playerDeviceManufacturerText.text = (isPlayerOne) ? "Player 1 Manufacturer: " + gamepads[0].description.manufacturer : "Player 2 Manufacturer: " + gamepads[1].description.manufacturer;
            }
            else
            {
                playerDeviceIDText.text = "Player 1 DeviceID: " + gamepads[0].deviceId.ToString();
                playerDeviceManufacturerText.text = "Player 1 Manufacturer: " + gamepads[0].description.manufacturer;
            }
        }


        if (isPlayerOne)
        {
            deviceList.text = "JoysticksDetected: " + joysticks.Count + "\n"
                              + "GamepadsDetected: " + gamepads.Count + "\n"
                              + ((joysticks.Count > 0 ) ? "Player 1 DeviceID: " + joysticks[0].deviceId.ToString() + "\n" : "")
                            + ((joysticks.Count > 1 ) ? "Player 2 DeviceID: " + joysticks[1].deviceId.ToString() + "\n" : "")
                            + ((gamepads.Count() > 0) ? "Gamepad 1 DeviceID: " + gamepads[0].deviceId + "\n" : "")
                            + ((gamepads.Count() > 1) ? "Gamepad 2 DeviceID: " + gamepads[1].deviceId + "\n" : "");
        }
    }
    
    void OnEnable()
    {
#if PLATFORM_STANDALONE_LINUX && !UNITY_EDITOR
        _bgsCabinetInput.LinuxGameplay.BottomRow1.performed += BottomRow1OnPerformed;
        _bgsCabinetInput.LinuxGameplay.BottomRow1.canceled += ctx => bottomRow1.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.BottomRow1.Enable();
        
        _bgsCabinetInput.LinuxGameplay.BottomRow2.performed += BottomRow2OnPerformed;
        _bgsCabinetInput.LinuxGameplay.BottomRow2.canceled += ctx => bottomRow2.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.BottomRow2.Enable();
        
        _bgsCabinetInput.LinuxGameplay.TopRow1.performed += TopRow1OnPerformed;
        _bgsCabinetInput.LinuxGameplay.TopRow1.canceled += ctx => topRow1.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.TopRow1.Enable();
        
        _bgsCabinetInput.LinuxGameplay.TopRow2.performed += TopRow2OnPerformed;
        _bgsCabinetInput.LinuxGameplay.TopRow2.canceled += ctx => topRow2.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.TopRow2.Enable();
        
        _bgsCabinetInput.LinuxGameplay.TopRow3.performed += TopRow3OnPerformed;
        _bgsCabinetInput.LinuxGameplay.TopRow3.canceled += ctx => topRow3.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.TopRow3.Enable();
        
        _bgsCabinetInput.LinuxGameplay.BottomRow3.performed += BottomRow3OnPerformed;
        _bgsCabinetInput.LinuxGameplay.BottomRow3.canceled += ctx => bottomRow3.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.BottomRow3.Enable();
        
        _bgsCabinetInput.LinuxGameplay.PlayerButton.started += PlayerButtonOnStarted;
        _bgsCabinetInput.LinuxGameplay.PlayerButton.canceled += ctx => playerButton.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.PlayerButton.Enable();
        
        _bgsCabinetInput.LinuxGameplay.CoinSlot.started += CoinSlotOnStarted;
        _bgsCabinetInput.LinuxGameplay.CoinSlot.canceled += ctx => coinSlot.sprite = idleButton;
        _bgsCabinetInput.LinuxGameplay.CoinSlot.Enable();
        
        _stick = _bgsCabinetInput.LinuxGameplay.Stick;
        _stick.performed += OnStickPerformed;
        _stick.Enable();
#else
        _bgsCabinetInput.Gameplay.BottomRow1.performed += BottomRow1OnPerformed;
        _bgsCabinetInput.Gameplay.BottomRow1.canceled += ctx => bottomRow1.sprite = idleButton;
        _bgsCabinetInput.Gameplay.BottomRow1.Enable();
        
        _bgsCabinetInput.Gameplay.BottomRow2.performed += BottomRow2OnPerformed;
        _bgsCabinetInput.Gameplay.BottomRow2.canceled += ctx => bottomRow2.sprite = idleButton;
        _bgsCabinetInput.Gameplay.BottomRow2.Enable();
        
        _bgsCabinetInput.Gameplay.TopRow1.performed += TopRow1OnPerformed;
        _bgsCabinetInput.Gameplay.TopRow1.canceled += ctx => topRow1.sprite = idleButton;
        _bgsCabinetInput.Gameplay.TopRow1.Enable();
        
        _bgsCabinetInput.Gameplay.TopRow2.performed += TopRow2OnPerformed;
        _bgsCabinetInput.Gameplay.TopRow2.canceled += ctx => topRow2.sprite = idleButton;
        _bgsCabinetInput.Gameplay.TopRow2.Enable();
        
        _bgsCabinetInput.Gameplay.TopRow3.performed += TopRow3OnPerformed;
        _bgsCabinetInput.Gameplay.TopRow3.canceled += ctx => topRow3.sprite = idleButton;
        _bgsCabinetInput.Gameplay.TopRow3.Enable();
        
        _bgsCabinetInput.Gameplay.BottomRow3.performed += BottomRow3OnPerformed;
        _bgsCabinetInput.Gameplay.BottomRow3.canceled += ctx => bottomRow3.sprite = idleButton;
        _bgsCabinetInput.Gameplay.BottomRow3.Enable();
        
        _bgsCabinetInput.Gameplay.PlayerButton.started += PlayerButtonOnStarted;
        _bgsCabinetInput.Gameplay.PlayerButton.canceled += ctx => playerButton.sprite = idleButton;
        _bgsCabinetInput.Gameplay.PlayerButton.Enable();
        
        _bgsCabinetInput.Gameplay.CoinSlot.started += CoinSlotOnStarted;
        _bgsCabinetInput.Gameplay.CoinSlot.canceled += ctx => coinSlot.sprite = idleButton;
        _bgsCabinetInput.Gameplay.CoinSlot.Enable();
        
        _stick = _bgsCabinetInput.Gameplay.Stick;
        _stick.performed += OnStickPerformed;
        _stick.Enable();
#endif

    }

    private void OnStickPerformed(InputAction.CallbackContext obj)
    {
        _lastDeviceID = obj.control.device.deviceId;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne && _lastDeviceID != DeviceConstants.playerOneDeviceID) return;
        if(!isPlayerOne && _lastDeviceID != DeviceConstants.playerTwoDeviceID) return;
        
        Vector2 stickValue = _stick.ReadValue<Vector2>();
        
        if (stickValue.y > 0.5f)
        {
            stickUp.color = stickActiveColor;
        }
        else
        {
            stickUp.color = Color.white;
        }
        
        if (stickValue.y < -0.5f)
        {
            stickDown.color = stickActiveColor;
        }
        else
        {
            stickDown.color = Color.white;
        }
        
        if (stickValue.x < -0.5f)
        {
            stickLeft.color = stickActiveColor;
        }
        else
        {
            stickLeft.color = Color.white;
        }
        
        if (stickValue.x > 0.5f)
        {
            stickRight.color = stickActiveColor;
        }
        else
        {
            stickRight.color = Color.white;
        }
    }

    private void BottomRow1OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            bottomRow1.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            bottomRow1.sprite = activeButton;
        }
    }
    
    private void BottomRow2OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            bottomRow2.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            bottomRow2.sprite = activeButton;
        }
    }
    
    private void TopRow1OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            topRow1.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            topRow1.sprite = activeButton;
        }
    }
    
    private void TopRow2OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            topRow2.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            topRow2.sprite = activeButton;
        }
    }
    
    private void TopRow3OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            topRow3.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            topRow3.sprite = activeButton;
        }
    }
    
    private void BottomRow3OnPerformed(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            bottomRow3.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            bottomRow3.sprite = activeButton;
        }
    }
    
    private void PlayerButtonOnStarted(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            playerButton.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            playerButton.sprite = activeButton;
        }
    }
    
    private void CoinSlotOnStarted(InputAction.CallbackContext obj)
    {
        if (isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerOneDeviceID)
        {
            coinSlot.sprite = activeButton;
        }
        else if (!isPlayerOne && obj.control.device.deviceId == DeviceConstants.playerTwoDeviceID)
        {
            coinSlot.sprite = activeButton;
        }
    }
    
    private void OnDisable()
    {
#if PLATFORM_STANDALONE_LINUX && !UNITY_EDITOR
        _bgsCabinetInput.LinuxGameplay.BottomRow1.Disable();
        _bgsCabinetInput.LinuxGameplay.BottomRow2.Disable();
        _bgsCabinetInput.LinuxGameplay.TopRow1.Disable();
        _bgsCabinetInput.LinuxGameplay.TopRow2.Disable();
        _bgsCabinetInput.LinuxGameplay.TopRow3.Disable();
        _bgsCabinetInput.LinuxGameplay.BottomRow3.Disable();
        _bgsCabinetInput.LinuxGameplay.PlayerButton.Disable();
        _bgsCabinetInput.LinuxGameplay.CoinSlot.Disable();
        _stick.Disable();
#else
        _bgsCabinetInput.Gameplay.BottomRow1.Disable();
        _bgsCabinetInput.Gameplay.BottomRow2.Disable();
        _bgsCabinetInput.Gameplay.TopRow1.Disable();
        _bgsCabinetInput.Gameplay.TopRow2.Disable();
        _bgsCabinetInput.Gameplay.TopRow3.Disable();
        _bgsCabinetInput.Gameplay.BottomRow3.Disable();
        _bgsCabinetInput.Gameplay.PlayerButton.Disable();
        _bgsCabinetInput.Gameplay.CoinSlot.Disable();
        _stick.Disable();
#endif
    }
}
