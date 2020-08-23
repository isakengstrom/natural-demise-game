using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static class AudioFade {
    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
        var startVolume = audioSource.volume;
        var currentTime = 0f;
        while (audioSource.volume > 0) {
            currentTime += Time.deltaTime;
            
            audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeTime);
 
            yield return null;
        }
 
        audioSource.Pause();
        audioSource.volume = startVolume;
    }
    
    public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float maxVolume) {
        var targetVolume =  maxVolume;
        var startVolume = 0f;
        var currentTime = 0f;
        
        audioSource.volume = 0;
        audioSource.Play();
 
        while (audioSource.volume < 1.0f) {
            currentTime += Time.deltaTime;
            
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / fadeTime);

            yield return null;
        }
 
        audioSource.volume = targetVolume;
    }
    
}