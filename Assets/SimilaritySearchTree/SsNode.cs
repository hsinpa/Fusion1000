using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Hsinpa.Algorithm {
    public class SsNode
    {
        public float2 centroid;

        public float radius;

        public SsNode[] children;

        public float2[] points;

        public bool Leaf;

        public SsNode(bool leaf, float2[] p_points, SsNode[] p_children) {
            this.Leaf = leaf;
            this.points = p_points;
            this.children = p_children;
        }

        public bool interserctsPoint(float2 target) {
            return math.distance(this.centroid, target) <= this.radius;
        }

        public SsNode findClosestChild(float2 target) {
            if (this.Leaf) return null;

            float minDistance = float.PositiveInfinity;
            SsNode result = null;

            foreach (SsNode childNode in this.children) {
                float dist = math.distance(childNode.centroid, target);
                if (dist < minDistance) {
                    minDistance = dist;
                    result = childNode;
                }
            }

            return result;
        }

    }
}
