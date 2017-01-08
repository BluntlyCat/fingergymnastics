namespace HSA.FingerGymnastics.Controller
{
    using Leap;
    using System.Collections.Generic;
    using UnityEngine;

    public class HandViewController : MonoBehaviour
    {
        protected static Mhaze.Unity.Logging.Logger<HandViewController> logger = new Mhaze.Unity.Logging.Logger<HandViewController>();

        public Material defaultMaterial;
        public Material wrongGestureMaterial;
        public Material correctGestureMaterial;

        public GameObject handsPrefab;

        public float scaleFactor = 1;

        private int leftHandId = -1;
        private int rightHandId = -1;

        void Start()
        {
            logger.AddLogAppender<Mhaze.Unity.Logging.ConsoleAppender>();
        }

        void Update()
        {
            var hands = FindHands();

            leftHandId = UpdateHand(hands, "leftHand", leftHandId);
            rightHandId = UpdateHand(hands, "rightHand", rightHandId);
        }

        public void SetHandMaterial(SkeletalHand hand, Material material)
        {
            SetPalmMaterial(hand, material);
            SetFingerMaterial(hand, material);
        }

        public void ScaleHand(SkeletalHand hand)
        {
            ScalePalm(hand);
            ScaleFingers(hand);
        }

        private int UpdateHand(IDictionary<string, SkeletalHand> hands, string name, int id)
        {
            int newId = id;

            if (hands.ContainsKey(name))
            {
                SkeletalHand handModel = hands[name];
                newId = handModel.GetInstanceID();

                if (id == -1 || id != newId)
                {
                    SetHandMaterial(handModel, defaultMaterial);
                    //ScaleHand(handModel);
                }

                //handModel.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }

            return newId;
        }

        private void ScalePalm(SkeletalHand hand)
        {
            if (hand.palm != null)
                hand.palm.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }

        private void SetPalmMaterial(SkeletalHand hand, Material material)
        {
            if (hand.palm != null)
            {
                Transform torus = hand.palm.Find("torus");

                if (torus != null)
                {
                    torus = torus.Find("torus");

                    if (torus != null)
                    {
                        Renderer r = torus.GetComponent<Renderer>();
                        r.material = material;
                    }
                }
            }
        }

        private void SetFingerMaterial(SkeletalHand hand, Material material)
        {
            foreach (var finger in hand.fingers)
            {
                if (finger != null)
                {
                    foreach (var bone in finger.bones)
                    {
                        if (bone != null)
                        {
                            foreach (Transform child in bone.transform)
                            {
                                if (child != null)
                                {
                                    Renderer r = child.GetComponent<Renderer>();
                                    r.material = material;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ScaleFingers(SkeletalHand hand)
        {
            foreach (var finger in hand.fingers)
            {
                if (finger != null)
                {
                    finger.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                }
            }
        }

        private IDictionary<string, SkeletalHand> FindHands()
        {
            IDictionary<string, SkeletalHand> hands = new Dictionary<string, SkeletalHand>();
            string name = "leftHand";

            foreach (Transform gameObject in handsPrefab.transform)
            {
                if (gameObject.name == "MinimalHand(Clone)")
                {
                    SkeletalHand sh = gameObject.GetComponent<SkeletalHand>();
                    Hand hand = sh.GetLeapHand();

                    if (hand.IsRight)
                        name = "rightHand";

                    hands[name] = sh;
                }
            }

            return hands;
        }
    }
}
