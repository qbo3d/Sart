using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IndiePixel
{
    public class IP_Airplane_ThrottleLever : MonoBehaviour
    {
        #region Variables
        [Header("Throttle Lever Properties")]
        //public IP_BaseAirplane_Input input;
        public float StickyThrottle;
        public RectTransform parentRect;
        public RectTransform handleRect;
        public float handleSpeed = 2f;
        #endregion

        #region Public Methods
        public void Update()
        {
            //if (input && parentRect && handleRect)
            if (parentRect && handleRect)
            {
                float height = parentRect.rect.height;
                //Vector2 wantedHandlePosition = new Vector2(0f, height * input.StickyThrottle);
                Vector2 wantedHandlePosition = new Vector2(0f, height * StickyThrottle);
                handleRect.anchoredPosition = Vector2.Lerp(handleRect.anchoredPosition, wantedHandlePosition, Time.deltaTime * handleSpeed);
            }
        }
        #endregion
    }
}
