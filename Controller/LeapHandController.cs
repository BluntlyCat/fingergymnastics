namespace HSA.FingerGymnastics.Controller
{
    using Leap;
    using Mhaze.Unity.Logging;
    using UnityEngine;

    public class LeapHandController : MonoBehaviour
    {
        protected static Logger<LeapHandController> logger = new Logger<LeapHandController>();

        private Controller controller;

        private Hand leftHand;
        private Hand rightHand;

        private bool hasLeftHand;
        private bool hasRightHand;

        void Awake()
        {
            controller = new Controller();
        }

        private void InitializeFlags()
        {
            Controller.PolicyFlag policy_flags = controller.PolicyFlags & ~Controller.PolicyFlag.POLICY_OPTIMIZE_HMD;
            controller.SetPolicyFlags(policy_flags);
        }

        public bool HasHands
        {
            get
            {
                return this.hasLeftHand && this.hasRightHand;
            }
        }

        public bool HasOneHand
        {
            get
            {
                return this.hasLeftHand || this.hasRightHand;
            }
        }

        public bool HasLeftHand
        {
            get
            {
                return this.hasLeftHand;
            }
        }

        public bool HasRightHand
        {
            get
            {
                return this.hasRightHand;
            }
        }

        public Hand LeftHand
        {
            get
            {
                return this.leftHand;
            }
        }

        public Hand RightHand
        {
            get
            {
                return this.rightHand;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.controller.IsConnected;
            }
        }

        void FixedUpdate()
        {
            if (controller == null)
                return;

            Frame frame = controller.Frame();

            hasLeftHand = false;
            hasRightHand = false;

            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsLeft)
                {
                    leftHand = hand;
                    hasLeftHand = true;
                }
                else
                {
                    rightHand = hand;
                    hasRightHand = true;
                }
            }
        }
    }
}
