/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Collaborators:   Lars Aalbertsen (@Rockylars)
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            05-Dec-17
 * Studio:          Tayx
 * 
 * This project is released under the MIT license.
 * Attribution is not required, but it is always welcomed!
 * -------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using Tayx.Graphy.Utils.NumString;

namespace Tayx.Graphy.Rpm
{
    public class G_RpmText : MonoBehaviour
    {
        /* ----- TODO: ----------------------------
         * Add summaries to the variables.
         * Add summaries to the functions.
         * Check if we should add a "RequireComponent" for "RamMonitor".
         * Improve the FloatString Init to come from the core instead.
         * --------------------------------------*/

        #region Variables -> Serialized Private

        [Header("Componentes")]
        [SerializeField] private Text m_allocatedSystemMemorySizeText = null;
        [SerializeField] private Text m_reservedSystemMemorySizeText = null;
        [SerializeField] private Text m_monoSystemMemorySizeText = null;
        [Range(1, 200)]
        [SerializeField] private float m_updateRate = 3f;

        [Header("Colores")]
        [SerializeField] private Color m_allocatedRamColor = new Color32(255, 190, 60, 255);
        [SerializeField] private Color m_reservedRamColor = new Color32(205, 84, 229, 255);
        [SerializeField] private Color m_monoRamColor = new Color(0.3f, 0.65f, 1f, 1);

        #endregion

        #region Variables -> Private

        //private GraphyManager m_graphyManager = null;

        private G_RpmMonitor m_ramMonitor = null;

        private float m_deltaTime = 0.0f;

        private readonly string m_memoryStringFormat = "0.0";

        #endregion

        #region Methods -> Unity Callbacks

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            m_deltaTime += Time.unscaledDeltaTime;

            if (m_deltaTime > 1f / m_updateRate)
            {
                // Update allocated, mono and reserved memory
                m_allocatedSystemMemorySizeText.text = m_ramMonitor.AllocatedRpm.ToStringNonAlloc(m_memoryStringFormat);
                m_reservedSystemMemorySizeText.text = m_ramMonitor.ReservedRpm.ToStringNonAlloc(m_memoryStringFormat);
                m_monoSystemMemorySizeText.text = m_ramMonitor.MonoRpm.ToStringNonAlloc(m_memoryStringFormat);

                m_deltaTime = 0f;
            }
        }

        #endregion

        #region Methods -> Public

        public void UpdateParameters()
        {
            m_allocatedSystemMemorySizeText.color = m_allocatedRamColor;
            m_reservedSystemMemorySizeText.color = m_reservedRamColor;
            m_monoSystemMemorySizeText.color = m_monoRamColor;
        }

        #endregion

        #region Methods -> Private

        private void Init()
        {
            //TODO: Replace this with one activated from the core and figure out the min value.
            if (!G_FloatString.Inited || G_FloatString.MinValue > -1000f || G_FloatString.MaxValue < 16384f)
            {
                G_FloatString.Init
                (
                    minNegativeValue: -1001f,
                    maxPositiveValue: 16386f
                );
            }

            //m_graphyManager = transform.root.GetComponentInChildren<GraphyManager>();

            m_ramMonitor = GetComponent<G_RpmMonitor>();

            UpdateParameters();
        }

        #endregion
    }
}