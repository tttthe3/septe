using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Playerinput : MonoBehaviour
{
    public bool HaveControl { get { return m_HaveControl; } }
    protected bool m_HaveControl = true;

    [SerializeField]
    public enum Inputtype
    {
        Mouseandkey, PS4Controller,
    }

    public enum PS4ControllerButton
    {
        None,
        Circle,
        Cross,
        Square,
        Triangle,
        Leftstick,
        Rightstick,
        R1,
        R2,
        L1,
        L2,
        View,
        Menu
    }

    public enum PS4COntrollerAxes
    {
        None,
        LeftStickHorizontal,
        LeftStickVertical,
        RightStickHorizontal,
        RightStickVertical,
        Select_Hoz,
        Select_Vert

    }

    public InputButoon Pause = new InputButoon(KeyCode.P, PS4ControllerButton.Menu);
    public InputButoon Attack = new InputButoon(KeyCode.K, PS4ControllerButton.Square); //四角攻撃
    public InputButoon Jump = new InputButoon(KeyCode.Space, PS4ControllerButton.Cross); //ジャンプ罰
    public InputButoon Dash = new InputButoon(KeyCode.C, PS4ControllerButton.Circle); //ジャンプ罰
    public InputButoon Skill = new InputButoon(KeyCode.Y, PS4ControllerButton.R1); //すきるまる
    public InputButoon PowerAttack = new InputButoon(KeyCode.Y, PS4ControllerButton.R2); //すきるまる
    public InputButoon Skill2 = new InputButoon(KeyCode.T, PS4ControllerButton.L1); //すきるまる
    public InputButoon Intract = new InputButoon(KeyCode.O, PS4ControllerButton.Triangle); //すきるまる
    public InputAxis Horizontal = new InputAxis(KeyCode.D, KeyCode.A, PS4COntrollerAxes.LeftStickHorizontal);
    public InputAxis Vertical = new InputAxis(KeyCode.W, KeyCode.S, PS4COntrollerAxes.LeftStickVertical);
    public InputAxis Select_Hoz = new InputAxis(KeyCode.LeftArrow, KeyCode.RightArrow, PS4COntrollerAxes.Select_Hoz);
    public InputAxis Select_Vert = new InputAxis(KeyCode.UpArrow, KeyCode.DownArrow, PS4COntrollerAxes.Select_Vert);


    public void keyconfig(KeyCode code, InputButoon button,PS4ControllerButton button2)
    {
        button = new InputButoon(code, button2);

    }



    public static Playerinput Instance
    {
        get { return s_instance; }

    }
    protected static Playerinput s_instance;

    bool m_fixedupdate;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;

        }

    }

    void OnEnable()
    {
        if (s_instance == null)
            s_instance = this;
    }

    void OnDisable()
    {
        s_instance = null;
    }

    [Serializable]
    public class InputButoon
    {
        public KeyCode key;
        public PS4ControllerButton controllerbutoon;
        public bool Down { get; protected set; }
        public bool Held { get; protected set; }
        public bool Up { get; protected set; }
        public bool Enabled
        {
            get { return m_Enabled; }
        }
        [SerializeField]
        protected bool m_Enabled = true;
        protected bool m_GettingInput = true;

        bool m_AfterFixedUpdateDown;
        bool m_AfterFixedUpdateHeld;
        bool m_AfterFixedUpdateUp;


        protected static readonly Dictionary<int, string> ButtonName = new Dictionary<int, string>
        {
            {(int)PS4ControllerButton.Circle,"Circle"},
             {(int)PS4ControllerButton.Cross,"Cross"},
              {(int)PS4ControllerButton.Square,"Square"},
             {(int)PS4ControllerButton.Triangle,"Triangle"},
             {(int)PS4ControllerButton.Leftstick, "Leftstick"},
             {(int)PS4ControllerButton.Rightstick, "Rightstick"},
             {(int)PS4ControllerButton.R1,"R1"},
             {(int)PS4ControllerButton.R2,"R2"},
              {(int)PS4ControllerButton.L1,"L1"},
             {(int)PS4ControllerButton.L2,"L2"},
             {(int)PS4ControllerButton.View,"View"},
             {(int)PS4ControllerButton.Menu,"Menu"},
        };

        public InputButoon(KeyCode key, PS4ControllerButton Controllerbutton)
        {
            this.key = key;
            this.controllerbutoon = Controllerbutton;
        }

        public void InputCheck(bool fixedupdatehappen, Inputtype inputtype)
        {
            if (!m_Enabled)
            {
                Down = false;
                Held = false;
                Up = false;
                return;
            }

            if (!m_GettingInput)
                return;


            if (inputtype == Inputtype.PS4Controller)
            {
                if (fixedupdatehappen)
                {
                    Down = Input.GetButtonDown(ButtonName[(int)controllerbutoon]);
                    Held = Input.GetButton(ButtonName[(int)controllerbutoon]);
                    Up = Input.GetButtonUp(ButtonName[(int)controllerbutoon]);

                    m_AfterFixedUpdateDown = Down;
                    m_AfterFixedUpdateHeld = Held;
                    m_AfterFixedUpdateUp = Up;
                }

                else
                {
                    Down = Input.GetButtonDown(ButtonName[(int)controllerbutoon]) || m_AfterFixedUpdateDown;
                    Held = Input.GetButton(ButtonName[(int)controllerbutoon]) || m_AfterFixedUpdateHeld;
                    Up = Input.GetButtonUp(ButtonName[(int)controllerbutoon]) || m_AfterFixedUpdateUp;

                    m_AfterFixedUpdateDown |= Down;
                    m_AfterFixedUpdateHeld |= Held;
                    m_AfterFixedUpdateUp |= Up;
                }
            }
            else if (inputtype == Inputtype.Mouseandkey)
            {
                if (fixedupdatehappen)
                {
                    Down = Input.GetKeyDown(key);
                    Held = Input.GetKey(key);
                    Up = Input.GetKeyUp(key);

                    m_AfterFixedUpdateDown = Down;
                    m_AfterFixedUpdateHeld = Held;
                    m_AfterFixedUpdateUp = Up;
                }
                else
                {
                    Down = Input.GetKeyDown(key) || m_AfterFixedUpdateDown;
                    Held = Input.GetKey(key) || m_AfterFixedUpdateHeld;
                    Up = Input.GetKeyUp(key) || m_AfterFixedUpdateUp;

                    m_AfterFixedUpdateDown |= Down;
                    m_AfterFixedUpdateHeld |= Held;
                    m_AfterFixedUpdateUp |= Up;
                }

            }
        }

        public void Enable()
        {
            m_Enabled = true;
        }

        public void Disable()
        {
            m_Enabled = false;
        }


        public void GainControl()
        {
            m_GettingInput = true;
        }

        public IEnumerator ReleaseControl(bool resetValues)
        {
            m_GettingInput = false;

            if (!resetValues)
                yield break;

            if (Down)
                Up = true;
            Down = false;
            Held = false;

            m_AfterFixedUpdateDown = false;
            m_AfterFixedUpdateHeld = false;
            m_AfterFixedUpdateUp = false;

            yield return null;

            Up = false;
        }

    }


    [Serializable]
    public class InputAxis
    {
        public KeyCode positive;
        public KeyCode negative;
        public PS4COntrollerAxes controllerAxis;

        protected bool m_Enabled = true;
        protected bool m_GettingInput = true;

        public float Value { get; protected set; } //入力拒否
        public bool ReceivingInput { get; protected set; }
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        protected static readonly Dictionary<int, string> AxisName = new Dictionary<int, string>
        {
            { (int)PS4COntrollerAxes.LeftStickHorizontal, "Leftstick Horizontal"},
            { (int)PS4COntrollerAxes.RightStickHorizontal, "Rightstick Horizontal"},
            { (int)PS4COntrollerAxes.LeftStickVertical, "Leftstick Vertical"},
            { (int)PS4COntrollerAxes.RightStickVertical, "Rightstick Vertical"},
            { (int)PS4COntrollerAxes.Select_Hoz, "Select_Hoz"},
            { (int)PS4COntrollerAxes.Select_Vert, "Select_Vert"},
        };

        public InputAxis(KeyCode positive, KeyCode negative, PS4COntrollerAxes controllerAxis)
        {
            this.positive = positive;
            this.negative = negative;
            this.controllerAxis = controllerAxis;
        }

        public void InputCheck(Inputtype inputType)
        {

            if (!m_Enabled)
            {
                Value = 0f;
                return;
            }

            if (!m_GettingInput)
                return;

            bool positiveHeld = false;
            bool negativeHeld = false;

            if (inputType == Inputtype.PS4Controller)
            {
                float value = Input.GetAxisRaw(AxisName[(int)controllerAxis]);
                positiveHeld = value > Single.Epsilon;
                negativeHeld = value < -Single.Epsilon;
            }
            else if (inputType == Inputtype.Mouseandkey)
            {
                positiveHeld = Input.GetKey(positive);
                negativeHeld = Input.GetKey(negative);
            }

            if (positiveHeld == negativeHeld)
                Value = 0f;
            else if (positiveHeld)
                Value = 1f;
            else
                Value = -1f;

            ReceivingInput = positiveHeld || negativeHeld;
        }
        public void GainControl()
        {
            m_GettingInput = true;
        }

        public void ReleaseControl(bool resetValues)
        {
            m_GettingInput = false;
            if (resetValues)
            {
                Value = 0f;
                ReceivingInput = false;
            }
        }

    }
    public Inputtype inputType = Inputtype.Mouseandkey;

    void Update()
    {
        Pause.InputCheck(m_fixedupdate, inputType);
        Attack.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Jump.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Skill.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Skill2.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        PowerAttack.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Intract.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Dash.InputCheck(m_fixedupdate || Mathf.Approximately(Time.timeScale, 0), inputType);
        Horizontal.InputCheck(inputType);
        Vertical.InputCheck(inputType);
        Select_Hoz.InputCheck(inputType);
        Select_Vert.InputCheck(inputType);
        m_fixedupdate = false;
    }


    private void FixedUpdate()
    {
        m_fixedupdate = true;
    }

    protected void GainControl(InputButoon inputButton)
    {
        inputButton.GainControl();
    }

    protected void GainControl(InputAxis inputAxis)
    {
        inputAxis.GainControl();
    }

    public void GainControl()
    {
        m_HaveControl = true;

        GainControl(Pause);
        GainControl(Attack);
        GainControl(Jump);
        GainControl(Skill);
        GainControl(Skill2);
        GainControl(PowerAttack);
        GainControl(Intract);
        GainControl(Horizontal);
        GainControl(Vertical);
        GainControl(Select_Hoz);
        GainControl(Select_Vert);
    }

    protected void ReleaseControllerButton(InputButoon inputButton, bool resetValues)
    {
        StartCoroutine(inputButton.ReleaseControl(resetValues));
    }

    public void ReleaseControllerAxis(InputAxis inputaxis, bool resetValues)
    {
        inputaxis.ReleaseControl(resetValues);
    }

    public void ReleaseController(bool resetvalue = true)

    {

        m_HaveControl = false;
        ReleaseControllerButton(Pause, resetvalue);
        ReleaseControllerButton(Attack, resetvalue);
        ReleaseControllerButton(PowerAttack, resetvalue);
        ReleaseControllerButton(Jump, resetvalue);
        ReleaseControllerButton(Skill, resetvalue);
        ReleaseControllerButton(Skill2, resetvalue);
        ReleaseControllerButton(Intract, resetvalue);
        ReleaseControllerAxis(Horizontal, resetvalue);
        ReleaseControllerAxis(Vertical, resetvalue);
        ReleaseControllerAxis(Select_Hoz, resetvalue);
        ReleaseControllerAxis(Select_Vert, resetvalue);
    }



}


