using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IndiePixel
{
    public class IP_Airplane_Airspeed : MonoBehaviour
    {
        #region Variables
        [Header("Airspeed Indicator Properties")]
        //public IP_Airplane_Characteristics characteristics;
        public float MPH;
        public RectTransform pointer;
        public float maxIndicatedKnots = 160f;
        #endregion
        
        public const float mphToKnts = 0.868976f;

        #region Public Methods
        public void Update()
        {
            //if (characteristics && pointer)
                if (pointer)
            {
                //float currentKnots = characteristics.MPH * mphToKnts;
                float currentKnots = MPH * mphToKnts;
                Debug.Log(currentKnots);

                float normalizedKnots = Mathf.InverseLerp(0f, maxIndicatedKnots, currentKnots);
                float wantedRotation = 360f * normalizedKnots;
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            }
        }
        #endregion
    }
}
