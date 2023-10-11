using Hsinpa.Nettwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hsinpa {
    public class CharacterLODManager : MonoBehaviour
    {
        [SerializeField]
        private Camera m_camera;

        public static CharacterLODManager Instance;

        private int i = 0;
        private int calculateStep = 25;
        private bool isProcessing = false;

        private Plane[] _planes;

        private Dictionary<int, NetPlayerBehaivor> meshDict = new Dictionary<int, NetPlayerBehaivor>();

        void Awake()
        {
            Instance = this;
        }

        public void Register(int index, NetPlayerBehaivor netPlayer) {
            meshDict = Hsinpa.UtilityFunc.SetDictionary<int, NetPlayerBehaivor>(meshDict, index, netPlayer);
        }

        public void Remove(int index)
        {
            meshDict.Remove(index);
        }

        // Update is called once per frame
        void Update()
        {
            if (isProcessing) return;
            isProcessing = true;
            StartCoroutine(Calculate(meshDict.Values.GetEnumerator()));
        }

        private IEnumerator Calculate(IEnumerator<NetPlayerBehaivor> dataset) 
        {
            while (dataset.MoveNext())
            {
                i++;
                
                if (dataset.Current == null) continue;

                ProcessCameraVisibility(m_camera, dataset.Current);

                if (i >= calculateStep)
                {
                    i = 0;
                    yield return null;
                }
            };

            isProcessing = false;
        }

        private bool ProcessCameraVisibility(Camera camera, NetPlayerBehaivor skinnedMesh) {
            if (_planes == null)
                _planes = GeometryUtility.CalculateFrustumPlanes(camera);
            else
                GeometryUtility.CalculateFrustumPlanes(camera, _planes);

            bool is_within_frustum = GeometryUtility.TestPlanesAABB(_planes, skinnedMesh.SkinMesh.bounds); // Not sure why its revert

            skinnedMesh.SetObjectVisible(is_within_frustum);

            return is_within_frustum;
        }


    }
}
