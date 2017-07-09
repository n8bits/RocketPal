using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RocketPal.Controller
{
    public static class RocketLeagueKeyboardBindings
    {
        public static VirtualKeyCode ThrottleForward = VirtualKeyCode.VK_W;
        public static VirtualKeyCode ThrottleReverse = VirtualKeyCode.VK_S;
        public static VirtualKeyCode SteerRight = VirtualKeyCode.VK_D;
        public static VirtualKeyCode SteerLeft = VirtualKeyCode.VK_A;
            //LookUp = WindowsInput.MouseBu                          MouseY; AxisSign = VirtualKeyCode.VK_                                 AxisSign_Positive
        //    public static VirtualKeyCode //LookDown = VirtualKeyCode.VK_                                 MouseY; AxisSign = VirtualKeyCode.VK_                                 AxisSign_Negative
        //    public static VirtualKeyCode //LookRight = VirtualKeyCode.VK_                                 MouseX; AxisSign = VirtualKeyCode.VK_                                 AxisSign_Positive
        //    public static VirtualKeyCode //LookLeft = VirtualKeyCode.VK_                                 MouseX; AxisSign = VirtualKeyCode.VK_                                 AxisSign_Negative
        public static VirtualKeyCode YawRight = VirtualKeyCode.VK_D;
        public static VirtualKeyCode YawLeft = VirtualKeyCode.VK_A;
        public static VirtualKeyCode PitchUp = VirtualKeyCode.VK_S;
        public static VirtualKeyCode PitchDown = VirtualKeyCode.VK_W;
        public static VirtualKeyCode RollRight = VirtualKeyCode.VK_E;
        public static VirtualKeyCode RollLeft = VirtualKeyCode.VK_Q;
        public static MouseButton Boost = MouseButton.LeftButton;
        public static MouseButton Jump = MouseButton.RightButton;
        public static VirtualKeyCode Handbrake = VirtualKeyCode.LSHIFT;
        public static VirtualKeyCode SecondaryCamera = VirtualKeyCode.SPACE;
        public static VirtualKeyCode ToggleRoll = VirtualKeyCode.LSHIFT;
        public static MouseButton ReadyUp = MouseButton.RightButton;
        public static MouseButton RearCamera = MouseButton.MiddleButton;
        public static VirtualKeyCode UsePickup = VirtualKeyCode.VK_R;
        public static VirtualKeyCode ToggleMidGameMenu = VirtualKeyCode.ESCAPE;
        public static VirtualKeyCode ToggleScoreboard = VirtualKeyCode.TAB;
        public static VirtualKeyCode PushToTalk = VirtualKeyCode.VK_F;
        public static VirtualKeyCode Chat = VirtualKeyCode.VK_T;
        public static VirtualKeyCode TeamChat = VirtualKeyCode.VK_Y;
        public static VirtualKeyCode PartyChat = VirtualKeyCode.VK_U;
        public static VirtualKeyCode ReplayMoveForward = VirtualKeyCode.VK_W;
        public static VirtualKeyCode ReplayMoveBackward = VirtualKeyCode.VK_S;
        public static VirtualKeyCode ReplayMoveRight = VirtualKeyCode.VK_D;
        public static VirtualKeyCode ReplayMoveLeft = VirtualKeyCode.VK_A;
        public static VirtualKeyCode ReplayRollRight = VirtualKeyCode.VK_E;
        public static VirtualKeyCode ReplayRollLeft = VirtualKeyCode.VK_Q;
        public static VirtualKeyCode ReplayMoveUp = VirtualKeyCode.SPACE;
        public static VirtualKeyCode ReplayMoveDown = VirtualKeyCode.LCONTROL;
        //public static VirtualKeyCode //ReplayZoomIn = VirtualKeyCode.VK_        MouseScrollUp
        //    public static VirtualKeyCode //ReplayZoomOut = VirtualKeyCode.VK_                                 MouseScrollDown
        //    public static VirtualKeyCode //ReplayAddKeyframe = VirtualKeyCode.UP; //_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayRemoveKeyframe = VirtualKeyCode.DOWN; //BPT_Hold
        //    public static VirtualKeyCode //ReplayPrevKeyframe = VirtualKeyCode.VK_Left; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayNextKeyframe = VirtualKeyCode.VK_                                 Right; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayScrubBackward = VirtualKeyCode.VK_                                 Left; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayScrubForward = VirtualKeyCode.VK_                                 Right; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayCycleHUD = VirtualKeyCode.VK_                                 H; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayPause = VirtualKeyCode.VK_                                 LeftShift; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplaySpeedMenu = VirtualKeyCode.VK_                                 LeftShift; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayCycleFocus = VirtualKeyCode.VK_                                 LeftMouseButton; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayFocusMenu = VirtualKeyCode.VK_                                 LeftMouseButton; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayCycleCamera = VirtualKeyCode.VK_                                 RightMouseButton; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayCameraMenu = VirtualKeyCode.VK_                                 RightMouseButton; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayResetView = VirtualKeyCode.VK_                                 R; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //    public static VirtualKeyCode //ReplayToggleRoll = VirtualKeyCode.VK_                                 R; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //    public static VirtualKeyCode //ReplayShowControls = VirtualKeyCode.VK_                                 I
            //public static VirtualKeyCode ChatPreset1 = VirtualKeyCode.OEM_1;
        public static VirtualKeyCode ChatPreset2 = VirtualKeyCode.OEM_2;
        public static VirtualKeyCode ChatPreset3 = VirtualKeyCode.OEM_3;
        public static VirtualKeyCode ChatPreset4 = VirtualKeyCode.OEM_4;
        public static VirtualKeyCode ResetTraining = VirtualKeyCode.BACK;
        public static VirtualKeyCode MusicNextTrack = VirtualKeyCode.VK_N;
        //AutoSaveReplay = VirtualKeyCode.VK_BACK; PressType = VirtualKeyCode.VK_                                 BPT_Hold
        //ReplayViewT0P0 = VirtualKeyCode.VK_                                 One
        //ReplayViewT0P1 = VirtualKeyCode.VK_                                 Two
        //ReplayViewT0P2 = VirtualKeyCode.VK_                                 Three
        //ReplayViewT0P3 = VirtualKeyCode.VK_                                 Four
        //ReplayViewT1P0 = VirtualKeyCode.VK_                                 Five
        //ReplayViewT1P1 = VirtualKeyCode.VK_                                 Six
        //ReplayViewT1P2 = VirtualKeyCode.VK_                                 Seven
        //ReplayViewT1P3 = VirtualKeyCode.VK_                                 Eight
        //ReplayViewBall = VirtualKeyCode.VK_                                 Nine
        //ReplayViewNone = VirtualKeyCode.VK_                                 Zero
        //EditorPreviewTrajectory = VirtualKeyCode.VK_                                 T
        //EditorCycleActor = VirtualKeyCode.VK_                                 LeftMouseButton
        //EditorReleaseActor = VirtualKeyCode.VK_                                 RightMouseButton
        //EditorIncreaseTime = VirtualKeyCode.VK_                                 Up
        //EditorDecreaseTime = VirtualKeyCode.VK_                                 Down
        //EditorUndo = VirtualKeyCode.VK_                                 Z
        //EditorRedo = VirtualKeyCode.VK_                                 X
        //EditorIncreasePower = VirtualKeyCode.VK_                                 MouseScrollUp
        //EditorDecreasePower = VirtualKeyCode.VK_                                 MouseScrollDown
        //EditorTestShot = VirtualKeyCode.VK_                                 Tab; PressType = VirtualKeyCode.VK_                                 BPT_Tap
        //EditorToggleCamera = VirtualKeyCode.VK_                                 MiddleMouseButton
        //StopEditing = VirtualKeyCode.VK_                                 LeftShift
        //EditorShowControls = VirtualKeyCode.VK_                                 C

    }

}
