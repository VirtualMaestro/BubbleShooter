using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client.Source.Gameplay.Utils
{
    public static class TrajectoryUtil
    {
        /// <summary>
        /// Calculate trajectory path and hitObject for given position and direction
        /// Returns 'false' if reflection count more than 1 or hiObject wasn't found. 
        /// </summary>
        public static bool HitTest(Vector2 position, Vector2 direction, List<Vector3> trajectory, out RaycastHit2D hitInfo, params string[] acceptTags)
        {
            var reflectionCount = 0;
            hitInfo = default;
            trajectory.Add(position);

            while (reflectionCount <= 1)
            {
                hitInfo = Physics2D.Raycast(position, direction);
                
                if (!hitInfo.collider)
                    return false;

                trajectory.Add(hitInfo.point);

                if (acceptTags.Contains(hitInfo.collider.tag))
                    return true;
                

                direction = Vector2.Reflect(direction, hitInfo.normal);
                position = hitInfo.point + hitInfo.normal * 0.01f;

                reflectionCount++;
            }

            return false;
        }
    }
}