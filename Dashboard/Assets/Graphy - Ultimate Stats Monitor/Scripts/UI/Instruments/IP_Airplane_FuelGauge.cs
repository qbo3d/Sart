using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IndiePixel
{
    public class IP_Airplane_FuelGauge : MonoBehaviour
    {
        #region Variables
        [Header("Fuel Guage Properties")]
        //public IP_Airplane_Fuel fuel;
        public float NormalizedFuel;
        public RectTransform pointer;
        public Vector2 minMaxRotation = new Vector2(-90f, 90f);
        #endregion

        #region Public Methods
        public void Update()
        {
            //if (fuel && pointer)
            if (pointer)
            {
                //float wantedRotation = Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, fuel.NormalizedFuel);
                float wantedRotation = Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, NormalizedFuel);
                pointer.rotation = Quaternion.Euler(0f, 0f, -wantedRotation);
            }
        }
        #endregion

    }
}
