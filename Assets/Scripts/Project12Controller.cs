using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Project12Controller : MonoBehaviour
{


    public MovablePhysicsObject LeftObject;
    public MovablePhysicsObject RightObject;
    public Vector3 LeftObjectMinVelocity;
    public Vector3 LeftObjectMaxVelocity;
    public Vector3 RightObjectMinVelocity;
    public Vector3 RightObjectMaxVelocity;
    public Vector3 VelIncrVal;

    public Vector3 LeftObjectInitVelocity;
    public Vector3 RightObjectInitVelocity;

    public KeyCode LeftIncVel;
    public KeyCode LeftDecVel;
    public KeyCode RightIncVel;
    public KeyCode RightDecVel;

    public float CoeffE;
    public float MinCoeffE;
    public float MaxCoeffE;
    public float CoeffEIncrVal;

    public KeyCode IncCoeffE;
    public KeyCode DecCoeffE;

    public KeyCode StartMotion;
    public KeyCode StopMotion;
    public KeyCode Reset;

    public int CollisionCounts;

    public Vector3 DstBtwn;

    public Vector3 nHat;
    public float uIn;
    public float vIn;
    public Vector3 uFt;
    public Vector3 vFt;
    public Vector3 uFn;
    public Vector3 vFn;
    public Vector3 tHat;
    public Vector3 n;



    public float CoeffMyu;
    public Vector2 CoeffMyuBounds;
    public float CoeffMyuIncr;



    public Text t_MassLeft;
    public Text t_MassRight;
    public Text t_InitVelocityLeft;
    public Text t_InitVelocityRight;
    public Text t_FinalVelocityLeft;
    public Text t_FinalVelocityRight;
    public Text t_CoeffE;
    public Text t_J;
    public Text t_CollCount;
    public Text t_pi;
    public Text t_pf;
    public Text t_n;
    public Text t_KEi;
    public Text t_KEf;
    public Text t_Ii;
    public Text t_If;
    public Text t_Friction;
    public Text t_M3;
    public Text t_M4;
    public Text t_M5;


    public float J;

    public Vector3 leftCurrW;
    public Vector3 rightCurrW;

    // Use this for initialization
    void Start()
    {

        LeftObject.m_Velocity = LeftObjectInitVelocity;
        RightObject.m_Velocity = RightObjectInitVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCoeffKeys();
        HandleLeftVelocityKeys();
        HandleRightVelocityKeys();
        HandleStartStopMotion();
        HandleReset();

        if (t_MassLeft)
            t_MassLeft.text = "Mass 1: " + LeftObject.m_Mass.ToString("F2");
        if (t_MassRight)
            t_MassRight.text = "Mass 2: " + RightObject.m_Mass.ToString("F2");
        if (t_CoeffE)
            t_CoeffE.text = "e: " + CoeffE.ToString("F2");
        if (t_InitVelocityLeft)
            t_InitVelocityLeft.text = "ui: " + LeftObjectInitVelocity.ToString("F2");
        if (t_InitVelocityRight)
            t_InitVelocityRight.text = "vi: " + RightObjectInitVelocity.ToString("F2");
        if (t_FinalVelocityLeft)
            t_FinalVelocityLeft.text = "uf: " + LeftObject.m_Velocity.ToString("F2");
        if (t_FinalVelocityRight)
            t_FinalVelocityRight.text = "vf: " + RightObject.m_Velocity.ToString("F2");
        if (t_J)
            t_J.text = "J: " + J.ToString("F2");
        if (t_CollCount)
            t_CollCount.text = "Collision Count: " + CollisionCounts.ToString("F2");
        if (t_pi)
            t_pi.text = "pi: " + (LeftObject.m_Mass * LeftObjectInitVelocity) + " + " + (RightObject.m_Mass * RightObjectInitVelocity) + "=" + (LeftObject.m_Mass * LeftObjectInitVelocity + RightObject.m_Mass * RightObjectInitVelocity).ToString("F2");
        if (t_pf)
            t_pf.text = "pf: " + (LeftObject.m_Mass * LeftObject.m_Velocity) + " + " + (RightObject.m_Mass * RightObject.m_Velocity) + "=" + (LeftObject.m_Mass * LeftObject.m_Velocity + RightObject.m_Mass * RightObject.m_Velocity).ToString("F2");
        if (t_Ii)
            t_Ii.text = "i_i: "
                + (leftR.y * LeftObject.m_Mass * LeftObjectInitVelocity.magnitude) + "+"
                + (rightR.y * RightObject.m_Mass * RightObjectInitVelocity.magnitude) + "="
                + (leftR.y * LeftObject.m_Mass * LeftObjectInitVelocity.magnitude + rightR.y * RightObject.m_Mass * RightObjectInitVelocity.magnitude);
        if (t_If)
            t_If.text = "i_f: "
                + (leftR.y * LeftObject.m_Mass * LeftObject.m_Velocity.magnitude) + "+"
                + (leftI * LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.magnitude) + "+"
                + (rightR.y * RightObject.m_Mass * RightObject.m_Velocity.magnitude) + "+"
                + (rightI * RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.magnitude) + "="
                + (leftR.y * LeftObject.m_Mass * LeftObject.m_Velocity.magnitude + leftI * LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.magnitude + rightR.y * RightObject.m_Mass * RightObject.m_Velocity.magnitude + rightI * RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.magnitude);
        if (t_n)
            t_n.text = "n: " + n;
        if (t_KEi)
            t_KEi.text = "KEi: " + ((0.5f * LeftObject.m_Mass * LeftObjectInitVelocity.sqrMagnitude) + (0.5f * RightObject.m_Mass * RightObjectInitVelocity.sqrMagnitude));
        if (t_KEf)
            t_KEf.text = "KEf: " +
                +(0.5f * LeftObject.m_Mass * LeftObject.m_Velocity.sqrMagnitude) + "+"
                + (0.5f * leftI * LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.sqrMagnitude) + "+"
                + (0.5f * RightObject.m_Mass * RightObject.m_Velocity.sqrMagnitude) + "+"
                + (0.5f * rightI * RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.sqrMagnitude) + "=" +
                ((0.5f * LeftObject.m_Mass * LeftObject.m_Velocity.sqrMagnitude) + (0.5f * leftI * LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.sqrMagnitude) + (0.5f * RightObject.m_Mass * RightObject.m_Velocity.sqrMagnitude) + (0.5f * rightI * RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity.sqrMagnitude)).ToString("F2");
        if (t_Friction)
            t_Friction.text = "Friction: " + CoeffMyu;
        if (t_M3)
            t_M3.text = "M3: " + M3;
        if (t_M4)
            t_M4.text = "M4: " + M4;
        if (t_M5)
            t_M5.text = "M5: " + M5;


    }

    void HandleCoeffKeys()
    {
        if (Input.GetKeyDown(IncCoeffE))
            CoeffE += CoeffEIncrVal;
        if (Input.GetKeyDown(DecCoeffE))
            CoeffE -= CoeffEIncrVal;

        CoeffE = BoundedVal.KeepInBounds(CoeffE, MinCoeffE, MaxCoeffE);
    }

    void HandleLeftVelocityKeys()
    {
        if (Input.GetKeyDown(LeftIncVel))
        {
            //LeftObject.m_Velocity += VelIncrVal;
            LeftObjectInitVelocity += VelIncrVal;
        }
        if (Input.GetKeyDown(LeftDecVel))
        {
            //LeftObject.m_Velocity -= VelIncrVal;
            LeftObjectInitVelocity -= VelIncrVal;
        }


        //LeftObject.m_Velocity = BoundedVal.KeepInBounds (LeftObject.m_Velocity, LeftObjectMinVelocity, LeftObjectMaxVelocity);
        LeftObjectInitVelocity = BoundedVal.KeepInBounds(LeftObjectInitVelocity, LeftObjectMinVelocity, LeftObjectMaxVelocity);
    }

    void HandleRightVelocityKeys()
    {
        if (Input.GetKeyDown(RightIncVel))
        {
            //RightObject.m_Velocity += VelIncrVal;
            RightObjectInitVelocity += VelIncrVal;
        }
        if (Input.GetKeyDown(RightDecVel))
        {
            //RightObject.m_Velocity -= VelIncrVal;
            RightObjectInitVelocity -= VelIncrVal;
        }

        //RightObject.m_Velocity = BoundedVal.KeepInBounds (RightObject.m_Velocity, RightObjectMinVelocity, RightObjectMaxVelocity);
        RightObjectInitVelocity = BoundedVal.KeepInBounds(RightObjectInitVelocity, RightObjectMinVelocity, RightObjectMaxVelocity);
    }

    void HandleStartStopMotion()
    {
        if (Input.GetKeyDown(StartMotion))
        {
            LeftObject.m_IsActive = true;
            RightObject.m_IsActive = true;
            LeftObject.GetComponent<AngularPhysics>().m_IsActive = true;
            RightObject.GetComponent<AngularPhysics>().m_IsActive = true;
            if (J == 0)
            {
                LeftObject.m_Velocity = LeftObjectInitVelocity;
                RightObject.m_Velocity = RightObjectInitVelocity;
            }
        }

        if (Input.GetKeyDown(StopMotion))
        {
            LeftObject.m_IsActive = false;
            RightObject.m_IsActive = false;
            LeftObject.GetComponent<AngularPhysics>().m_IsActive = false;
            RightObject.GetComponent<AngularPhysics>().m_IsActive = false;
        }
    }

    void HandleReset()
    {
        if (Input.GetKeyDown(Reset))
        {
            LeftObject.Reset();
            RightObject.Reset();
            LeftObject.m_Velocity = LeftObjectInitVelocity;
            RightObject.m_Velocity = RightObjectInitVelocity;
            CollisionCounts = 0;
        }

    }

    public float leftI;
    public float rightI;
    public Vector3 leftR;
    public Vector3 rightR;

    public Vector3 uF;
    public Vector3 vF;

    public float M3;
    public float M4;
    public float M5;

    void FixedUpdate()
    {
        if (CollisionCounts == 0)
        {
            DstBtwn = RightObject.m_Position - LeftObject.m_Position;


            int count = 0;
            while (DstBtwn.magnitude < 40 && count < 10000)
            {
                LeftObject.Move(new Vector3(-0.001f, 0, 0));
                DstBtwn = RightObject.m_Position - LeftObject.m_Position;
                count++;
            }

            leftI = 0.5f * LeftObject.m_Mass * 20 * 20;
            rightI = 0.5f * RightObject.m_Mass * 20 * 20;

            leftR = (RightObject.m_Position - LeftObject.m_Position) / 2;
            rightR = (LeftObject.m_Position - RightObject.m_Position) / 2;

            if (DstBtwn.magnitude <= 41)
            {
                Vector3 u = LeftObject.m_Velocity;
                Vector3 v = RightObject.m_Velocity;

                n = (RightObject.m_Position - LeftObject.m_Position);
                


                nHat = n.normalized;

                Vector3 t = Vector3.Cross(Vector3.Cross(nHat, u), nHat);
                tHat = t.normalized;
                Vector3 vRn = (uIn - vIn) * nHat;

                Vector3 vr = u - v;

                M3 = Vector3.Dot(nHat, Vector3.Cross(Vector3.Cross(leftR, nHat)/leftI, leftR));
                M4 = Vector3.Dot(nHat, Vector3.Cross(Vector3.Cross(rightR, nHat)/rightI, rightR));
                M5 = (1.0f / (1.0f / LeftObject.m_Mass + 1.0f / RightObject.m_Mass + M3 + M4));

                J = -Vector3.Dot(vr, nHat) * (CoeffE + 1.0f) * M5;

                uF = u + J / LeftObject.m_Mass * (nHat + CoeffMyu * tHat);
                vF = v - J / RightObject.m_Mass * (nHat - CoeffMyu * tHat);

                Debug.Log(J / leftI * Vector3.Cross(leftR, nHat + CoeffMyu * tHat));

                LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity = 
                    LeftObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity + J / leftI * Vector3.Cross(leftR, nHat + CoeffMyu * tHat);
                RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity =
                    RightObject.GetComponent<AngularPhysics>().m_CurrentAngularVelocity - J / rightI * Vector3.Cross(rightR, CoeffMyu * tHat - nHat);

                LeftObject.m_Velocity = uF;
                RightObject.m_Velocity = vF;

                CollisionCounts += 1;
            }


        }




    }
}
