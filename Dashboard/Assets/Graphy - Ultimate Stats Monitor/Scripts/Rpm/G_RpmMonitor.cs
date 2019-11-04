/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Collaborators:   Lars Aalbertsen (@Rockylars)
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            15-Dec-17
 * Studio:          Tayx
 * 
 * This project is released under the MIT license.
 * Attribution is not required, but it is always welcomed!
 * -------------------------------------*/

using UnityEngine;

#if UNITY_5_5_OR_NEWER
using UnityEngine.Profiling;
#endif

namespace Tayx.Graphy.Rpm
{
    public class G_RpmMonitor : MonoBehaviour
    {
        /* ----- TODO: ----------------------------
         * Add summaries to the variables.
         * Add summaries to the functions.
         * --------------------------------------*/

        #region Variables -> Private

        private float m_allocatedRpm = 0;
        private float m_reservedRpm = 0;
        private float m_monoRpm = 0;

        #endregion

        #region Properties -> Public

        public float AllocatedRpm { get { return m_allocatedRpm; } set { m_allocatedRpm = value; } }
        public float ReservedRpm { get { return m_reservedRpm; } set { m_reservedRpm = value; } }
        public float MonoRpm { get { return m_monoRpm; } set { m_monoRpm = value; } }

        #endregion

        #region Methods -> Unity Callbacks

        private void Update()
        {
#if UNITY_5_6_OR_NEWER
            //m_allocatedRam  = Profiler.GetTotalAllocatedMemoryLong()/ 1048576f;
            //m_reservedRam   = Profiler.GetTotalReservedMemoryLong() / 1048576f;
            //m_monoRam       = Profiler.GetMonoUsedSizeLong()        / 1048576f;

            //m_reservedRpm = Random.Range(200.0f, 250.0f);
            //m_allocatedRpm = Random.Range(100.0f, 150.0f);
            //m_monoRpm = (Time.timeSinceLevelLoad * 1000f) / 1000f;
#else
            m_allocatedRam  = Profiler.GetTotalAllocatedMemory()    / 1048576f;
            m_reservedRam   = Profiler.GetTotalReservedMemory()     / 1048576f;
            m_monoRam       = Profiler.GetMonoUsedSize()            / 1048576f;
#endif
        }

        #endregion 
    }
}