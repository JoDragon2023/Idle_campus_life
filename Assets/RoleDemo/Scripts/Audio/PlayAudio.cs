using UnityEngine;
 
public class PlayAudio : MonoBehaviour
{
    //音乐文件
    public AudioSource music;
 
    /// <summary>播放放音乐</summary>
    private void playMusic()
    {
        if (music!=null&&!music.isPlaying)
        {
            music.Play();
        }
    }
 
    /// <summary>关闭音乐播放</summary>
    private void stopMusic()
    {
        if (music != null && !music.isPlaying)
        {
            music.Stop();
        }
    }
 
    /// <summary>暂停音乐播放</summary>
    private void pauseMusic()
    {
        if (music != null && !music.isPlaying)
        {
            music.Pause();
        }
    }
 
    /// <summary>
    /// 设置播放音量
    /// </summary>
    /// <param name="volume"></param>
    private void setMusicVolume(float volume)
    {
        if (music != null && !music.isPlaying)
        {
            music.volume = volume;
        }
    }
}