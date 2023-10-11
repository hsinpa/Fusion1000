using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Hsinpa.Algorithm
{
    public class SsTree
    {
        public SsNode root;

        public int m;
        public int M;

        public SsTree(int p_m, int p_M) {
            this.m = p_m;
            this.M = p_M;
        }

        public SsNode search(SsNode node, float2 target) {

            if (node.Leaf) {
                int pointlens = node.points.Length;

                for (int i = 0; i < pointlens; i++) {
                    bool2 pointCompare = node.points[i] == target;

                    if (pointCompare.x && pointCompare.y)
                        return node;
                }
            }

            foreach (var childNode in node.children) {
                if (childNode.interserctsPoint(target)) {

                    SsNode result = search(childNode, target);

                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        public SsNode searchParentLeaf(SsNode node, float2 target) {
            if (node.Leaf) return node;
            return null;

        }
    }
}
