using UnityEngine;

namespace Bioengstrom {
    /// <summary>
    /// Useful functions/methods, not specific to any certain project.
    ///
    /// Concatenated and written by Isak 'Bio' Engström. 
    /// </summary>
    public class HelperFunctions: MonoBehaviour {
   
        
        private Vector3 SmoothVector3(Vector3 startVector, Vector3 endVector, float smoothTime, float sTime) {
            var vx= SmoothFloat(startVector.x, endVector.x, smoothTime, sTime);
            var vy= SmoothFloat(startVector.y, endVector.y, smoothTime, sTime);
            var vz= SmoothFloat(startVector.z, endVector.z, smoothTime, sTime);
        
            return new Vector3(vx,vy,vz);
        }
        
        public float SmoothFloat(float startValue, float endValue, float smoothTime, float sTime) {
            var  smoothPercentage = (Time.time - sTime) / smoothTime;
        
            return Mathf.SmoothStep(startValue, endValue, smoothPercentage);
        }
    }
}
